using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MernisService;
using BirlesikOdeme.Models;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;

namespace BirlesikOdeme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Context _context;

        public ValuesController(Context context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Customer>> Register(Customer customer)
        {
            //POSTMAN tarafında customer sınıfına uygun olarak verileri body tarafına yazdıktan sonra ekleme işlemini yapıp sonrasında
            //tckimlik doğrulama servisinde ilgili tcyi kontrol edip verified kolonunu true ise 1 değilse 0 yapıyor.


            _context.Customers.Add(customer);
            CheckIdentityNoVerified(customer);
            await _context.SaveChangesAsync();
            return customer;

        }

        private async void CheckIdentityNoVerified(Customer customer)
        {
            var client = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            var response = await client.TCKimlikNoDogrulaAsync(Convert.ToInt64(customer.IdentityNo), customer.Name, customer.Surname, customer.BirthDate.Year);
            var result = response.Body.TCKimlikNoDogrulaResult;
            if (result) customer.IdentityNoVerified = 1;
            else customer.IdentityNoVerified = 0;
            _context.Customers.Update(customer);


        }


        [HttpPost("login")]
        public async Task<ActionResult<ResponseAuthentication>> Login(LoginDTO loginDTO)
        {
            //POSTMAN tarafında worddeki örneği body kısmında yolladığımız zaman endpointe gönderilen istek doğrultusunda tokenı elde ediyoruz.
            //Örnek body for postman : 
            //            {
            //                "Password": "kU8@iP3@",
            //    "Email": "murat.karayilan@dotto.com.tr",
            //    "Lang": "TR",
            //    "ApiKey" : "SKI0NDHEUP60J7QVCFATP9TJFT2OQFSO",
            //    "MerchantId" : "1894",
            //    "MemberId"  : 1
            //}



            HttpClient client = new HttpClient();
            var endPoint = new Uri("https://ppgsecurity-test.birlesikodeme.com:55002/api/ppg/Securities/authenticationMerchant");
            var newPostJson = JsonConvert.SerializeObject(loginDTO);
            var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
            var jsonResult = client.PostAsync(endPoint, payload).Result.Content.ReadAsStringAsync().Result;

            //responseu deserialize ediyoruz.
            var data = JsonConvert.DeserializeObject<ResponseAuthentication>(jsonResult);

            var token = data.result.token;
            //Tokenı aldıktan sonra sessiona kaydediyoruz sonra payzee payment servisinde tokenı kullanıyoruz.
            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Session.SetString("ApiKey", loginDTO.ApiKey);
            return data;

        }
        [HttpPost("payment")]
        public async Task<ActionResult<ResponsePaymentDTO>> Payment(PaymentDTO paymentDTO)
        {
            HttpClient clientPayment = new HttpClient();
            //payment işlemimizi hash fonksiyonu ile encrypteın işlemini yapıyoruz.
            paymentDTO.hash = PaymentHash(paymentDTO);
            //İlgili tokenımızı sessiondan çekip payment apimizin headerına setliyoruz.
            var accessToken = HttpContext.Session.GetString("JWToken");
            clientPayment.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var endPointPayment = "https://ppgpayment-test.birlesikodeme.com:20000/api/ppg/Payment/Payment3d";

            var newPostJsonPayment = JsonConvert.SerializeObject(paymentDTO);
            var payloadPayment = new StringContent(newPostJsonPayment, Encoding.UTF8, "application/json");
            var jsonResultPayment = clientPayment.PostAsync(endPointPayment, payloadPayment).Result.Content.ReadAsStringAsync().Result;

            //api return value
            var dataPayment = JsonConvert.DeserializeObject<ResponsePaymentDTO>(jsonResultPayment);
            if (dataPayment != null)
            {
                //dönen işlem sonucunda payment işleminin logunu dbde tutuyoruz.
                LogSell(JsonConvert.DeserializeObject<PaymentLog>(jsonResultPayment));
            }
            return dataPayment;
        }

        private async void LogSell(PaymentLog dataPayment)
        {
            _context.PaymentLogs.Add(dataPayment);
            await _context.SaveChangesAsync();
        }

        private string PaymentHash(PaymentDTO request)
        {
            var apiKey = HttpContext.Session.GetString("ApiKey");
            var hashString = $"{apiKey}{request.userCode}{request.rnd}{request.txnType}{request.totalAmount}{request.customerId}{request.orderId}{request.okUrl}{request.failUrl}";
            var s512 = SHA512.Create();
            var byteConverter = new UnicodeEncoding();
            var bytes = s512.ComputeHash(byteConverter.GetBytes(hashString));
            var hash = BitConverter.ToString(bytes).Replace("-", "");
            return hash;
        }


    }
}
