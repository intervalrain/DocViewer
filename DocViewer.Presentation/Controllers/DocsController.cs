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
	private readonly string _userId = "Rain Hu";

    public DocsController(ISender sender)
	{
        _sender = sender;
    }

	public async Task<IActionResult> Doc(int id)
	{
        var query = new GetDocQuery(_userId, id);
        var result = await _sender.Send(query, default);
        return result.Match(
            View,
            Problem);
    }

	public async Task<IActionResult> Index(string sort = "",string filter = "")
	{
		var query = new ListDocsQuery(_userId, sort, filter);
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

