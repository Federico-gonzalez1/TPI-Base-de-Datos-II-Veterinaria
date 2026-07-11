USE VeterinariaDB;
GO
/* =====================================================
   TRIGGER 1: DESCONTAR STOCK DE MEDICAMENTO

   Este trigger se ejecuta automáticamente después de insertar
   un registro en la tabla TratamientoMedicamento.

   Su objetivo es controlar que exista stock suficiente del
   medicamento antes de asignarlo a un tratamiento.

   Si no hay stock suficiente, la operación se cancela.
   Si hay stock disponible, se descuenta la cantidad utilizada
   de la tabla Medicamentos.
   ===================================================== */

CREATE TRIGGER TR_DescontarStockMedicamento
ON TratamientoMedicamento
AFTER INSERT
AS
BEGIN

    -- Evita que SQL Server muestre mensajes innecesarios de filas afectadas.
    SET NOCOUNT ON;
     /* 
       Primero se valida si algún medicamento insertado supera
       el stock disponible.

       La tabla inserted es una tabla temporal que SQL Server
       genera automáticamente dentro del trigger. Contiene los
       registros que se acaban de insertar en TratamientoMedicamento.
    */

    IF EXISTS (
        SELECT 1
        FROM Medicamentos AS M
         /* 
           Esta subconsulta agrupa los registros insertados por medicamento.
           Esto es útil porque se podría insertar más de un registro a la vez.
           SUM(Cantidad) calcula la cantidad total solicitada de cada medicamento.
        */
        INNER JOIN (
            SELECT
                IDMedicamento,
                SUM(Cantidad) AS CantidadSolicitada
            FROM inserted
            GROUP BY IDMedicamento
        ) AS I
            ON M.IDMedicamento = I.IDMedicamento

        -- Compara el stock actual con la cantidad solicitada.
        WHERE M.Stock < I.CantidadSolicitada
    )
    BEGIN
        -- Si el stock no alcanza, se lanza un error y se cancela la operación.
        THROW 50010,
              'No hay stock suficiente para asignar el medicamento.',
              1;
    END;
    /*
       Si la validación anterior no encontró problemas,
       se actualiza el stock del medicamento.
    */
    UPDATE M
    SET M.Stock = M.Stock - I.CantidadSolicitada
    FROM Medicamentos AS M
    /*
       Se vuelve a agrupar lo insertado por medicamento para descontar
       la cantidad total utilizada.
    */
    
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