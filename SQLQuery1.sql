CREATE TABLE Productos (
Id Int IDENTITY (1,1) PRIMARY KEY,
Nombre NVARCHAR (100) NOT NULL,
Precio DECIMAL (10,2) NOT NULL,
Cantidad INT NOT NULL
);

INSERT INTO Productos (Nombre, Precio, Cantidad)
VALUES ('Cuaderno', 2.50, 12)

INSERT INTO Productos (Nombre, Precio, Cantidad)
VALUES ('Lapicero', 2.25, 40)

Select * from Productos;