using System;
using System.Collections.Generic;
using System.Data;
using SimilarRecordsDetector.Common.Core.Enums;
using SimilarRecordsDetector.Common.Core.Interfaces;
using SimilarRecordsDetector.Common.Core.Interfaces.Business;

namespace SimilarRecordsDetector.Common.Data
{
    /// <summary>
    /// The implementation of ISource for Csv files.
    /// </summary>
    /// <seealso cref="SimilarRecordsDetector.Common.Core.Interfaces.ISource" />
    public class CsvSource : ISource
    {
        /// <summary>
        /// Gets the source name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; } = DataSourceEnum.CsvSource.ToString();

        /// <summary>
        /// Gets or sets the source data.
        /// </summary>
        /// <value>
        /// The source data.
        /// </value>
        public List<string> SourceData { get; set; }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        public List<string> Columns { get; set; }

        /// <summary>
        /// The detector manager
        /// </summary>
        private readonly IDetectorManager detectorManager;

        /// <summary>
        /// The manager base
        /// </summary>
        private readonly IManagerBase managerBase;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvSource"/> class.
        /// </summary>
        /// <param name="detectorManager">The detector manager.</param>
        /// <param name="managerBase">The manager base.</param>
        public CsvSource(IDetectorManager detectorManager,
            IManagerBase managerBase)
        {
            this.detectorManager = detectorManager;
            this.managerBase = managerBase;
        }

        public IList<IDictionary<string, int>> GetSimilaritiesTable()
        {
            IList<IDictionary<string, int>> data = new List<IDictionary<string, int>>();

            foreach (var table in this.SourceData)
            {
                data.Add(detectorManager.GetSimilar(table, this.Columns));
            }

            return data;
        }

        public List<DataTable> GetDataTables()
        {
            List<DataTable> dataTables = new List<DataTable>();

            foreach (var table in this.SourceData)
            {
                dataTables
                    .Add(this.detectorManager.GetDataAndHeader(this.Columns, table, (DataSourceEnum)Enum.Parse(typeof(DataSourceEnum), this.Name)));
            }

            return dataTables;
        }
    }
}
