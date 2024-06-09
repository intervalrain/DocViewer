using DocViewer.Application.Common.Interfaces;
using DocViewer.Application.Common.Security.Users;
using DocViewer.Application.Docs.Queries.GetDoc;
using DocViewer.Application.Docs.Queries.ListDocs;
using DocViewer.Presentation.Models.Docs;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace DocViewer.Presentation.Controllers;

public class DocsController : ControllerRoot
{
    private readonly ISender _sender;
	private readonly CurrentUser _currentUser;

    public DocsController(ISender sender, ICurrentUserProvider currentUserProvider)
	{
        _sender = sender;
		_currentUser = currentUserProvider.CurrentUser;
    }

	public async Task<IActionResult> Doc(int id)
	{
        var query = new GetDocQuery(_currentUser.UserId, id);
        var result = await _sender.Send(query, default);
        return result.Match(
            View,
            Problem);
    }

	public async Task<IActionResult> Index(string sort = "",string filter = "")
	{
		var query = new ListDocsQuery(_currentUser.UserId, sort, filter);
		var result = await _sender.Send(query, default);
		return result.Match(
			board => View(new DocsViewModel
			{
				Board = board,
				Sort = sort,
				Filter = filter
			}),
			Problem);
	}
}

