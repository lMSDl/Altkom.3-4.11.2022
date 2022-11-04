using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebAppRazor.Pages.Users.AddOrEdit
{
	public class AddOrEditModel : UserPageModel
	{
		public AddOrEditModel(IService<User> service) : base(service)
		{
		}

		[BindProperty]
		public User SelectedUser { get; set; }


		public async Task OnGet(int userId)
		{
			SelectedUser = await Service.ReadAsync(userId);
		}

		public async Task<IActionResult> OnPost()
		{
			if(SelectedUser.Id == 0)
			{
				await Service.CreateAsync(SelectedUser);
			}
			else
			{
				await Service.UpdateAsync(SelectedUser.Id, SelectedUser);
			}

			return RedirectToPage("../Index");
		}
	}
}
