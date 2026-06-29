USE VeterinariaDB;
GO

/* =====================================================
   VISTA 1: TURNOS PENDIENTES O CONFIRMADOS
   ===================================================== */

CREATE VIEW VW_TurnosPendientes
AS
SELECT
    T.IDTurno,
    T.Fecha,
    T.Hora,
    T.Estado,
    T.Motivo,
    T.Observaciones,

    M.IDMascota,
    M.Nombre AS Mascota,

    C.IDCliente,
    C.Nombre AS NombreCliente,
    C.Apellido AS ApellidoCliente,
    C.Telefono AS TelefonoCliente,

    V.IDVeterinario,
    V.Nombre AS NombreVeterinario,
    V.Apellido AS ApellidoVeterinario,

    E.Nombre AS Especialidad
FROM Turnos AS T
INNER JOIN Mascotas AS M
    ON T.IDMascota = M.IDMascota
INNER JOIN Clientes AS C
    ON M.IDCliente = C.IDCliente
INNER JOIN Veterinarios AS V
    ON T.IDVeterinario = V.IDVeterinario
INNER JOIN Especialidades AS E
    ON V.IDEspecialidad = E.IDEspecialidad
WHERE T.Estado IN ('Pendiente', 'Confirmado');
GO


/* =====================================================
   VISTA 2: HISTORIAL CLÍNICO DE MASCOTAS
   ===================================================== */

CREATE VIEW VW_HistorialClinicoMascotas
AS
SELECT
    M.IDMascota,
    M.Nombre AS Mascota,

    C.IDCliente,
    C.Nombre AS NombreCliente,
    C.Apellido AS ApellidoCliente,

    CO.IDConsulta,
    CO.FechaConsulta,
    CO.Diagnostico,
    CO.Observaciones,
    CO.Peso,
    CO.Temperatura,

    V.IDVeterinario,
    V.Nombre AS NombreVeterinario,
    V.Apellido AS ApellidoVeterinario,

    E.Nombre AS Especialidad
FROM Consultas AS CO
INNER JOIN Mascotas AS M
    ON CO.IDMascota = M.IDMascota
INNER JOIN Clientes AS C
    ON M.IDCliente = C.IDCliente
INNER JOIN Veterinarios AS V
    ON CO.IDVeterinario = V.IDVeterinario
INNER JOIN Especialidades AS E
    ON V.IDEspecialidad = E.IDEspecialidad;
GO


/* =====================================================
   VISTA 3: TRATAMIENTOS CON MEDICAMENTOS
   ===================================================== */

CREATE VIEW VW_TratamientosMedicamentos
AS
SELECT
    T.IDTratamiento,
    T.Descripcion AS Tratamiento,
    T.FechaInicio,
    T.FechaFin,
    T.Indicaciones,
    T.Estado,

    M.IDMascota,
    M.Nombre AS Mascota,

    MED.IDMedicamento,
    MED.Nombre AS Medicamento,
    MED.Precio,

    TM.Dosis,
    TM.Frecuencia,
    TM.DuracionDias,
    TM.Cantidad
FROM Tratamientos AS T
INNER JOIN Consultas AS C
    ON T.IDConsulta = C.IDConsulta
INNER JOIN Mascotas AS M
    ON C.IDMascota = M.IDMascota
INNER JOIN TratamientoMedicamento AS TM
    ON T.IDTratamiento = TM.IDTratamiento
INNER JOIN Medicamentos AS MED
    ON TM.IDMedicamento = MED.IDMedicamento;
GO