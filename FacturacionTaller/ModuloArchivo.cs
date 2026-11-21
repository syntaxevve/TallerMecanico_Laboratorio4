using System.Globalization;

namespace FacturacionTaller;

public static class ModuloArchivo
{
    private const string Ruta = "atenciones.txt";

    public static void GuardarAtencion(string linea)
    {
        File.AppendAllText(Ruta, linea + Environment.NewLine);
    }

    public static void BuscarAtencion(string placaBuscada)
    {
        if (!File.Exists(Ruta))
        {
            Console.WriteLine("Aún no hay atenciones registradas.");
            return;
        }

        bool encontrado = false;
        foreach (string linea in File.ReadAllLines(Ruta))
        {
            string[] campos = linea.Split('|');
            if (campos.Length == 5 &&
                campos[0].Equals(placaBuscada.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Placa: {campos[0]} | Propietario: {campos[1]} | " +
                                  $"Servicio: {campos[2]} | Total: S/ {campos[3]} | Fecha: {campos[4]}");
                encontrado = true;
            }
        }

        if (!encontrado)
            Console.WriteLine("No se encontraron atenciones para esa placa.");
    }

    public static void ListarTodasLasAtenciones()
    {
        if (!File.Exists(Ruta))
        {
            Console.WriteLine("Aún no hay atenciones registradas.");
            return;
        }

        double suma = 0;
        int cantidad = 0;
        foreach (string linea in File.ReadAllLines(Ruta))
        {
            string[] campos = linea.Split('|');
            if (campos.Length != 5) continue;

            Console.WriteLine($"Placa: {campos[0]} | Propietario: {campos[1]} | " +
                              $"Servicio: {campos[2]} | Total: S/ {campos[3]} | Fecha: {campos[4]}");
            suma += double.Parse(campos[3], CultureInfo.InvariantCulture);
            cantidad++;
        }

        Console.WriteLine($"\nVehículos atendidos: {cantidad}");
        Console.WriteLine($"Total recaudado: S/ {suma:F2}");
    }
}
