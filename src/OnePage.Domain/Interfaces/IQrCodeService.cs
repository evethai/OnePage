namespace OnePage.Domain.Interfaces
{
    public interface IQrCodeService
    {
        byte[] GenerateQrCode(string url);
    }
}
