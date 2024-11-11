using Microsoft.AspNetCore.Mvc;
using Oasis.Communication.Business;

namespace Oasis.Communication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfGenerationController : ControllerBase
    {
        public PdfGenerationController() { }

        [HttpPost]
        [Route("GeneratePdf")]
        public async void GeneratePdf([FromBody] PdfGenerationData pdfGenerationData)
        {
            string localFunctionUrl = "http://localhost:7129/api/GeneratePdf";
            string azureFunctionMainUrl = "https://oasiscommunicationgeneratepdf.azurewebsites.net/api/GeneratePdf?code=gBKSYuylr1hIul4c1ziOnu4RjO2tlGXmwqWL1QsYRJJVAzFuTDOBsQ==";

            try
            {
                using var httpClient = new HttpClient();
                var r = await httpClient.PostAsJsonAsync(localFunctionUrl, new PdfGenerationData()
                {
                    FileName = "File" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt",
                    Title = "Order Detail",
                    DeviceName = "Iphone 13 midnight blue 128GB",
                    DeviceValue = 55000.00m
                });
                var c = await r.Content.ReadAsStringAsync();
            }
            catch(Exception ex) 
            { 
            }
        }
    }
}
