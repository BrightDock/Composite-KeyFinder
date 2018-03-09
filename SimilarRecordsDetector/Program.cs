using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimilarRecordsDetector.Common.Core.Enums;
using SimilarRecordsDetector.Common.Core.Interfaces;
using SimilarRecordsDetector.Common.Core.Interfaces.Business;
using SimilarRecordsDetector.Common.Core.Interfaces.Context;
using SimilarRecordsDetector.Common.Core.Interfaces.Repositories;
using SimilarRecordsDetector.Common.Core.Interfaces.UnitOfWork;
using SimilarRecordsDetector.Common.Data;
using SimilarRecordsDetector.Common.Data.Context;
using SimilarRecordsDetector.Common.Data.UnitOfWork;

using StructureMap;

namespace SimilarRecordsDetector
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("Params not provided.");
            }

            var serviceCollection = new ServiceCollection();
            var container = new Container();
            Dictionary<string, List<string>> parameters = GetParameters(args);
            List<ISource> source = new List<ISource>(); 
            var usedColumns = new List<string>();

            container.Configure(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(Program));
                    _.WithDefaultConventions();
                    _.AddAllTypesOf<IManagerBase>();
                    _.AddAllTypesOf<IRepositoryBase>();
                    _.SingleImplementationsOfInterface();
                });
                ConfigureServices(serviceCollection);
                // Populate the container using the service collection
                config.Populate(serviceCollection);
            });

            IServiceProvider serviceProvider = null;

            foreach (var parameter in parameters)
            {
                switch (parameter.Key)
                {
                    case "-db":
                        if (parameter.Value.Count == 0)
                        {
                            throw new ArgumentNullException($"No values provided for \"{parameter.Key}\" parameter.");
                        }

                        serviceCollection
                            .AddDbContext<SRDDbContext>(options => options.UseSqlServer(parameter.Value[0]));
                        ConfigureServices(serviceCollection);
                        container.Populate(serviceCollection);
                        serviceProvider = container.GetInstance<IServiceProvider>();

                        source.Add(serviceProvider.GetService<DbSource>());
                        break;
                    case "-csv":
                        if (parameter.Value.Count == 0)
                        {
                            throw new ArgumentNullException($"No values provided for \"{parameter.Key}\" parameter.");
                        }

                        serviceProvider = container.GetInstance<IServiceProvider>();

                        source.Add(serviceProvider.GetService<CsvSource>());

                        var src = source.FindLast(s => s.Name == DataSourceEnum.CsvSource.ToString());
                        src.SourceData = parameter.Value;
                        break;
                    case "-c":
                        usedColumns.AddRange(parameter.Value);
                        break;
                    case "-dc":
                        src = source.FindLast(s => s.Name == DataSourceEnum.DbSource.ToString());
                        if (src == null)
                        {
                            break;
                        }
                        if (src.Columns == null)
                        {
                            src.Columns = parameter.Value;
                        }
                        else
                        {
                            src.Columns.AddRange(parameter.Value);
                        }
                        break;
                    case "-fc":
                        src = source.FindLast(s => s.Name == DataSourceEnum.CsvSource.ToString());
                        if (src == null)
                        {
                            break;
                        }
                        if (src.Columns == null)
                        {
                            src.Columns = parameter.Value;
                        }
                        else
                        {
                            src.Columns.AddRange(parameter.Value);
                        }
                        break;
                    case "-t":
                        src = source.FindLast(s => s.Name == DataSourceEnum.DbSource.ToString());
                        if (src == null)
                        {
                            break;
                        }
                        if (src.SourceData == null)
                        {
                            src.SourceData = parameter.Value;
                        }
                        else
                        {
                            src.SourceData.AddRange(parameter.Value);
                        }
                        break;
                    case "-f":
                        src = source.FindLast(s => s.Name == DataSourceEnum.CsvSource.ToString());
                        if (src == null)
                        {
                            break;
                        }
                        if (src.SourceData == null)
                        {
                            src.SourceData = parameter.Value;
                        }
                        else
                        {
                            src.SourceData.AddRange(parameter.Value);
                        }
                        break;
                        
                }
            }

            if (source.Count == 0)
            {
                throw new ArgumentException("Insufficient or incorrect parameters have been added.");
            }

            source.ForEach(s => s.Columns = s.Columns == null ? usedColumns : s.Columns.Concat(usedColumns).ToList());

            var brokenSources = source.Where(s => (s.Columns == null || s.Columns.Count == 0) 
                || (s.SourceData == null || s.SourceData.Count() == 0));
            if (brokenSources.Count() > 0)
            {
                Console.WriteLine(@"Some sources has no columns for comparisons or datasource provided.\n
                                    This sources will be excluded from comparison.\n");

                brokenSources.ToList().ForEach(s => source.Remove(s));
            }
            
            ConfigureServices(serviceCollection);

            serviceProvider = container.GetInstance<IServiceProvider>();
            
            /*
             Start processing.
             */
            serviceProvider.GetService<App>().Run(source);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Add services.
            serviceCollection
                .AddTransient<ISRDDbContext, SRDDbContext>()
                .AddScoped<IUnitOfWork, UnitOfWork>();

            // Add app entities.
            serviceCollection.AddTransient<App>();
            serviceCollection.AddTransient<DbSource>();
            serviceCollection.AddTransient<CsvSource>();
        }

        private static Dictionary<string, List<string>> GetParameters(string[] args)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            string key = string.Empty;

            foreach (var param in args)
            {

                if (!string.IsNullOrEmpty(key) && !param.StartsWith("-"))
                {
                    if (result.ContainsKey(key))
                    {
                        result[key].Add(param);
                    }
                    else
                    {
                        result.Add(key, new List<string>() { param });
                    }
                }

                if(param.StartsWith("-"))
                {
                    if (!string.IsNullOrEmpty(key) && result.GetValueOrDefault(key).Count == 0)
                    {
                        result.Add(key, new List<string>());
                    }

                    key = param;
                }
            }

            return result;
        }
    }
}
