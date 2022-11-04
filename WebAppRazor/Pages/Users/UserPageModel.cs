using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services.Interfaces;

namespace WebAppRazor.Pages.Users
{
	public abstract class UserPageModel : PageModel
	{
		public IService<User> Service { get; }

		protected UserPageModel(IService<User> service)
		{
			Service = service;
		}
	}
}
