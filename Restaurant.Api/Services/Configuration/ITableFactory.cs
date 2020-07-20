namespace Restaurant.Api.Services.Configuration
{
    using System.Collections.Generic;
    using Restaurant.Api.Models;

    /// <summary>
    /// Interface table factory
    /// </summary>
    public interface ITableFactory
    {
        /// <summary>
        /// Create tables list
        /// </summary>
        /// <returns>Tables list</returns>
        List<Table> CreateTables();
    }
}