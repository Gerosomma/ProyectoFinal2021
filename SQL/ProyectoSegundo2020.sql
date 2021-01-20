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
	logueo VARCHAR(50) NOT NULL PRIMARY KEY,
	contrasena VARCHAR(6) NOT NULL,
	nombreCompleto VARCHAR(50) NOT NULL,
	activo BIT NOT NULL DEFAULT 1
)

CREATE TABLE Empresa (
	logueo VARCHAR(50) NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Usuario(logueo),
	telefono INT NOT NULL,
	direccion VARCHAR(50) NOT NULL,
	email VARCHAR(50) NOT NULL,
	activo BIT NOT NULL DEFAULT 1
)

CREATE TABLE Empleado (
	logueo VARCHAR(50) NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Usuario(logueo),
	horaInicio TIME NOT NULL,
	horaFin TIME NOT NULL,
	activo BIT NOT NULL DEFAULT 1
)

CREATE TABLE Paquete (
	codigo INT NOT NULL PRIMARY KEY,
	tipo VARCHAR(6) NOT NULL CHECK (tipo = 'fragil' OR tipo = 'comun' OR tipo = 'bulto'),
	descripcion VARCHAR(100) NOT NULL,
	peso DECIMAL NOT NULL,
	empresa VARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Empresa(logueo)
)

CREATE TABLE Solicitud (
	numero INT NOT NULL PRIMARY KEY  IDENTITY,
	fechaEntrega DATETIME NOT NULL, 
	nombreDestinatario VARCHAR(50) NOT NULL, 
	direccionDestinatario VARCHAR(50) NOT NULL,
	estado VARCHAR(11) NOT NULL  DEFAULT 'en deposito' CHECK (estado = 'en deposito' OR estado = 'en camino' OR estado = 'entregado'),
	empleado VARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Empleado(logueo)
)

CREATE TABLE Contienen (
	numeroSolicitud INT NOT NULL FOREIGN KEY REFERENCES Solicitud(numero),
	codigoPaquete INT NOT NULL FOREIGN KEY REFERENCES Paquete(codigo)
	PRIMARY KEY (numeroSolicitud, codigoPaquete)
)

GO

