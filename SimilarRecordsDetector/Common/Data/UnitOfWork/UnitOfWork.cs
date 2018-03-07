using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimilarRecordsDetector.Common.Core.Interfaces.Context;
using SimilarRecordsDetector.Common.Core.Interfaces.UnitOfWork;
using SimilarRecordsDetector.Common.Data.Context;

namespace SimilarRecordsDetector.Common.Data.UnitOfWork
{
    /// <summary>
    /// Unit Of Work.
    /// </summary>
    /// <seealso cref="SimilarRecordsDetector.Common.Core.Interfaces.UnitOfWork.IUnitOfWork" />
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The context entity.
        /// </summary>
        private readonly DbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(ISRDDbContext context)
        {
            this.context = context as DbContext;
        }

        /// <summary>
        /// Gets the data context.
        /// </summary>
        public ISRDDbContext DataContext
        {
            get
            {
                return (ISRDDbContext)this.context;
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>Gets count saved changes.</returns>
        public int Save()
        {
            int result = 0;
            result = this.context.SaveChanges();
            return result;
        }

        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <returns>Gets count saved changes.</returns>
        public async Task<int> SaveAsync()
        {
            int result = 0;
            result = await this.context.SaveChangesAsync();
            return result;
        }
    }
}
