USE VeterinariaDB;
GO

CREATE TABLE Clientes (
    IDCliente INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    DNI VARCHAR(20) NOT NULL UNIQUE,
    Telefono VARCHAR(30),
    Email VARCHAR(100) UNIQUE,
    Direccion VARCHAR(150),
    FechaAlta DATE NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Especies (
    IDEspecie INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL UNIQUE
);
GO

CREATE TABLE Razas (
    IDRaza INT IDENTITY(1,1) PRIMARY KEY,
    IDEspecie INT NOT NULL,
    Nombre VARCHAR(50) NOT NULL,

    CONSTRAINT FK_Razas_Especies
        FOREIGN KEY (IDEspecie)
        REFERENCES Especies(IDEspecie),

    CONSTRAINT UQ_Raza_Especie
        UNIQUE (IDEspecie, Nombre)
);
GO

CREATE TABLE Especialidades (
    IDEspecialidad INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(80) NOT NULL UNIQUE,
    Descripcion VARCHAR(255)
);
GO