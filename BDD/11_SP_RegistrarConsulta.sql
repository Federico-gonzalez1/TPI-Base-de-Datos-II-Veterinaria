CREATE PROCEDURE SP_RegistrarConsulta
(
    @id_turno INT,
    @diagnostico VARCHAR(500),
    @observaciones VARCHAR(500),
    @peso DECIMAL(5,2),
    @temperatura DECIMAL(4,1)
)
AS
BEGIN

    -- Se verifica que el turno exista
    IF NOT EXISTS
    (
        SELECT *
        FROM Turnos
        WHERE IDTurno = @id_turno AND Estado IN ('Confirmado')
    )
    BEGIN
        PRINT 'El turno no existe o no está confirmado.';
    END
    ELSE
    BEGIN

        INSERT INTO Consultas
        (
            IDTurno,
            IDMascota,
            IDVeterinario,
            Diagnostico,
            Observaciones,
            Peso,
            Temperatura
        )
        SELECT
            IDTurno,
            IDMascota,
            IDVeterinario,
            @diagnostico,
            @observaciones,
            @peso,
            @temperatura
        FROM Turnos
        WHERE IDTurno = @id_turno;

        UPDATE Turnos
        SET Estado = 'Atendido'
        WHERE IDTurno = @id_turno;

        PRINT 'Consulta registrada correctamente.';

    END

END;
GO