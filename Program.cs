using System;
using System.Collections.Generic;
using System.Linq;

// ==========================
// Clase Producto
// ==========================
public class Producto
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public double Precio { get; set; }
    public int Cantidad { get; set; }

    public Producto(string codigo, string nombre, double precio, int cantidad)
    {
        Codigo = codigo;
        Nombre = nombre;
        Precio = precio;
        Cantidad = cantidad;
    }

    public override string ToString()
    {
        return $"Código: {Codigo} | Nombre: {Nombre} | Precio: {Precio:C} | Cantidad: {Cantidad}";
    }
}

// ==========================
// Clase Almacen
// ==========================
public class Almacen
{
    private List<Producto> productos = new List<Producto>();

    public bool AgregarProducto(Producto nuevo)
    {
        if (productos.Any(p => p.Codigo == nuevo.Codigo))
            return false;

        productos.Add(nuevo);
        return true;
    }

    // 🔥 Puede devolver null → lo indicamos con ?
    public Producto? BuscarProducto(string codigo)
    {
        return productos.FirstOrDefault(p => p.Codigo == codigo);
    }

    public bool EliminarProducto(string codigo)
    {
        Producto? producto = BuscarProducto(codigo);

        if (producto != null)
        {
            productos.Remove(producto);
            return true;
        }

        return false;
    }

    public List<Producto> ObtenerProductos()
    {
        return productos;
    }

    public int ObtenerCantidadTotal()
    {
        return productos.Count;
    }
}

// ==========================
// Clase Program
// ==========================
class Program
{
    static void Main(string[] args)
    {
        Almacen almacen = new Almacen();
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("\n===== SISTEMA DE GESTIÓN DE PRODUCTOS =====");
            Console.WriteLine("1. Registrar producto");
            Console.WriteLine("2. Buscar producto");
            Console.WriteLine("3. Eliminar producto");
            Console.WriteLine("4. Listar productos");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    RegistrarProducto(almacen);
                    break;

                case "2":
                    BuscarProducto(almacen);
                    break;

                case "3":
                    EliminarProducto(almacen);
                    break;

                case "4":
                    ListarProductos(almacen);
                    break;

                case "5":
                    salir = true;
                    Console.WriteLine("Saliendo del sistema...");
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }

    static void RegistrarProducto(Almacen almacen)
    {
        Console.Write("Código: ");
        string? codigo = Console.ReadLine();

        Console.Write("Nombre: ");
        string? nombre = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("Código y nombre no pueden estar vacíos.");
            return;
        }

        Console.Write("Precio: ");
        if (!double.TryParse(Console.ReadLine(), out double precio))
        {
            Console.WriteLine("Precio inválido.");
            return;
        }

        Console.Write("Cantidad: ");
        if (!int.TryParse(Console.ReadLine(), out int cantidad))
        {
            Console.WriteLine("Cantidad inválida.");
            return;
        }

        Producto nuevo = new Producto(codigo, nombre, precio, cantidad);

        if (almacen.AgregarProducto(nuevo))
            Console.WriteLine("Producto registrado correctamente.");
        else
            Console.WriteLine("Ya existe un producto con ese código.");
    }

    static void BuscarProducto(Almacen almacen)
    {
        Console.Write("Ingrese código del producto: ");
        string? codigo = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(codigo))
        {
            Console.WriteLine("Código inválido.");
            return;
        }

        Producto? producto = almacen.BuscarProducto(codigo);

        if (producto != null)
            Console.WriteLine(producto);
        else
            Console.WriteLine("Producto no encontrado.");
    }

    static void EliminarProducto(Almacen almacen)
    {
        Console.Write("Ingrese código del producto a eliminar: ");
        string? codigo = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(codigo))
        {
            Console.WriteLine("Código inválido.");
            return;
        }

        if (almacen.EliminarProducto(codigo))
            Console.WriteLine("Producto eliminado correctamente.");
        else
            Console.WriteLine("Producto no encontrado.");
    }

    static void ListarProductos(Almacen almacen)
    {
        var lista = almacen.ObtenerProductos();

        if (lista.Count == 0)
        {
            Console.WriteLine("No hay productos registrados.");
            return;
        }

        Console.WriteLine("\n===== LISTADO DE PRODUCTOS =====");
        foreach (var producto in lista)
        {
            Console.WriteLine(producto);
        }

        Console.WriteLine($"Total de productos: {almacen.ObtenerCantidadTotal()}");
    }
}