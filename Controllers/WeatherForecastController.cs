using Microsoft.AspNetCore.Mvc;
using _2026_10_06.Models;

namespace _2026_10_06.Controllers;

[ApiController]
[Route("[controller]")]
public class DrinkController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "water","coffee","iceCoffee","jump","KokaKola"
    };

    private static List<drinkForecast>? arr;

    public DrinkController()
    {
        arr = Enumerable.Range(1, 5).Select(index => new drinkForecast
        {
            name = Summaries[index - 1],
            Id = index,
            price =  Random.Shared.Next(5, 25),
            wight = (Summaries[index - 1] == "water" ||
         Summaries[index - 1] == "KokaKola") ? 1 : 0
        })
        .ToList();
   

    [HttpGet]
    public IEnumerable<drinkForecast> Get()
    {
        return arr;
    }

    [HttpGet("{id}")]
    public ActionResult<drinkForecast> Get(int id)
    {
    
        var res = arr?.FirstOrDefault(w => w.Id == id); 

        if (res == null)
            return NotFound();
        
        return res;
    }
    
}
