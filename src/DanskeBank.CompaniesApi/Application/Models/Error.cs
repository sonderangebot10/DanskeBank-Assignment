
namespace DanskeBank.Application.Api.Models
{
    /// <summary>
    /// JSON:API v1 error.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Unique identifier for this particular occurrence of the problem
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The HTTP status code applicable to this problem, expressed as a string value.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Application-specific error code, expressed as a string value.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Short, human-readable summary of the problem that SHOULD NOT change from occurrence to occurrence of the problem, except for purposes of localization.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Human-readable explanation specific to this occurrence of the problem. Like title, this field’s value can be localized.
        /// </summary>
        public string Detail { get; set; }
    }
}
