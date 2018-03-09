using System.Collections.Generic;
using System.Data;
using SimilarRecordsDetector.Common.Core.Enums;

namespace SimilarRecordsDetector.Common.Core.Interfaces.Business
{
    public interface IDetectorManager
    {
        Dictionary<string, int> GetSimilar(string table, List<string> columns);

        DataTable GetDataAndHeader(List<string> columns, string dataSource, DataSourceEnum dataSourceType);
    }
}
