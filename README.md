# Desafío práctico 02: modelos de vehículos

Esta parte implementa la clase abstracta `Vehiculo` y sus clases derivadas
`Camion`, `Automovil` y `Motocicleta`, en `DSPDesafio02.Models`.
El repositorio con `Dictionary`, el CRUD, el controlador y las vistas quedan
pendientes para la otra integrante.

## Herencia y polimorfismo

`Vehiculo` contiene las propiedades comunes: `Placa`, `Marca`, `Modelo`,
`Anio` y `Kilometraje`. `Modelo` es `string`, como exige el enunciado;
en el diagrama adjunto aparece como `int` y debe corregirse.

Las tres clases heredan esas propiedades y agregan su atributo propio.
`CalcularCostoMantenimiento()` es abstracto: cada clase lo implementa con
`override`. Al llamarlo desde una referencia `Vehiculo`, se ejecuta el cálculo
de la clase concreta, sin usar condiciones para identificar el tipo de vehículo.

`ObtenerResumen()` es virtual. Las tres clases lo sobrescriben y llaman a
`base.ObtenerResumen()` para conservar los datos comunes y agregar sus datos propios.

## Fórmulas de mantenimiento anual

El enunciado propone criterios, pero no fija montos. Se eligieron estos valores
en dólares como supuestos del ejercicio, no como tarifas reales:

| Clase | Atributo propio | Costo anual estimado | Justificación |
| --- | --- | --- | --- |
| `Camion` | `double CapacidadCargaToneladas` | 1000 + 100 por tonelada | Mayor capacidad de carga implica mayor desgaste estimado. |
| `Automovil` | `string TipoCombustible` | 500; se suman 150 si usa `Gasolina Premium` | Se supone un servicio más costoso para este combustible, siguiendo el criterio sugerido. |
| `Motocicleta` | `int Cilindraje` | 200; se suman 100 si supera 500 cc | Tiene una base menor y un recargo para motores de mayor cilindrada. |

Las condiciones del automóvil y la motocicleta evalúan sus atributos propios.
La selección del cálculo entre clases se resuelve mediante polimorfismo.

## Integración con el repositorio, controlador y vistas

- Importar `DSPDesafio02.Models` para usar las clases.
- Crear una instancia de `Camion`, `Automovil` o `Motocicleta` según la selección
  del formulario; `Vehiculo` es abstracto y no se puede instanciar directamente.
- Guardar las instancias como `Vehiculo` en el `Dictionary<string, Vehiculo>`
  requerido, usando `Placa` como llave.
- Llamar a `CalcularCostoMantenimiento()` y `ObtenerResumen()` desde la referencia
  `Vehiculo` recuperada del repositorio.
- Usar comprobaciones con `is` en las vistas para mostrar el tipo y sus campos,
  como permite el enunciado.
- En el formulario, usar exactamente `Gasolina Premium` como valor de la opción
  que activa el recargo del automóvil.
- Completar las validaciones de entrada en los formularios/controlador y las de
  placas duplicadas o inexistentes en el repositorio.

Ejemplo de llamada polimórfica, utilizable dentro de una acción del controlador:

```csharp
Vehiculo vehiculo = new Camion
{
    Placa = "C123456",
    Marca = "Isuzu",
    Modelo = "NPR",
    Anio = 2022,
    Kilometraje = 45000,
    CapacidadCargaToneladas = 5
};

double costo = vehiculo.CalcularCostoMantenimiento(); // 1500
string resumen = vehiculo.ObtenerResumen();
```

## Verificación realizada

- El proyecto compila con `dotnet build --no-restore`, sin errores ni advertencias.
- Se verificaron siete casos llamando al cálculo mediante referencias `Vehiculo`:
  camión de 0 y 2.5 toneladas (1000 y 1250), automóvil regular y premium
  (500 y 650), y motocicletas de 499, 500 y 501 cc (200, 200 y 300).
- Se verificó que los resúmenes conservan los datos comunes y agregan los propios.

Para completar la entrega conjunta aún faltan la parte MVC y el repositorio,
la prueba de integración y el documento de 1–2 páginas con el diagrama corregido
y la explicación del polimorfismo. Esta guía sirve de base para esa explicación.
