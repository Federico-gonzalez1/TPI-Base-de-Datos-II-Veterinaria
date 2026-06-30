USE VeterinariaDB;
GO

CREATE TRIGGER TR_DescontarStockMedicamento
ON TratamientoMedicamento
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM Medicamentos AS M
        INNER JOIN (
            SELECT
                IDMedicamento,
                SUM(Cantidad) AS CantidadSolicitada
            FROM inserted
            GROUP BY IDMedicamento
        ) AS I
            ON M.IDMedicamento = I.IDMedicamento
        WHERE M.Stock < I.CantidadSolicitada
    )
    BEGIN
        THROW 50010,
              'No hay stock suficiente para asignar el medicamento.',
              1;
    END;

    UPDATE M
    SET M.Stock = M.Stock - I.CantidadSolicitada
    FROM Medicamentos AS M
    INNER JOIN (
        SELECT
            IDMedicamento,
            SUM(Cantidad) AS CantidadSolicitada
        FROM inserted
        GROUP BY IDMedicamento
    ) AS I
        ON M.IDMedicamento = I.IDMedicamento;
END;
GO