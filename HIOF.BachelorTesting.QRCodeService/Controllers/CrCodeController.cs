using HIOF.BachelorTesting.QRCodeService.Model;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using ZXing.QrCode;

namespace HIOF.BachelorTesting.QRCodeService.Controllers
{
    [ApiController]
    [Route("api/QrCodeGenerator")]
    public class CrCodeController : ControllerBase
    {
        private readonly ILogger<CrCodeController> _logger;

        public CrCodeController(ILogger<CrCodeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GenerateQrCode")]
        public IActionResult GetQrCode(string targetUrl, string fileName)
        {
            var writer = new QRCodeWriter();
            var matrix = writer.encode(targetUrl, BarcodeFormat.QR_CODE, 200, 200);
            int scale = 2;
            // Sets QR-code as Bitmap
            Bitmap QrCodeBitmap = new(matrix.Width * scale, matrix.Height * scale);

            for (int x = 0; x < matrix.Height; x++)
            {
                for (int y = 0; y < matrix.Width; y++)
                {
                    Color pixel = matrix[x, y] ? Color.Black : Color.White;
                    for (int i = 0; i < scale; i++)
                        for (int j = 0; j < scale; j++)
                            QrCodeBitmap.SetPixel(x * scale + i, y * scale + j, pixel);
                }
            }

            string qrCodeImagePath = $"Images\\{fileName}" + ".png";
            QrCodeBitmap.Save(qrCodeImagePath, ImageFormat.Png);

            byte[] imageData = System.IO.File.ReadAllBytes(qrCodeImagePath);
            return File(imageData, "image/png", fileName + ".png");
        }

        //public static Image LoadImage (QrCodeDTO qrCodeDTO)
        //{
        //    using (var memoryStream = new MemoryStream(qrCodeDTO.Data))
        //    {
        //        return Image.FromStream(memoryStream);
        //    }

        //}

        //public static void SaveImage(QrCodeDTO imageDto, string destinationPath)
        //{
        //    var target = System.IO.File.Open(destinationPath, FileMode.Create);

        //    using (var memoryStream = new MemoryStream(imageDto.Data))
        //    using (var binaryWriter = new BinaryWriter(target))
        //    {
        //        binaryWriter.Write(memoryStream.ToArray());
        //    }


        //}

        //public void SaveImage(QrCodeDTO qrCodeDTO)
        //{
        //    // create a memory stream from the byte array
        //    using (var ms = new MemoryStream(qrCodeDTO.Data))
        //    {
        //        // create a new image from the memory stream
        //        using (var image = Image.FromStream(ms))
        //        {
        //            // save the image to the root folder of the project
        //            image.Save(qrCodeDTO.FileName, ImageFormat.Png);
        //        }
        //    }
        //}

        //[HttpPost("GenerateQrCode")]
        //public Image GenerateQrCode(string qrString)
        //{
        //    // TODO: implement POST method for generating QR-codes based in string input. 


        //    var writer = new QRCodeWriter();
        //    var matrix = writer.encode(qrString, BarcodeFormat.QR_CODE, 200, 200);
        //    int scale = 2;
        //    // Sets QR-code as Bitmap
        //    Bitmap QrCodeBitmap = new(matrix.Width * scale, matrix.Height * scale);

        //    for (int x = 0; x < matrix.Height; x++)
        //    {
        //        for (int y = 0; y < matrix.Width; y++)
        //        {
        //            Color pixel = matrix[x, y] ? Color.Black : Color.White;
        //            for (int i = 0; i < scale; i++)
        //                for (int j = 0; j < scale; j++)
        //                    QrCodeBitmap.SetPixel(x * scale + i, y * scale + j, pixel);
        //        }
        //    }

        //    // Saves Bitmap as png
        //    //QrCodeBitmap.Save("YourGeneratedQrCode.png");

        //    var ms = new MemoryStream();
        //    QrCodeBitmap.Save(ms, ImageFormat.Png);

        //    //turns Bitmap into a byte array
        //    byte[] bytes = ms.ToArray();

        //    // turns byte array into Image (png)
        //    using (var memoryStream = new MemoryStream(bytes))
        //    {
        //        // use the FromStream method to create an Image object from the byte array
        //        var image = Image.FromStream(memoryStream);

        //        // save the image as a PNG file
        //        image.Save("output.png", ImageFormat.Png);
        //        return image;
        //    }

        //}
    }
}