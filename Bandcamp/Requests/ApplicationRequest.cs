using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Bandcamp.Requests
{
    public class ApplicationRequest<R>
    {

        private readonly HttpClient _HttpClient;
        public ApplicationRequest(HttpClient _httpClient) { 
            _HttpClient = _httpClient;
        }

        public async Task<R> GetQuery(string _url ) {
            HttpResponseMessage Result = await _HttpClient.GetAsync(_url); 
            Result.EnsureSuccessStatusCode();
            string Data = await Result.Content.ReadAsStringAsync();

            R Response =  (R)Activator.CreateInstance(typeof(R));
            try { 
                Response = JsonConvert.DeserializeObject<R>(Data);
            }
            catch(Exception ex) { 
                Debug.WriteLine("Error deserialize:"+ex);
                Debug.WriteLine("Error destiny:"+_url);
                return default(R);
            }

            return Response;
        }

        public async Task<R> PostQuery(string _url, HttpContent _content)
        {
            
            HttpResponseMessage Result = await _HttpClient.PostAsync(_url,_content);
            Result.EnsureSuccessStatusCode();
            string Data = await Result.Content.ReadAsStringAsync();
            Debug.WriteLine("Data::::"+Data);
            R Response = (R)Activator.CreateInstance(typeof(R));
            try
            {
                Response = JsonConvert.DeserializeObject<R>(Data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error deserialize:" + ex);
                Debug.WriteLine("Error destiny:" + _url);
                return default(R);
            }

            return Response;
        }
    }
}
