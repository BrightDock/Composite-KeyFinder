using System.Collections.Generic;

namespace SimilarRecordsDetector.Common.Core.Dto
{
    public class SourceDataDto
    {
        public IList<string> Headers { get; set; }

        public IList<IList<string>> Data { get; set; }
    }
}
