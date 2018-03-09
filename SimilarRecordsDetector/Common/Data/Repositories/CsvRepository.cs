using System.Collections.Generic;
using SimilarRecordsDetector.Common.Core.Interfaces.Repositories;

namespace SimilarRecordsDetector.Common.Data.Repositories
{
    /// <summary>
    /// The repository for csv files.
    /// </summary>
    /// <seealso cref="SimilarRecordsDetector.Common.Core.Interfaces.Repositories.ICsvRepository" />
    public class CsvRepository : ICsvRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvRepository"/> class.
        /// </summary>
        public CsvRepository()
        {
        }

        public List<string> GetHeadersAndData()
        {
            throw new System.NotImplementedException();
        }
    }
}
