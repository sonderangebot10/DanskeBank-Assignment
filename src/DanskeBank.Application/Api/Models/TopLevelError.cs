using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DanskeBank.Application.Api.Models
{
    public class TopLevelError
    {
        internal TopLevelError(int id, HttpStatusCode statusCode, string code, string title, string details)
        {
            Errors = new[]{ new Error
                {
                    Id = id,
                    Status = GetErrorStatus(statusCode),
                    Code = code,
                    Title = title,
                    Detail = details
                }
            };
        }

        internal TopLevelError(int id, HttpStatusCode statusCode, string code, string title, Exception details)
        {
            Errors = new[]{ new Error
                {
                    Id = id,
                    Status = GetErrorStatus(statusCode),
                    Code = code,
                    Title = title,
                    Detail = details != null ? $"[{details.GetType()}] {details.Message}": null
                }
            };
        }

        internal TopLevelError(IEnumerable<Error> errors)
            : this(errors, null)
        {
        }

        internal TopLevelError(IEnumerable<Error> errors, object meta)
        {
            Errors = errors.ToArray();
            Meta = meta;
        }
        public Error[] Errors { get; }

        public object Meta { get; }

        private static string GetErrorStatus(HttpStatusCode statusCode)
        {
            const string format = "{0} {1}";
            switch (statusCode)
            {
                case HttpStatusCode.BadGateway:
                    return string.Format(format, statusCode.ToString("D"), "Bad Gateway");
                case HttpStatusCode.BadRequest:
                    return string.Format(format, statusCode.ToString("D"), "Bad Request");
                case HttpStatusCode.Conflict:
                    return string.Format(format, statusCode.ToString("D"), "Conflict");
                case HttpStatusCode.Forbidden:
                    return string.Format(format, statusCode.ToString("D"), "Forbidden");
                case HttpStatusCode.GatewayTimeout:
                    return string.Format(format, statusCode.ToString("D"), "Gateway Timeout");
                case HttpStatusCode.InternalServerError:
                    return string.Format(format, statusCode.ToString("D"), "Internal Server Error");
                case HttpStatusCode.NotFound:
                    return string.Format(format, statusCode.ToString("D"), "Not Found");
                case HttpStatusCode.NotImplemented:
                    return string.Format(format, statusCode.ToString("D"), "Not Implemented");
                case HttpStatusCode.RequestTimeout:
                    return string.Format(format, statusCode.ToString("D"), "Request Timeout");
                case HttpStatusCode.Unauthorized:
                    return string.Format(format, statusCode.ToString("D"), "Unauthorized");
                default:
                    return string.Format(format, statusCode.ToString("D"), statusCode.ToString("G")); ;
            }
        }
    }
}
