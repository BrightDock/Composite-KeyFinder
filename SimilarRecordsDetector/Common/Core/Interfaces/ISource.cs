using System.Collections.Generic;
using System.Data;

namespace SimilarRecordsDetector.Common.Core.Interfaces
{
    public interface ISource
    {
        string Name { get; }

        List<string> SourceData { get; set; }

        List<string> Columns { get; set; }

        List<DataTable> GetDataTables();
    }
}
