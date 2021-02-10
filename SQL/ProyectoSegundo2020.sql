USE master

GO

IF EXISTS(SELECT * FROM SysDataBases WHERE name='ProyectoSegundo2020')
BEGIN
	DROP DATABASE ProyectoSegundo2020
END

GO

CREATE DATABASE ProyectoSegundo2020

GO

USE ProyectoSegundo2020

CREATE TABLE Usuario (
	logueo VARCHAR(12) NOT NULL PRIMARY KEY,-- 12 caracteres
	contrasena VARCHAR(6) NOT NULL CHECK (contrasena LIKE '[a-zA-Z][a-zA-Z][a-zA-Z][0-9][0-9][|°¬¿?¡!"#$%&/\()=@´`¨+-*~{}^_<>,;.:]'), -- checks formato
	nombreCompleto VARCHAR(50) DEFAULT 'N/A' NOT NULL CHECK (nombreCompleto <> ''),
	activo BIT NOT NULL DEFAULT 1
)

CREATE TABLE Empresa (
	logueo VARCHAR(12) NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Usuario(logueo), -- 12 caracteres
	telefono VARCHAR(9) NOT NULL CHECK (LEN(telefono) = 9 AND telefono not like '%[^0-9]%'), -- 9 caracteres, check solo numeros
	direccion VARCHAR(50) NOT NULL CHECK (direccion <> ''),
	email VARCHAR(50) NOT NULL CHECK (CHARINDEX('@',email,1)>0 AND CHARINDEX('.', email, CHARINDEX( '@', email))>0 ) -- check formato email
)
-- borro columna activo
-- check hora fin > hora inicio????ahora no lo entiendo
CREATE TABLE Empleado (
	logueo VARCHAR(12) NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Usuario(logueo), -- 12 caracteres
	horaInicio TIME NOT NULL, 
	horaFin TIME NOT NULL 
)

CREATE TABLE Paquete (
	codigo INT NOT NULL PRIMARY KEY,
	tipo VARCHAR(6) NOT NULL CHECK (tipo in ('fragil', 'comun', 'bulto')), -- cambio
	descripcion VARCHAR(100) DEFAULT 'N/A' NOT NULL CHECK (descripcion <> ''),
	peso DECIMAL NOT NULL CHECK (peso > 0), -- check
	empresa VARCHAR(12) NOT NULL FOREIGN KEY REFERENCES Empresa(logueo)
)

CREATE TABLE Solicitud (
	numero INT NOT NULL PRIMARY KEY  IDENTITY,
	fechaEntrega DATETIME NOT NULL CHECK (fechaEntrega > GETDATE()),  
	nombreDestinatario VARCHAR(50) DEFAULT 'N/A' NOT NULL CHECK (nombreDestinatario <> ''), 
	direccionDestinatario VARCHAR(50) DEFAULT 'N/A' NOT NULL CHECK (direccionDestinatario <> ''),
	estado VARCHAR(11) NOT NULL  DEFAULT 'en deposito' CHECK (estado in ('en deposito', 'en camino', 'entregado')), -- cambio
	empleado VARCHAR(12) NOT NULL FOREIGN KEY REFERENCES Empleado(logueo)
)


CREATE TABLE PaquetesSolicitud (
	numeroSolicitud INT NOT NULL FOREIGN KEY REFERENCES Solicitud(numero),
	codigoPaquete INT NOT NULL FOREIGN KEY REFERENCES Paquete(codigo)
	PRIMARY KEY (numeroSolicitud, codigoPaquete)
)

GO


select * from Empresa;

CREATE ROLE db_rol_empleado
CREATE ROLE db_rol_empresa
CREATE ROLE db_rol_publico

GO

USE master
GO

CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS WITH DEFAULT_DATABASE = ProyectoSegundo2020
GO

USE ProyectoSegundo2020
GO

CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO

EXEC sp_addrolemember [db_rol_publico], [IIS APPPOOL\DefaultAppPool]

GO

--------------------------------------------SP Empleado------------------------------------------
CREATE PROCEDURE BuscarEmpleado
@logueo VARCHAR(12)
AS
BEGIN
	SELECT Usuario.*, Empleado.horaInicio, Empleado.horaFin
	FROM Usuario INNER JOIN Empleado
	ON Usuario.logueo = Empleado.logueo
	WHERE Usuario.logueo = @logueo AND Usuario.activo = 1
