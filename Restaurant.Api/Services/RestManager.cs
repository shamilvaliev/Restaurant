namespace Restaurant.Api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Restaurant.Api.Models;

    /// <summary>
    /// Restaurant manger
    /// </summary>
    public class RestManager
    {
        private readonly object syncObj = new object();
        private readonly List<Table> tables;
        private readonly IWaitingClientsQueueService waitingClientsQueue;
        private List<Func<Table, int, bool>> getTablesByPriorityRules = new List<Func<Table, int, bool>>
        {
            (t, size)=>  t.FreeSize >= size && t.IsEmpty,
            (t, size)=>  t.FreeSize >= size
        };

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="tables">Init tables</param>
        public RestManager(List<Table> tables, IWaitingClientsQueueService waitingClientsQueue)
        {
            this.tables = tables;
            this.waitingClientsQueue = waitingClientsQueue;
        }

        /// <summary>
        /// new client(s) show up
        /// </summary>
        /// <param name="group">New clients group or one person</param>
        public void OnArrive(ClientsGroup group)
        {
            this.waitingClientsQueue.Add(group);
            this.WorkClientsQueue();
        }

        /// <summary>
        /// client(s) leave, either served or simply abandoning the queue
        /// </summary>
        /// <param name="group">Waiting clients group or one person</param>
        public void OnLeave(ClientsGroup group)
        {
            this.waitingClientsQueue.Remove(group);
            this.WorkClientsQueue();
        }

        /// <summary>
        /// Get table where a given client group is seated,
        /// or null if it is still queuing or has already left
        /// </summary>
        /// <param name="group">Finding group</param>
        /// <returns>Returns table</returns>
        public Table Lookup(ClientsGroup group)
        {
            return this.tables.SingleOrDefault(t => t.GetGroup(group.Id) != null);
        }

        /// <summary>
        /// Get clients group by id
        /// </summary>
        /// <param name="id">Group id</param>
        /// <returns>Returns clients group by id</returns>
        public ClientsGroup GetGroup(Guid id)
        {
            var group = this.waitingClientsQueue.FirstOrDefault(t => t.Id == id);
            return group;
        }

        /// <summary>
        /// Get Table with free seats
        /// </summary>
        /// <param name="size">Table size to find</param>
        /// <returns>Returns table</returns>
        private Table GetTableWithFreeSeats(int size)
        {
            Table freeTable = null;
            foreach (var rule in this.getTablesByPriorityRules)
            {
                freeTable = this.tables.FirstOrDefault(t => rule(t, size));
                if (freeTable != null)
                {
                    return freeTable;
                }
            }

            return freeTable;
        }

        /// <summary>
        /// Work clients in queue
        /// </summary>
        private void WorkClientsQueue()
        {
            lock (this.syncObj)
            {
                var anyPlaced = false;
                var workingClients = this.waitingClientsQueue.ToList();
                foreach (var group in workingClients)
                {
                    var freeTable = this.GetTableWithFreeSeats(group.Size);
                    if (freeTable != null)
                    {
                        freeTable.PlaceClients(group);
                        this.waitingClientsQueue.Remove(group);
                        anyPlaced = true;
                    }
                }

                if (anyPlaced)
                {
                    foreach (var group in this.waitingClientsQueue)
                    {
                        group.IncrementSkipedOwnQueue();
                    }
                }
            }
        }
    }
}
