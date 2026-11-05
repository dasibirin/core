using Microsoft.AspNetCore.Mvc;
using _2026_10_06.Models;

namespace _2026_10_06.Controllers;

[ApiController]
[Route("[controller]")]
public class DrinkController : ControllerBase
{
    private static readonly string[] DrinkNames = ["Water", "Sparkling Water", "Coca Cola", "Orange Juice", "Ice Coffee"];
    private static readonly string[] Categories = ["Bottle", "Bottle", "Can", "Bottle", "Cup"];
    private static readonly int[] Volumes = [500, 750, 330, 500, 350];
    private static readonly string[] ImageUrls = [
        "/images/water-bottle.png",
        "/images/sparkling-bottle.png",
        "/images/coke-can.png",
        "/images/orange-bottle.png",
        "/images/ice-coffee-cup.png"
    ];

    private static readonly List<Drink> Drinks = Enumerable.Range(1, 5)
        .Select(index => new Drink
        {
            Id = index,
            Name = DrinkNames[index - 1],
            Price = Random.Shared.Next(8, 28),
            VolumeMl = Volumes[index - 1],
            Category = Categories[index - 1],
            ImageUrl = ImageUrls[index - 1]
        })
        .ToList();

    [HttpGet]
    public ActionResult<IEnumerable<Drink>> Get()
    {
        return Ok(Drinks);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Drink> Get(int id)
    {
        var result = Drinks.FirstOrDefault(item => item.Id == id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Drink> Post([FromBody] Drink drink)
    {
        if (drink is null)
        {
            return BadRequest();
        }

        drink.Id = Drinks.Count == 0 ? 1 : Drinks.Max(d => d.Id) + 1;
        Drinks.Add(drink);

        return CreatedAtAction(nameof(Get), new { id = drink.Id }, drink);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var drink = Drinks.FirstOrDefault(item => item.Id == id);

        if (drink is null)
        {
            return NotFound();
        }

        Drinks.Remove(drink);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public ActionResult<Drink> Put(int id, [FromBody] Drink drink)
    {
        if (drink is null)
        {
            return BadRequest();
        }

        var existingDrink = Drinks.FirstOrDefault(item => item.Id == id);
        if (existingDrink is null)
        {
            return NotFound();
        }

        existingDrink.Name = drink.Name;
        existingDrink.Price = drink.Price;
        existingDrink.VolumeMl = drink.VolumeMl;
        existingDrink.Category = drink.Category;
        existingDrink.ImageUrl = drink.ImageUrl;

        return Ok(existingDrink);
    }
}
