// UrlFriendlyGuidRouteConstraint.cs
using System.Buffers.Text; // for the XML comment

namespace Api.SharedKernel.UrlFriendlyGuid;

/// <summary>
/// Matches a <see cref="Base64Url"/> encoded string to a <see cref="UrlFriendlyGuid"/>
/// </summary>
public class UrlFriendlyGuidRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        if (!values.TryGetValue(routeKey, out object? value))
        {
            return false;
        }

        return value switch
        {
            UrlFriendlyGuid => true,
            string str => UrlFriendlyGuid.TryParse(str, null, out _),
            _ => false
        };
    }

}
