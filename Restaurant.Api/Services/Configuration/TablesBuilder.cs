using Restaurant.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Services.Configuration
{
    /// <summary>
    /// Simple table builder
    /// </summary>
    public class TablesBuilder : ITablesBuilder
    {
        private LinkedList<int> tablesSizes;

        /// <summary>
        /// ctor
        /// </summary>
        public TablesBuilder()
        {
            this.tablesSizes = new LinkedList<int>();
        }

        /// <summary>
        /// Add one table by size
        /// </summary>
        /// <param name="tableSize">Table size</param>
        /// <returns>Returns self</returns>
        public ITablesBuilder Add(int tableSize)
        {
            if (tableSize <= 0)
            {
                throw new ArgumentException("Table size cannot be less then 1", nameof(tableSize));
            }

            this.tablesSizes.AddLast(tableSize);
            return this;
        }

        /// <summary>
        /// Adds multiple tables by sizes
        /// </summary>
        /// <param name="tableSizes">Tables sizes</param>
        /// <returns>Returns self</returns>
        public ITablesBuilder Add(IEnumerable<int> tableSizes)
        {
            foreach (var tableSize in tableSizes)
            {
                this.Add(tableSize);
            }

            return this;
        }

        /// <summary>
        /// Build tables list
        /// </summary>
        /// <returns></returns>
        public List<Table> Build()
        {
            return this.tablesSizes.Select(tableSize => new Table(tableSize)).ToList();
        }
    }
}
