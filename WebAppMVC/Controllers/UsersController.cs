using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebAppMVC.Controllers
{
	public class UsersController : Controller
	{
		private readonly IService<User> _service;

		public UsersController(IService<User> service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _service.ReadAsync());
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Search(string? phrase)
		{
			var users = await _service.ReadAsync();
			if(!string.IsNullOrWhiteSpace(phrase))
			{
				var properties = typeof(User).GetProperties().Where(x => x.CanRead).ToList();
				users = users.Where(x => properties.Any(xx => xx.GetValue(x)?.ToString()?.Contains(phrase) ?? false)).ToList();
			}
			return View(nameof(Index), users);
		}
	}
}
