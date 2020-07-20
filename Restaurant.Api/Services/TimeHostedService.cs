using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Api.Services
{
    internal class TimedHostedService : IHostedService, IDisposable
    {
        private Timer timer;
        private readonly IRestaurantService restaurantService;

        public TimedHostedService(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            timer = new Timer(this.CheckClients, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));

            return Task.CompletedTask;
        }

        private void CheckClients(object state)
        {
            this.restaurantService.CheckLongWaitingQueueClients();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
           
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
