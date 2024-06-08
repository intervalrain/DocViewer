using DocViewer.Application.Common.Interfaces.Persistence;
using DocViewer.Domain;
using DocViewer.Presentation.Models.Docs;

using Microsoft.AspNetCore.Mvc;

namespace DocViewer.Presentation.Controllers;

public class DocsController : Controller
{
	private readonly Board _board;

    public DocsController(IBoardRepository boardRepository)
	{
		_board = boardRepository.Get();
    }

	public IActionResult Doc(int id)
	{
		var doc = _board.Docs.FirstOrDefault(doc => doc.DocId == id);
		return View(doc);
	}

	public IActionResult Index()
	{
		var model = new DocsViewModel
		{
			Board = _board
		};
		return View(model);
	}
}

