# FlexWallpaper

Aplicación Windows Forms para gestionar fondos de pantalla en múltiples monitores.

## Características

- **Fondos por monitor**: Asigna una imagen diferente a cada monitor
- **Bóveda de fondos**: Guarda combinaciones de fondos para aplicarlas rápidamente
- **Cambio automático**: Temporizador para rotar fondos cada cierto intervalo
- **Modo oscuro/claro**: Tema oscuro y claro con soporte para título oscuro en Windows
- **Bandeja del sistema**: Minimiza a la bandeja con acceso rápido a funciones
- **Estilos de ajuste**: Fill, Fit, Stretch, Tile, Center, Span
- **Arrastrar y soltar**: Soporte drag & drop para imágenes
- **Inicio automático**: Opción para iniciar con Windows
- **Iconos SVG personalizados**: Iconos UI renderizados con GDI+

## Requisitos

- Windows (compatible con múltiples monitores)
- .NET 10.0 o superior

## Paquetes NuGet

- [CuoreUI.Winforms](https://www.nuget.org/packages/CuoreUI.Winforms/) — componentes UI modernos

## Instalación

1. Clona el repositorio
2. Abre `Aplicacion Fondos.csproj` en Visual Studio o Rider
3. Restaura los paquetes NuGet
4. Compila y ejecuta

## Uso

1. Selecciona una imagen para cada monitor usando el botón **Seleccionar**
2. Elige el estilo de ajuste (Fill, Fit, etc.)
3. Haz clic en **APLICAR FONDOS**
4. Las combinaciones se guardan automáticamente en la Bóveda

## Licencia

MIT
