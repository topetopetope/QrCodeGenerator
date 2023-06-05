using System.Drawing;

namespace HIOF.BachelorTesting.QRCodeService.Model
{
    public class QrCode
    {
        Guid Id { get; set; }
        Bitmap QrCodeImage { get; set; }

    }
}
