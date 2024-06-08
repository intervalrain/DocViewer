using Microsoft.AspNetCore.Mvc;

namespace DocViewer.Presentation.Controllers;

public class DocsController : Controller
{
	public DocsController()
	{
	}

	public IActionResult Index()
	{
		return View();
	}
}

