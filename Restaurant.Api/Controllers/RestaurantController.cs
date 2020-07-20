namespace Restaurant.Api.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Restaurant.Api.Models;
    using Restaurant.Api.Services;
    using Restaurant.Api.ViewModels;

    /// <summary>
    /// Restaurant api
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RestaurantController : ControllerBase
    {
        private readonly ILogger<RestaurantController> _logger;
        private readonly IRestaurantService restaurantService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="restaurantService">restaurantService</param>
        public RestaurantController(ILogger<RestaurantController> logger, IRestaurantService restaurantService)
        {
            this._logger = logger;
            this.restaurantService = restaurantService;
        }

        /// <summary>
        /// Lookup groups table
        /// </summary>
        /// <param name="groupId">Group id</param>
        /// <returns>Retruns table</returns>
        [HttpGet]
        public Table Lookup(Guid groupId)
        {
            return this.restaurantService.Lookup(groupId);
        }

        /// <summary>
        /// New client arrives
        /// </summary>
        /// <param name="model">Client group size</param>
        /// <returns>Return new registered group</returns>
        [HttpPost]
        public ClientsGroup Arrive([FromBody]GroupViewModel model)
        {
            return this.restaurantService.Arrive(model.GroupSize);
        }

        /// <summary>
        /// Leave group queue
        /// </summary>
        /// <param name="id">Group id</param>
        /// <returns>Returns group</returns>
        [HttpDelete("{id}")]
        public ClientsGroup Leave(Guid id)
        {
            return this.restaurantService.Leave(id);
        }
    }
}
