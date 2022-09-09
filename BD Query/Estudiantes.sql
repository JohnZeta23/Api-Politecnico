set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SpEstudiantesInsertar
@Nombre AS VARCHAR(30),
@Grado AS VARCHAR(30),
@Aula AS VARCHAR(15),
@Fecha_Inscripcion AS DATE
AS
BEGIN
INSERT INTO Estudiantes (Nombre,Grado,Aula,Fecha_Inscripcion)
VALUES (@Nombre,@Grado,@Aula,@Fecha_Inscripcion)
END
GO

CREATE PROCEDURE SpEstudiantesActualizar
@ID_Estudiante AS INT,
@Nombre AS VARCHAR(30),
@Grado AS VARCHAR(30),
@Aula AS VARCHAR(15),
@Fecha_Inscripcion AS DATE
AS
BEGIN
UPDATE Estudiantes
SET Nombre = @Nombre,
Grado = @Grado,
Aula = @Aula,
Fecha_Inscripcion = @Fecha_Inscripcion
WHERE ID_Estudiante = @ID_Estudiante
END
GO

CREATE PROCEDURE SpEstudiantesEliminar
@Nombre AS VARCHAR(40)
AS
BEGIN
DELETE FROM Estudiantes
WHERE Nombre = @Nombre
END
GO

CREATE PROCEDURE SpEstudiantesListar
AS
BEGIN
SELECT ID_Estudiante,
Nombre,
Grado,
Aula,
FORMAT (Fecha_Inscripcion, 'dd/MM/yyyy ') as Fecha_Inscripcion
FROM Estudiantes
END
GO

CREATE PROCEDURE SpEstudiantesObtener
@Nombre AS VARCHAR(40)
AS
BEGIN
SELECT TOP 1 
ID_Estudiante,
Nombre,
Grado,
Aula,
FORMAT (Fecha_Inscripcion, 'dd/MM/yyyy ') as Fecha_Inscripcion
FROM Estudiantes
WHERE Nombre = @Nombre
END
GO

exec SpEstudiantesObtener 'Joel Grullon'

CREATE PROCEDURE SpAulaObtener
@Aula AS VARCHAR(15)
AS
BEGIN
SELECT
ID_Estudiante,
Nombre,
Grado,
Aula,
FORMAT (Fecha_Inscripcion, 'dd/MM/yyyy ') as Fecha_Inscripcion
FROM Estudiantes
WHERE Aula = @Aula
END
GO