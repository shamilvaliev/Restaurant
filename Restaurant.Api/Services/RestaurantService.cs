namespace Restaurant.Api.Services
{
    using System;
    using System.Linq;
    using Restaurant.Api.Common.Exceptions;
    using Restaurant.Api.Models;
    using Restaurant.Api.Services.Configuration;

    /// <summary>
    /// Restaurant service
    /// </summary>
    public class RestaurantService : IRestaurantService
    {
        private RestManager restManager;
        private readonly IWaitingClientsQueueService waitingClientsQueue;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="tablesBuilder"></param>
        public RestaurantService(IWaitingClientsQueueService waitingClientsQueue, ITableFactory tableFactory)
        {
            this.waitingClientsQueue = waitingClientsQueue;
            this.restManager = new RestManager(tableFactory.CreateTables(), this.waitingClientsQueue);
        }

        /// <summary>
        /// New client arrives
        /// </summary>
        /// <param name="groupSize">Group size</param>
        /// <returns>Returns new registered group</returns>
        public ClientsGroup Arrive(int groupSize)
        {
            if (Table.MaxSize < groupSize)
            {
                throw new RestaurantException($"Sorry, but we have only tables with {Table.MaxSize} places");
            }

            var newGroup = new ClientsGroup(groupSize, Guid.NewGuid());
            this.restManager.OnArrive(newGroup);
            return newGroup;
        }

        /// <summary>
        /// Group leaves queue
        /// </summary>
        /// <param name="id">Group id</param>
        /// <returns>Returns leaved group</returns>
        public ClientsGroup Leave(Guid id)
        {
            var group = this.restManager.GetGroup(id);
            if (group != null)
            {
                this.restManager.OnLeave(group);
            }

            return group;
        }

        /// <summary>
        /// Lookup group in restaurant and return table
        /// </summary>
        /// <param name="groupId">Group id</param>
        /// <returns>Returns Table</returns>
        public Table Lookup(Guid groupId)
        {
            return this.restManager.Lookup(new ClientsGroup(1, groupId));
        }

        /// <summary>
        /// Check long waiting clients queue
        /// </summary>
        public void CheckLongWaitingQueueClients()
        {
            var clients = this.waitingClientsQueue.ToList();
            foreach (var client in clients)
            {
                if (client.SkippedOwnQueueCount >= ClientsGroup.MaxSkippingOwnQueue)
                {
                    this.waitingClientsQueue.Remove(client);
                }
            }
        }
    }
}