--------------------------------------------SP Base Datos------------------------------------------
CREATE PROCEDURE NuevoUsuarioSQL
@nombre VARCHAR(50),
@pass VARCHAR(6),
@rol VARCHAR(30)
AS
BEGIN
	DECLARE @VarSentencia VARCHAR(200)
	SET @VarSentencia = 'CREATE LOGIN [' + @nombre + '] WITH PASSWORD = ' + QUOTENAME(@pass, '''')
	EXEC (@VarSentencia)

	IF (@@ERROR <> 0)
		RETURN -1

	EXEC sp_addsrvrolemember @loginame=@nombre, @rolename=@rol
	
	IF (@@ERROR = 0)
		RETURN 1
	ELSE
		RETURN -2
END

GO

CREATE PROCEDURE NuevoUsuarioBD
@nombre VARCHAR(50),
@rol VARCHAR(30),
@logueo VARCHAR(50)
AS
BEGIN
	DECLARE @VarSentencia VARCHAR(200)
	SET @VarSentencia = 'CREATE USER [' + @nombre + '] FROM LOGIN [' + @logueo + ']'
	EXEC @VarSentencia

	IF (@@ERROR <> 0)
		RETURN -1

	EXEC sp_addrolemember @rolename=@rol, @membername=@nombre

	IF (@@ERROR = 0)
		RETURN 1
	ELSE
		RETURN -2
END

GO

CREATE PROCEDURE EliminarUsuarioSQL
@nombre VARCHAR(50)
AS
BEGIN
   DECLARE @VarSentencia VARCHAR(200)
   SET @VarSentencia = 'DROP LOGIN [' + @nombre + ']'
   EXEC (@VarSentencia)

   IF (@@ERROR <> 0)
       RETURN -1
END

GO

CREATE PROCEDURE EliminarUsuarioBD 
@nombre VARCHAR(50)
AS
BEGIN
   DECLARE @VarSentencia VARCHAR(200)
   SET @VarSentencia = 'DROP LOGIN [' + @nombre + ']'
   EXEC (@VarSentencia)

   IF (@@ERROR <> 0)
       RETURN -1
END   

GO

--------------------------------------------SP Empleado------------------------------------------
CREATE PROCEDURE BuscarEmpleado
@logueo VARCHAR(50)
AS
BEGIN
	SELECT Usuario.*, Empleado.horaInicio, Empleado.horaFin
	FROM Usuario INNER JOIN Empleado
	ON Usuario.logueo = Empleado.logueo
	WHERE Usuario.logueo = @logueo AND Usuario.activo = 1
END

GO

CREATE PROCEDURE AltaEmpleado
@logueo VARCHAR(50),
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
	IF EXISTS (SELECT * FROM Usuario WHERE logueo = @logueo AND activo = 0)
		BEGIN TRANSACTION
			UPDATE Usuario SET activo = 1 WHERE logueo = @logueo
			IF (@@ERROR <> 0)
				BEGIN
					ROLLBACK TRANSACTION
					RETURN -2
				END
			UPDATE Empleado SET activo = 1 WHERE logueo = @logueo
			IF (@@ERROR <> 0)
				BEGIN
					ROLLBACK TRANSACTION
					RETURN -3
				END	
	BEGIN TRANSACTION 
		INSERT INTO Usuario VALUES (@logueo, @contrasena, @nombreCompleto)
		IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION
				RETURN -4
			END
		INSERT INTO Empleado VALUES (@logueo, @horaInicio, @horaFin)
		IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION
				RETURN -5
			END
	COMMIT TRANSACTION

	EXEC NuevoUsuarioSQL @logueo, @contrasena, 'securityadmin';
	IF (@@ERROR <> 0)
		BEGIN
			RETURN -6
		END

	EXEC NuevoUsuarioBD @logueo, 'db_securityadmin', @logueo;

	DECLARE @VarSentencia VARCHAR(200)
	SET @VarSentencia = 'GRANT select, Insert, Update, delete ON Usuario TO ' + @logueo
	EXEC (@VarSentencia)
	SET @VarSentencia = 'GRANT select, Insert, Update, delete ON Empleado TO ' + @logueo
	EXEC (@VarSentencia)
	SET @VarSentencia = 'GRANT select, Insert, Update, delete ON Empresa TO ' + @logueo
	EXEC (@VarSentencia)
	SET @VarSentencia = 'GRANT select, Insert, Update, delete ON Paquete TO ' + @logueo
	EXEC (@VarSentencia)
	SET @VarSentencia = 'GRANT select, Insert, Update, delete ON Solicitud TO ' + @logueo
	EXEC (@VarSentencia)
	SET @VarSentencia = 'GRANT EXECUTE TO ' + @logueo
	EXEC (@VarSentencia)
	IF (@@ERROR <> 0)
		BEGIN
			RETURN -7
		END
END

GO

CREATE PROCEDURE ModificarEmpleado
@logueo VARCHAR(50),
@contrasena VARCHAR(6),
@nombreCompleto VARCHAR(50),
@horaInicio TIME,
@horaFin TIME
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Usuario WHERE logueo = @logueo AND activo = 1)
		BEGIN
			RETURN -1
		END

		BEGIN TRANSACTION
		UPDATE Usuario
		SET contrasena = @contrasena, nombreCompleto = @nombreCompleto
		WHERE logueo = @logueo
		IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION
				RETURN -2
			END
		UPDATE Empleado
		SET horaInicio = @horaInicio, horaFin = @horaFin
		WHERE logueo = @logueo
		IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION
				RETURN -3
			END
	COMMIT TRANSACTION
END

GO

CREATE PROCEDURE BajaEmpleado
@logueo VARCHAR(50)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Empleado WHERE logueo = @logueo)
		BEGIN
			RETURN -1
		END
	IF EXISTS (SELECT * FROM Solicitud WHERE empleado = @logueo)
		BEGIN
		BEGIN TRANSACTION
			UPDATE Usuario SET activo = 0 WHERE logueo = @logueo
			IF (@@ERROR <> 0)
				BEGIN
					ROLLBACK TRANSACTION
					RETURN -2
				END
			UPDATE Empleado SET activo = 0 WHERE logueo = @logueo
			IF (@@ERROR <> 0)
				BEGIN
					ROLLBACK TRANSACTION
					RETURN -3
				END
		COMMIT TRANSACTION
		END
	ELSE
		BEGIN TRANSACTION
			DELETE FROM Empleado WHERE logueo = @logueo
			IF (@@ERROR <> 0)
				BEGIN
					ROLLBACK TRANSACTION
					RETURN -4
				END
			DELETE FROM Usuario WHERE logueo = @logueo
			IF (@@ERROR <> 0)
				BEGIN
					ROLLBACK TRANSACTION
					RETURN -5
				END
		COMMIT TRANSACTION
END	

GO

--------------------------------------------SP Empresa------------------------------------------
CREATE PROCEDURE BuscarEmpresa
@logueo VARCHAR(50)
AS
BEGIN
	SELECT Usuario.*, Empresa.telefono, Empresa.direccion, Empresa.email
	FROM Usuario INNER JOIN Empresa
	ON Usuario.logueo = Empresa.logueo
	WHERE Usuario.logueo = @logueo AND Usuario.activo = 1
END

GO

CREATE PROCEDURE AltaEmpresa
@logueo VARCHAR(50),
@contrasena VARCHAR(6),
@nombreCompleto VARCHAR(50),
@telefono INT,
@direccion VARCHAR(50),
@email VARCHAR(50)
AS
BEGIN
	IF EXISTS (SELECT * FROM Usuario WHERE logueo = @logueo AND activo = 1)
		BEGIN
			RETURN -1
		END
	IF EXISTS (SELECT * FROM Usuario WHERE logueo = @logueo AND activo = 0)
		BEGIN TRANSACTION
			UPDATE Usuario SET activo = 1 WHERE logueo = @logueo
			IF (@@ERROR <> 0)
				BEGIN
					ROLLBACK TRANSACTION
					RETURN -2
				END
			UPDATE Empresa SET activo = 1 WHERE logueo = @logueo
			IF (@@ERROR <> 0)
				BEGIN
					ROLLBACK TRANSACTION
					RETURN -3
				END
	BEGIN TRANSACTION 
		INSERT INTO Usuario VALUES (@logueo, @contrasena, @nombreCompleto)
		IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION
				RETURN -4
			END
		INSERT INTO Empresa VALUES (@logueo, @telefono, @direccion, @email)
		IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION
				RETURN -5
			END
	COMMIT TRANSACTION

	EXEC NuevoUsuarioSQL @logueo, @contrasena, 'securityadmin';
	IF (@@ERROR <> 0)
		BEGIN
			RETURN -6
		END

	EXEC NuevoUsuarioBD @logueo, 'db_securityadmin', @logueo;

	DECLARE @VarSentencia VARCHAR(200)
	SET @VarSentencia = 'GRANT select ON Solicitud TO ' + @logueo
	EXEC (@VarSentencia)
	SET @VarSentencia = 'GRANT EXECUTE TO ' + @logueo
	EXEC (@VarSentencia)
	IF (@@ERROR <> 0)
		BEGIN
			RETURN -7
		END
END

GO

CREATE PROCEDURE ModificarEmpresa
@logueo VARCHAR(50),
@contrasena VARCHAR(6),
@nombreCompleto VARCHAR(50),
@telefono INT,
@direccion VARCHAR(50),
@email VARCHAR(50)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Usuario WHERE logueo = @logueo AND activo = 1)
		BEGIN
			RETURN -1
		END

		BEGIN TRANSACTION
		UPDATE Usuario
		SET contrasena = @contrasena, nombreCompleto = @nombreCompleto
		WHERE logueo = @logueo
		IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION
				RETURN -2
			END
		UPDATE Empresa
		SET telefono = @telefono, direccion = @direccion, email = @email
		WHERE logueo = @logueo
		IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION
				RETURN -3
			END
	COMMIT TRANSACTION
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
		BEGIN TRANSACTION
			UPDATE Usuario SET activo = 0 WHERE logueo = @logueo
			IF (@@ERROR <> 0)
				BEGIN
					RETURN -2
				END
			UPDATE Empresa SET activo = 0 WHERE logueo = @logueo
			IF (@@ERROR <> 0)
				BEGIN
					RETURN -3
				END
		COMMIT TRANSACTION
		END
	ELSE
		BEGIN TRANSACTION
			DELETE FROM Empleado WHERE logueo = @logueo
			IF (@@ERROR <> 0)
				BEGIN
					RETURN -4
				END
			DELETE FROM Usuario WHERE logueo = @logueo
			IF (@@ERROR <> 0)
				BEGIN
					RETURN -5
				END
		COMMIT TRANSACTION
END	

GO

--------------------------------------------SP Paquete------------------------------------------
CREATE PROCEDURE AltaPaquete
@codigo INT,
@tipo VARCHAR(6),
@descripcion VARCHAR(100),
@peso DECIMAL,
@empresa VARCHAR(50)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Empresa WHERE logueo = @empresa)	
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

CREATE PROCEDURE ListarPaquetes
AS
BEGIN
	SELECT * FROM Paquete
END

GO

--------------------------------------------SP Solicitud------------------------------------------
CREATE PROCEDURE AltaSolicitud
@fechaEntrega DATETIME, 
@nombreDestinatario VARCHAR(50), 
@direccionDestinatario VARCHAR(50),
@estado VARCHAR(11),
@empleado VARCHAR(50)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Empleado WHERE logueo = @empleado)	
	BEGIN
		RETURN -1
	END
	INSERT INTO Solicitud VALUES (@fechaEntrega, @nombreDestinatario, @direccionDestinatario, @estado, @empleado)
	IF (@@ERROR <> 0)
	BEGIN
		RETURN -2
	END
	RETURN @@IDENTITY
END