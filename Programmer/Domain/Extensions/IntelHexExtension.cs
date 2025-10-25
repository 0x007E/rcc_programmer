using RaGae.Projects.RCC.Core;

namespace RaGae.Projects.RCC.Domain.Extensions
{
    internal static class IntelHexExtension
    {
        internal static byte CalculateChecksum(this IntelHex hexline)
        {
            byte[] bytes = new byte[4 + hexline.Data.Length];

            bytes[0] = hexline.Count;
            bytes[1] = hexline.HighAddress;
            bytes[2] = hexline.LowAddress;
            bytes[3] = hexline.Record;

            Array.Copy(hexline.Data, 0, bytes, 4, hexline.Data.Length);

            int sum = 0;

            foreach (byte b in bytes)
            {
                sum += b;
            }
            byte checksum = (byte)((~sum + 1) & 0xFF);

            return checksum;
        }

        internal static IntelHex FromString(this string hexLine)
        {
            if (string.IsNullOrWhiteSpace(hexLine) || !hexLine.StartsWith(":"))
            {
                throw new ArgumentException(StringResource.ErrorInvalidHexLine);
            }

            IntelHex intelHex = new IntelHex
            {
                Count = Convert.ToByte(hexLine.Substring(1, 2), 16),
                HighAddress = Convert.ToByte(hexLine.Substring(3, 2), 16),
                LowAddress = Convert.ToByte(hexLine.Substring(5, 2), 16),
                Record = Convert.ToByte(hexLine.Substring(7, 2), 16),
            };

            byte[] data = new byte[intelHex.Count];

            for (int i = 0; i < intelHex.Count; i++)
            {
                data[i] = Convert.ToByte(hexLine.Substring(9 + i * 2, 2), 16);
            }

            intelHex.Data = data;
            intelHex.Checksum = Convert.ToByte(hexLine.Substring(9 + intelHex.Count * 2, 2), 16);

            return intelHex;
        }

        internal static IntelHex ModifyByte(this IntelHex hexLine, int position, byte databyte)
        {
            if (hexLine == null)
            {
                throw new ArgumentNullException(nameof(hexLine));
            }
            if (hexLine.Data == null)
            {
                throw new InvalidOperationException(StringResource.ErrorEmptyDataArray);
            }
            if (position < 0 || position >= hexLine.Data.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(position), StringResource.ErrorPositionOutsideDataarea);
            }

            hexLine.Data[position] = databyte;

            return hexLine;
        }

    }
}
