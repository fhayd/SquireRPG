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
        private static readonly ConcurrentDictionary<MethodInfo, MethodInfoAdvise> ApiAddresses =
            new ConcurrentDictionary<MethodInfo, MethodInfoAdvise>();

        public static MethodInfoAdvise GetCall(MethodInfo targetMethod)
        {
            if (ApiAddresses.TryGetValue(targetMethod, out var call)) return call;
            throw new NotSupportedException("Api not supported");
        }

        public static void SetAddresses(IEnumerable<(MethodInfo, MethodInfoAdvise)> addresses)
        {
            foreach (var valueTuple in addresses)
            {
                ApiAddresses[valueTuple.Item1] = valueTuple.Item2;
            }
        }
    }
}