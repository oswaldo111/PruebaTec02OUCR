create database Editorial

use Editorial

CREATE TABLE Autores (
    IdAutor INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);
GO

-- Crear la tabla Productos
CREATE TABLE Libros (
    LibrosId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Imagen IMAGE,
    IdAutor INT FOREIGN KEY REFERENCES Autores(IdAutor) NOT NULL
);