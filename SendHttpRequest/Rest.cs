using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SendHttpRequest
{
    class Rest
    {
        public enum ReqType
        { GET,
            POST,
            PUT,
            DELETE
        }

        public static async Task<HttpResponseMessage> ReqRest(string url, ReqType reqType, HttpContent content = null)
        {
            HttpClient clientRest = new HttpClient();
            HttpResponseMessage response = null;

            switch (reqType)
            {
                case ReqType.GET:    response = await clientRest.GetAsync(url); break;
                case ReqType.POST:   response = await clientRest.PostAsync(url, content); break;
                case ReqType.PUT:    response = await clientRest.PutAsync(url, content); break;
                case ReqType.DELETE: response = await clientRest.DeleteAsync(url); break;
            }
        
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
