USE VeterinariaDB;
GO
/* =====================================================
   TRIGGER 3: EVITAR TURNOS DUPLICADOS PARA UNA MASCOTA

   Este trigger se ejecuta automáticamente después de insertar
   o modificar un turno.

   Su objetivo es evitar que una misma mascota tenga dos turnos
   activos en la misma fecha y hora.

   Se consideran turnos activos aquellos que están en estado
   'Pendiente' o 'Confirmado'.
   ===================================================== */

CREATE TRIGGER TR_EvitarTurnosDuplicadosMascota
ON Turnos
AFTER INSERT, UPDATE
AS
BEGIN
    -- Evita mensajes adicionales de filas afectadas.
    SET NOCOUNT ON;
    /*
       Se verifica si, luego de insertar o modificar un turno,
       existe más de un turno activo para la misma mascota,
       en la misma fecha y en la misma hora.
    */
    IF EXISTS (
        SELECT
            T.IDMascota,
            T.Fecha,
            T.Hora
        FROM Turnos AS T
        /*
           Se compara la tabla Turnos con la tabla temporal inserted,
           que contiene los turnos recién insertados o modificados.
        */
        INNER JOIN inserted AS I
            ON T.IDMascota = I.IDMascota
            AND T.Fecha = I.Fecha
            AND T.Hora = I.Hora
        /*
           Solo se tienen en cuenta los turnos activos.
           Los turnos atendidos o cancelados no bloquean un nuevo turno.
        */
        WHERE T.Estado IN ('Pendiente', 'Confirmado')
        /*
           Se agrupa por mascota, fecha y hora para contar
           cuántos turnos activos existen en ese mismo horario.
        */
        GROUP BY
            T.IDMascota,
            T.Fecha,
            T.Hora
        /*
           Si hay más de un turno activo para la misma mascota
           en la misma fecha y hora, entonces hay duplicación.
        */         
            HAVING COUNT(*) > 1
    )
    BEGIN
       -- Cancela la operación si la mascota ya tiene un turno activo en ese horario.
        THROW 50012,
              'La mascota ya tiene un turno activo en esa fecha y hora.',
              1;
    END;
END;
GO