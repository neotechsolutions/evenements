using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DemoWebApi.Models;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNet.SignalR;
using DemoWebApi.Hubs;
using System.Threading;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoWebApi.Controllers
{
    [Route("api")]
    public class BiereController : Controller
    {
        private IConnectionManager _connectionManager;
        private IHubContext _notifyHub;

        [FromServices]
        public IConnectionManager ConnectionManager
        {
            get
            {
                return _connectionManager;
            }
            set
            {
                _connectionManager = value;
                _notifyHub = _connectionManager.GetHubContext<NotifyHub>();
            }
        }

        private readonly IRepository _repository;
        public BiereController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("houblons")]
        public IActionResult GetAllHoublon()
        {
            return Ok(_repository.GetAllHoublons());
        }

        [HttpGet("houblons/{nom}")]
        public IActionResult GetHoublon(string nom)
        {
            Houblon houblon = _repository.FindHoublonByName(nom);
            if (houblon == null)
                return HttpNotFound();
            return Ok(houblon);
        }

        [HttpGet("houblons/slow")]
        public IActionResult GetAllHoublonSlow()
        {
            return Ok(_repository.GetAllHoublonsSlow());
        }

        [HttpGet("malts")]
        public IActionResult GetAllMalt()
        {
            return Ok(_repository.GetAllMalts());
        }

        [HttpGet("malts/{nom}")]
        public IActionResult GetMalt(string nom)
        {
            Malt malt = _repository.FindMaltByName(nom);
            if (malt == null)
                return HttpNotFound();
            return Ok(malt);
        }

        private IActionResult Ok<T>(T obj)
        {
            return new ObjectResult(obj) { StatusCode = 200 };
        }

        [HttpPost("biere")]
        public IActionResult CreerBiere(string id)
        {
            Task.Run(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));
                _notifyHub.Clients.Clients(NotifyHub.ConnectionIdList.Where(x => x.Value == id).Select(x => x.Key).ToList()).notify("La recette est prête ...");
            });
            return Ok("Recette en cours d'élaboration ...");
        }
    }
}
