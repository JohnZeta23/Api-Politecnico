CREATE DATABASE Politecnico

USE Politecnico

CREATE TABLE Profesores(
ID_Profesor int IDENTITY(1,1) primary key not null,
Nombre varchar(40),
Materia varchar(30),
TurnoLunes varchar(30),
TurnoMartes varchar(30),
TurnoMiercoles varchar(30),
TurnoJueves varchar(30),
TurnoViernes varchar(30),
Fecha_Matriculacion date,
Fecha_Salida date,
Estatus varchar (10) Default 'Activo' Check (Estatus = 'Activo' or Estatus = 'Inactivo') not null 
)

CREATE TABLE Estudiantes(
ID_Estudiante int IDENTITY(1,1) primary key not null,
Nombre varchar(30),
Grado varchar(30),
Aula varchar(15),
Fecha_Inscripcion date
)