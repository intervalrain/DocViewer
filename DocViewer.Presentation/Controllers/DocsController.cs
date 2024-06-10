using DocViewer.Application.Common.Interfaces;
using DocViewer.Application.Common.Security.Users;
using DocViewer.Application.Docs.Commands.NewPost;
using DocViewer.Application.Docs.Queries.GetDoc;
using DocViewer.Application.Docs.Queries.ListDocs;
using DocViewer.Contracts.Requests;
using DocViewer.Presentation.Models.Docs;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocViewer.Presentation.Controllers;

public class DocsController : ApiController
{
    private readonly ISender _sender;
    private readonly IWebHostEnvironment _environment;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly CurrentUser _currentUser;

    public DocsController(ISender sender, ICurrentUserProvider currentUserProvider, IWebHostEnvironment environment, IDateTimeProvider dateTimeProvider)
	{
        _sender = sender;
        _environment = environment;
        _dateTimeProvider = dateTimeProvider;
        _currentUser = currentUserProvider.CurrentUser;
    }

	[HttpGet(nameof(Index))]
	public async Task<IActionResult> Index(string sort = "id_desc", string filter = "All")
	{
        var query = new ListDocsQuery(_currentUser.UserId);
		var result = await _sender.Send(query, default);

		return result.Match(
			board => View(new DocsViewModel
            {
                Docs = board.GetDocs(sort, filter),
                Sort = sort,
                Filter = filter,
                Categories = board.Categories
            }),
			Problem);
	}

	[HttpGet(nameof(Doc) + "/{id:int}")]
    public async Task<IActionResult> Doc(int id)
    {
        var query = new GetDocQuery(_currentUser.UserId, id);
        var result = await _sender.Send(query, default);
        return result.Match(
            View,
            Problem);
    }

	[HttpPost(nameof(Search))]
	public async Task<IActionResult> Search([FromBody] SearchRequest request)
	{
		var query = new ListDocsQuery(_currentUser.UserId);
		var result = await _sender.Send(query, default);
		return result.Match(
			board => PartialView("_DocsPartial", new DocsViewModel
			{
				Docs = string.IsNullOrWhiteSpace(request.Text)
						? board.Docs.ToList()
						: board.SearchDocs(request.Text),
				Categories = board.Categories
			}),
			Problem);
	}

	[HttpGet(nameof(NewPost))]
	public IActionResult NewPost()
	{
		return View(new NewPostViewModel
        {
            Author = _currentUser.UserName
        });
	}

    [HttpPost(nameof(NewPost))]
    public async Task<IActionResult> NewPost(NewPostViewModel model)
    {
        var command = new NewPostCommand(
            UserId: _currentUser.UserId,
            Title: model.Title,
            Author: model.Author,
            Category: model.Category,
            Keywords: model.Keywords,
            Description: model.Description,
            Content: model.Content,
            DateTime: _dateTimeProvider.UtcNow);
        var result = await _sender.Send(command, default);
        return result.Match(
            doc => RedirectToAction(nameof(Doc), new { id = doc.DocId }),
            Problem);
    }


    [HttpPost("UploadImage")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return Json(new { success = false, message = "No file selected" });

        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        var fileUrl = Url.Content("~/uploads/" + fileName);
        return Json(new { success = true, url = fileUrl });
    }
}

