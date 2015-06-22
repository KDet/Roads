using System;
using System.Threading.Tasks;

namespace LvivRoads.Core.Services.Internal.Http
{
    public interface IHtmlPageLoaderService
    {
        Task<string> LoadAsync(string url);
        string HtmlContent { get; }
        event EventHandler<PageLoaderErrorEventArgs> PageLoaderError;
    }
}