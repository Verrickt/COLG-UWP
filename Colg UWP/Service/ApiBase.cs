using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;

namespace Colg_UWP.Service
{
    using Colg_UWP.Helper;

    public class ApiBase
    {
        public static async Task<string> GetPost(string uri)
        {
            return await GetPost(uri, null).ConfigureAwait(false);
        }

        public static async Task<string> GetPost(string uri, Dictionary<string, string> content)
        {
            using (HttpClient client = HttpClientManager.CreateClient())
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                Debug.WriteLine($"Requesting Page: {uri}");
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
                Debug.WriteLine($"Response received");
                return await response.Content.ReadAsStringAsync().AsTask(false);
            }
        }
    }
}
