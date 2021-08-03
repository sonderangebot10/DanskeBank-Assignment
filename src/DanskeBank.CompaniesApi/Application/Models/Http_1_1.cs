using System;
using System.Collections.Generic;
using System.Net;

namespace DanskeBank.Application.Api.Models
{
    /// <summary>
    /// HTTP 1/1 Json API standard.
    /// </summary>
    public static class Http_1_1
    {
        /// <summary>
        /// gets a standard HTTP/1.1 ok response body
        /// </summary>
        public static TopLevelDocument<TData> ToStdBody<TData>(this TData data, object meta = null)
        {
            return new TopLevelDocument<TData>(data, meta);
        }

        /// <summary>
        /// gets a standard HTTP/1.1 error body
        /// </summary>
        public static TopLevelError GetErr(HttpStatusCode statusCode, string title, string details)
        {
            return GetErr(statusCode, default, title, details);
        }

        /// <summary>
        /// gets a standard HTTP/1.1 error body
        /// </summary>
        public static TopLevelError GetErr(HttpStatusCode statusCode, string code, string title, string details)
        {
            return GetErr(default, statusCode, code, title, details);
        }

        /// <summary>
        /// gets a standard HTTP/1.1 error body
        /// </summary>
        public static TopLevelError GetErr(int id, HttpStatusCode statusCode, string code, string title, string details)
        {
            var e = new TopLevelError(id, statusCode, code, title, details);

            return e;
        }

        /// <summary>
        /// gets a standard HTTP/1.1 error body
        /// </summary>
        public static TopLevelError GetErr(IEnumerable<Error> errors)
        {
            return new TopLevelError(errors);
        }

        /// <summary>
        /// gets a standard HTTP/1.1 error body
        /// </summary>
        public static TopLevelError GetErr(int id, HttpStatusCode statusCode, string code, string title, Exception ex)
        {
            var e = new TopLevelError(id, statusCode, code, title, ex);

            return e;
        }
    }
}
