using System;
using System.Collections.Generic;
using System.Text;
using SimilarRecordsDetector.Common.Core.Dto;
using SimilarRecordsDetector.Common.Core.Interfaces.Business;

namespace SimilarRecordsDetector
{
    public class App
    {
        private readonly IDetectorManager detectorManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="managerBase">The manager base.</param>
        public App(IDetectorManager detectorManager)
        {
            this.detectorManager = detectorManager;
        }

        public void Run(InputParamsDto inputParamsDto)
        {
            detectorManager.GetSimilar(inputParamsDto.TableName, inputParamsDto.CompareColumns);
        }
    }
}