END

GO

CREATE PROCEDURE LogueoEmpleado
@logueo VARCHAR(12),
@contrasena varchar(6)
AS
BEGIN
	SELECT a.*, b.horaInicio, b.horaFin
	FROM Usuario a INNER JOIN Empleado b
	ON a.logueo = b.logueo
	WHERE a.logueo = @logueo AND a.contrasena = @contrasena AND a.activo = 1
END

go

CREATE PROCEDURE interBuscarEmpleado
@logueo VARCHAR(12)
AS
BEGIN
	SELECT Usuario.*, Empleado.horaInicio, Empleado.horaFin
	FROM Usuario INNER JOIN Empleado
	ON Usuario.logueo = Empleado.logueo
	WHERE Usuario.logueo = @logueo
END
-- un buscar activos y un buscar todos.
-- todos para mapear objetos dependientes.

-- mismo caso para los buscar empresa



GO

create PROCEDURE AltaEmpleado
@logueo VARCHAR(12),
@contrasena VARCHAR(6),
@nombreCompleto VARCHAR(50),
@horaInicio TIME,
@horaFin TIME
AS
BEGIN
	IF EXISTS (SELECT * FROM Usuario WHERE logueo = @logueo AND activo = 1)
	BEGIN
		RETURN -1
	END
	IF EXISTS (SELECT * FROM Usuario a  
			inner join Empleado b on a.logueo = b.logueo
			WHERE a.logueo = @logueo AND a.activo = 0) -- buscar con join a Empleado inactivo
	BEGIN
		BEGIN TRANSACTION;
		UPDATE Usuario 
		SET activo = 1,
			contrasena = @contrasena,
			nombreCompleto = @nombreCompleto
		WHERE logueo = @logueo -- aparte de activar actualizar datos
		IF (@@ERROR <> 0)
		BEGIN
			ROLLBACK TRANSACTION;
			RETURN -2
		END
		UPDATE Empleado 
		SET horaInicio = @horaInicio,
			horaFin = @horaFin
		WHERE logueo = @logueo -- aca ya no existe activo, aparte de activar actualizar datos
		IF (@@ERROR <> 0)
		BEGIN
			ROLLBACK TRANSACTION;
			RETURN -3
		END	
		
		DECLARE @VarSentencia VARCHAR(200)
		SET @VarSentencia = 'CREATE LOGIN [' + @logueo + '] WITH PASSWORD = ' + QUOTENAME(@contrasena, '''')
		EXEC (@VarSentencia)
		IF (@@ERROR <> 0)
		BEGIN
			ROLLBACK TRANSACTION;
			RETURN -4
		END
	
		SET @VarSentencia = 'Create User [' + @logueo + '] From Login [' + convert(varchar(MAX),@logueo) + ']'
		EXEC (@VarSentencia)
		IF (@@ERROR <> 0)
		BEGIN
			ROLLBACK TRANSACTION;
			RETURN -5 
		END

		COMMIT TRANSACTION;

		EXEC sp_addsrvrolemember @loginame=@logueo, @rolename='securityadmin'	
		IF (@@ERROR <> 0)
			RETURN -6

		EXEC sp_addrolemember @rolename='db_securityadmin', @membername=@logueo
		IF (@@ERROR <> 0)
			RETURN -7
		
		EXEC sp_addrolemember @rolename='db_rol_empleado' , @membername=@logueo
		IF (@@ERROR <> 0)
			RETURN -8
	END
	ELSE
	BEGIN
		BEGIN TRANSACTION;
		INSERT INTO Usuario VALUES (@logueo, @contrasena, @nombreCompleto, 1)
		IF (@@ERROR <> 0)
		BEGIN
			ROLLBACK TRANSACTION;
			RETURN -2
		END
		INSERT INTO Empleado VALUES (@logueo, @horaInicio, @horaFin)
		IF (@@ERROR <> 0)
		BEGIN
			ROLLBACK TRANSACTION;
			RETURN -3
		END

		DECLARE @VarSentencia2 VARCHAR(200)
		SET @VarSentencia2 = 'CREATE LOGIN [' + @logueo + '] WITH PASSWORD = ' + QUOTENAME(@contrasena, '''')
		EXEC (@VarSentencia2)
		IF (@@ERROR <> 0)
		BEGIN
			ROLLBACK TRANSACTION;
			RETURN -4
		END
	
		SET @VarSentencia2 = 'Create User [' + @logueo + '] From Login [' + convert(varchar(MAX),@logueo) + ']'
		EXEC (@VarSentencia2)
		IF (@@ERROR <> 0)
		BEGIN
			ROLLBACK TRANSACTION;
			RETURN -5
		END

		COMMIT TRANSACTION;

		EXEC sp_addsrvrolemember @loginame=@logueo, @rolename='securityadmin'	
		IF (@@ERROR <> 0)
			RETURN -6

		EXEC sp_addrolemember @rolename='db_securityadmin', @membername=@logueo
		IF (@@ERROR <> 0)
			RETURN -7
		
		EXEC sp_addrolemember @rolename='db_rol_empleado' , @membername=@logueo
		IF (@@ERROR <> 0)
			RETURN -8

	END
END
-- aca deberiamos crear un rol con los sp que puede ejecutar un empleado y que permisos necesita para poder administrar usuarios de base de datos

-- todos los grant necesarios van en un rol, y debemos ver cuales sp puede ejecutar el empleados
-- puedo crear un rol con todos los permisos y revokar los que no sean necesarios.
-- tambien se debe crear un tercer rol para el IIS publico, que se seteara cuando se creae dicho usuario
-- pasos 1 crear roles go 2 crear sp 3 asignar permisos a roles



GO

CREATE PROCEDURE ModificarEmpleado
@usLog VARCHAR(12),
@logueo VARCHAR(12),
@contrasena VARCHAR(6),
@nombreCompleto VARCHAR(50),
@horaInicio TIME,
@horaFin TIME
AS
BEGIN
	IF (@usLog <> @logueo) -- ES OTRA LA FORMA DE VALIDAR QUE EL USUARIO LOGUEADO SE AEL MISMO QUE ESTA QUERIENDO MODIFICAR EL EMPLEADO?.
	BEGIN
		RETURN -1
	END
	IF NOT EXISTS (SELECT * 
				FROM Usuario a
				inner join Empleado b on a.logueo = b.logueo
				WHERE a.logueo = @logueo AND a.activo = 1) -- join empleado
	BEGIN
		RETURN -2
	END

	BEGIN TRANSACTION;
	UPDATE Usuario
	SET contrasena = @contrasena, 
		nombreCompleto = @nombreCompleto -- solo se puede cambiar la contraseña del propio usuario logueado
	WHERE logueo = @logueo
	IF (@@ERROR <> 0)
		BEGIN
			ROLLBACK TRANSACTION;
			RETURN -3
		END
	UPDATE Empleado
	SET horaInicio = @horaInicio, horaFin = @horaFin
	WHERE logueo = @logueo
	IF (@@ERROR <> 0)
		BEGIN
			ROLLBACK TRANSACTION;
			RETURN -4
		END
	COMMIT TRANSACTION;
	
	DECLARE @VarSentencia VARCHAR(50);
	SET @VarSentencia = 'EXEC sp_PASSWORD NULL, [' + @contrasena + '], ' + convert(varchar(MAX),@logueo) + ';'
	EXEC (@VarSentencia)
	IF (@@ERROR <> 0)
	BEGIN
		RETURN -5
	END
	-- faltaria cambiar contraseña sql, usar sp_password para no tener que modificar permisos
END

GO

CREATE PROCEDURE BajaEmpleado
@logueo VARCHAR(12)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Empleado WHERE logueo = @logueo)
	BEGIN
		RETURN -1
	END
	IF EXISTS (SELECT * FROM Solicitud WHERE empleado = @logueo)
	BEGIN
		BEGIN TRANSACTION;
			UPDATE Usuario SET activo = 0 WHERE logueo = @logueo
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -2
			END

			DECLARE @VarSentencia VARCHAR(200)
			SET @VarSentencia = 'DROP USER [' + convert(varchar(MAX),@logueo) + ']'
			EXEC (@VarSentencia)
			IF (@@ERROR <> 0)
			BEGIN
			   ROLLBACK TRANSACTION;
			   RETURN -4
			END
    
			SET @VarSentencia = 'DROP LOGIN [' + convert(varchar(MAX),@logueo) + ']'
			EXEC (@VarSentencia)
			IF (@@ERROR <> 0)
			BEGIN
			   ROLLBACK TRANSACTION;
			   RETURN -5
			END
			-- borrar usuarios sql y BD
		COMMIT TRANSACTION;
	END
	ELSE
	BEGIN
		BEGIN TRANSACTION;
			DELETE FROM Empleado WHERE logueo = @logueo
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -2
			END
			DELETE FROM Usuario WHERE logueo = @logueo
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -3
			END
			-- borrar usuarios sql y BD
			DECLARE @VarSentencia2 VARCHAR(200)
			SET @VarSentencia2 = 'DROP USER [' + convert(varchar(MAX),@logueo) + ']'
			EXEC (@VarSentencia2)
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -4
			END
    
			SET @VarSentencia2 = 'DROP LOGIN [' + convert(varchar(MAX),@logueo) + ']'
			EXEC (@VarSentencia2)
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -5
			END
		COMMIT TRANSACTION;
	END
