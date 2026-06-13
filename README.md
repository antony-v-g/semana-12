# Evaluación 3: API Inteligente de Tareas y Análisis

## Stack
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQLite
- ML.NET

## Ramas implementadas
- `feature/api-tareas` — CRUD de tareas y validación de datos
- `feature/filtros-tareas` — filtros en `GET /api/tareas`
- `feature/api-externa-todos` — consumo de API externa `jsonplaceholder.typicode.com/todos`
- `feature/mlnet-basico` — análisis de sentimiento con ML.NET

## Cómo ejecutar localmente
```bash
cd "evaluacion 3"
dotnet restore
dotnet build
dotnet run
```

La API se ejecuta en `https://localhost:5001` o el puerto asignado por ASP.NET Core.

## Migraciones
El proyecto usa EF Core con SQLite.

Para crear o aplicar migraciones:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

La aplicación también aplica migraciones automáticamente al iniciar.

## Endpoints implementados

### Tareas
- `GET /api/tareas`
- `GET /api/tareas/{id}`
- `POST /api/tareas`
- `PUT /api/tareas/{id}`
- `DELETE /api/tareas/{id}`

### Filtros en tareas
- `GET /api/tareas?estado=Pendiente`
- `GET /api/tareas?prioridad=Alta`
- `GET /api/tareas?fechaInicio=2026-05-01&fechaFin=2026-05-31`

Validaciones:
- `fechaInicio` no puede ser mayor que `fechaFin`
- `estado` inválido devuelve `400`
- `prioridad` inválida devuelve `400`

### API externa
- `GET /api/tareas-externas`
- `GET /api/tareas-externas/{id}`

Ejemplo externo:
```http
GET /api/tareas-externas
```
Respuesta mapeada:
```json
{
  "externalId": 1,
  "titulo": "delectus aut autem",
  "completado": false
}
```

### ML.NET
- `POST /api/ml/sentimiento`

Request:
```json
{
  "comentario": "La tarea fue completada correctamente y el sistema funciona bien"
}
```

Response:
```json
{
  "comentario": "La tarea fue completada correctamente y el sistema funciona bien",
  "sentimiento": "Positivo"
}
```

## ML.NET usado
El servicio de sentimiento usa ML.NET con:
- `TextFeaturizingEstimator` para convertir texto en características numéricas
- `SdcaLogisticRegression` para clasificación binaria
- Dataset local en `evaluacion 3/Data/sentimiento_dataset.csv`

Las frases positivas se etiquetan con `1` y las negativas con `0`.

## Notas adicionales
- La base de datos SQLite se crea en `evaluacion 3/tareas.db`.
- Swagger está habilitado en entorno de desarrollo.
- Los branches están empujados a GitHub y el código final está fusionado en `main`.
