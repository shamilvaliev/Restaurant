namespace Restaurant.Api.Services.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Restaurant.Api.Models;

    /// <summary>
    /// Interface table builder
    /// </summary>
    public interface ITablesBuilder
    {
        /// <summary>
        /// Add one table by size
        /// </summary>
        /// <param name="tableSize">Table size</param>
        /// <returns>Returns self</returns>
        ITablesBuilder Add(int tableSize);

        /// <summary>
        /// Adds multiple tables by sizes
        /// </summary>
        /// <param name="tableSizes">Tables sizes</param>
        /// <returns>Returns self</returns>
        ITablesBuilder Add(IEnumerable<int> tableSizes);

        /// <summary>
        /// Build tables list
        /// </summary>
        /// <returns></returns>
        List<Table> Build();
    }
}