END	

GO

--------------------------------------------SP Empresa------------------------------------------
CREATE PROCEDURE BuscarEmpresa
@logueo VARCHAR(12)
AS
BEGIN
	SELECT Usuario.*, Empresa.telefono, Empresa.direccion, Empresa.email
	FROM Usuario INNER JOIN Empresa
	ON Usuario.logueo = Empresa.logueo
	WHERE Usuario.logueo = @logueo AND Usuario.activo = 1
END

GO

CREATE PROCEDURE LogueoEmpresa
@logueo VARCHAR(12),
@contrasena varchar(6)
AS
BEGIN
	SELECT a.*, b.direccion, b.email, b.telefono
	FROM Usuario a INNER JOIN Empresa b
	ON a.logueo = b.logueo
	WHERE a.logueo = @logueo AND a.contrasena = @contrasena AND a.activo = 1
END

go

CREATE PROCEDURE interBuscarEmpresa
@logueo VARCHAR(12)
AS
BEGIN
	SELECT Usuario.*, Empresa.telefono, Empresa.direccion, Empresa.email
	FROM Usuario INNER JOIN Empresa
	ON Usuario.logueo = Empresa.logueo
	WHERE Usuario.logueo = @logueo
END

-- son los mismos cambios que tenemos apra Empleado


