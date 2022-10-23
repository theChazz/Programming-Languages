using API_Clone_1._00.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;


namespace API_Clone_1._01.Controllers
{
    // Enable Cors
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class JseIndicesController : ApiController
    {
        // BindingList
        BindingList<JseIndices> JseIndicesList = new BindingList<JseIndices>();
        List<JseIndices> list = new List<JseIndices>();
        JseIndices jseIndices;

        // GET: api/JseIndices
        public IEnumerable<string> Get()
        {
            var html = @"https://www.sharenet.co.za/v3/indices.php";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var nodes = htmlDoc.DocumentNode.SelectNodes("//table/tbody/tr");

            string[] arr = new string[400];

            int rowCount = nodes.Count; // 93 rows in table
            int colCount = 0;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < nodes[i].ChildNodes.Count; j++) // 4 columns and sometime 1 column in table
                {
                    arr[colCount] = nodes[i].ChildNodes[j].InnerText.ToString();
                    colCount++;
                }
            }
            return arr;
        }


        [HttpGet]
        [Route("api/JseIndices/GetResults")]
        public string GetResults()
        {
            var html = @"https://www.sharenet.co.za/v3/indices.php";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var nodes = htmlDoc.DocumentNode.SelectNodes("//table/tbody/tr");

            int rowCount = nodes.Count; // 93 rows in table



            for (int i = 0; i < rowCount; i++)
            {
                if (nodes[i].ChildNodes.Count == 4)
                {
                    jseIndices = new JseIndices();
                    jseIndices.Name = nodes[i].ChildNodes[0].InnerText.ToString();
                    jseIndices.RP = Convert.ToDouble(nodes[i].ChildNodes[1].InnerText.ToString());
                    jseIndices.MoveValue = Convert.ToDouble(nodes[i].ChildNodes[2].InnerText.ToString());
                    jseIndices.MovePercentage = nodes[i].ChildNodes[3].InnerText.ToString();
                    JseIndicesList.Add(jseIndices);
                }
                else if (nodes[i].ChildNodes.Count == 1)
                {
                    jseIndices = new JseIndices();
                    jseIndices.Name = nodes[i].ChildNodes[0].InnerText.ToString();
                    jseIndices.RP = 0.00;
                    jseIndices.MoveValue = 0.00;
                    jseIndices.MovePercentage = "0.00%";
                    JseIndicesList.Add(jseIndices);
                }
                else
                {
                    // do nothing
                }
            }
            return JsonConvert.SerializeObject(JseIndicesList);
        }

        [HttpGet]
        [Route("api/JseIndices/GetResult")]
        public List<JseIndices> GetResult()
        {
            var html = @"https://www.sharenet.co.za/v3/indices.php";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var nodes = htmlDoc.DocumentNode.SelectNodes("//table/tbody/tr");

            int rowCount = nodes.Count; // 93 rows in table
 

            for (int i = 0; i < rowCount; i++)
            {
                if (nodes[i].ChildNodes.Count == 4)
                {
                    jseIndices = new JseIndices();
                    jseIndices.Name = nodes[i].ChildNodes[0].InnerText.ToString();
                    jseIndices.RP = Convert.ToDouble(nodes[i].ChildNodes[1].InnerText.ToString());
                    jseIndices.MoveValue = Convert.ToDouble(nodes[i].ChildNodes[2].InnerText.ToString());
                    jseIndices.MovePercentage = nodes[i].ChildNodes[3].InnerText.ToString();
                    list.Add(jseIndices);
                }
                else if (nodes[i].ChildNodes.Count == 1)
                {
                    jseIndices = new JseIndices();
                    jseIndices.Name = nodes[i].ChildNodes[0].InnerText.ToString();
                    jseIndices.RP = 0.00;
                    jseIndices.MoveValue = 0.00;
                    jseIndices.MovePercentage = "0.00%";
                    list.Add(jseIndices);
                }
                else
                {
                    // do nothing
                }
            }
            return list;
        }
    


        // GET: api/JseIndices/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/JseIndices
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/JseIndices/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/JseIndices/5
        public void Delete(int id)
        {
        }
    }
}
