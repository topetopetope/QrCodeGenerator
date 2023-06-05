using System.Drawing;

namespace HIOF.BachelorTesting.QrCodeReceiver.Model
{
    public class QrCodeReceived
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
}
