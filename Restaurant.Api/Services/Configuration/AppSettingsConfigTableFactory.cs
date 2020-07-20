namespace Restaurant.Api.Services.Configuration
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Options;
    using Restaurant.Api.Configuration;
    using Restaurant.Api.Models;

    /// <summary>
    /// Table factory creates tables from appsettings.json
    /// </summary>
    public class AppSettingsConfigTableFactory: ITableFactory
    {
        private readonly IOptions<TableSettings> tableSettings;
        private readonly ITablesBuilder tablesBuilder;

        public AppSettingsConfigTableFactory(IOptions<TableSettings> tableSettings, ITablesBuilder tablesBuilder)
        {
            this.tableSettings = tableSettings;
            this.tablesBuilder = tablesBuilder;
        }

        /// <summary>
        /// Create tables list
        /// </summary>
        /// <returns>Tables list</returns>
        public List<Table> CreateTables()
        {
            this.tablesBuilder.Add(this.tableSettings.Value.Tables);
            return this.tablesBuilder.Build();
        }
    }
}
