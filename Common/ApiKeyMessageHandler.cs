using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class ApiKeyMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization == null || request.Headers.Authorization.Scheme != "ApiKey"){
                var resp = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                resp.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("ApiKey"));
                return SyncTask.From(() => resp);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}
