using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;

namespace FacturacionTaller
{
    public class ModuloTaller
    {
        public static double ObtenerTarifaBase(string tipoServicio)
        {
            string tipo = tipoServicio.Trim().ToUpper();

            if (tipo == "PREVENTIVO")
                return 180.00;
            else if (tipo == "CORRECTIVO")
                return 320.00;
            else
                return 120.00;
        }

        public static double CalcularDescuentoCliente(double tarifaBase, string esFrecuente)
        {
            Console.WriteLine("Calculando el descuento para el cliente");
            if (esFrecuente.Trim().ToUpper() == "SI")
                return tarifaBase * 0.15;
            else
                return 0.0;
        }

        public static double CalcularImpuestoIGV(double tarifaBaseConDescuento)
        {
            return tarifaBaseConDescuento * 0.18;
        }

        public static double CalcularTotalPagar(double tarifaBase, double descuento, double igv)
        {
            return (tarifaBase - descuento + igv);
        }

        public static string ConstruirRegistroPipe(string placa, string propietario, string tipoServicio, double totalPagar, string fecha)
        {
            string totalFormateado = totalPagar.ToString("F2", CultureInfo.InvariantCulture);
            return $"{placa.Trim().ToUpper()}|{propietario.Trim()}|{tipoServicio.Trim().ToUpper()}|{totalFormateado}|{fecha}";
        }
    }
}