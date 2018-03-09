using SimilarRecordsDetector.Common.Data.Context;

namespace SimilarRecordsDetector.Common.Core.Interfaces.Repositories
{
    public interface IDbRepository : IRepositoryBase
    {
        SRDDbContext DataContext { get; }
    }
}
