using System.Globalization;

namespace FacturacionTaller;

public static class ModuloTaller
{
    public static double ObtenerTarifaBase(string tipoServicio)
    {
        switch (tipoServicio.Trim().ToUpper())
        {
            case "PREVENTIVO": return 180.00;
            case "CORRECTIVO": return 320.00;
            default: return 120.00; // "OTRO" o cualquier otro texto
        }
    }

    public static double CalcularDescuentoCliente(double tarifaBase, string esFrecuente)
    {
        return esFrecuente.Trim().ToUpper() == "SI" ? tarifaBase * 0.15 : 0;
    }

    public static double CalcularImpuestoIGV(double tarifaBaseConDescuento)
    {
        return tarifaBaseConDescuento * 0.18;
    }

    public static double CalcularTotalPagar(double tarifaBase, double descuento, double igv)
    {
        return tarifaBase - descuento + igv;
    }

    public static string ConstruirRegistroPipe(string placa, string propietario,
        string tipoServicio, double totalPagar, DateTime fecha)
    {
        string total = totalPagar.ToString("F2", CultureInfo.InvariantCulture);
        string fechaTxt = fecha.ToString("yyyy-MM-dd HH:mm:ss");
        return $"{placa}|{propietario}|{tipoServicio}|{total}|{fechaTxt}";
    }
}
