using System.Collections.Generic;

namespace SimilarRecordsDetector.Common.Core.Interfaces.Business
{
    public interface IDetectorManager
    {
        Dictionary<string, int> GetSimilar(string table, string[] columns);
    }
}
