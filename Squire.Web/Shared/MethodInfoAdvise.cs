using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace Squire.Web.Shared
{
    public class MethodInfoAdvise
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string DeclaringType { get; set; }
        public ICollection<Parameter> Parameters { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public string Address { get; set; }
    }

    public class Parameter
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }
}