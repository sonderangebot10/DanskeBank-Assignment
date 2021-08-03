using System.Threading.Tasks;

namespace DanskeBank.Application.Services
{
    /// <summary>
    /// defining a service for handling of social security numbers.
    /// </summary>
    public interface ISsnService
    {
        /// <summary>
        /// checks if a given ssn is valid
        /// </summary>
        /// <returns>boolean</returns>
        Task<bool> CheckSSNAsync(string ssn);
    }
}
