using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FacturacionTaller
{
    public class ModuloArchivo
    {
        private const string RUTA_ARCHIVO = "atenciones.txt";

        public static void GuardarAtencion(string linea)
        {
            using (StreamWriter sw = new StreamWriter(RUTA_ARCHIVO, true))
            {
                sw.WriteLine(linea);
            }
        }

        public static void BuscarAtencion(string placaBuscada)
        {
            if (!File.Exists(RUTA_ARCHIVO))
            {
                Console.WriteLine("No existen registros todavía.");
                return;
            }

            string objetivo = placaBuscada.Trim().ToUpper();
            bool encontrado = false;

            using (StreamReader sr = new StreamReader(RUTA_ARCHIVO))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;

                    string[] campos = linea.Split('|');
                    if (campos.Length == 5 && campos[0].Trim().ToUpper() == objetivo)
                    {
                        Console.WriteLine($"Placa: {campos[0]} | Propietario: {campos[1]} | Servicio: {campos[2]} | Total: S/ {campos[3]} | Fecha: {campos[4]}");
                        encontrado = true;
                    }
                }
            }

            if (!encontrado)
            {
                Console.WriteLine($"No se encontraron atenciones para la placa '{objetivo}'.");
            }

            Console.Write("Esto desaparecera en 5 segundos");
            Thread.Sleep(5000);
            Console.Clear();

        }

        public static void ListarTodasLasAtenciones()
        {
            if (!File.Exists(RUTA_ARCHIVO))
            {
                Console.WriteLine("No existen registros para mostrar.");
                return;
            }

            double totalRecaudado = 0.0;
            int contador = 0;

            Console.WriteLine("\n--- REPORTE GENERAL DE ATENCIONES ---");
            using (StreamReader sr = new StreamReader(RUTA_ARCHIVO))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;

                    string[] campos = linea.Split('|');
                    if (campos.Length == 5)
                    {
                        Console.WriteLine($"Placa: {campos[0]} | Propietario: {campos[1]} | Servicio: {campos[2]} | Total: S/ {campos[3]} | Fecha: {campos[4]}");

                        if (double.TryParse(campos[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double monto))
                        {
                            totalRecaudado += monto;
                        }
                        contador++;
                    }
                }
            }

            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"Total de vehículos atendidos: {contador}");
            Console.WriteLine($"Suma total recaudada: S/ {totalRecaudado:F2}");

            Console.Write("Presione para continuar");
            Console.ReadKey(intercept: true);
            Console.Clear();
        }
    }
}
