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
		[StringLength(50, ErrorMessage = "La marca no debe superar los 50 caracteres.")]
		public string Marca { get; set; } = string.Empty;

		[Required(ErrorMessage = "Ingrese el modelo.")]
		[StringLength(50, ErrorMessage = "El modelo no debe superar los 50 caracteres.")]
		public string Modelo { get; set; } = string.Empty;

		[Range(1886, 9999, ErrorMessage = "El año debe ser igual o mayor que 1886.")]
		[Display(Name = "Año")]
		public int Anio { get; set; }

		[Range(0, double.MaxValue,
			ErrorMessage = "El kilometraje no puede ser negativo.")]
		public double Kilometraje { get; set; }

		[Display(Name = "Capacidad de carga en toneladas")]
		[Range(0.01, double.MaxValue, ErrorMessage = "La capacidad debe ser de al menos 0.01 toneladas.")]
		public double? CapacidadCargaToneladas { get; set; }

		[Display(Name = "Tipo de combustible")]
		public string? TipoCombustible { get; set; }

		[Display(Name = "Cilindraje en cc")]
		public int? Cilindraje { get; set; }
	}
}
