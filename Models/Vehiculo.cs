namespace DSPDesafio02.Models
{
    // Reúne los datos comunes de todos los vehículos de la flota.
    public abstract class Vehiculo
    {
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Anio { get; set; }
        public double Kilometraje { get; set; }

        // Cada clase derivada debe definir su propio cálculo de mantenimiento.
        public abstract double CalcularCostoMantenimiento();

        public virtual string ObtenerResumen()
        {
            return $"{Marca} {Modelo} ({Anio}) - Placa: {Placa}";
        }
    }
}
