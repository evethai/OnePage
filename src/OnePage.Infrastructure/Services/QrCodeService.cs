using QRCoder;
using OnePage.Domain.Interfaces;

namespace OnePage.Infrastructure.Services
{
    public class QrCodeService : IQrCodeService
    {
        public byte[] GenerateQrCode(string url)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            return qrCode.GetGraphic(20); // Returns byte[] of the PNG image (pixel size = 20)
        }
    }
}
