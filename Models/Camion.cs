namespace DSPDesafio02.Models
{
    public class Camion : Vehiculo
    {
        public double CapacidadCargaToneladas { get; set; }

        public override double CalcularCostoMantenimiento()
        {
            // Base de $1000 más $100 por tonelada por el desgaste asociado a la carga.
            return 1000 + CapacidadCargaToneladas * 100;
        }

        public override string ObtenerResumen()
        {
            return $"{base.ObtenerResumen()} - Camión: {CapacidadCargaToneladas} toneladas";
        }
    }
}
