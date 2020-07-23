using System.Collections.Generic;

namespace Common.Domain.Options
{
    public class SwaggerOptions
    {

        public string Title { get; set; }

        public string Version { get; set; }

        public string Description { get; set; }

        public string UiEndpoint { get; set; }

        public string PathToXMLDocument { get; set; }

        public Dictionary<string, string> SwaggerScopes { get; set; }
    }
}
