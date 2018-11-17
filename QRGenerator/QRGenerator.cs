using System.Drawing.Imaging;
using System.IO;
using ZXing;

namespace QRGenerator
{
    public class QRGenerator
    {
        public static byte[] GetQRCode(string data)
        {
            var writer = new BarcodeWriter { Format = BarcodeFormat.QR_CODE };
            var qr = writer.Write(data);
            using (var memstream = new MemoryStream())
            {
                qr.Save(memstream, ImageFormat.Png);
                return memstream.ToArray();
            }
        }
    }
}