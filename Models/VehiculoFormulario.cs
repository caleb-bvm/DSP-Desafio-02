using System.ComponentModel.DataAnnotations;

namespace DSPDesafio02.Models
{
	public class VehiculoFormulario
	{
		[Required(ErrorMessage = "Seleccione el tipo de vehículo.")]
		[Display(Name = "Tipo de vehículo")]
		public string TipoVehiculo { get; set; } = string.Empty;

		[Required(ErrorMessage = "Ingrese la placa.")]
		public string Placa { get; set; } = string.Empty;

		[Required(ErrorMessage = "Ingrese la marca.")]
		public string Marca { get; set; } = string.Empty;

		[Required(ErrorMessage = "Ingrese el modelo.")]
		public string Modelo { get; set; } = string.Empty;

		[Range(1, 9999, ErrorMessage = "Ingrese un año entre 1 y 9999.")]
		[Display(Name = "Año")]
		public int Anio { get; set; }

		[Range(0, double.MaxValue,
			ErrorMessage = "El kilometraje no puede ser negativo.")]
		public double Kilometraje { get; set; }

		[Display(Name = "Capacidad de carga en toneladas")]
		public double? CapacidadCargaToneladas { get; set; }

		[Display(Name = "Tipo de combustible")]
		public string? TipoCombustible { get; set; }

		[Display(Name = "Cilindraje en cc")]
		public int? Cilindraje { get; set; }
	}
}