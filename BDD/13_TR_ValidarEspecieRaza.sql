USE VeterinariaDB;
GO

CREATE TRIGGER TR_ValidarEspecieRaza
ON Mascotas
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM inserted AS I
        INNER JOIN Razas AS R
            ON I.IDRaza = R.IDRaza
        WHERE I.IDEspecie <> R.IDEspecie
    )
    BEGIN
        THROW 50011,
              'La raza seleccionada no pertenece a la especie indicada.',
              1;
    END;
END;
GO