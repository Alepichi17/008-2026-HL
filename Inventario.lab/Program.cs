using System;
using System.IO;

namespace Inventario.lab
{
    internal class Program
    {   
        static void Main(string[] args)
        {
            string ruta = "inventario.csv";
            string opcion;
            List<Producto> inventario = Cargar(ruta);
            ProductoRepositorio repo = new ProductoRepositorio();
            int SiguienteId = 1;
            foreach (Producto p in inventario)
            {
                if (p.Id >= SiguienteId)
                {
                    SiguienteId = p.Id + 1;
                }
            }
           
            do
            {
                
                opcion = LeerOpcionMenu();

                switch (opcion)
                {
                    case "1":
                        Agregar(inventario, ref SiguienteId);
                        break;
                    case "2":
                        Listar(inventario);
                        break;
                    case "3":
                        BuscarProducto(inventario);
                        break;
                    case "4":
                        ListarEnSqlServer(repo);
                        break;
                    case "5":
                        AgregarEnSqlServer(repo);
                        break;
                    case "6":
                        Guardar(inventario, ruta);
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida, intenta de nuevo.");
                        break;
                }
            }
            while (opcion != "6");
        }
        public static string LeerOpcionMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== MENÚ INVENTARIO ===");
            Console.WriteLine("1. Agregar producto");
            Console.WriteLine("2. Listar productos");
            Console.WriteLine("3. Buscar producto");
            Console.WriteLine("4. Listar Sql");
            Console.WriteLine("5. Agregar Producto en SQL SERVER");
            Console.WriteLine("6. Salir");
            Console.Write("Elige una opción: ");
            return Console.ReadLine();
            

        }
        private static void Agregar(List<Producto> inv, ref int id)
        {
            Console.WriteLine();
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine().Trim(); 
            Console.Write("Precio: ");
            bool precioOk = decimal.TryParse(Console.ReadLine(), out decimal precio);
            Console.Write("Cantidad: ");
            bool CantidadOk = int.TryParse(Console.ReadLine(), out int cantidad);

            if (precioOk || precio <= 0 || CantidadOk || cantidad < 0 || string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Datos Invalidos");
                return;
               
            }
            else
            {
                inv.Add(new Producto
                {
                    Id = id,
                    Nombre = nombre,
                    Precio = precio,
                    Cantidad = cantidad,
                }

                    );
                id++;
                Console.WriteLine("Producto Aregado");
            }

        }
        private static void Listar(List<Producto> inv )
        {
            if (inv.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Inventario Vacio");
                return;
            }
            decimal totalGeneral = 0;
            Console.WriteLine($"{"Id",-3} {"Nombre",-20} {"Precio",10} {"Cantidad",-4} = Q {"ValorTotal",10}  ");
            foreach (Producto p in inv)
            {
                Console.WriteLine(p);
                    totalGeneral += p.ValorTotal();
            }

            Console.WriteLine($"Valor Total: {totalGeneral}");
        }
        private static void BuscarProducto(List<Producto> inv)
        {
            Console.Write("Texto a buscar: ");
            string t = Console.ReadLine().Trim().ToUpper();
            bool encontrado = false;
            Console.WriteLine($"{"Id",-3} {"Nombre",-20} {"Precio",10} {"Cantidad",-4} = Q {"ValorTotal",10}  ");
           foreach (Producto p in inv)
            {
                if(p.Nombre.ToUpper().Contains(t))
                {
                Console.WriteLine($"- {p}");
                encontrado = true;
                }
            }

           if (!encontrado)
            {
                Console.WriteLine("Sin coincidencias");
            }
        }
        private static void Guardar(List<Producto> inv, string ruta)
        {
            var lineas = new List<string>();

            foreach(Producto p in inv)
            {
                lineas.Add($"{p.Id}; {p.Nombre}; {p.Precio}; {p.Cantidad}");
            }

            File.WriteAllLines(ruta, lineas);
            Console.WriteLine($"Inventario Guardado en {ruta} {(inv.Count)} Productos");
        }
        private static List<Producto> Cargar(string ruta)
        {
            var inv = new List<Producto>();
            if(!File.Exists(ruta))return inv;

            foreach(string linea in File.ReadLines(ruta))
            {
                string[] campos = linea.Split(';');

                if(campos.Length !=4 ) continue;

                inv.Add(new Producto
                {
                    Id = int.Parse(campos[0]),
                    Nombre = campos[1],
                    Precio = decimal.Parse(campos[2]),
                    Cantidad = int.Parse(campos[3]),
                });
            }

            Console.WriteLine($"Cargados {inv.Count} Productos desde {ruta}.");

            return inv;
        }
        private static void AgregarEnSqlServer(ProductoRepositorio repo)
        {
            Console.WriteLine();
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine().Trim();
            Console.Write("Precio: ");
            bool precioOk = decimal.TryParse(Console.ReadLine(), out decimal precio);
            Console.Write("Cantidad: ");
            bool cantidadOk = int.TryParse(Console.ReadLine(), out int cantidad);

            if (!precioOk || precio <= 0 || !cantidadOk || cantidad < 0 || string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Datos Invalidos");
                return;
            }

            repo.Insertar(new Producto { Nombre = nombre, Precio = precio, Cantidad = cantidad });
        }
        private static void ListarEnSqlServer(ProductoRepositorio repo)
        {
            List<Producto> lista = repo.ListarTodos();
            if(lista.Count == 0)
            {
                Console.WriteLine("No Hay Productos en SQL Server");
                return;
            }

            decimal TotalGeneral = 0;
            foreach (Producto p in lista)
            {
                Console.WriteLine(p);
                TotalGeneral += p.ValorTotal();
            }
            Console.WriteLine($"Valor Total Del Inventario: Q{TotalGeneral:N2}");
        }
    }
}