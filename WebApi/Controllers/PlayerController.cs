using Microsoft.AspNetCore.Mvc;
using ObjectToArrayContruct2.Helpers;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<ActionResult<object>> Get()
    {
        //Dictionary of Construct2
        /*{
            "c2dictionary": true,
            "data": {
                "playerName": "John",
                "hiScore": 999
            }
        }*/
        
        //Array 2 chieu of Construct2, ngoai ra 1 array va 3d(x,y,z)
        //Format: 1 chieu: [1,2,3],2 chieu: [[1,2,3],[[1,2,3]]
        //Size: [2,3,1] tuong ung x,y,z(2 dong,3 cot, 1 z)
        /*
         {"c2array":true,"size":[2,3,1],"data":[[[0],["Lan"],[90]],[[1],["Huong"],[100]]]}
         */

        
        // An example object with the data to be sent to the game
        PlayerData playerData = new PlayerData();
        playerData.PlayerName = "John";
        playerData.HiScore = 999;

        // Use WebAPI2Construct to transform your data in a Construct readable format
        var playerDataJSONDictionary = Construct2Convert.ToDictionary(playerData);

        return Ok(playerDataJSONDictionary); // Send the data to the game
        
    }
}