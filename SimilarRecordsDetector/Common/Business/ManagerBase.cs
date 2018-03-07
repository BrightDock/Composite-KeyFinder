using System;

using SimilarRecordsDetector.Common.Core.Interfaces.Business;
using SimilarRecordsDetector.Common.Core.Interfaces.UnitOfWork;

namespace SimilarRecordsDetector.Common.Business
{
    public abstract class ManagerBase : IManagerBase
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerBase"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repositoryBase">The repository base.</param>
        public ManagerBase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void PrintText(string str)
        {
            Console.WriteLine(str);
        }
    }
}
