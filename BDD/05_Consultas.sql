USE VeterinariaDB;
GO

CREATE TABLE Consultas (
    IDConsulta INT IDENTITY(1,1) PRIMARY KEY,
    IDTurno INT NOT NULL UNIQUE,
    IDMascota INT NOT NULL,
    IDVeterinario INT NOT NULL,
    FechaConsulta DATETIME NOT NULL DEFAULT GETDATE(),
    Diagnostico VARCHAR(500) NOT NULL,
    Observaciones VARCHAR(500),
    Peso DECIMAL(5,2),
    Temperatura DECIMAL(4,1),

    CONSTRAINT FK_Consultas_Turnos
        FOREIGN KEY (IDTurno)
        REFERENCES Turnos(IDTurno),

    CONSTRAINT FK_Consultas_Mascotas
        FOREIGN KEY (IDMascota)
        REFERENCES Mascotas(IDMascota),

    CONSTRAINT FK_Consultas_Veterinarios
        FOREIGN KEY (IDVeterinario)
        REFERENCES Veterinarios(IDVeterinario),

    CONSTRAINT CK_Consultas_Peso
        CHECK (Peso IS NULL OR Peso > 0),

    CONSTRAINT CK_Consultas_Temperatura
        CHECK (
            Temperatura IS NULL
            OR Temperatura BETWEEN 30 AND 45
        )
);
GO