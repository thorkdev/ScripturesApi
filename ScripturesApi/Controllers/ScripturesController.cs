using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScripturesApi.Domain.SelectModels.Chapters;
using ScripturesApi.Domain.SelectModels.Verses;
using ScripturesApi.Extensions;
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

    #region Books

    [HttpGet("books/{id:int}")]
    public async Task<IActionResult> GetBookById([FromRoute] int id)
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
    public async Task<IActionResult> FilterBooks([FromBody] FilterBooksRequest filter)
    {
        try
        {
            if (filter == null)
            {
                return BadRequest("The request field is required.");
            }

            filter = filter.DefaultPageSize(5);

            var response = await _scriptureService.FilterBooksAsync<Book>(filter);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error filtering books. Message: {ex.Message}", filter);

            return BadRequest(ex.Message);
        }
    }

    #endregion Books

    #region Chapters

    [HttpGet("chapters/{id:int}")]
    public async Task<IActionResult> GetChapterById([FromRoute] int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest($"{id} is not a valid chapter id.");
            }

            var result = await _scriptureService.GetChapterAsync<ChapterBase>(id);

            if (result == null)
            {
                return NotFound();
            }

            var chapter = _mapper.Map<Chapter>(result);

            return Ok(chapter);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting chapter. Message: {ex.Message}", id);

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("chapters")]
    public async Task<IActionResult> FilterChapters([FromBody] FilterChaptersRequest filter)
    {
        try
        {
            if (filter == null)
            {
                return BadRequest("The request field is required.");
            }

            filter = filter.DefaultPageSize(10);

            var response = await _scriptureService.FilterChaptersAsync<Chapter, ChapterBase>(filter);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error filtering chapters. Message: {ex.Message}", filter);

            return BadRequest(ex.Message);
        }
    }

    #endregion Chapters

    #region Verses

    [HttpGet("verses/{id:int}")]
    public async Task<IActionResult> GetVerseById([FromRoute] int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest($"{id} is not a valid verse id.");
            }

            var result = await _scriptureService.GetVerseAsync<VerseBase>(id);

            if (result == null)
            {
                return NotFound();
            }

            var verse = _mapper.Map<Verse>(result);

            return Ok(verse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting verse. Message: {ex.Message}", id);

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("verses")]
    public async Task<IActionResult> GetVerses([FromBody] PagedVersesRequest request)
    {
        try
        {
            if (request == null)
            {
                return BadRequest("The request field is required.");
            }

            request = request.DefaultPageSize(20);

            var response = await _scriptureService.GetVersesAsync<Verse, VerseBase>(request);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting verses. Message: {ex.Message}", request);

            return BadRequest(ex.Message);
        }
    }

    #endregion Verses
}
