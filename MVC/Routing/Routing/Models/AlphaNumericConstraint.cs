using System.Text.RegularExpressions;

namespace Routing.Models
{
    // Define a class that implements the IRouteConstraint interface
    public class AlphaNumericConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // Check if the httpContext is null to avoid using a null reference later
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext)); // Throw an exception if httpContext is null
            // Check if the route is null to avoid using a null reference later
            if (route == null)
                throw new ArgumentNullException(nameof(route)); // Throw an exception if route is null
            // Check if the routeKey is null to ensure we have a valid route key to work with
            if (routeKey == null)
                throw new ArgumentNullException(nameof(routeKey)); // Throw an exception if routeKey is null
            // Check if the values dictionary is null to ensure we have route data to validate
            if (values == null)
                throw new ArgumentNullException(nameof(values)); // Throw an exception if values dictionary is null
            // Try to retrieve the value associated with routeKey from the route values dictionary
            if (values.TryGetValue(routeKey, out object? routeValue))
            {
                // Convert the route value to a string for regex matching
                var parameterValueString = Convert.ToString(routeValue);
                // Check if the parameter value string matches the required alphanumeric pattern
                // The regex pattern checks for at least one alphabet character and one numeric character
                if (Regex.IsMatch(parameterValueString ?? string.Empty, "^(?=.*[a-zA-Z])(?=.*[0-9])[A-Za-z0-9]+$"))
                {
                    return true; // Return true if the string is alphanumeric as per the pattern
                }
                else
                {
                    return false; // Return false if the string does not match the pattern
                }
            }
            return false; // Return false if the routeKey is not found in the values dictionary
        }
    }
}