GO

CREATE PROCEDURE AltaEmpresa
@logueo VARCHAR(12),
@contrasena VARCHAR(6),
@nombreCompleto VARCHAR(50),
@telefono VARCHAR(9),
@direccion VARCHAR(50),
@email VARCHAR(50)
AS
BEGIN
	IF EXISTS (SELECT * FROM Usuario WHERE logueo = @logueo AND activo = 1)
	BEGIN
		RETURN -1
	END
	IF EXISTS (SELECT * FROM Usuario WHERE logueo = @logueo AND activo = 0)
	BEGIN
		BEGIN TRANSACTION;
			UPDATE Usuario 
			SET activo = 1,
				contrasena = @contrasena,
				nombreCompleto = @nombreCompleto
			WHERE logueo = @logueo
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -2
			END
			UPDATE Empresa 
			SET telefono = @telefono,
				direccion = @direccion,
				email = @email
			WHERE logueo = @logueo
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -3
			END

			DECLARE @VarSentencia VARCHAR(200)
			SET @VarSentencia = 'CREATE LOGIN [' + @logueo + '] WITH PASSWORD = ' + QUOTENAME(@contrasena, '''')
			EXEC (@VarSentencia)
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -4
			END
	
			SET @VarSentencia = 'Create User [' + @logueo + '] From Login [' + convert(varchar(MAX),@logueo) + ']'
			EXEC (@VarSentencia)
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -5
			END

		COMMIT TRANSACTION;

		EXEC sp_addrolemember @rolename='db_rol_empresa' , @membername=@logueo
		IF (@@ERROR <> 0)
		BEGIN
			RETURN -6
		END
	END
	ELSE
	BEGIN
		BEGIN TRANSACTION;
			INSERT INTO Usuario VALUES (@logueo, @contrasena, @nombreCompleto, 1)
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -2
			END
			INSERT INTO Empresa VALUES (@logueo, @telefono, @direccion, @email)
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -3
			END

			DECLARE @VarSentencia2 VARCHAR(200)
			SET @VarSentencia2 = 'CREATE LOGIN [' + @logueo + '] WITH PASSWORD = ' + QUOTENAME(@contrasena, '''')
			EXEC (@VarSentencia2)
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -4
			END
	
			SET @VarSentencia2 = 'Create User [' + @logueo + '] From Login [' + convert(varchar(MAX),@logueo) + ']'
			EXEC (@VarSentencia2)
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -5
			END
		
		COMMIT TRANSACTION;

		EXEC sp_addrolemember @rolename='db_rol_empresa' , @membername=@logueo
		IF (@@ERROR <> 0)
		BEGIN
			RETURN -6
		END
	END
END

GO

CREATE PROCEDURE ModificarEmpresa
@usLog VARCHAR(12),
@logueo VARCHAR(12),
@contrasena VARCHAR(6),
@nombreCompleto VARCHAR(50),
@telefono INT,
@direccion VARCHAR(50),
@email VARCHAR(50)
AS
BEGIN
	IF (@usLog <> @logueo)
	BEGIN
		RETURN -1
	END
	IF NOT EXISTS (SELECT * 
					FROM Usuario a
					inner join Empresa b on a.logueo = b.logueo
					WHERE a.logueo = @logueo AND a.activo = 1) -- join a empresa 
	BEGIN
		RETURN -2
	END

	BEGIN TRANSACTION;
	UPDATE Usuario
	SET contrasena = @contrasena, 
		nombreCompleto = @nombreCompleto -- solo el propio usuario empresa puede modificar la contraseña
	WHERE logueo = @logueo
	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRANSACTION;
		RETURN -3
	END
	UPDATE Empresa
	SET telefono = @telefono, 
		direccion = @direccion, 
		email = @email
	WHERE logueo = @logueo
	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRANSACTION;
		RETURN -4
	END
	COMMIT TRANSACTION;

	DECLARE @VarSentencia VARCHAR(50);
	SET @VarSentencia = 'EXEC sp_PASSWORD NULL, [' + @contrasena + '], ' + convert(varchar(MAX),@logueo) + ';'
	EXEC (@VarSentencia)
	IF (@@ERROR <> 0)
	BEGIN
		RETURN -5
	END
END

GO

CREATE PROCEDURE BajaEmpresa
@logueo VARCHAR(50)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Empresa WHERE logueo = @logueo)
	BEGIN
		RETURN -1
	END
	IF EXISTS (SELECT * FROM Paquete WHERE empresa = @logueo)
	BEGIN
		BEGIN TRANSACTION;
			UPDATE Usuario SET activo = 0 WHERE logueo = @logueo
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -2
			END
			
			DECLARE @VarSentencia VARCHAR(200)
			SET @VarSentencia = 'DROP USER [' + convert(varchar(MAX),@logueo) + ']'
			EXEC (@VarSentencia)
			IF (@@ERROR <> 0)
			BEGIN
			   ROLLBACK TRANSACTION;
			   RETURN -4
			END
    
			SET @VarSentencia = 'DROP LOGIN [' + convert(varchar(MAX),@logueo) + ']'
			EXEC (@VarSentencia)
			IF (@@ERROR <> 0)
			BEGIN
			   ROLLBACK TRANSACTION;
			   RETURN -5
			END

		COMMIT TRANSACTION;
	END
	ELSE
	BEGIN
		BEGIN TRANSACTION;
			DELETE FROM Empresa WHERE logueo = @logueo
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -3
			END
			DELETE FROM Usuario WHERE logueo = @logueo
			IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION;
				RETURN -2
			END

			DECLARE @VarSentencia2 VARCHAR(200)
			SET @VarSentencia2 = 'DROP USER [' + convert(varchar(MAX),@logueo) + ']'
			EXEC (@VarSentencia2)
			IF (@@ERROR <> 0)
			BEGIN
			   ROLLBACK TRANSACTION;
			   RETURN -4
			END
    
			SET @VarSentencia2 = 'DROP LOGIN [' + convert(varchar(MAX),@logueo) + ']'
			EXEC (@VarSentencia2)
			IF (@@ERROR <> 0)
			BEGIN
			   ROLLBACK TRANSACTION;
			   RETURN -5
			END
		COMMIT TRANSACTION;
	END
