﻿using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace Colg_UWP.Util
{
    public static class HttpClientManager
    {
        public static HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(new HttpProductInfoHeaderValue("Mozilla", "5.0"));
            return client;
        }
    }
}