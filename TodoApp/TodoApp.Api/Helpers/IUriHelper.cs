using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Api.Helpers
{
    public interface IUriHelper
    {
        Uri BuildUri(HttpRequestMessage request, Guid id);
    }
}