END	

GO

CREATE PROCEDURE ListarEmpresas
AS
BEGIN
	SELECT Usuario.*, Empresa.telefono, Empresa.direccion, Empresa.email 
	FROM Usuario INNER JOIN Empresa
	ON Usuario.logueo = Empresa.logueo
END

GO

--------------------------------------------SP Paquete------------------------------------------
CREATE PROCEDURE BuscarPaquete
@codigo int
AS
BEGIN
	select *
	from Paquete
	where codigo = @codigo
END

GO

CREATE PROCEDURE AltaPaquete
@codigo INT,
@tipo VARCHAR(6),
@descripcion VARCHAR(100),
@peso DECIMAL,
@empresa VARCHAR(50)
AS
BEGIN
	IF NOT EXISTS (SELECT * 
					FROM Usuario a 
					inner join Empresa b on a.logueo = b.logueo
					WHERE a.logueo = @empresa AND a.activo = 1)	-- joiniar tiene que estar  activa la empresa
	BEGIN
		RETURN -1
	END
	IF EXISTS (SELECT * FROM Paquete WHERE codigo = @codigo)	
	BEGIN
		RETURN -2
	END
	INSERT INTO Paquete VALUES (@codigo, @tipo, @descripcion, @peso, @empresa)
	IF (@@ERROR <> 0)
	BEGIN
		RETURN -3
	END
