namespace Restaurant.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Table model
    /// </summary>
    public class Table
    {
        // TODO: set up to settings
        public const int MaxSize = 6;

        private readonly int size;
        private List<ClientsGroup> groups;

        public Table(int size)
        {
            if (size < 1)
            {
                throw new ArgumentException("TableSize must be a greater then 0", nameof(size));
            }

            if (size > MaxSize)
            {
                throw new ArgumentException($"TableSize must be a less then {MaxSize}", nameof(size));
            }

            this.size = size;
            this.groups = new List<ClientsGroup>();
        }

        /// <summary>
        /// Returns tables free places
        /// </summary>
        public int FreeSize
        {
            get
            {
                return this.size - this.groups.Sum(t => t.Size);
            }
        }

        /// <summary>
        /// Returns flat that table is empty
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return !this.groups.Any();
            }
        }

        /// <summary>
        /// Place clients group
        /// </summary>
        /// <param name="group">Group or one person</param>
        public void PlaceClients(ClientsGroup group)
        {
            this.groups.Add(group);
        }

        /// <summary>
        /// Clients group free places
        /// </summary>
        /// <param name="group">Group or one person</param>
        public void ClientsFreePlace(ClientsGroup group)
        {
            this.groups.Remove(group);
        }

        /// <summary>
        /// Get group by Id
        /// </summary>
        /// <param name="id">Group id</param>
        /// <returns>Returns group by Id</returns>
        public ClientsGroup GetGroup(Guid id)
        {
            return this.groups.SingleOrDefault(t => t.Id == id);
        }
    }
}
