using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScripturesApi.Requests;
using ScripturesApi.Services.Abstract;
using ScripturesApi.ViewModels;

namespace ScripturesApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ScripturesController : ControllerBase
{
    private readonly ILogger<ScripturesController> _logger;
    private readonly IMapper _mapper;
    private readonly IScripturesService _scriptureService;

    public ScripturesController(ILogger<ScripturesController> logger,
        IMapper mapper,
        IScripturesService scriptureService)
    {
        _logger = logger;
        _mapper = mapper;
        _scriptureService = scriptureService;
    }

    [HttpGet("books/{id:int}")]
    public async Task<IActionResult> FilterBooks([FromRoute] int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest($"{id} is not a valid book id.");
            }

            var result = await _scriptureService.GetBookAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            var book = _mapper.Map<Book>(result);

            return Ok(book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting book. Message: {ex.Message}", id);

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("books")]
    public async Task<IActionResult> FilterBooks([FromQuery] FilterBooksRequest filter)
    {
        try
        {
            var results = await _scriptureService.FilterBooksAsync(filter);

            var books = _mapper.Map<IList<Book>>(results);

            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error filtering books. Message: {ex.Message}", filter);

            return BadRequest(ex.Message);
        }
    }

    // todo: chapters
    //  - get chapter by id
    //  - get chapters by book id

    // todo: verses
    //  - get verse by id
    //  - get verses by chapter id

#if DEBUG // todo: scope api keys instead?

    // todo: generate api key

#endif
}
