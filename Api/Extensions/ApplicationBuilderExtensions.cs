using Microsoft.AspNetCore.Builder;
using System;

namespace StarWarsAPI.Extensions
{
    /// <summary>
    /// class ApplicationBuilderExtensions
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// IApplicationBuilder extensions
        /// </summary>
        /// <param name="app"></param>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseIf(this IApplicationBuilder app, bool condition, Func<IApplicationBuilder, IApplicationBuilder> action)
        {
            return condition ? action(app) : app;
        }
    }
}
