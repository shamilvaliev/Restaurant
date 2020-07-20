using Restaurant.Api.Models;
using System;

namespace Restaurant.Api.Services
{
    public interface IRestaurantService
    {
        /// <summary>
        /// New client arrives
        /// </summary>
        /// <param name="groupSize">Group size</param>
        /// <returns>Returns new registered group</returns>
        ClientsGroup Arrive(int groupSize);

        /// <summary>
        /// Group leaves queue
        /// </summary>
        /// <param name="id">Group id</param>
        /// <returns>Returns leaved group</returns>
        ClientsGroup Leave(Guid id);

        /// <summary>
        /// Lookup group in restaurant and return table
        /// </summary>
        /// <param name="groupId">Group id</param>
        /// <returns>Returns Table</returns>
        Table Lookup(Guid groupId);

        /// <summary>
        /// Check long waiting clients queue
        /// </summary>
        void CheckLongWaitingQueueClients();
    }
}