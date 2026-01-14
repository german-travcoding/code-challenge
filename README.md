# 🏆 Product Aggregator - Code Challenge

## 🎯 Escenario

Eres parte de un equipo que mantiene un servicio de agregación de productos. Este servicio consulta múltiples proveedores externos para obtener:

- **Precios** de 3 proveedores diferentes
- **Stock** de 2 regiones (East/West coast)

El servicio actual tiene problemas de rendimiento. Tu tarea es identificar los problemas y optimizarlo.

## 📁 Estructura del Proyecto

```
src/
├── ProductAggregator.Api/
│   ├── Controllers/
│   │   └── ProductsController.cs    # Endpoints de la API
│   └── Program.cs                   # Configuración DI
│
└── ProductAggregator.Core/
    ├── Models/                      # Modelos de dominio
    ├── Interfaces/                  # Contratos
    ├── Services/
    │   ├── ProductAggregatorService.cs  # Servicio a optimizar
    │   └── MockProviders/               # Simulan APIs externas
    └── Factories/
        └── ProviderFactory.cs       
```

## 🚀 Cómo Ejecutar

```bash
# Restaurar paquetes
dotnet restore

# Ejecutar la API
cd src/ProductAggregator.Api
dotnet run
```

## 🧪 Endpoints para Testing

### 1. Benchmark (Medir rendimiento)
```bash
curl "http://localhost:5000/api/products/benchmark?productCount=10"
```

### 2. Agregar múltiples productos
```bash
curl -X POST "http://localhost:5000/api/products/aggregate" \
  -H "Content-Type: application/json" \
  -d '{
    "productIds": ["PROD-001", "PROD-002", "PROD-003"],
    "includePrices": true,
    "includeStock": true
  }'
```

### 3. Obtener un producto
```bash
curl "http://localhost:5000/api/products/PROD-001"
```

## 📝 Tareas

1. Analizar el código actual en `ProductAggregatorService.cs`
2. Identificar los problemas de rendimiento
3. Proponer e implementar mejoras
