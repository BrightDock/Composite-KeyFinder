using System;
using System.Collections.Generic;
using System.Text;

namespace SimilarRecordsDetector.Common.Core.Dto
{
    public class InputParamsDto
    {
        public string TableName { get; set; }

        public string[] CompareColumns { get; set; }
    }
}
