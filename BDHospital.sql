Create database HotelEvaluacion
go
use HotelEvaluacion
go

--Tablas independientes

Create table Rol (
    idRol int identity(1,1) primary key,
    nombreRol varchar(25) not null
);
go

Create table EstadoReserva (
    idEstadoReserva int identity(1,1) primary key,
    nombreEstadoReserva varchar(25) not null
);
go

Create table EstadoHabitacion (
    idEstadoHabitacion int identity(1,1) primary key,
    nombreEstadoHabitacion varchar(25) not null
);
go

Create table Servicio (
    idServicio int identity(1,1) primary key,
    nombreServicio varchar(30) not null
);
go

Create table Cliente (
    idCliente int identity(1,1) primary key,
    nombreCliente varchar(35) not null,
    apellidoCliente varchar(35) not null,
    correoCliente varchar(60) not null,
);
go
 

--Tablas dependientes

Create table Usuario (
    idUsuario int identity(1,1) primary key,
    correoUsuario varchar(60) not null,
    clave varchar(100) not null,
    id_Rol int not null,
    constraint fk_idRol foreign key (id_Rol) references Rol(idRol) on delete cascade
);
go

Create table Reservacion (
    idReserva int identity(1,1) primary key,
    fechaRegistro date not null,
    checkIn datetime not null,
    checkOut datetime not null,
    id_Cliente int not null,
    id_EstadoReserva int not null,
    constraint fk_idCliente foreign key (id_Cliente) references Cliente(idCliente) on delete cascade,
    constraint fk_idEstadoReserva foreign key (id_EstadoReserva) references EstadoReserva(idEstadoReserva) on delete cascade
);
go

Create table Habitacion (
    idHabitacion int identity(1,1) primary key,
    nombreHabitacion varchar(5) not null,
    ubicacion varchar(50) not null,
    numCamas varchar(1) not null,
    id_EstadoHabitacion int not null,
    constraint fk_idEstadoHabitacion foreign key (id_EstadoHabitacion) references EstadoHabitacion(idEstadoHabitacion) on delete cascade
);
go

--Tablas intermedias

Create table HabitacionReserva (
    idHabitacionReserva int identity(1,1) primary key,
    id_Reserva int not null,
    id_Habitacion int not null,
    constraint fk_idReserva foreign key (id_Reserva) references Reservacion(idReserva) on delete cascade,
    constraint fk_idHabitacion foreign key (id_Habitacion) references Habitacion(idHabitacion) on delete cascade
);
go

Create table ServicioReserva (
    idServicioReserva int identity(1,1) primary key,
    id_HabitacionReserva int not null,
    id_Servicio int not null,
    constraint fk_idHabitacionReserva foreign key (id_HabitacionReserva) references HabitacionReserva(idHabitacionReserva) on delete cascade,
    constraint fk_idServicio foreign key (id_Servicio) references Servicio(idServicio) on delete cascade
);
go

-- inserciones --

insert into Rol values 
('Administrador'),
('Recepcionista'),
('Gerente');

select *from Rol
select *from Usuario
