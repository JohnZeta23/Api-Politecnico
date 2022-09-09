set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SpProfesoresInsertar
@Nombre AS VARCHAR(40),
@Materia AS VARCHAR(30),
@TurnoLunes AS VARCHAR(30),
@TurnoMartes AS VARCHAR(30),
@TurnoMiercoles AS VARCHAR(30),
@TurnoJueves AS VARCHAR(30),
@TurnoViernes AS VARCHAR(30),
@Fecha_Matriculacion AS DATE
AS
BEGIN
INSERT INTO Profesores (Nombre,Materia,TurnoLunes,TurnoMartes,TurnoMiercoles,TurnoJueves,TurnoViernes,Fecha_Matriculacion)
VALUES (@Nombre,@Materia,@TurnoLunes,@TurnoMartes,@TurnoMiercoles,@TurnoJueves,@TurnoViernes,@Fecha_Matriculacion)
END
GO

CREATE PROCEDURE SpProfesoresActualizar
@ID_Profesor AS INT,
@Nombre AS VARCHAR(40),
@Materia AS VARCHAR(30),
@TurnoLunes AS VARCHAR(30),
@TurnoMartes AS VARCHAR(30),
@TurnoMiercoles AS VARCHAR(30),
@TurnoJueves AS VARCHAR(30),
@TurnoViernes AS VARCHAR(30),
@Fecha_Matriculacion AS DATE
AS
BEGIN
UPDATE Profesores
SET Nombre = @Nombre,
Materia = @Materia,
TurnoLunes = @TurnoLunes,
TurnoMartes = @TurnoMartes,
TurnoMiercoles = @TurnoMiercoles,
TurnoJueves = @TurnoJueves,
TurnoViernes = @TurnoViernes,
Fecha_Matriculacion = @Fecha_Matriculacion
WHERE ID_Profesor = @ID_Profesor
END
GO

CREATE PROCEDURE SpProfesoresEliminar
@ID_Profesor AS INT
AS
BEGIN
UPDATE Profesores
SET Estatus = 'Inactivo'
WHERE ID_Profesor = @ID_Profesor
END
GO

CREATE PROCEDURE SpProfesoresListar
AS
BEGIN
SELECT ID_Profesor,
Nombre,
Materia,
TurnoLunes,
TurnoMartes,
TurnoMiercoles,
TurnoJueves,
TurnoViernes,
FORMAT (Fecha_Matriculacion, 'dd/MM/yyyy ') as Fecha_Matriculacion,
FORMAT (Fecha_Salida, 'dd/MM/yyyy ') as Fecha_Salida,
Estatus
FROM Profesores
END
GO

CREATE PROCEDURE SpProfesoresObtener
@Nombre AS VARCHAR(40)
AS
BEGIN
SELECT TOP 1 
ID_Profesor,
Nombre,
Materia,
TurnoLunes,
TurnoMartes,
TurnoMiercoles,
TurnoJueves,
TurnoViernes,
FORMAT (Fecha_Matriculacion, 'dd/MM/yyyy ') as Fecha_Matriculacion,
FORMAT (Fecha_Salida, 'dd/MM/yyyy ') as Fecha_Salida,
Estatus
FROM Profesores
WHERE Nombre = @Nombre
END
GO

CREATE PROCEDURE SpTurnoLunesObtener
@Nombre AS VARCHAR(40),
@TurnoLunes AS VARCHAR(30)
AS
BEGIN
SELECT 
ID_Profesor
FROM Profesores
WHERE TurnoLunes = @TurnoLunes AND Nombre NOT IN(@Nombre)
END
GO

CREATE PROCEDURE SpTurnoMartesObtener
@Nombre AS VARCHAR(40),
@TurnoMartes AS VARCHAR(30)
AS
BEGIN
SELECT 
ID_Profesor
FROM Profesores
WHERE TurnoMartes = @TurnoMartes AND Nombre NOT IN(@Nombre)
END
GO

CREATE PROCEDURE SpTurnoMiercolesObtener
@Nombre AS VARCHAR(40),
@TurnoMiercoles AS VARCHAR(30)
AS
BEGIN
SELECT 
ID_Profesor
FROM Profesores
WHERE TurnoMiercoles = @TurnoMiercoles AND Nombre NOT IN(@Nombre)
END
GO

CREATE PROCEDURE SpTurnoJuevesObtener
@Nombre AS VARCHAR(40),
@TurnoJueves AS VARCHAR(30)
AS
BEGIN
SELECT 
ID_Profesor
FROM Profesores
WHERE TurnoJueves = @TurnoJueves AND Nombre NOT IN(@Nombre)
END
GO

CREATE PROCEDURE SpTurnoViernesObtener
@Nombre AS VARCHAR(40),
@TurnoViernes AS VARCHAR(30)
AS
BEGIN
SELECT 
ID_Profesor
FROM Profesores
WHERE TurnoViernes = @TurnoViernes AND Nombre NOT IN(@Nombre)
END
GO