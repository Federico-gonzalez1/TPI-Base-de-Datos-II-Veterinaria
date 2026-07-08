-- Indica que vamos a trabajar sobre la base de datos VeterinariaDB.
USE VeterinariaDB;
GO

/* =====================================================
   VISTA 1: TURNOS PENDIENTES O CONFIRMADOS

   Esta vista muestra los turnos que todavía están activos,
   es decir, los que se encuentran en estado 'Pendiente' o 'Confirmado'.
   Une información de turnos, mascotas, clientes, veterinarios
   y especialidades para mostrar los datos completos del turno.
   ===================================================== */

CREATE VIEW VW_TurnosPendientes
AS
SELECT
    -- Datos principales del turno.
    T.IDTurno,
    T.Fecha,
    T.Hora,
    T.Estado,
    T.Motivo,
    T.Observaciones,

    -- Datos de la mascota asociada al turno.
    M.IDMascota,
    M.Nombre AS Mascota,

    -- Datos del cliente dueño de la mascota.
    C.IDCliente,
    C.Nombre AS NombreCliente,
    C.Apellido AS ApellidoCliente,
    C.Telefono AS TelefonoCliente,

    -- Datos del veterinario asignado al turno.
    V.IDVeterinario,
    V.Nombre AS NombreVeterinario,
    V.Apellido AS ApellidoVeterinario,

    -- Especialidad del veterinario.
    E.Nombre AS Especialidad

FROM Turnos AS T

-- Une cada turno con la mascota correspondiente.
INNER JOIN Mascotas AS M
    ON T.IDMascota = M.IDMascota

-- Une cada mascota con su cliente/dueño.
INNER JOIN Clientes AS C
    ON M.IDCliente = C.IDCliente

-- Une cada turno con el veterinario asignado.
INNER JOIN Veterinarios AS V
    ON T.IDVeterinario = V.IDVeterinario

-- Une cada veterinario con su especialidad.
INNER JOIN Especialidades AS E
    ON V.IDEspecialidad = E.IDEspecialidad

-- Filtra únicamente los turnos pendientes o confirmados.
WHERE T.Estado IN ('Pendiente', 'Confirmado');
GO


/* =====================================================
   VISTA 2: HISTORIAL CLÍNICO DE MASCOTAS

   Esta vista muestra las consultas realizadas a las mascotas.
   Permite ver el historial clínico, incluyendo diagnóstico,
   observaciones, peso, temperatura, veterinario y especialidad.
   ===================================================== */

CREATE VIEW VW_HistorialClinicoMascotas
AS
SELECT
    -- Datos de la mascota atendida.
    M.IDMascota,
    M.Nombre AS Mascota,

    -- Datos del cliente dueño de la mascota.
    C.IDCliente,
    C.Nombre AS NombreCliente,
    C.Apellido AS ApellidoCliente,

    -- Datos clínicos de la consulta.
    CO.IDConsulta,
    CO.FechaConsulta,
    CO.Diagnostico,
    CO.Observaciones,
    CO.Peso,
    CO.Temperatura,

    -- Datos del veterinario que realizó la consulta.
    V.IDVeterinario,
    V.Nombre AS NombreVeterinario,
    V.Apellido AS ApellidoVeterinario,

    -- Especialidad del veterinario.
    E.Nombre AS Especialidad

FROM Consultas AS CO

-- Une cada consulta con la mascota atendida.
INNER JOIN Mascotas AS M
    ON CO.IDMascota = M.IDMascota

-- Une la mascota con su cliente/dueño.
INNER JOIN Clientes AS C
    ON M.IDCliente = C.IDCliente

-- Une la consulta con el veterinario que la realizó.
INNER JOIN Veterinarios AS V
    ON CO.IDVeterinario = V.IDVeterinario

-- Une el veterinario con su especialidad.
INNER JOIN Especialidades AS E
    ON V.IDEspecialidad = E.IDEspecialidad;
GO


/* =====================================================
   VISTA 3: TRATAMIENTOS CON MEDICAMENTOS

   Esta vista muestra los tratamientos indicados y los medicamentos
   asociados a cada tratamiento.
   Permite consultar qué medicamento se indicó, en qué dosis,
   con qué frecuencia, durante cuántos días y qué cantidad se usó.
   ===================================================== */

CREATE VIEW VW_TratamientosMedicamentos
AS
SELECT

    -- Datos principales del tratamiento.
    T.IDTratamiento,
    T.Descripcion AS Tratamiento,
    T.FechaInicio,
    T.FechaFin,
    T.Indicaciones,
    T.Estado,

    -- Datos de la mascota asociada al tratamiento.
    M.IDMascota,
    M.Nombre AS Mascota,

    -- Datos del medicamento indicado.
    MED.IDMedicamento,
    MED.Nombre AS Medicamento,
    MED.Precio,

    -- Datos específicos de la relación entre tratamiento y medicamento.
    TM.Dosis,
    TM.Frecuencia,
    TM.DuracionDias,
    TM.Cantidad

FROM Tratamientos AS T

-- Une el tratamiento con la consulta en la que fue indicado.
INNER JOIN Consultas AS C
    ON T.IDConsulta = C.IDConsulta

-- Une la consulta con la mascota atendida.
INNER JOIN Mascotas AS M
    ON C.IDMascota = M.IDMascota

-- Une el tratamiento con la tabla intermedia que guarda los medicamentos indicados.
INNER JOIN TratamientoMedicamento AS TM
    ON T.IDTratamiento = TM.IDTratamiento

-- Une la tabla intermedia con los datos del medicamento.
INNER JOIN Medicamentos AS MED
    ON TM.IDMedicamento = MED.IDMedicamento;
GO