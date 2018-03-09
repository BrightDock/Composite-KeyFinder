using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimilarRecordsDetector.Common.Core.Enums;
using SimilarRecordsDetector.Common.Core.Interfaces.Business;
using SimilarRecordsDetector.Common.Core.Interfaces.Repositories;
using SimilarRecordsDetector.Common.Core.Interfaces.UnitOfWork;

namespace SimilarRecordsDetector.Common.Business
{
    public class DetectorManager : ManagerBase, IDetectorManager
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IDbRepository dbRepository;

        private readonly ICsvRepository csvRepository;

        public DetectorManager(IUnitOfWork unitOfWork,
            IDbRepository dbRepository,
            ICsvRepository csvRepository)
            :base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.dbRepository = dbRepository;
            this.csvRepository = csvRepository;
        }

        public Dictionary<string, int> GetSimilar(string table, List<string> columns)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            

            return result;
        }

        public DataTable GetDataAndHeader(List<string> columns, string dataSource, DataSourceEnum dataSourceType)
        {
            DataTable table = new DataTable();

            table.Columns.AddRange(columns.Select(c => new DataColumn(c)).ToArray());

            List<DataRow> dataRows = new List<DataRow>();
            DataRow dataRow;

            switch (dataSourceType)
            {
                case DataSourceEnum.DbSource:
                    string conStr = this.dbRepository.DataContext.Database.GetDbConnection().ConnectionString;
                    string RequestStr = $"select * from {dataSource}";

                    string str = string.Empty;
                    using (var connection = new SqlConnection(conStr))
                    {
                        SqlCommand cmd = new SqlCommand(RequestStr, connection);
                        connection.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var fCount = reader.FieldCount;
                            dataRow = table.NewRow();
                            for (int i = 0; i < fCount; i++)
                            {
                                if (columns.Any(s => s.Equals(reader.GetName(i), StringComparison.OrdinalIgnoreCase)))
                                {
                                    dataRow[$"{reader.GetName(i)}"] = reader.GetValue(i);
                                    str += $"{reader.GetName(i)} {reader.GetValue(i)} ";
                                }
                            }
                            dataRows.Add(dataRow);
                            str += '\n';
                        }
                        connection.Close();
                    }
                    Console.WriteLine(str);
                    break;
                case DataSourceEnum.CsvSource:

                    break;
            }

            return table;
        }
    }
}
