using System.Threading.Tasks;
using SimilarRecordsDetector.Common.Core.Interfaces.Context;

namespace SimilarRecordsDetector.Common.Core.Interfaces.UnitOfWork
{
    /// <summary>
    /// Unit Of Work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the data context.
        /// </summary>
        ISRDDbContext DataContext { get; }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>Gets count saved changes.</returns>
        int Save();

        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <returns>Gets count saved changes.</returns>
        Task<int> SaveAsync();
    }
}
