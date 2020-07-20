namespace Restaurant.Api.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Restaurant.Api.Models;

    /// <summary>
    /// Service queue waiting clients 
    /// </summary>
    public class WaitingClientsQueueService : IWaitingClientsQueueService, IEnumerable<ClientsGroup>
    {
        private readonly object syncObj = new object();
        private readonly SortedList<long, ClientsGroup> waitingQueue = new SortedList<long, ClientsGroup>();

        /// <summary>
        /// Add to queue clients group
        /// </summary>
        /// <param name="group">One group or one person</param>
        public void Add(ClientsGroup group)
        {
            lock (this.syncObj)
            {
                this.waitingQueue.Add(DateTime.Now.Ticks, group);
            }
        }

        /// <summary>
        /// Removes from queue group
        /// </summary>
        /// <param name="group">One group or one person</param>
        public void Remove(ClientsGroup group)
        {
            lock (this.syncObj)
            {
                var leavingGroup = this.waitingQueue.SingleOrDefault(t => t.Value.Id == group.Id);
                if (leavingGroup.Key != default(long))
                {
                    this.waitingQueue.Remove(leavingGroup.Key);
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Returns an enumerator that iterates through the collection.</returns>
        public IEnumerator<ClientsGroup> GetEnumerator()
        {
            return this.waitingQueue.Values.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Returns an enumerator that iterates through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
