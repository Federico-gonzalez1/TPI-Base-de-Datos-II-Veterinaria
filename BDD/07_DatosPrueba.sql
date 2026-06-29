USE VeterinariaDB;
GO

/* =========================
   ESPECIES
   ========================= */
INSERT INTO Especies (Nombre)
VALUES
('Perro'),
('Gato');
GO

/* =========================
   RAZAS
   ========================= */
INSERT INTO Razas (IDEspecie, Nombre)
VALUES
(1, 'Labrador'),
(1, 'Caniche'),
(2, 'Siamés'),
(2, 'Mestizo');
GO

/* =========================
   ESPECIALIDADES
   ========================= */
INSERT INTO Especialidades (Nombre, Descripcion)
VALUES
('Clínica general', 'Atención veterinaria general'),
('Cirugía', 'Intervenciones quirúrgicas'),
('Dermatología', 'Diagnóstico y tratamiento de enfermedades de la piel');
GO

/* =========================
   CLIENTES
   ========================= */
INSERT INTO Clientes (
    Nombre,
    Apellido,
    DNI,
    Telefono,
    Email,
    Direccion
)
VALUES
('Juan', 'Pérez', '30111222', '1123456789', 'juan.perez@email.com', 'San Martín 120'),
('Lucía', 'Gómez', '32444555', '1134567890', 'lucia.gomez@email.com', 'Belgrano 450'),
('Carlos', 'Rodríguez', '28777888', '1145678901', 'carlos.rodriguez@email.com', 'Rivadavia 870');
GO

/* =========================
   VETERINARIOS
   ========================= */
INSERT INTO Veterinarios (
    IDEspecialidad,
    Nombre,
    Apellido,
    Matricula,
    Telefono,
    Email
)
VALUES
(1, 'Mariana', 'López', 'MAT-1001', '1151111111', 'mariana.lopez@veterinaria.com'),
(2, 'Diego', 'Fernández', 'MAT-1002', '1152222222', 'diego.fernandez@veterinaria.com'),
(3, 'Paula', 'Martínez', 'MAT-1003', '1153333333', 'paula.martinez@veterinaria.com');
GO

/* =========================
   MASCOTAS
   ========================= */
INSERT INTO Mascotas (
    IDCliente,
    IDEspecie,
    IDRaza,
    Nombre,
    Sexo,
    FechaNacimiento,
    Color,
    Peso,
    Observaciones
)
VALUES
(1, 1, 1, 'Toby', 'M', '2020-05-12', 'Dorado', 28.50, 'Sin observaciones'),
(2, 2, 3, 'Luna', 'H', '2021-08-20', 'Blanco y gris', 4.20, 'Presenta alergias alimentarias'),
(3, 1, 2, 'Milo', 'M', '2019-11-03', 'Blanco', 7.80, 'Controlar peso');
GO

/* =========================
   TURNOS
   ========================= */
INSERT INTO Turnos (
    IDMascota,
    IDVeterinario,
    Fecha,
    Hora,
    Estado,
    Motivo,
    Observaciones
)
VALUES
(1, 1, '2026-06-30', '10:00', 'Atendido', 'Control anual', NULL),
(2, 3, '2026-07-01', '11:30', 'Confirmado', 'Problema en la piel', 'Presenta picazón'),
(3, 1, '2026-07-02', '09:00', 'Pendiente', 'Control de peso', NULL);
GO

/* =========================
   CONSULTA REALIZADA
   Corresponde al turno 1
   ========================= */
INSERT INTO Consultas (
    IDTurno,
    IDMascota,
    IDVeterinario,
    FechaConsulta,
    Diagnostico,
    Observaciones,
    Peso,
    Temperatura
)
VALUES
(
    1,
    1,
    1,
    '2026-06-30 10:15:00',
    'Estado general saludable',
    'Se recomienda control anual y desparasitación',
    28.50,
    38.5
);
GO

/* =========================
   TRATAMIENTO
   ========================= */
INSERT INTO Tratamientos (
    IDConsulta,
    Descripcion,
    FechaInicio,
    FechaFin,
    Indicaciones,
    Estado
)
VALUES
(
    1,
    'Tratamiento antiparasitario preventivo',
    '2026-06-30',
    '2026-07-02',
    'Administrar una dosis diaria durante tres días',
    'Finalizado'
);
GO

/* =========================
   MEDICAMENTOS
   ========================= */
INSERT INTO Medicamentos (
    Nombre,
    Descripcion,
    Stock,
    Precio
)
VALUES
('Antiparasitario oral', 'Tratamiento antiparasitario para perros y gatos', 30, 4800.00),
('Amoxicilina 500 mg', 'Antibiótico de amplio espectro', 40, 8500.00),
('Prednisona 20 mg', 'Medicamento antiinflamatorio', 25, 6200.00);
GO

/* =========================
   MEDICAMENTO ASOCIADO
   AL TRATAMIENTO
   ========================= */
INSERT INTO TratamientoMedicamento (
    IDTratamiento,
    IDMedicamento,
    Dosis,
    Frecuencia,
    DuracionDias,
    Cantidad
)
VALUES
(
    1,
    1,
    '1 comprimido',
    'Cada 24 horas',
    3,
    3
);
GO