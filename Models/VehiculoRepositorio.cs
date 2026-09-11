namespace DSPDesafio02.Models
{
	public class VehiculoRepositorio
	{
		// La placa es la llave. No distingue mayúsculas de minúsculas.
		private static Dictionary<string, Vehiculo> _vehiculos =
			new Dictionary<string, Vehiculo>(StringComparer.OrdinalIgnoreCase);

		public bool Agregar(Vehiculo vehiculo)
		{
			return _vehiculos.TryAdd(vehiculo.Placa, vehiculo);
		}

		public List<Vehiculo> ObtenerTodos()
		{
			return _vehiculos.Values.ToList();
		}

		public Vehiculo? ObtenerPorPlaca(string placa)
		{
			_vehiculos.TryGetValue(placa, out Vehiculo? vehiculo);
			return vehiculo;
		}

		public bool Actualizar(string placa, Vehiculo vehiculo)
		{
			if (!_vehiculos.ContainsKey(placa))
			{
				return false;
			}

			bool cambioPlaca = !string.Equals(
				placa,
				vehiculo.Placa,
				StringComparison.OrdinalIgnoreCase);

			if (cambioPlaca && _vehiculos.ContainsKey(vehiculo.Placa))
			{
				return false;
			}

			_vehiculos.Remove(placa);
			_vehiculos[vehiculo.Placa] = vehiculo;

			return true;
		}

		public bool Eliminar(string placa)
		{
			return _vehiculos.Remove(placa);
		}
	}
}