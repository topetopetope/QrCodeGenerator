using HIOF.BachelorTesting.QrCodeReceiver.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace HIOF.BachelorTesting.QrCodeReceiver.Controllers
{
    [ApiController]
    [Route("api/QrCodeReceiver")]
    public class QrCodeReceiverController : ControllerBase
    {

        private readonly ILogger<QrCodeReceiverController> _logger;

        public QrCodeReceiverController(ILogger<QrCodeReceiverController> logger)
        {
            _logger = logger;
        }

        [HttpGet("ReceiveQrCode")]
        public async Task<IActionResult> GetQrCode(string targetUrl, string fileName)
        {
            using var client = new HttpClient();

            var requestUrl = $"https://localhost:7188/api/QrCodeGenerator/GenerateQrCode?targetUrl={targetUrl}&fileName={fileName}";

            HttpResponseMessage response = await client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            //string responseBody = await response.Content.ReadAsStringAsync();
            //QrCodeReceived? qrCodeReceived = JsonConvert.DeserializeObject<QrCodeReceived>(responseBody);

            //string qrCodeImagePath = $"Images\\{fileName}" + ".png";

            //// create a memory stream from the byte array
            //using (var qrCodeByteArrayStream = new MemoryStream(qrCodeReceived.Data))
            //{
            //    // create a new image from the memory stream
            //    using (var image = Image.FromStream(qrCodeByteArrayStream))
            //    {
            //        // save the image to the root folder of the project
            //        image.Save(qrCodeImagePath, ImageFormat.Png);
            //    }


            //    return qrCodeReceived;

            // get the image data from the response
            var imageBytes = await response.Content.ReadAsByteArrayAsync();

            // create a FileContentResult with the image data and the appropriate content type
            var fileResult = new FileContentResult(imageBytes, "image/png")
            {
                FileDownloadName = $"{fileName}.png"
            };

            return fileResult;
        }

    }
}