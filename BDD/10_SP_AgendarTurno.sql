CREATE PROCEDURE SP_AgendarTurno
(
    @id_mascota INT,
    @id_veterinario INT,
    @fecha DATE,
    @hora TIME,
    @motivo VARCHAR(255)

)
AS
BEGIN

-- si existe un turno con el mismo veterinario, fecha y hora, y el estado es pendiente o confirmado, no se puede agendar el turno
    IF EXISTS
    
        (SELECT *
        FROM Turnos
        WHERE IDVeterinario = @id_veterinario
          AND Fecha = @fecha
          AND Hora = @hora
          AND Estado IN ('Pendiente', 'Confirmado')
        )
    BEGIN
        PRINT 'Horario no disponible.';
    END
    -- sino insertar el turno
    ELSE
    BEGIN
        INSERT INTO Turnos
        (
            IDMascota,
            IDVeterinario,
            Fecha,
            Hora,
            Motivo
        )
        VALUES
        (
            @id_mascota,
            @id_veterinario,
            @fecha,
            @hora,
            @motivo

        );
PRINT 'Turno registrado correctamente.';
    END

END;
GO