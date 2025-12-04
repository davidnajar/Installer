# Kairos Installer

Aplicación de instalación tipo wizard construida con Blazor Server (.NET 10) y Tailwind CSS.

## Características

- ✨ Formulario dinámico generado desde JSON
- 🎨 Interfaz moderna con Tailwind CSS (sin CDNs)
- 🔄 Navegación tipo wizard (Next/Back/Finish)
- 💾 Guardado de configuración en JSON
- 🌐 Sin dependencias externas (apto para air-gapped)
- 🚀 Ejecuta en http://0.0.0.0:8080

## Requisitos Previos

- .NET 10.0 SDK
- Node.js 18+ y npm (para compilar Tailwind CSS)

## Estructura del Proyecto

```
KairosInstaller/
├── config/
│   └── form-schema.json       # Esquema del formulario
├── Models/
│   ├── FormSchema.cs
│   ├── FormStep.cs
│   └── FormField.cs
├── Services/
│   ├── IFormSchemaService.cs
│   └── FormSchemaService.cs
├── Components/
│   ├── Pages/
│   │   └── Wizard.razor       # Componente principal del wizard
│   └── Layout/
│       └── MainLayout.razor
├── Styles/
│   └── input.css              # CSS de entrada de Tailwind
├── wwwroot/
│   └── css/
│       └── site.css           # CSS generado por Tailwind
└── output/                    # Configuraciones guardadas
```

## Instalación

1. **Clonar/descargar el proyecto**

2. **Restaurar dependencias de .NET:**
   ```bash
   cd KairosInstaller
   dotnet restore
   ```

3. **Instalar dependencias de npm:**
   ```bash
   npm install
   ```

4. **Compilar CSS de Tailwind:**
   ```bash
   npm run css:build
   ```

## Desarrollo

### Ejecutar la aplicación

```bash
dotnet run
```

La aplicación estará disponible en http://localhost:8080

### Modo watch de Tailwind CSS

Para desarrollo, puedes ejecutar Tailwind en modo watch en una terminal separada para que recompile automáticamente los estilos cuando cambies los archivos Razor:

```bash
npm run css:watch
```

Este comando observará cambios en los archivos `Components/**/*.razor` y regenerará `wwwroot/css/site.css` automáticamente.

### Desarrollo con recarga automática

Recomendación para desarrollo:

**Terminal 1 - Tailwind CSS Watch:**
```bash
npm run css:watch
```

**Terminal 2 - Aplicación .NET:**
```bash
dotnet watch run
```

## Compilación para Producción

1. **Compilar CSS de Tailwind (modo producción):**
   ```bash
   npm run css:build
   ```
   Esto genera un archivo CSS minificado con solo las clases utilizadas.

2. **Compilar la aplicación:**
   ```bash
   dotnet build -c Release
   ```

3. **Publicar la aplicación:**
   ```bash
   dotnet publish -c Release -o ./publish
   ```

## Configuración del Esquema

El esquema del formulario se define en `config/form-schema.json`. Este archivo controla:

- Título del instalador
- Pasos del wizard
- Campos de cada paso

### Ejemplo de Esquema

```json
{
  "title": "Configuración del nodo Kairos",
  "steps": [
    {
      "id": "network",
      "title": "Red",
      "fields": [
        {
          "name": "ip",
          "label": "IP",
          "type": "text",
          "placeholder": "192.168.1.10"
        }
      ]
    }
  ]
}
```

### Tipos de Campo Soportados

- `text` - Campo de texto
- `password` - Campo de contraseña
- `number` - Campo numérico
- `checkbox` - Casilla de verificación
- `select` - Lista desplegable (requiere array `options`)

### Ejemplo de Campo Select

```json
{
  "name": "environment",
  "label": "Entorno",
  "type": "select",
  "options": [
    { "value": "dev", "label": "Desarrollo" },
    { "value": "prod", "label": "Producción" }
  ]
}
```

## Salida de Configuración

Al completar el wizard, la configuración se guarda en:

```
output/configuration_YYYYMMDD_HHMMSS.json
```

Formato del archivo de salida:
```json
{
  "ip": "192.168.1.10",
  "mask": "255.255.255.0",
  "hostname": "nodo-01",
  "enable_ssh": true
}
```

## Despliegue Air-Gapped

Para entornos sin conexión a Internet:

1. Compila la aplicación y genera el CSS en un entorno con conexión
2. Copia la carpeta `publish/` al servidor de destino
3. La aplicación no requiere conexión a Internet para ejecutarse
4. No se usan CDNs externos - todos los recursos están incluidos

## Ejecutar en Producción

```bash
cd publish
dotnet KairosInstaller.dll
```

O usando el ejecutable nativo (si se publicó con `--self-contained`):
```bash
cd publish
./KairosInstaller
```

## Personalización

### Cambiar Puerto

Edita `Program.cs`:
```csharp
builder.WebHost.UseUrls("http://0.0.0.0:NUEVO_PUERTO");
```

### Modificar Estilos

Los estilos de Tailwind están en `Styles/input.css`. Puedes agregar estilos personalizados:

```css
@tailwind base;
@tailwind components;
@tailwind utilities;

/* Tus estilos personalizados */
.mi-clase-custom {
  /* ... */
}
```

Después de modificar, ejecuta:
```bash
npm run css:build
```

## Troubleshooting

### El CSS no se aplica
- Verifica que `wwwroot/css/site.css` existe
- Ejecuta `npm run css:build`
- Limpia el caché del navegador

### Error al cargar el esquema
- Verifica que `config/form-schema.json` existe
- Verifica que el JSON es válido
- Revisa los logs de la aplicación

### Puerto en uso
- Cambia el puerto en `Program.cs`
- O detén el proceso que usa el puerto 8080

## Licencia

Este proyecto es de código abierto y está disponible bajo la licencia que el propietario decida aplicar.
