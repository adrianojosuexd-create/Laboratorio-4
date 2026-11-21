using FacturacionTaller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaTaller
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion = 0;

            do
            {
                try
                {
                    Console.WriteLine("      SISTEMA DE FACTURACIÓN - TALLER MECÁNICO      ");
                    Console.WriteLine("1. Registrar atención");
                    Console.WriteLine("2. Buscar atención por Placa");
                    Console.WriteLine("3. Reporte general de atenciones");
                    Console.WriteLine("4. Salir");
                    Console.Write("Seleccione una opción: ");

                    opcion = int.Parse(Console.ReadLine());

                    switch (opcion)
                    {
                        case 1:
                            RegistrarNuevaAtencion();
                            break;
                        case 2:
                            Console.Write("\nIngrese la placa a buscar: ");
                            string placaBuscar = Console.ReadLine();
                            ModuloArchivo.BuscarAtencion(placaBuscar);
                            break;
                        case 3:
                            ModuloArchivo.ListarTodasLasAtenciones();
                            break;
                        case 4:
                            Console.WriteLine("Saliendo del sistema...");
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Ingrese un número del 1 al 3 (4 Es para salir).");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Debe ingresar un valor numérico válido.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
                }

            } while (opcion != 4);
        }

        static void RegistrarNuevaAtencion()
        {
            Console.WriteLine("\n--- REGISTRAR ATENCIÓN ---");
            Console.WriteLine("Placa del vehículO (Ejemplo 000-000): ");
            string placa = Console.ReadLine();

            Console.WriteLine("Nombre del propietario: ");
            string propietario = Console.ReadLine();

            Console.WriteLine("Tipo de servicio (PREVENTIVO / CORRECTIVO / OTRO): ");
            string tipoServicio = Console.ReadLine();

            Console.WriteLine("¿Es cliente frecuente? (SI / NO): ");
            string esFrecuente = Console.ReadLine();
            Console.WriteLine("");

            double tarifaBase = ModuloTaller.ObtenerTarifaBase(tipoServicio);
            double descuento = ModuloTaller.CalcularDescuentoCliente(tarifaBase, esFrecuente);
            double igv = ModuloTaller.CalcularImpuestoIGV(tarifaBase - descuento);
            double totalPagar = ModuloTaller.CalcularTotalPagar(tarifaBase, descuento, igv);

            string fechaHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string registro = ModuloTaller.ConstruirRegistroPipe(placa, propietario, tipoServicio, totalPagar, fechaHora);

            ModuloArchivo.GuardarAtencion(registro);

            Console.WriteLine("\nAtención registrada con éxito:");
            Console.WriteLine($"Tarifa Base: S/ {tarifaBase:F2}");
            Console.WriteLine($"Descuento:   S/ {descuento:F2}");
            Console.WriteLine($"IGV (18%):   S/ {igv:F2}");
            Console.WriteLine($"Total Pagar: S/ {totalPagar:F2}");
            Console.WriteLine($"Fecha/Hora:  {fechaHora}");
            Console.WriteLine("Tiempo para leer 7 segundos");
            Thread.Sleep(7000);
            Console.Clear();
        }
    }
}