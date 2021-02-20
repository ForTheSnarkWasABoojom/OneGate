using System;
using System.Linq;
using System.Reflection;
using Flurl;
using Microsoft.AspNetCore.Mvc;

namespace OneGate.Backend.Core.Shared.Api.Client
{
    public static class SetParamsFlurlExtension
    {
        public static Url SetQueryParamsFromModel(this Url url, object atr)
        {
            var type = atr.GetType();
            var props = type.GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(FromQueryAttribute)));

            foreach (var memo in props)
            {
                var attArr = memo.GetCustomAttributes(typeof(FromQueryAttribute)).ToArray();
                var fromQueryAttribute = (FromQueryAttribute) attArr.First();
                var entityName = fromQueryAttribute.Name;
                var value = memo.GetValue(atr);
                url.SetQueryParam(entityName, value);
            }

            return url;
        }
    }
}