using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SimilarRecordsDetector.Common.Core.Interfaces.Business;
using SimilarRecordsDetector.Common.Core.Interfaces.Repositories;
using SimilarRecordsDetector.Common.Core.Interfaces.UnitOfWork;

namespace SimilarRecordsDetector.Common.Business
{
    public class DetectorManager : ManagerBase, IDetectorManager
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IRepositoryBase repositoryBase;

        public DetectorManager(IUnitOfWork unitOfWork,
            IRepositoryBase repositoryBase)
            :base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repositoryBase = repositoryBase;
        }

        public Dictionary<string, int> GetSimilar(string table, string[] columns)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            string conStr = this.repositoryBase.DataContext.Database.GetDbConnection().ConnectionString;
            string RequestStr = $"select {(columns != null ? string.Join(", ", columns) : "*")} from categories";

            string str = string.Empty;
            using (var connection = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(RequestStr, connection);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var fCount = reader.FieldCount;
                    for (int i = 0; i < fCount; i++)
                    {
                        str += $"{reader.GetName(i)} {reader.GetValue(i)} ";
                    }
                    str += '\n';
                }
                connection.Close();
            }
            Console.WriteLine(str);

            return result;
        }
    }
}
