using System.ComponentModel.DataAnnotations;

namespace RaGae.Projects.RCC.Core
{
    internal class DudeConfig
    {
        public string DownloadUrl { get; set; }

        public string ExtractPath { get; set; }

        [Required]
        public string Programmer { get; set; }

        [Required, Range(9600, 115200)]
        public int Baudrate { get; set; }

        [Required]
        public string Mode { get; set; }

        public bool Verbose { get; set; } = false;
    }
}