USE VeterinariaDB;
GO

CREATE TRIGGER TR_EvitarTurnosDuplicadosMascota
ON Turnos
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT
            T.IDMascota,
            T.Fecha,
            T.Hora
        FROM Turnos AS T
        INNER JOIN inserted AS I
            ON T.IDMascota = I.IDMascota
            AND T.Fecha = I.Fecha
            AND T.Hora = I.Hora
        WHERE T.Estado IN ('Pendiente', 'Confirmado')
        GROUP BY
            T.IDMascota,
            T.Fecha,
            T.Hora
        HAVING COUNT(*) > 1
    )
    BEGIN
        THROW 50012,
              'La mascota ya tiene un turno activo en esa fecha y hora.',
              1;
    END;
END;
GO