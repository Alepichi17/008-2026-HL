namespace Inventario.lab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            string opcion;
            List<string> nombres = new List<string>();
            List<decimal> precios = new List<decimal>();
            List<int> cantidades = new List<int>();

            do
            {
                
                opcion = LeerOpcionMenu();

                switch (opcion)
                {
                    case "1":
                        AgregarProducto(nombres, precios, cantidades);
                        break;
                    case "2":
                        ListarProductos(nombres, precios, cantidades);
                        break;
                    case "3":
                        BuscarProducto(nombres, precios, cantidades);
                        break;
                    case "4":
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida, intenta de nuevo.");
                        break;
                }
            }
            while (opcion != "4");
        }

           

        public static string LeerOpcionMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== MENÚ INVENTARIO ===");
            Console.WriteLine("1. Agregar producto");
            Console.WriteLine("2. Listar productos");
            Console.WriteLine("3. Buscar producto");
            Console.WriteLine("4. Salir");
            Console.Write("Elige una opción: ");
            return Console.ReadLine();
            

        }

        private static void AgregarProducto(List<string> nombres, List<decimal> precios, List<int> cantidades)
        {
            Console.WriteLine();
            Console.Write("Nombre: ");
            string nombreProducto = Console.ReadLine().Trim(); 
            Console.Write("Precio: ");
            bool precioOk = decimal.TryParse(Console.ReadLine(), out decimal PrecioProducto);
            Console.Write("Cantidad: ");
            bool CantidadOk = int.TryParse(Console.ReadLine(), out int CantidadProducto);

            if (precioOk && PrecioProducto > 0 && CantidadOk && CantidadProducto > 0 && !string.IsNullOrWhiteSpace(nombreProducto))
            {
                nombres.Add(nombreProducto);
                precios.Add(PrecioProducto);
                cantidades.Add(CantidadProducto);
                Console.WriteLine("Producto Agregado");
            }
            else
            {
                Console.WriteLine("Datos Invalidos");
            }

        }

        private static void ListarProductos(List<string> nombres, List<decimal> precios, List<int> cantidades)
        {
            if (nombres.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("No hay productos.");
                return;
            }
            Console.WriteLine($"{"N.",2} {"Nombre",-20} {"Precio",10} {"Cant.",5} {"Total",13}");
            for (int i = 0; i < nombres.Count; i++)
            {
                decimal valorLinea = CalcularValorTotal(precios[i], cantidades[i]);
                Console.WriteLine($"{i + 1, -2} {nombres[i], -20} {"Q",1} {"Q"+precios[i],10:N2} {cantidades[i], 5} Q{valorLinea, 13:N2}");
            }
        }

        private static void BuscarProducto(List<string> nombres, List<decimal> precios, List<int> cantidades)
        {
            Console.Write("Texto a buscar: ");
            string t = Console.ReadLine().Trim().ToUpper();
            bool encontrado = false;
            for (int i = 0; i < nombres.Count; i++)
            {
                if (nombres[i].ToUpper().Contains(t))
                {
                    Console.WriteLine();
                    Console.WriteLine($"- {nombres[i]} (Q {precios[i]:N2})");
                    encontrado = true;
                }
            }
            if (!encontrado)
            {
                Console.WriteLine();
                Console.WriteLine("Sin coincidencias");
            }
        }
        private static decimal  CalcularValorTotal(decimal precio, int cantidad)
        {
            return precio * cantidad;
        }

       

    }
}
