using System.ComponentModel.DataAnnotations;

namespace RaGae.Projects.RCC.Core
{
    internal class CubeColor
    {
        [Required, Range(1, 15)]
        public int Alpha { get; set; }

        [Required, Range(0, 255)]
        public int Blue { get; set; }

        [Required, Range(0, 255)]
        public int Green { get; set; }

        [Required, Range(0, 255)]
        public int Red { get; set; }
    }
}