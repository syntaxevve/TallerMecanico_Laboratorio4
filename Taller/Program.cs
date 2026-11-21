using FacturacionTaller;

int opcion = 0;

while (opcion != 4)
{
    try
    {
        Console.WriteLine("\n=== TALLER MECÁNICO ===");
        Console.WriteLine("1. Registrar atención");
        Console.WriteLine("2. Buscar atención por Placa");
        Console.WriteLine("3. Reporte general de atenciones");
        Console.WriteLine("4. Salir");
        Console.Write("Elija una opción: ");
        opcion = int.Parse(Console.ReadLine() ?? "");

        switch (opcion)
        {
            case 1:
                Console.Write("Placa: ");
                string placa = Console.ReadLine() ?? "";
                Console.Write("Propietario: ");
                string propietario = Console.ReadLine() ?? "";
                Console.Write("Tipo de servicio (PREVENTIVO / CORRECTIVO / OTRO): ");
                string tipo = Console.ReadLine() ?? "";
                Console.Write("¿Cliente frecuente? (SI/NO): ");
                string frecuente = Console.ReadLine() ?? "";

                double tarifa = ModuloTaller.ObtenerTarifaBase(tipo);
                double descuento = ModuloTaller.CalcularDescuentoCliente(tarifa, frecuente);
                double igv = ModuloTaller.CalcularImpuestoIGV(tarifa - descuento);
                double total = ModuloTaller.CalcularTotalPagar(tarifa, descuento, igv);

                DateTime fecha = DateTime.Now;
                string registro = ModuloTaller.ConstruirRegistroPipe(
                    placa, propietario, tipo.ToUpper(), total, fecha);
                ModuloArchivo.GuardarAtencion(registro);

                Console.WriteLine($"\nTarifa: S/ {tarifa:F2} | Descuento: S/ {descuento:F2} | " +
                                  $"IGV: S/ {igv:F2} | TOTAL: S/ {total:F2}");
                Console.WriteLine("Atención registrada.");
                break;

            case 2:
                Console.Write("Placa a buscar: ");
                ModuloArchivo.BuscarAtencion(Console.ReadLine() ?? "");
                break;

            case 3:
                ModuloArchivo.ListarTodasLasAtenciones();
                break;

            case 4:
                Console.WriteLine("Hasta luego.");
                break;

            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Error: ingrese un número válido.");
        opcion = 0;
    }
    catch (IOException ex)
    {
        Console.WriteLine("Error de archivo: " + ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error inesperado: " + ex.Message);
    }
}
