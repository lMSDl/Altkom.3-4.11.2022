using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebAppMVC.Controllers
{
	[AutoValidateAntiforgeryToken] //walidacja tokena formularza dla wszystkich akcji POST
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

		//[ValidateAntiForgeryToken] //walidacja tokena formularza dla oznaczonej akcji
		//[HttpPost]
		public async Task<IActionResult> Search(string? phrase)
		{
			var users = await _service.ReadAsync();
			if (!string.IsNullOrWhiteSpace(phrase))
			{
				var properties = typeof(User).GetProperties().Where(x => x.CanRead).ToList();
				users = users.Where(x => properties.Any(xx => xx.GetValue(x)?.ToString()?.Contains(phrase) ?? false)).ToList();
			}
			return View(nameof(Index), users);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var user = await _service.ReadAsync(id);
			if (user == null)
				return NotFound();
			return View(user);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteUser(int id)
		{
			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id)
        {
            var user = await _service.ReadAsync(id);
            if (user == null)
                return NotFound();
            return View(user);
        }

        [HttpPost]
        //public async Task<IActionResult> EditUser(int id, string name, string password)
        //public async Task<IActionResult> EditUser(int id, User user)
        //public async Task<IActionResult> EditUser(int id, [Bind("Name")] User user1, [Bind("Password")] User user2)
        public async Task<IActionResult> EditUser(int id, [Bind("Name", "Password")] User user)
        {
			if (id == 0)
				await _service.CreateAsync(user);
			else
			{
				if(string.IsNullOrEmpty(user.Password))
				{
					user.Password = (await _service.ReadAsync(id))!.Password;
				}
				await _service.UpdateAsync(id, user);
			}

            return RedirectToAction(nameof(Index));
        }

		public IActionResult Add()
		{
			return View(nameof(Edit), new User());
		}
    }
}
