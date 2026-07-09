USE VeterinariaDB;
GO
/* =====================================================
   TRIGGER 2: VALIDAR ESPECIE Y RAZA

   Este trigger se ejecuta automáticamente después de insertar
   o modificar una mascota.

   Su objetivo es controlar que la raza asignada a la mascota
   pertenezca realmente a la especie indicada.

   Por ejemplo, evita cargar una mascota como "Gato" con una
   raza correspondiente a "Perro".
   ===================================================== */

CREATE TRIGGER TR_ValidarEspecieRaza
ON Mascotas
AFTER INSERT, UPDATE
AS
BEGIN
    -- Evita mensajes adicionales de filas afectadas.
    SET NOCOUNT ON;
    /*
       Se verifica si existe alguna mascota insertada o modificada
       cuya especie no coincida con la especie asociada a su raza.
    */
    IF EXISTS (
        SELECT 1
        FROM inserted AS I
        /*
           Se une la tabla temporal inserted con la tabla Razas
           para consultar a qué especie pertenece la raza seleccionada.
        */
        INNER JOIN Razas AS R
            ON I.IDRaza = R.IDRaza
        /*
           Si la especie de la mascota es distinta a la especie
           asociada a la raza, entonces hay una inconsistencia.
        */
        WHERE I.IDEspecie <> R.IDEspecie
    )
    BEGIN
        -- Cancela la operación si la raza no corresponde a la especie.

        THROW 50011,
              'La raza seleccionada no pertenece a la especie indicada.',
              1;
    END;
END;
GO