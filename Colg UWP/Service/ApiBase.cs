﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Colg_UWP.Service
{
    using Colg_UWP.Util;

    public class ApiBase
    {
        protected static async Task<string> GetPost(string uri)
        {
            return await GetPost(uri, null).ConfigureAwait(false);
        }

        protected static async Task<string> GetPost(string uri, Dictionary<string, string> content)
        {
            var client = HttpClientManager.ClientForCurrentLifeCycle;
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                Util.Logging.WriteLine($"Request {Environment.NewLine}{uri}");
                if (content == null)
                {
                    content = new Dictionary<string, string>()
                    {
                        {"Time", DateTimeOffset.Now.ToUnixTimeSeconds().ToString()}
                    };
                }
                HttpFormUrlEncodedContent _content = new HttpFormUrlEncodedContent(content);
                var response = await client.PostAsync(new Uri(uri), _content).AsTask(false, cts.Token);
                response.EnsureSuccessStatusCode();
                Util.Logging.WriteLine($"response received {Environment.NewLine}{uri}");
                return await response.Content.ReadAsStringAsync().AsTask(false);
            }
        }
    }
}