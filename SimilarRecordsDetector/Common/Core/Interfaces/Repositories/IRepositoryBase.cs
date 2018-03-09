using System.Collections.Generic;

namespace SimilarRecordsDetector.Common.Core.Interfaces.Repositories
{
    public interface IRepositoryBase
    {
        List<string> GetHeadersAndData();
    }
}