END

GO
-- listado de paquetes asi no sirve, 
-- es necesario listar paquetes sin solicitud 
-- y listar paquetes de una solicitud en particular.

CREATE PROCEDURE ListarPaquetesSinSolicitud
AS
BEGIN
	SELECT * 
	FROM Paquete
	WHERE codigo not in (SELECT codigoPaquete FROM PaquetesSolicitud)
END

GO

CREATE PROCEDURE ListarPaquetesSolicitud
@solicitud int
AS
BEGIN
	SELECT *
	FROM Paquete
	WHERE codigo in (
		SELECT codigoPaquete 
		FROM PaquetesSolicitud 
		WHERE numeroSolicitud = @solicitud)
END

GO

--------------------------------------------SP Solicitud------------------------------------------
CREATE PROCEDURE AltaSolicitud
@fechaEntrega DATETIME, 
@nombreDestinatario VARCHAR(50), 
@direccionDestinatario VARCHAR(50),
@empleado VARCHAR(50)
AS
BEGIN
	IF NOT EXISTS (SELECT * 
					FROM Usuario a
					inner join Empleado b on a.logueo = b.logueo
					WHERE a.logueo = @empleado AND a.activo = 1 )	-- empleado activo?
	BEGIN
		RETURN -1
	END
	INSERT INTO Solicitud (fechaEntrega, nombreDestinatario, direccionDestinatario, empleado)
	VALUES (@fechaEntrega, @nombreDestinatario, @direccionDestinatario, @empleado)
	IF (@@ERROR <> 0)
	BEGIN
		RETURN -2
	END
	RETURN @@IDENTITY
END


GO

CREATE PROCEDURE AltaPaqueteSolicitud
@numeroSolicitud INT,
@codigoPaquete INT
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Solicitud WHERE numero = @numeroSolicitud)	
	BEGIN
		RETURN -1
	END

	IF NOT EXISTS (SELECT * FROM Paquete WHERE codigo = @codigoPaquete)	
	BEGIN
		RETURN -2
	END

	IF EXISTS (SELECT * FROM PaquetesSolicitud 
		WHERE numeroSolicitud = @numeroSolicitud AND codigoPaquete = @codigoPaquete)	
	BEGIN
		RETURN -3
	END

	INSERT INTO PaquetesSolicitud
	VALUES (@numeroSolicitud, @codigoPaquete)

	IF (@@ERROR <> 0)
	BEGIN
		RETURN -4
	END
