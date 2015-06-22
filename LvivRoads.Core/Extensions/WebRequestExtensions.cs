using System;
using System.Net;
using System.Threading.Tasks;

namespace LvivRoads.Core.Extensions
{
    public static class WebRequestExtensions
    {
        public static Task<WebResponse> GetResponseAsync(this WebRequest request, TimeSpan timeout)
        {
            return Task.Factory.StartNew(() =>
            {
                var t = Task.Factory.FromAsync<WebResponse>(
                    request.BeginGetResponse,
                    request.EndGetResponse,
                    null);

                if (!t.Wait(timeout)) throw new TimeoutException();

                return t.Result;
            });
        }
        public static Task<WebResponse> GetResponseAsync(this WebRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                var t = Task.Factory.FromAsync<WebResponse>(
                    request.BeginGetResponse,
                    request.EndGetResponse,
                    null);

                return t.Result;
            });
        }
    }
}