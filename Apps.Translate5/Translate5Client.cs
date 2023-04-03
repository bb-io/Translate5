using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5
{
    public class Translate5Client : RestClient
    {
        public Translate5Client(string url) : base(new RestClientOptions() { ThrowOnAnyError = true, BaseUrl = new Uri(url) }) { }
    }
}
