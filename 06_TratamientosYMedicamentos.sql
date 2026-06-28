USE VeterinariaDB;
GO

CREATE TABLE Tratamientos (
    IDTratamiento INT IDENTITY(1,1) PRIMARY KEY,
    IDConsulta INT NOT NULL,
    Descripcion VARCHAR(500) NOT NULL,
    FechaInicio DATE NOT NULL DEFAULT GETDATE(),
    FechaFin DATE,
    Indicaciones VARCHAR(500),
    Estado VARCHAR(20) NOT NULL DEFAULT 'Activo',

    CONSTRAINT FK_Tratamientos_Consultas
        FOREIGN KEY (IDConsulta)
        REFERENCES Consultas(IDConsulta),

    CONSTRAINT CK_Tratamientos_Fechas
        CHECK (
            FechaFin IS NULL
            OR FechaFin >= FechaInicio
        ),

    CONSTRAINT CK_Tratamientos_Estado
        CHECK (
            Estado IN (
                'Activo',
                'Finalizado',
                'Suspendido'
            )
        )
);
GO

CREATE TABLE Medicamentos (
    IDMedicamento INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL UNIQUE,
    Descripcion VARCHAR(255),
    Stock INT NOT NULL DEFAULT 0,
    Precio DECIMAL(10,2) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,

    CONSTRAINT CK_Medicamentos_Stock
        CHECK (Stock >= 0),

    CONSTRAINT CK_Medicamentos_Precio
        CHECK (Precio > 0)
);
GO

CREATE TABLE TratamientoMedicamento (
    IDTratamientoMedicamento INT IDENTITY(1,1) PRIMARY KEY,
    IDTratamiento INT NOT NULL,
    IDMedicamento INT NOT NULL,
    Dosis VARCHAR(100) NOT NULL,
    Frecuencia VARCHAR(100) NOT NULL,
    DuracionDias INT NOT NULL,
    Cantidad INT NOT NULL,

    CONSTRAINT FK_TratamientoMedicamento_Tratamientos
        FOREIGN KEY (IDTratamiento)
        REFERENCES Tratamientos(IDTratamiento),

    CONSTRAINT FK_TratamientoMedicamento_Medicamentos
        FOREIGN KEY (IDMedicamento)
        REFERENCES Medicamentos(IDMedicamento),

    CONSTRAINT CK_TratamientoMedicamento_Duracion
        CHECK (DuracionDias > 0),

    CONSTRAINT CK_TratamientoMedicamento_Cantidad
        CHECK (Cantidad > 0),

    CONSTRAINT UQ_TratamientoMedicamento
        UNIQUE (IDTratamiento, IDMedicamento)
);
GO