using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RaGae.Projects.RCC.Core
{
    internal class IntelHex
    {
        public IntelHex() { }

        [Required]
        public byte Count { get; set; }

        [Required]
        public byte HighAddress { get; set; }

        [Required]
        public byte LowAddress { get; set; }

        [Required]
        public byte Record { get; set; }
        public byte[] Data { get; set; }
        public byte Checksum { get; set; }


        public override string ToString()
        {
            if (this is null)
            {
                throw new ArgumentException(StringResource.ErrorInvalidHexLine);
            }
            StringBuilder line = new StringBuilder();

            line.Append(":");
            line.AppendFormat("{0:X2}", this.Count);
            line.AppendFormat("{0:X2}{1:X2}", this.HighAddress, this.LowAddress);
            line.AppendFormat("{0:X2}", this.Record);

            foreach (byte b in this.Data)
            {
                line.AppendFormat("{0:X2}", b);
            }
            line.AppendFormat("{0:X2}", this.Checksum);

            return line.ToString();
        }
    }
}
