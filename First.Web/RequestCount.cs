using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace First.Web
{

    public class Counter
    {
        private long _counter;
        public Counter()
        {
            _counter = 0;
        }

        public void Increment()
        {
            Interlocked.Increment(ref _counter);
        }

        public long GetValue()
        {
            return Interlocked.Read(ref _counter);
        }

        public void Reset()
        {
            Interlocked.Exchange(ref _counter, 0);
        }
    }

    public class RequestCountHandler : DelegatingHandler
    {
        private readonly Counter _counter;

        public RequestCountHandler(Counter counter)
        {
            _counter = counter;
        }

        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _counter.Increment();
            return base.SendAsync(request, cancellationToken);
        }
    }

    public class CounterController : ApiController
    {
        private readonly Counter _counter;

        public CounterController(Counter counter)
        {
            _counter = counter;
        }

        public long Get()
        {
            return _counter.GetValue();
        }

        public void Post()
        {
            _counter.Reset();
        }
    }
}