using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stock_Broker_WebApp_1._00.Models;

namespace Stock_Broker_WebApp_1._00.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            return View();
        }

        public string baseURL = "";
        public async Task<ActionResult> Index2()
    {
            DataTable dt = new DataTable();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("GetResults"); ;

                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    dt = JsonConvert.DeserializeObject<DataTable>(result);
                }
                else
                {
                    Console.WriteLine("Error in getting the stock values.");
                }
            }

            return View();
        }

    }
}