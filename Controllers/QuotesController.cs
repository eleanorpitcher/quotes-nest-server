using QuotesNestServer.Models;
using QuotesNestServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace QuotesNestServer.Controllers;

[ApiController]
[Route("api/[controller]")] //Defines the route for this controller
public class QuotesController : ControllerBase
{
    private readonly QuotesService _quotesService; //What the controller will use to talk to the database

    public QuotesController(QuotesService quotesService) =>
        _quotesService = quotesService; //A constructor that injects the service via dependency injection

    [HttpGet]
    public async Task<List<Quote>> Get() =>
        await _quotesService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Quote>> Get(string id)
    {
        var quote = await _quotesService.GetAsync(id);

        if (quote is null)
        {
            return NotFound();
        }
        return quote;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Quote newQuote)
    {
        await _quotesService.CreateAsync(newQuote);
        return CreatedAtAction(nameof(Get), new { id = newQuote.Id }, newQuote);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<ActionResult<Quote>> Update(string id, Quote updatedQuote)
    {
        var quote = await _quotesService.GetAsync(id);
        if (quote is null)
        {
            return NotFound();
        }
        updatedQuote.Id = quote.Id;
        await _quotesService.UpdateAsync(id, updatedQuote);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var quote = await _quotesService.GetAsync(id);

        if (quote is null)
        {
            return NotFound();
        }

        await _quotesService.RemoveAsync(id);
        return NoContent();
    }
}