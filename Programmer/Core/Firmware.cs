using System.ComponentModel.DataAnnotations;

namespace RaGae.Projects.RCC.Core
{
    internal class Firmware
    {
        [Required, Url]
        public string DownloadUrl { get; set; }

        [Required]
        public string MCU { get; set; }

        [Required]
        public string FlashFile { get; set; }

        [Required]
        public string EEPROMFile { get; set; }
    }
}