using DocViewer.Domain;
using DocViewer.Presentation.Models.Docs;

using Microsoft.AspNetCore.Mvc;

namespace DocViewer.Presentation.Controllers;

public class DocsController : Controller
{
	private Board board;

	public DocsController()
	{
		board = new Board();
		board.AddDoc(new Doc(1, "doc1", "Rain", "desc1", "A", "test1", new[] { "keyword1" }, DateTime.Now.AddDays(-30)));
        board.AddDoc(new Doc(2, "doc2", "Rain", "desc2", "B", "test2", new[] { "keyword2" }, DateTime.Now.AddDays(-25)));
        board.AddDoc(new Doc(3, "doc3", "Mark", "desc3", "A", "test3", new[] { "keyword3" }, DateTime.Now.AddDays(-20)));
        board.AddDoc(new Doc(4, "doc4", "Rain", "desc4", "C", "test4", new[] { "keyword4" }, DateTime.Now.AddDays(-15)));
        board.AddDoc(new Doc(5, "doc5", "Kun", "desc5", "B",  "test5", new[] { "keyword5" }, DateTime.Now.AddDays(-10)));
	}

	public IActionResult Doc(int id)
	{
		var doc = board.Docs.FirstOrDefault(d => d.DocId == id);
		return View(doc);
	}

	public IActionResult Index()
	{
		var model = new DocsViewModel
		{
			Board = board
		};
		return View(model);
	}
}

