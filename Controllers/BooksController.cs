using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BooksService _booksService;
    //private readonly AuthService _authService;

    public BooksController(BooksService booksService)
    {
        _booksService = booksService;
        //_authService = authService;
    }

    
    [HttpGet]
    public async Task<List<Book>> Get() =>
        await _booksService.GetAsync();

    //[Authorize]
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Book>> Get(string id)
    {
        var book = await _booksService.GetAsync(id);
        if (book is null)
        {
            return NotFound();
        }
        return book;
        //try
        //{
        //    // Retrieve the access token
        //    var token = await _authService.GetAccessTokenAsync();

        //    var book = await _booksService.GetAsync(id);

        //    if (book is null)
        //    {
        //        return NotFound();
        //    }

        //    return book;
        //}
        //catch (Exception ex)
        //{
        //    // Handle exceptions and return an appropriate status code
        //    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //}
    }

    [HttpPost]
    public async Task<IActionResult> Post(Book newBook)
    {
        await _booksService.CreateAsync(newBook);

        return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Book updatedBook)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedBook.Id = book.Id;

        await _booksService.UpdateAsync(id, updatedBook);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _booksService.RemoveAsync(id);

        return NoContent();
    }
}