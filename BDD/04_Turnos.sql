USE VeterinariaDB;
GO

CREATE TABLE Turnos (
    IDTurno INT IDENTITY(1,1) PRIMARY KEY,
    IDMascota INT NOT NULL,
    IDVeterinario INT NOT NULL,
    Fecha DATE NOT NULL,
    Hora TIME NOT NULL,
    Estado VARCHAR(20) NOT NULL DEFAULT 'Pendiente',
    Motivo VARCHAR(255) NOT NULL,
    Observaciones VARCHAR(255),

    CONSTRAINT FK_Turnos_Mascotas
        FOREIGN KEY (IDMascota)
        REFERENCES Mascotas(IDMascota),

    CONSTRAINT FK_Turnos_Veterinarios
        FOREIGN KEY (IDVeterinario)
        REFERENCES Veterinarios(IDVeterinario),

    CONSTRAINT CK_Turnos_Estado
        CHECK (Estado IN (
            'Pendiente',
            'Confirmado',
            'Atendido',
            'Cancelado'
        ))
);
GO