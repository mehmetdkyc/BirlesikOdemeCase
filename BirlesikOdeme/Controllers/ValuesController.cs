using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MernisService;
using BirlesikOdeme.Models;
using Newtonsoft.Json;
using System.Text;

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
        public async void Login(LoginDTO loginDTO)
        {
            //POSTMAN tarafında worddeki örneği body kısmında yolladığımız zaman endpointe gönderilen istek doğrultusunda tokenı elde ediyoruz.
            HttpClient client = new HttpClient();
            var endPoint = new Uri("https://ppgsecurity-test.birlesikodeme.com:55002/api/ppg/Securities/authenticationMerchant");
            var newPostJson = JsonConvert.SerializeObject(loginDTO);
            var payload= new StringContent(newPostJson, Encoding.UTF8, "application/json");
            var jsonResult = client.PostAsync(endPoint, payload).Result.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<ResponseAuthentication>(jsonResult);

            var token = data.result.token;
            //Tokenı aldıktan sonra payzee payment servisini çağırıyoruz.


        }
    }
}
