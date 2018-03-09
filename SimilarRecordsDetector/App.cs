using System;
using System.Collections.Generic;
using System.Data;
using SimilarRecordsDetector.Common.Core.Interfaces;

namespace SimilarRecordsDetector
{
    public class App
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="managerBase">The manager base.</param>
        public App()
        {
        }

        public void Run(List<ISource> sources)
        {
            if (sources == null || sources.Count == 0)
            {
                throw new ArgumentException("No Sources provided.");
            }


            List<DataTable> data = new List<DataTable>();

            foreach (var source in sources)
            {
                data.AddRange(source.GetDataTables());
            }

            //IList<IDictionary<string, int>> similaritiesTable = source.GetSimilaritiesTable();
        }
    }
}
