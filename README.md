# DiabeLife Backend API

API backend para la aplicación DiabeLife desarrollada con ASP.NET Core usando Domain-Driven Design (DDD).

## 🚀 Características

- **Arquitectura DDD**: Separación clara entre dominio, aplicación, infraestructura e interfaces
- **Base de datos MySQL**: Configurada con Entity Framework Core
- **API RESTful**: Endpoints completos para gestión de métricas de salud
- **Documentación Swagger**: API completamente documentada
- **CORS configurado**: Preparado para conectar con frontend en Netlify

## 📋 Requisitos

- .NET 9.0 SDK
- MySQL Server 8.0+
- Visual Studio 2022 o VS Code

## 🔧 Configuración

### 1. Configurar Base de Datos

```sql
-- Crear base de datos
CREATE DATABASE diabelife;
CREATE DATABASE diabelife_dev; -- Para desarrollo

-- Crear usuario (opcional)
CREATE USER 'diabelife_user'@'localhost' IDENTIFIED BY 'password';
GRANT ALL PRIVILEGES ON diabelife.* TO 'diabelife_user'@'localhost';
GRANT ALL PRIVILEGES ON diabelife_dev.* TO 'diabelife_user'@'localhost';
FLUSH PRIVILEGES;
```

### 2. Configurar Cadena de Conexión

Actualizar `appsettings.json` y `appsettings.Development.json` con tu configuración de MySQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=diabelife;Uid=root;Pwd=tu_password;"
  }
}
```

### 3. Ejecutar la Aplicación

```bash
cd DevsPros.Diabelife.Platform.API
dotnet run
```

La aplicación estará disponible en:
- **Swagger UI**: `https://localhost:7000` (puerto por defecto)
- **API Base URL**: `https://localhost:7000/api/v1`

## 📚 Endpoints Principales

### Health Metrics
- `GET /api/v1/healthmetrics` - Obtener todas las métricas
- `GET /api/v1/healthmetrics/{id}` - Obtener métrica por ID
- `GET /api/v1/healthmetrics/latest` - Obtener última métrica
- `POST /api/v1/healthmetrics` - Crear nueva métrica
- `PUT /api/v1/healthmetrics/{id}` - Actualizar métrica
- `DELETE /api/v1/healthmetrics/{id}` - Eliminar métrica

### Recommendations
- `GET /api/v1/recommendations` - Obtener todas las recomendaciones
- `GET /api/v1/recommendations/{id}` - Obtener recomendación por ID
- `POST /api/v1/recommendations` - Crear nueva recomendación
- `PUT /api/v1/recommendations/{id}` - Actualizar recomendación
- `DELETE /api/v1/recommendations/{id}` - Eliminar recomendación

### Food Data
- `GET /api/v1/fooddata` - Obtener todos los alimentos
- `GET /api/v1/fooddata/{id}` - Obtener alimento por ID
- `GET /api/v1/fooddata/recent?count=10` - Obtener alimentos recientes
- `POST /api/v1/fooddata` - Registrar nuevo alimento
- `PUT /api/v1/fooddata/{id}` - Actualizar alimento
- `DELETE /api/v1/fooddata/{id}` - Eliminar alimento

### Dashboard
- `GET /api/v1/healthy/dashboard` - Datos completos del dashboard
- `GET /api/v1/healthy/metrics/latest` - Últimas métricas
- `GET /api/v1/healthy/summary` - Resumen de salud

## 🏗️ Estructura del Proyecto

```
DevsPros.Diabelife.Platform.API/
├── HealthyLife/
│   ├── application/
│   │   └── internal/
│   │       ├── commandservices/     # Servicios de comando
│   │       ├── queryservices/       # Servicios de consulta
│   │       └── outboundservices/    # Interfaces de servicios
│   ├── domain/
│   │   ├── model/                   # Entidades del dominio
│   │   └── repositories/            # Interfaces de repositorio
│   ├── infrastructure/
│   │   └── persistence/
│   │       └── EFC/
│   │           └── Repositories/    # Implementaciones de repositorio
│   └── interfaces/
│       └── REST/                    # Controladores de API
└── Shared/
    ├── Domain/
    │   ├── Model/                   # Entidades base
    │   └── Repositories/            # Repositorios base
    └── Infrastructure/
        └── Persistence/
            └── EFC/
                ├── Configuration/   # DbContext
                └── Repositories/    # Repositorio base
```

## 📊 Modelos de Datos

### HealthMetric
```csharp
{
  "id": 1,
  "heartRate": 2123,
  "glucose": 11.0,
  "weight": 11.0,
  "bloodPressure": "1111",
  "createdAt": "2025-11-06T10:30:00Z",
  "updatedAt": "2025-11-06T10:30:00Z"
}
```

### Recommendation
```csharp
{
  "id": 1,
  "text": "Reduce added sugars and processed snacks.",
  "createdAt": "2025-11-06T10:30:00Z",
  "updatedAt": "2025-11-06T10:30:00Z"
}
```

### FoodData
```csharp
{
  "id": 1,
  "food": "Platano",
  "timestamp": "2025-10-05T02:09:22.652Z",
  "createdAt": "2025-11-06T10:30:00Z",
  "updatedAt": "2025-11-06T10:30:00Z"
}
```

## 🔗 Conectar con Frontend

La API está configurada para aceptar requests desde:
- **Producción**: `https://diabelife-frontend.netlify.app`
- **Desarrollo**: Cualquier origen (localhost, etc.)

### Configuración CORS
- Headers permitidos: Todos
- Métodos permitidos: GET, POST, PUT, DELETE
- Credenciales: Habilitadas para producción

## 🛠️ Comandos Útiles

```bash
# Restaurar dependencias
dotnet restore

# Compilar proyecto
dotnet build

# Ejecutar en modo desarrollo
dotnet run --environment Development

# Limpiar y rebuilder
dotnet clean && dotnet build

# Ver logs detallados
dotnet run --verbosity detailed
```

## 📁 Inicialización de Datos

Ejecutar el script `database_init.sql` para cargar datos de ejemplo:

```bash
mysql -u root -p diabelife < database_init.sql
```

## 🔍 Testing

Usar Swagger UI para probar todos los endpoints:
1. Navegar a `https://localhost:7000`
2. Explorar y probar endpoints
3. Ver esquemas de datos y respuestas

## 📧 Soporte

- **Equipo**: DevsPros
- **Email**: devspros@diabelife.com
- **Proyecto**: DiabeLife Backend API v1.0