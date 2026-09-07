namespace DSPDesafio02.Models
{
    public class Motocicleta : Vehiculo
    {
        public int Cilindraje { get; set; }

        public override double CalcularCostoMantenimiento()
        {
            double costo = 200;

            // Base reducida, con un recargo para motores de mayor cilindrada.
            if (Cilindraje > 500)
            {
                costo += 100;
            }

            return costo;
        }

        public override string ObtenerResumen()
        {
            return $"{base.ObtenerResumen()} - Motocicleta: {Cilindraje} cc";
        }
    }
}
