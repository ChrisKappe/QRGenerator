using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace QRFunction
{
    public static class Function1
    {
        [FunctionName("GenerateQR")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string data = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "data", true) == 0)
                .Value;

            if (data == null)
                data = await req.Content.ReadAsStringAsync();

            if (data == null)
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass the QR code data on query string or in the request body");

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(QRGenerator.QRGenerator.GetQRCode(data));
            result.Content.Headers.Add("Content-Type", "application/octet-stream");
            return result;
        }
    }
}
