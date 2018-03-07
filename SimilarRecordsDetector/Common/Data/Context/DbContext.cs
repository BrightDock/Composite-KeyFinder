using Microsoft.EntityFrameworkCore;

using SimilarRecordsDetector.Common.Core.Interfaces.Context;

namespace SimilarRecordsDetector.Common.Data.Context
{
    /// <summary>
    /// The application database context.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// <seealso cref="SimilarRecordsDetector.Common.Core.Interfaces.Context.ISRDDbContext" />
    public class SRDDbContext : DbContext, ISRDDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SRDDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public SRDDbContext(DbContextOptions<SRDDbContext> options)
            : base(options)
        { }
    }
}
