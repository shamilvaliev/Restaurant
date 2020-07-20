namespace Restaurant.Api.Common.Exceptions
{
    using System;

    public class RestaurantException : Exception
    {
        public RestaurantException(string message):base(message)
        {
        }
    }
}
