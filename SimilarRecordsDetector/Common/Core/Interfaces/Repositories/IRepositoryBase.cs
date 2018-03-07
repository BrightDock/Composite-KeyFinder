using SimilarRecordsDetector.Common.Data.Context;

namespace SimilarRecordsDetector.Common.Core.Interfaces.Repositories
{
    public interface IRepositoryBase
    {
        SRDDbContext DataContext { get; }
    }
}
