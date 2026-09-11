using DSPDesafio02.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSPDesafio02.Controllers
{
	public class VehiculosController : Controller
	{
		private readonly VehiculoRepositorio _repositorio =
			new VehiculoRepositorio();

		public IActionResult Index()
		{
			return View(_repositorio.ObtenerTodos());
		}

		public IActionResult Details(string placa)
		{
			if (string.IsNullOrWhiteSpace(placa))
			{
				return NotFound();
			}

			Vehiculo? vehiculo = _repositorio.ObtenerPorPlaca(placa);

			if (vehiculo == null)
			{
				return NotFound();
			}

			return View(vehiculo);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View(new VehiculoFormulario());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(VehiculoFormulario formulario)
		{
			ValidarFormulario(formulario);

			if (!ModelState.IsValid)
			{
				return View(formulario);
			}

			Vehiculo vehiculo = CrearVehiculo(formulario);

			if (!_repositorio.Agregar(vehiculo))
			{
				ModelState.AddModelError(
					nameof(formulario.Placa),
					"Ya existe un vehículo con esa placa.");

				return View(formulario);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public IActionResult Edit(string placa)
		{
			if (string.IsNullOrWhiteSpace(placa))
			{
				return NotFound();
			}

			Vehiculo? vehiculo = _repositorio.ObtenerPorPlaca(placa);

			if (vehiculo == null)
			{
				return NotFound();
			}

			ViewBag.PlacaOriginal = placa;

			return View(CrearFormulario(vehiculo));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(
			string placaOriginal,
			VehiculoFormulario formulario)
		{
			if (string.IsNullOrWhiteSpace(placaOriginal) ||
				_repositorio.ObtenerPorPlaca(placaOriginal) == null)
			{
				return NotFound();
			}

			ViewBag.PlacaOriginal = placaOriginal;
			ValidarFormulario(formulario);

			if (!ModelState.IsValid)
			{
				return View(formulario);
			}

			Vehiculo vehiculo = CrearVehiculo(formulario);

			if (!_repositorio.Actualizar(placaOriginal, vehiculo))
			{
				ModelState.AddModelError(
					string.Empty,
					"No se pudo actualizar. La placa ya está en uso " +
					"o el vehículo ya no existe.");

				return View(formulario);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public IActionResult Delete(string placa)
		{
			if (string.IsNullOrWhiteSpace(placa))
			{
				return NotFound();
			}

			Vehiculo? vehiculo = _repositorio.ObtenerPorPlaca(placa);

			if (vehiculo == null)
			{
				return NotFound();
			}

			return View(vehiculo);
		}

		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(string placa)
		{
			if (string.IsNullOrWhiteSpace(placa) ||
				!_repositorio.Eliminar(placa))
			{
				return NotFound();
			}

			return RedirectToAction(nameof(Index));
		}

		private void ValidarFormulario(VehiculoFormulario formulario)
		{
			switch (formulario.TipoVehiculo)
			{
				case "Camion":
					if (formulario.CapacidadCargaToneladas == null ||
						formulario.CapacidadCargaToneladas <= 0)
					{
						ModelState.AddModelError(
							nameof(formulario.CapacidadCargaToneladas),
							"Ingrese una capacidad mayor que cero.");
					}
					break;

				case "Automovil":
					if (formulario.TipoCombustible != "Gasolina Regular" &&
						formulario.TipoCombustible != "Gasolina Premium" &&
						formulario.TipoCombustible != "Diésel")
					{
						ModelState.AddModelError(
							nameof(formulario.TipoCombustible),
							"Seleccione un combustible válido.");
					}
					break;

				case "Motocicleta":
					if (formulario.Cilindraje == null ||
						formulario.Cilindraje <= 0)
					{
						ModelState.AddModelError(
							nameof(formulario.Cilindraje),
							"Ingrese un cilindraje mayor que cero.");
					}
					break;

				default:
					ModelState.AddModelError(
						nameof(formulario.TipoVehiculo),
						"Seleccione un tipo de vehículo válido.");
					break;
			}
		}

		private Vehiculo CrearVehiculo(VehiculoFormulario formulario)
		{
			Vehiculo vehiculo;

			// Se crea la clase concreta elegida en el formulario.
			switch (formulario.TipoVehiculo)
			{
				case "Camion":
					vehiculo = new Camion
					{
						CapacidadCargaToneladas =
							formulario.CapacidadCargaToneladas.GetValueOrDefault()
					};
					break;

				case "Automovil":
					vehiculo = new Automovil
					{
						TipoCombustible = formulario.TipoCombustible!
					};
					break;

				case "Motocicleta":
					vehiculo = new Motocicleta
					{
						Cilindraje = formulario.Cilindraje.GetValueOrDefault()
					};
					break;

				default:
					throw new ArgumentException("Tipo de vehículo inválido.");
			}

			vehiculo.Placa = formulario.Placa.Trim();
			vehiculo.Marca = formulario.Marca.Trim();
			vehiculo.Modelo = formulario.Modelo.Trim();
			vehiculo.Anio = formulario.Anio;
			vehiculo.Kilometraje = formulario.Kilometraje;

			return vehiculo;
		}

		private VehiculoFormulario CrearFormulario(Vehiculo vehiculo)
		{
			var formulario = new VehiculoFormulario
			{
				Placa = vehiculo.Placa,
				Marca = vehiculo.Marca,
				Modelo = vehiculo.Modelo,
				Anio = vehiculo.Anio,
				Kilometraje = vehiculo.Kilometraje
			};

			if (vehiculo is Camion camion)
			{
				formulario.TipoVehiculo = "Camion";
				formulario.CapacidadCargaToneladas =
					camion.CapacidadCargaToneladas;
			}
			else if (vehiculo is Automovil automovil)
			{
				formulario.TipoVehiculo = "Automovil";
				formulario.TipoCombustible = automovil.TipoCombustible;
			}
			else if (vehiculo is Motocicleta motocicleta)
			{
				formulario.TipoVehiculo = "Motocicleta";
				formulario.Cilindraje = motocicleta.Cilindraje;
			}

			return formulario;
		}
	}
}