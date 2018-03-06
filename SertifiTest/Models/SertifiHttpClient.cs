using System;
using System.Net.Http;

namespace SertifiTest.Models
{
    public class SertifiHttpClient : HttpClient
    {
        // Static members are 'eagerly initialized', that is, 
        // immediately when class is loaded for the first time.
        // .NET guarantees thread safety for static initialization
        private static readonly SertifiHttpClient _instance = new SertifiHttpClient();

        private SertifiHttpClient()
        {
            //base url assign here, could also assign default headers
            this.BaseAddress = new Uri("http://apitest.sertifi.net");
        }

        public static SertifiHttpClient GetMyHttpClient()
        {
            return _instance;
        }
    }
}
