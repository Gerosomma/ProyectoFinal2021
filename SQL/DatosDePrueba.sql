
---------------DATOS-DE-PRUEBA----------------------------------------------------

EXEC AltaEmpleado 'emi', 'asd12?', 'luquitas rodriguez', '09:00:00', '18:00:00';

EXEC AltaEmpleado 'ge', 'asd12?', 'Gero 1', '09:00:00', '18:00:00';
EXEC AltaEmpleado 'ajajaj', '123456', 'Gero 1', '09:00:00.0000', '18:00:0.0000';
EXEC AltaEmpleado 'agu', '123456', 'Gero 1', '09:00:00.0000', '18:00:0.0000';

EXEC AltaEmpleado 'pppp', '123456', 'Gero 1', '09:00:00.0000', '18:00:0.0000';

EXEC AltaEmpresa 'jero', 'asd12?', 'Gero 2', '091654252', '18 de julio y rio negro', 'geronimo.somma@gsoft.com.uy';


exec AltaEmpleado 'luque', 'losque', 'los que queres', '9:00:00', '13:00:00'

select * from Usuario;

select * from Empleado;
select * from Empresa;


delete from Empleado
delete from Usuario



select * from Solicitud;
select * from Paquete;
select * from PaquetesSolicitud;


GRANT EXECUTE ON sp_addrolemember TO [db_rol_empleado]


exec AltaSolicitud '2021-05-08 12:35:29.123', 'geronimo somma', '18 de julio y rio negro', 'en deposito', 'gero';

exec AltaPaquete 1, 'fragil', 'paquete sospechoso', 25, 'jero'; 

exec AltaPaqueteSolicitud 1, 1;

exec listadoSolicitudes

EXEC AltaEmpleado 'gero', 'asd12?', 'Geronimo somma', '09:00:00', '18:00:00';

EXEC AltaEmpresa 'jero', 'asd12?', 'Gero 2', '091654252', '18 de julio y rio negro', 'geronimo.somma@gsoft.com.uy';


insert into Usuario
values ('ursula', 'asd12?', 'ursula pinola', 1);

insert into empresa
values ('ursula', '091283234', 'asdasdasd 353', 'asda@asds.com');

select * from Empresa
select * from Usuario

update empresa set telefono = '091283234'

exec sp_who

GRANT ALTER ANY USER ON DATABASE::ProyectoSegundo2020 TO [ge]

GRANT ALTER ANY ROLE ON DATABASE::ProyectoSegundo2020 TO ge



select b.name as dbname,a.*
from sys.sysprocesses a , sys.sysdatabases b
where a.dbid=b.dbid


SELECT   *
FROM     sys.dm_exec_sessions
WHERE    login_name = 'gero'


kill 51

CREATE LOGIN rafa WITH PASSWORD = 'asd12?'

Create User rafa From Login rafa

	
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


exec ListarPaquetesSinSolicitud;
exec listadoSolicitudes;

exec AltaSolicitud  '2021-05-08 12:35:29.123', 'jeremain', '18 de julio', 'gero';

exec AltaPaqueteSolicitud 2, 2222

select * from Solicitud
select * from Empresa

select * from Paquete

exec listadoSolicitudes
exec ListadoSolicitudesEmpresa 'nacho'

exec listadoSolicitudesEnCamino



select * from solicitud
select * from PaquetesSolicitud