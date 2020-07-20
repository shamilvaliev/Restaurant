namespace Restaurant.Api.Services
{
    using Restaurant.Api.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Interface wainting clients queue service
    /// </summary>
    public interface IWaitingClientsQueueService: IEnumerable<ClientsGroup>
    {
        /// <summary>
        /// Add to queue clients group
        /// </summary>
        /// <param name="group">One group or one person</param>
        void Add(ClientsGroup group);

        /// <summary>
        /// Removes from queue group
        /// </summary>
        /// <param name="group">One group or one person</param>
        void Remove(ClientsGroup group);
    }
}