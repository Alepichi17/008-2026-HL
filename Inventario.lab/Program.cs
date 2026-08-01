namespace Inventario.lab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Program.cs — Laboratorio 03: validación del producto y menú inicial
            // Las instrucciones se ejecutan de arriba hacia abajo.

            Console.WriteLine("=== Registro de producto ===");

            // Paso 1 — Capturar y validar los datos con TryParse
            Console.Write("Código del producto: ");
            int codigo = int.Parse(Console.ReadLine());

            Console.Write("Nombre del producto: ");
            string nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Error: el nombre del producto no puede estar vacío.");
                return;
            }

            Console.Write("Precio unitario (Q): ");
            bool precioOk = decimal.TryParse(Console.ReadLine(), out decimal precio);
            if (!precioOk || precio <= 0)
            {
                Console.WriteLine("Error: el precio debe ser un número mayor que cero.");
                return;
            }

            Console.Write("Cantidad en existencia: ");
            bool cantOk = int.TryParse(Console.ReadLine(), out int cantidad);
            if (!cantOk || cantidad < 0)
            {
                Console.WriteLine("Error: la cantidad debe ser un entero no negativo.");
                return;
            }

            Console.WriteLine("Producto válido. ¡Datos aceptados!");

            // Paso 2 — Clasificar el stock con if / else if / else
            string estado;
            if (cantidad == 0)
                estado = "AGOTADO";
            else if (cantidad < 10)
                estado = "STOCK BAJO";
            else
                estado = "DISPONIBLE";

            decimal valorTotal = precio * cantidad;
            int faltante = 50 - cantidad;

            Console.WriteLine();
            Console.WriteLine("--- Ficha del producto ---");
            Console.WriteLine($"Código   : {codigo}");
            Console.WriteLine($"Producto : {nombre}");
            Console.WriteLine($"Precio   : Q {precio:N2}");
            Console.WriteLine($"Cantidad : {cantidad}");
            Console.WriteLine($"Estado   : {estado}");
            Console.WriteLine($"Valor en inventario: Q {valorTotal:N2}");
            Console.WriteLine($"Faltante para stock objetivo (50): {faltante}");

            // Paso 3 — Menú inicial con switch
            Console.WriteLine();
            Console.WriteLine("=== MENÚ INVENTARIO ===");
            Console.WriteLine("1. Agregar producto");
            Console.WriteLine("2. Listar productos");
            Console.WriteLine("3. Salir");
            Console.Write("Elige una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("→ (Aquí agregaremos productos)");
                    break;
                case "2":
                    Console.WriteLine("→ (Aquí listaremos productos)");
                    break;
                case "3":
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
            // Paso 4 — Probar errores: ejecuta de nuevo con precio "abc",
            // precio 0 o cantidad negativa y confirma que el programa NO se cae.
        }
    }
}
