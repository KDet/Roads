using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using LvivRoads.Core.Extensions;

namespace LvivRoads.Core.Services.Internal.Http
{
    public class HtmlPageLoaderService : IHtmlPageLoaderService
    {
        public event EventHandler<PageLoaderErrorEventArgs> PageLoaderError;
        protected virtual void OnPageLoaderError(PageLoaderErrorEventArgs e)
        {
            var handler = PageLoaderError;
            if (handler != null) handler(this, e);
        }
        public string HtmlContent { get; private set; }
        public async Task<string> LoadAsync(string url)
        {
            var htmlContent = string.Empty;
            await MakeRequest(url, content => htmlContent = content,
                exception => OnPageLoaderError(new PageLoaderErrorEventArgs(exception, url)));
            HtmlContent = htmlContent;
            return htmlContent;
        }
        public static async Task MakeRequest(string requestUrl, Action<string> successAction, Action<Exception> errorAction)
        {
            HttpWebRequest request = null;
            try
            {
                request = (HttpWebRequest) WebRequest.Create(requestUrl);
                WebResponse response = await request.GetResponseAsync();
                using (var stream = response.GetResponseStream())
                {
                    var reader = new StreamReader(stream);
                    successAction(reader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                if (request != null)
                    Mvx.Error("ERROR: '{0}' when making {1} request to {2}", ex.Message, request.Method,
                        request.RequestUri.AbsoluteUri);
                else
                    Mvx.Error("ERROR: '{0}'", ex.Message);
                errorAction(ex);
            }
        }
    }

    public class PageLoaderErrorEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }
        public string Url { get; private set; }
        public PageLoaderErrorEventArgs(Exception exception, string url)
        {
            Exception = exception;
            Url = url;
        }
    }
}