using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Data;
using Dapper;
using System.Net;

internal class Program
{
    private static void Main(string[] args)
    {
        while(true){ 
            menu();
        }
    }
    private static void menu(){
        Console.WriteLine("Que desea hacer?");
        Console.WriteLine("1. Afiliar");
        Console.WriteLine("2. ver todos los empleados afiliados");
        Console.WriteLine("3. ver un empleado");
        Console.WriteLine("4. Insertar un empleado");
        Console.WriteLine("5. Actualizar un empleado");
        Console.WriteLine("6. Eliminar un empleado");
        Console.WriteLine("7. Salir");
        int opcion = int.Parse(Console.ReadLine());
        opciones(opcion);
    }

    private static async void opciones(int opcion){
        // se crea la conexion
        Conexion conexion = new Conexion();
        switch (opcion)
        {
            case 1:
                Console.WriteLine("Ingrese la identificacion del empleado para afiliar: ");
                var IEnumerableEmpleados= await conexion.GetDataEmpleado(Console.ReadLine());
                List<Empleado> listaEmpleados = IEnumerableEmpleados.ToList<Empleado>();
                Empleado afiliar = new Empleado();
                foreach(var empleado in listaEmpleados){
                    afiliar = empleado;
                }
                
                Console.WriteLine("Ingrese el estado: ");
                var estado = Console.ReadLine();
                Console.WriteLine("Ingrese la fecha de retiro example: (01/11/2024) ");
                var fecharetiro = Console.ReadLine();
                Console.WriteLine("Ingrese la fecha de ingreso example: (01/11/2024) ");
                var fechaingreso = Console.ReadLine();
                var idEmpleado = afiliar.id;
                var afiliciacion = await conexion.PostAfiliacion(afiliar);
                if(afiliciacion){
                    Console.WriteLine("Se ha afiliado correctamente");
                }else{
                     Console.WriteLine("No se ha afiliado correctamente");
                }
                break;
            case 2:
                Console.WriteLine("Viendo todos los empleados: ");
                var IEnumerableEmpleadosAfiliados = await conexion.GetEmpleadosAfiliados();
                List<Empleado> listaEmpleadosAfiliados = IEnumerableEmpleadosAfiliados.ToList<Empleado>();

                foreach(var empleado in listaEmpleadosAfiliados){
                    Console.WriteLine(empleado + "Afiliación:\n" + empleado.Afiliado);
                }
                break;
            case 3:
                Console.WriteLine("Ingrese la identificacion del empleado: ");
                int identificacionBusqueda = int.Parse(Console.ReadLine());
                var IEnumerableEmpleadoAfiliado = await conexion.GetEmpleadoAfiliado(identificacionBusqueda);
                List<Empleado> listaEmpleadoAfiliado = IEnumerableEmpleadoAfiliado.ToList<Empleado>();
                Console.WriteLine($"Viendo al empleado {listaEmpleadoAfiliado[0].nombres} {listaEmpleadoAfiliado[0].apellidos} ");

                foreach(var empleado in listaEmpleadoAfiliado){
                    Console.WriteLine(empleado + "Afiliación:\n" + empleado.Afiliado);
                }
                break;
            case 4:
                
                Empleado empleado1 = pedirDatos();
                
                bool accepted = await conexion.PostEmpleado(empleado1);
                if(accepted){
                    Console.Write("Se ha insertado correctamente");
                }else{
                    Console.Write("No se ha insertado correctamente");
                }
                break;
            case 5:
                Console.WriteLine("Ingrese los datos a modificar: ");
                Empleado modificado = pedirDatos();
                var dato = await conexion.UpdateEmpleadoAfiliado(modificado.id, modificado);
                break;
            case 6:
                Console.WriteLine("Ingrese la identificacion del empleado para eliminar: ");
                var identificacion = Console.ReadLine();
                var eliminacion = await conexion.DeleteEmpleadoAfiliado(identificacion);
                break;
            case 7:
                Environment.Exit(0);
                break;
            default:
                Environment.Exit(0);
                break;
        }
    }

    private static Empleado pedirDatos(){
                Console.Write("Ingrese el/los nombre/s del empleado: ");
                string nombres = Console.ReadLine();

                Console.Write("Ingrese el/los apellido/s del empleado: ");
                string apellidos = Console.ReadLine();

                Console.Write("Ingrese la identificacion del empleado: ");
                string identificacion = Console.ReadLine();

                Console.Write("Ingrese el telefono del empleado: ");
                string telefono = Console.ReadLine();

                Console.Write("Ingrese el correo del empleado: ");
                string correo = Console.ReadLine();

                return new Empleado(nombres, apellidos, identificacion, telefono, correo);
    }
}