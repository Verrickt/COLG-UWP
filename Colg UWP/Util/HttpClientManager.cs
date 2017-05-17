using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace Colg_UWP.Util
{
    public static class HttpClientManager
    {
        public static HttpClient ClientForCurrentLifeCycle { get; private set; }


       


        static HttpClientManager()
        {
            ClientForCurrentLifeCycle = new HttpClient();


        }

    }
}