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

    IF @fecha < CAST(GETDATE() AS DATE)
    -- Se verifica que la fecha no sea anterior a la fecha actual
    BEGIN
        PRINT 'No se pueden registrar turnos en fechas pasadas.';
        RETURN;
    END;

    -- Verifica que la mascota exista
    IF NOT EXISTS
    (
        SELECT 1
        FROM Mascotas
        WHERE IDMascota = @id_mascota
    )
    BEGIN
        PRINT 'La mascota no existe.';
        RETURN;
    END;

    -- Verifica que el veterinario exista
    IF NOT EXISTS
    (
        SELECT 1
        FROM Veterinarios
        WHERE IDVeterinario = @id_veterinario
    )
    BEGIN
        PRINT 'El veterinario no existe.';
        RETURN;
    END;

    -- se verifica si la mascota ya tiene un turno en ese horario
    IF EXISTS
    (
        SELECT 1
        FROM Turnos
        WHERE IDMascota = @id_mascota
          AND Fecha = @fecha
          AND Hora = @hora
          AND Estado IN ('Pendiente', 'Confirmado')
    )
    BEGIN
        PRINT 'La mascota ya tiene un turno en ese horario.';
        RETURN;
    END;

    -- se verifica si el veterinario ya tiene un turno en ese horario
    IF EXISTS
    (
        SELECT 1
        FROM Turnos
        WHERE IDVeterinario = @id_veterinario
          AND Fecha = @fecha
          AND Hora = @hora
          AND Estado IN ('Pendiente', 'Confirmado')
    )
    BEGIN
        PRINT 'El veterinario no tiene disponibilidad en ese horario.';
        RETURN;
    END;

    -- Se inserta el turno
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

END;
GO