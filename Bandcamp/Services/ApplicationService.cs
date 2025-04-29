using Bandcamp.Models;
using Bandcamp.Stores;
using Bandcamp.Requests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;

namespace Bandcamp.Services
{
    public class ApplicationService
    {
        private string _UrlBase = "https://bandcamp.com/api/";
        
        private GlobalStore _GlobalStore;
        private StreamStore _StreamStore;
        public ApplicationService(GlobalStore globalStore, StreamStore streamstore ) {
            _GlobalStore = globalStore;
            _StreamStore = streamstore;
        }

        async public Task<ResponseIndexDiscover> IndexDiscover(Action<ResponseIndexDiscover> eventStore, string _cursor, List<string> tagnames, int _size = 60) {

            Debug.WriteLine("_________________________________ index discover:::::::::::::::::");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_UrlBase);
            string _url = "discover/1/discover_web";
            HttpContent _content = JsonContent.Create(new { category_id = 0, cursor = _cursor, geoname_id = 0, include_result_types = new string[] { "a", "s" }, size = _size, slice = "top", tag_norm_names = tagnames, time_facet_id = (object?)null });
            ApplicationRequest<ResponseIndexDiscover> applicationRequest = new ApplicationRequest<ResponseIndexDiscover>(client);
            ResponseIndexDiscover result = await applicationRequest.PostQuery(_url,_content);

            if (result != null)
            {
                 eventStore(result);
            }
            
            return result;
        }


        public async Task<long> GetTotalFileSizeStream(string url) { 
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            request.Headers.Range = new RangeHeaderValue(0,0);
            Debug.WriteLine("Intentando obtener los header del archivo");

            int intent = 0;
            
            async Task<long> IntentQuery() { 

                long _totalFileSize = 0;
                
                Debug.WriteLine("Intento de obtener header #:"+intent);
                
                intent++;
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode && response.Content.Headers.ContentRange != null) {
                    _totalFileSize = response.Content.Headers.ContentRange.Length ?? 0;
                }

                if (_totalFileSize == 0 && intent <= 3)
                {
                    await Task.Delay(intent*1000);
                    _totalFileSize = await IntentQuery();
                }

                return _totalFileSize;
            }

            long totalFileSize = await IntentQuery();
            Debug.WriteLine("respuesta seteada en lengthStream:" + totalFileSize);
            _StreamStore.SetLengthStream(totalFileSize);

            return totalFileSize;
        }

        public async Task DownloadNextChunk(string url, long startbyte, long chunksize) {
            
            long endByte = startbyte+chunksize;
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            request.Headers.Range = new RangeHeaderValue(startbyte,endByte);

            HttpResponseMessage Result = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            if (Result.IsSuccessStatusCode && Result.StatusCode == HttpStatusCode.PartialContent)
            {
                Debug.WriteLine("descargar stream start:"+startbyte);
                Debug.WriteLine("descargar stream end:"+endByte);
                await Result.Content.CopyToAsync(_StreamStore.AudioStreamContainer);
                _StreamStore.NotifyStateChangeStream();
            }
            else {
                Debug.WriteLine("Stream completo");
            }

        }
    }
}
