using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;

namespace Squire.Web.Shared.API
{
    public class ApiHelper
    {
        private static readonly ConcurrentDictionary<string, MethodInfoAdvise> ApiAddresses =
            new ConcurrentDictionary<string, MethodInfoAdvise>();

        public static MethodInfoAdvise GetCall(MethodInfo targetMethod)
        {
            if (ApiAddresses.TryGetValue(targetMethod.DeclaringType + targetMethod.Name, out var call)) return call;
            throw new NotSupportedException("Api not supported");
        }

        public static void SetAddresses(IEnumerable<MethodInfoAdvise> methods)
        {
            foreach (var method in methods)
            {
                ApiAddresses[method.Type + method.Name] = method;
            }
        }
    }
}