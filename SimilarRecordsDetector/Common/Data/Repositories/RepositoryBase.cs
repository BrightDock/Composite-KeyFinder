using SimilarRecordsDetector.Common.Core.Interfaces.Repositories;
using SimilarRecordsDetector.Common.Core.Interfaces.UnitOfWork;
using SimilarRecordsDetector.Common.Data.Context;

namespace SimilarRecordsDetector.Common.Data.Repositories
{
    public class RepositoryBase : IRepositoryBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase"/> class.
        /// </summary>
        /// <param name="unitOfwork">The unit ofwork.</param>
        public RepositoryBase(IUnitOfWork unitOfwork)
        {
            this.DataContext = unitOfwork.DataContext as SRDDbContext;
        }

        /// <summary>
        /// Gets the data context.
        /// </summary>        
        public SRDDbContext DataContext
        {
            get;
            private set;
        }
    }
}
