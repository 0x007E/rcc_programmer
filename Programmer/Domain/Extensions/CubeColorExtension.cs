using RaGae.Projects.RCC.Core;

namespace RaGae.Projects.RCC.Domain.Extensions
{
    internal static class CubeColorExtension
    {
        internal static Color GetColor(this CubeColor color) => Color.FromArgb(color.Alpha * 255 / 15, color.Red, color.Green, color.Blue);
    }
}
