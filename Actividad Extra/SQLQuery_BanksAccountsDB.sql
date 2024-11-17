create database BanksAccountsDB;
use BanksAccountsDB;

create table Monedas(
	IDMoneda           int primary key identity,
	MonedaNombre       varchar(50)  not null unique,
	MonedaNacionalidad varchar(200) not null,
	MonedaClave        varchar(10)  not null unique
);

create table Clientes(
	IDCliente        bigint primary key identity,
	ClienteNombre    varchar(100) not null,
	ClienteApellido  varchar(150) not null,
	ClienteRFC       varchar(15)  not null unique,
	ClienteTelefono  varchar(15) unique,
	ClienteDireccion varchar(255) not null
);

create table Bancos(
	IDBanco       int primary key identity,
	BancoNombre   varchar(255) not null,
	BancoCodigo   varchar(13)  not null unique,
	BancoNacional bit          not null,
	BancoMoneda   varchar(10)  not null
);

create table Cuentas(
	IDCuenta           bigint primary key identity,
	CuentaClave        bigint       not null unique,
	CuentaSaldo        float        not null,
    CuentaCorreo       varchar(255) not null,
	CuentaIDCliente    bigint       not null,
	CuentaBancoClave   varchar(13)  not null,
	CuentaMonedaClave  varchar(10)  not null,
);

create table Registros(
	IDRegistro              bigint primary key identity,
	RegistroSaldo           float         not null,
	RegistroFecha           varchar(30) not null,
	RegistroCuentaClave     bigint        not null
);

create table Movimientos(
	IDMovimiento             bigint primary key identity,
	MovimientoMonto          bigint        not null,
	MovimientoFecha          varchar(30)   not null,
	MovimientoConcepto       bigint        not null,
	MovimientoCuentaEmisor   bigint        not null,
	MovimientoCuentaReceptor bigint        not null,
);

create table Cambios(
	IDCambio           bigint primary key identity,
	CambioTipo         int           not null,
	CambioFecha        smalldatetime not null,
	CambioIDMovimiento bigint        not null,
);

--FOREIGN KEYS
ALTER TABLE Cambios
ADD CONSTRAINT FK_CambioIDMovimiento FOREIGN KEY (CambioIDMovimiento) REFERENCES Movimientos(IDMovimiento);

ALTER TABLE Bancos
ADD CONSTRAINT FK_BancoMoneda FOREIGN KEY (BancoMoneda) REFERENCES Monedas(MonedaClave);

ALTER TABLE Cuentas
ADD CONSTRAINT FK_CuentaIDCliente FOREIGN KEY (CuentaIDCliente) REFERENCES Clientes(IDCliente),
    CONSTRAINT FK_CuentaBancoClave FOREIGN KEY (CuentaBancoClave) REFERENCES Bancos(BancoCodigo),
    CONSTRAINT FK_CuentaMonedaClave FOREIGN KEY (CuentaMonedaClave) REFERENCES Monedas(MonedaClave);

ALTER TABLE Registros
ADD CONSTRAINT FK_RegistroCuentaClave FOREIGN KEY (RegistroCuentaClave) REFERENCES Cuentas(CuentaClave);

ALTER TABLE Movimientos
ADD CONSTRAINT FK_MovimientoCuentaEmisor FOREIGN KEY (MovimientoCuentaEmisor) REFERENCES Cuentas(CuentaClave),
    CONSTRAINT FK_MovimientoCuentaReceptor FOREIGN KEY (MovimientoCuentaReceptor) REFERENCES Cuentas(CuentaClave);

-- Borrar foreign keys!!!
ALTER TABLE Cambios
DROP CONSTRAINT FK_CambioIDMovimiento;

ALTER TABLE Bancos
DROP CONSTRAINT FK_BancoMoneda;

ALTER TABLE Cuentas
DROP CONSTRAINT FK_CuentaIDCliente,
    CONSTRAINT FK_CuentaBancoClave,
    CONSTRAINT FK_CuentaMonedaClave;

ALTER TABLE Registros
DROP CONSTRAINT FK_RegistroMonedaClave,
    CONSTRAINT FK_RegistroBancoClave,
    CONSTRAINT FK_RegistroCuentaClave,
    CONSTRAINT FK_RegistroIDCliente,
    CONSTRAINT FK_RegistroIDMovimiento;

ALTER TABLE Movimientos
DROP CONSTRAINT FK_MovimientoCuentaEmisor,
    CONSTRAINT FK_MovimientoCuentaReceptor;



-- INSERTS
insert into Monedas (MonedaNombre, MonedaNacionalidad, MonedaClave) 
values 
	('Dolar estadounidense','Estados Unidos','USD'),
	('Peso mexicano'       ,'Mexico'        ,'MXN'),
	('Euro'                ,'Union europea' ,'EUR'),
	('Dolar canadiense'    ,'Canada'        ,'CAD'),
	('Libra esterlina'     ,'Reino unido'   ,'GBP'),
	('Peso argentino'      ,'Argentina'     ,'ARS'),
	('Peso chileno'        ,'Chile'         ,'CLP'),
	('Rublo ruso'          ,'Rusia'         ,'RUB'),
	('Rupia india'         ,'India'         ,'INR'),
	('Won surcoreano'      ,'Corea del sur' ,'KRW'),
	('Yen japones'         ,'Japon'         ,'JPY'),
	('Yuan chino'          ,'China'         ,'CNY');

