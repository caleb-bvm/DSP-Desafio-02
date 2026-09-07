namespace DSPDesafio02.Models
{
    public class Automovil : Vehiculo
    {
        public string TipoCombustible { get; set; } = string.Empty;

        public override double CalcularCostoMantenimiento()
        {
            double costo = 500;

            // Se estima un servicio más costoso para vehículos que usan gasolina premium.
            if (TipoCombustible == "Gasolina Premium")
            {
                costo += 150;
            }

            return costo;
        }

        public override string ObtenerResumen()
        {
            return $"{base.ObtenerResumen()} - Automóvil: {TipoCombustible}";
        }
    }
}
