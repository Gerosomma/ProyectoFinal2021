
---------------DATOS-DE-PRUEBA----------------------------------------------------


EXEC AltaEmpleado 'gero', '123456', 'Gero 1', '09:00:00', '18:00:00';
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


exec AltaSolicitud '2021-05-08 12:35:29.123', 'geronimo somma', '18 de julio y rio negro', 'en deposito', 'gero';

exec AltaPaquete 1, 'fragil', 'paquete sospechoso', 25, 'jero'; 

exec AltaPaqueteSolicitud 1, 1;

exec listadoSolicitudes


exec sp_who


select b.name as dbname,a.*
from sys.sysprocesses a , sys.sysdatabases b
where a.dbid=b.dbid


SELECT   *
FROM     sys.dm_exec_sessions
WHERE    login_name = 'gero'


kill 51