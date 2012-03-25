using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace Common
{
    public class CsvMediaTypeFormatter : BufferedMediaTypeFormatter
    {
        public CsvMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
            this.AddQueryStringMapping("accept", "csv", "text/csv");
        }

        protected override bool CanWriteType(Type type)
        {
            return typeof(JsonArray).IsAssignableFrom(type);
        }

        protected override bool CanReadType(Type type)
        {
            return false;
        }

        protected override void OnWriteToStream(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, FormatterContext formatterContext, System.Net.TransportContext transportContext)
        {
            var json = value as JsonArray;
            var head = json[0];

            var tw = new StreamWriter(stream);

            foreach (var p in head)
            {
                tw.Write(p.Key);
                tw.Write(", ");
            }
            tw.WriteLine();
            foreach (var line in json)
            {
                foreach (var p in line)
                {
                    tw.Write(p.Value.ReadAs<string>());
                    tw.Write(", ");
                }
                tw.WriteLine();
            }
            tw.Flush();
        }
    }
}
