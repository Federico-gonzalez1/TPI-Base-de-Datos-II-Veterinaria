USE VeterinariaDB;
GO

CREATE TABLE Veterinarios (
    IDVeterinario INT IDENTITY(1,1) PRIMARY KEY,
    IDEspecialidad INT NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Matricula VARCHAR(30) NOT NULL UNIQUE,
    Telefono VARCHAR(30),
    Email VARCHAR(100) UNIQUE,
    Activo BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Veterinarios_Especialidades
        FOREIGN KEY (IDEspecialidad)
        REFERENCES Especialidades(IDEspecialidad)
);
GO

CREATE TABLE Mascotas (
    IDMascota INT IDENTITY(1,1) PRIMARY KEY,
    IDCliente INT NOT NULL,
    IDEspecie INT NOT NULL,
    IDRaza INT NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    Sexo CHAR(1) NOT NULL,
    FechaNacimiento DATE,
    Color VARCHAR(50),
    Peso DECIMAL(5,2),
    Observaciones VARCHAR(255),
    Activo BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Mascotas_Clientes
        FOREIGN KEY (IDCliente)
        REFERENCES Clientes(IDCliente),

    CONSTRAINT FK_Mascotas_Especies
        FOREIGN KEY (IDEspecie)
        REFERENCES Especies(IDEspecie),

    CONSTRAINT FK_Mascotas_Razas
        FOREIGN KEY (IDRaza)
        REFERENCES Razas(IDRaza),

    CONSTRAINT CK_Mascotas_Sexo
        CHECK (Sexo IN ('M', 'H')),

    CONSTRAINT CK_Mascotas_Peso
        CHECK (Peso IS NULL OR Peso > 0)
);
GO