insert  into Monedas (MonedaNombre, MonedaNacionalidad, MonedaClave)
values ('Peso X','Nacionalidad X','Clv');

insert into Clientes (ClienteNombre, ClienteApellido, ClienteRFC, ClienteTelefono, ClienteDireccion)
values 
	('Roberto'  ,'Grijalva'   ,'RFCCliente001',0123456789,'Direccion001'),
	('Nombre002','Apellido002','RFCCliente002',0123456788,'Direccion002'),
	('Nombre003','Apellido003','RFCCliente003',0123456787,'Direccion003'),
	('Nombre004','Apellido004','RFCCliente004',0123456786,'Direccion004'),
	('Nombre005','Apellido005','RFCCliente005',0123456785,'Direccion005'),
	('Nombre006','Apellido006','RFCCliente006',0123456784,'Direccion006'),
	('Nombre007','Apellido007','RFCCliente007',0123456783,'Direccion007'),
	('Nombre008','Apellido008','RFCCliente008',0123456782,'Direccion008'),
	('Nombre009','Apellido009','RFCCliente009',0123456781,'Direccion009'),
	('Nombre010','Apellido010','RFCCliente010',0123456780,'Direccion010');

insert into Bancos (BancoNombre, BancoCodigo, BancoNacional, BancoMoneda)
values 
	('BancoUno'   ,'BANK001' ,1,'USD'),
	('BancoDos'   ,'BANK002' ,0,'MXN'),
	('BancoTres'  ,'BANK003' ,1,'EUR'),
	('BancoCuatro','BANK004' ,0,'CAD'),
	('BancoCinco' ,'BANK005' ,1,'GBP'),
	('BancoSeis'  ,'BANK006' ,0,'ARS'),
	('BancoSiete' ,'BANK007' ,1,'CLP'),
	('BancoOcho'  ,'BANK008' ,0,'RUB'),
	('BancoNueve' ,'BANK009' ,1,'INR'),
	('BancoDiez'  ,'BANK010' ,0,'KRW'),
	('BancoOnce'  ,'BANK0011',1,'JPY'),
	('BancoDoce'  ,'BANK0012',0,'CNY'),
	('BancoTrece' ,'BANK0013',1,'MXN');

insert into Cuentas (CuentaClave, CuentaSaldo, CuentaCorreo, CuentaIDCliente, CuentaBancoClave, CuentaMonedaClave)
values 
	(0000000001,10000,'correo0001@ejemplo.com',1 ,'BANK001' ,'USD'),
	(0000000002,20000,'correo0001@ejemplo.com',1 ,'BANK001' ,'USD'),
	(0000000003,30000,'correo0001@ejemplo.com',1 ,'BANK002' ,'USD'),
	(0000000004,40000,'correo0002@ejemplo.com',2 ,'BANK001' ,'USD'),
	(0000000005,52000,'correo0002@ejemplo.com',2 ,'BANK002' ,'MXN'),
	(0000000006,62000,'correo0002@ejemplo.com',2 ,'BANK003' ,'EUR'),
	(0000000007,72000,'correo0003@ejemplo.com',3 ,'BANK004' ,'CAD'),
	(0000000008,82000,'correo0003@ejemplo.com',3 ,'BANK004' ,'CAD'),
	(0000000009,92000,'correo0004@ejemplo.com',4 ,'BANK005' ,'GBP'),
	(0000000010,12000,'correo0004@ejemplo.com',4 ,'BANK006' ,'ARS'),
	(0000000011,20000,'correo0005@ejemplo.com',5 ,'BANK007' ,'CLP'),
	(0000000012,32000,'correo0005@ejemplo.com',5 ,'BANK008' ,'RUB'),
	(0000000013,42000,'correo0006@ejemplo.com',6 ,'BANK009' ,'INR'),
	(0000000014,52000,'correo0007@ejemplo.com',7 ,'BANK010' ,'KRW'),
	(0000000015,62000,'correo0008@ejemplo.com',8 ,'BANK0011','JPY'),
	(0000000016,72000,'correo0009@ejemplo.com',9 ,'BANK0012','CNY'),
	(0000000017,82000,'correo0010@ejemplo.com',10,'BANK0013','MXN'),
	(0000000018,92000,'correo0010@ejemplo.com',10,'BANK0012','CNY'),
	(0000000019,12000,'correo0009@ejemplo.com',9 ,'BANK0011','JPY');



















--insert into Movimientos (MovimientoMonto, MovimientoFecha, MovimientoConcepto, MovimientoCuentaEmisor, MovimientoCuentaReceptor)
--values ();
select * from Monedas where IDMoneda = 1;
select * from Monedas;
--drop table Monedas, Movimientos, Cambios, Clientes, Cuentas, Bancos, Registros
--drop table Registros;