END

GO 

CREATE PROCEDURE ModificarEstadoSolicitud -- seria modificarEstadoSolicitud con la logica necesaria.
@numero INT,
@empleado VARCHAR(50)
AS
BEGIN
	IF NOT EXISTS (SELECT * 
					FROM Usuario a
					inner join Empleado b on a.logueo = b.logueo
					WHERE a.logueo = @empleado AND a.activo = 1 )	
	BEGIN
		RETURN -1
	END

	IF NOT EXISTS (SELECT * FROM Solicitud WHERE numero = @numero)	
	BEGIN
		RETURN -2
	END
	-- seria con todos los datos de la solicitud por parametro,
	-- Y consulto el estado a travez de allí
	-- seria con este ejemplo y solo el numero de solicitud como parametro?
	UPDATE Solicitud 
	SET  estado = CASE 
					WHEN  estado = 'en deposito' then 'en camino'
					WHEN estado = 'en camino' then 'entregado'
				 END,
		empleado = @empleado -- Modifico el empleado? y los demas datos de la solicitud?
 	WHERE numero = @numero

	IF (@@ERROR <> 0)
	BEGIN
		RETURN -3
	END
END

GO

-- todas no, faltan listadoSolicitudeEnCamino y ListadoSolicitudesEmpresa

CREATE PROCEDURE listadoSolicitudesEnCamino
AS
BEGIN
	SELECT *
	FROM Solicitud
	WHERE estado = 'en camino'
END

GO

CREATE PROCEDURE ListadoSolicitudesEmpresa
AS
BEGIN
	SELECT a.*
	FROM Solicitud a
	INNER JOIN PaquetesSolicitud b on a.numero = b.numeroSolicitud
	INNER JOIN Paquete c on b.codigoPaquete = c.codigo
END

GO

CREATE PROCEDURE listadoPaquetesSolicitud
@numeroSolicitud INT
AS
BEGIN
	SELECT *
	FROM PaquetesSolicitud
	WHERE numeroSolicitud = @numeroSolicitud
END


GO

--publico
GRANT EXECUTE ON dbo.LogueoEmpleado TO [db_rol_publico]
GRANT EXECUTE ON dbo.LogueoEmpresa TO [db_rol_publico]
GRANT EXECUTE ON dbo.listadoSolicitudesEnCamino TO [db_rol_publico]
GRANT EXECUTE ON dbo.interBuscarEmpleado TO [db_rol_publico]
GRANT EXECUTE ON dbo.interBuscarEmpresa TO [db_rol_publico]
GRANT EXECUTE ON dbo.listadoPaquetesSolicitud TO [db_rol_publico]


--empleado
GRANT EXECUTE ON dbo.AltaEmpleado TO [db_rol_empleado]
GRANT EXECUTE ON dbo.BajaEmpleado TO [db_rol_empleado]
GRANT EXECUTE ON dbo.ModificarEmpleado TO [db_rol_empleado]
GRANT EXECUTE ON dbo.AltaEmpresa TO [db_rol_empleado]
GRANT EXECUTE ON dbo.BajaEmpresa TO [db_rol_empleado]
GRANT EXECUTE ON dbo.ModificarEmpresa TO [db_rol_empleado]
GRANT EXECUTE ON dbo.AltaPaquete TO [db_rol_empleado]
GRANT EXECUTE ON dbo.AltaSolicitud TO [db_rol_empleado]
GRANT EXECUTE ON dbo.AltaPaqueteSolicitud TO [db_rol_empleado]
GRANT EXECUTE ON dbo.ModificarEstadoSolicitud TO [db_rol_empleado]
GRANT EXECUTE ON dbo.ListarPaquetesLibres TO [db_rol_empleado]
GRANT EXECUTE ON dbo.BuscarEmpleado TO [db_rol_empleado]
GRANT EXECUTE ON dbo.BuscarEmpresa TO [db_rol_empleado]
GRANT EXECUTE ON dbo.BuscarPaquete TO [db_rol_empleado]
GRANT EXECUTE ON dbo.listarEmpresas TO [db_rol_empleado]

--empresa
GRANT EXECUTE ON dbo.ListadoSolicitudesEmpresa TO [db_rol_empresa]





