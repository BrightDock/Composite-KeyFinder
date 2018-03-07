using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimilarRecordsDetector.Common.Business;
using SimilarRecordsDetector.Common.Core.Dto;
using SimilarRecordsDetector.Common.Core.Interfaces.Business;
using SimilarRecordsDetector.Common.Core.Interfaces.Context;
using SimilarRecordsDetector.Common.Core.Interfaces.Repositories;
using SimilarRecordsDetector.Common.Core.Interfaces.UnitOfWork;
using SimilarRecordsDetector.Common.Data.Context;
using SimilarRecordsDetector.Common.Data.Repositories;
using SimilarRecordsDetector.Common.Data.UnitOfWork;
using StructureMap;

namespace SimilarRecordsDetector
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var serviceCollection = new ServiceCollection();
                var usedColumns = new string[] { };

                switch (args.GetValue(0))
                {
                    case "-db":
                        if (args.Length > 1)
                        {
                            try
                            {
                                serviceCollection
                                    .AddDbContext<SRDDbContext>(options => options.UseSqlServer(args.GetValue(1).ToString()));

                                if (args.Length > 3)
                                {
                                    var columns = args.GetValue(3).ToString();
                                    if (args.GetValue(2).ToString().StartsWith("-c"))
                                    {
                                        usedColumns = columns.Split(' ');
                                    }
                                    else
                                    {
                                        usedColumns = null;
                                    }
                                }
                                else
                                {
                                    throw new ArgumentException("Params not provided.");
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        else
                        {
                            throw new ArgumentException("No db connection string provided.");
                        }
                        break;
                    default:
                        throw new NotImplementedException("Not implemented yet.");
                }

                ConfigureServices(serviceCollection);


                var container = new Container();

                container.Configure(config =>
                {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(_ =>
                    {
                        _.AssemblyContainingType(typeof(Program));
                        _.WithDefaultConventions();
                        _.AddAllTypesOf<IManagerBase>();
                    });
                // Populate the container using the service collection
                config.Populate(serviceCollection);
                });

                var serviceProvider = container.GetInstance<IServiceProvider>();

                InputParamsDto inputParamsDto = new InputParamsDto() { CompareColumns = usedColumns };

                // entry to run app
                serviceProvider.GetService<App>().Run(inputParamsDto);
            }
            else
            {
                throw new ArgumentException("Params not provided.");
            }

        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // add services
            serviceCollection
                .AddTransient<ISRDDbContext, SRDDbContext>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddTransient<IManagerBase, ManagerBase>()
                .AddScoped<IDetectorManager, DetectorManager>()
                .AddTransient<IRepositoryBase, RepositoryBase>();

            // add app
            serviceCollection.AddTransient<App>();
        }
    }
}
