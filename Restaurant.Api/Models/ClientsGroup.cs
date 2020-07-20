namespace Restaurant.Api.Models
{
    using System;

    public class ClientsGroup
    {
        // TODO: set up to settings
        public const int MaxSkippingOwnQueue = 7;

        private readonly int size;
        private readonly Guid id;
        private int skippedOwnQueueCount = 0;

        public ClientsGroup(int size, Guid id)
        {
            if (size < 1)
            {
                throw new ArgumentException("GroupSize must be a greater then 0", nameof(size));
            }

            this.size = size;
            this.id = id;
        }

        public Guid Id
        {
            get
            {
                return this.id;
            }
        }

        public int Size
        {
            get
            {
                return this.size;
            }
        }

        public int SkippedOwnQueueCount
        {
            get
            {
                return this.skippedOwnQueueCount;
            }
        }

        public void IncrementSkipedOwnQueue()
        {
            this.skippedOwnQueueCount++;
        }
    }
}
