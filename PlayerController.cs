using Microsoft.AspNetCore.Mvc;
using Server.Services;
using SharedLibrary;

namespace Server.Controllers;
//api controller, route allows us to not have to do manual mapping for our controllers
[ApiController]
[Route("[controller]")]
//inherit from controller base provides us with controller functionality
public class PlayerController : ControllerBase {
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService) {
      _playerService = playerService;
    }
    
   //controller is group od endpoints
   
   [HttpGet("{id}")] //get request (endpoint no payload datapoint)
   public Player Get([FromRoute]int id) {
       var player = new Player();
       _playerService.DoSomething();

       return player;
   }

   //this is to post data to our server
    [HttpPost]
    public Player Post(Player player)
    {
        Console.WriteLine("Player has been added to the DB");
        return player;
    }
}