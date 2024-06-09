using DocViewer.Application.Common.Interfaces;
using DocViewer.Application.Common.Security.Users;
using DocViewer.Application.Docs.Queries.GetDoc;
using DocViewer.Application.Docs.Queries.ListDocs;
using DocViewer.Application.Docs.Queries.SearchDocs;
using DocViewer.Domain;
using DocViewer.Presentation.Models.Docs;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocViewer.Presentation.Controllers;

public class DocsController : ApiController
{
    private readonly ISender _sender;
	private readonly CurrentUser _currentUser;

    public DocsController(ISender sender, ICurrentUserProvider currentUserProvider)
	{
        _sender = sender;
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
				Docs = board.SearchDocs(request.Text),
				Categories = board.Categories
			}),
			Problem);
	}

	public class SearchRequest
	{
		public string Text { get; set; }
	}
}

