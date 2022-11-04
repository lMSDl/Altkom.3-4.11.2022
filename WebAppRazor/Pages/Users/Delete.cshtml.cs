using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services.Interfaces;

namespace WebAppRazor.Pages.Users
{
    public class DeleteModel : UserPageModel
    {
        public DeleteModel(IService<User> service) : base(service)
        {
        }

        public User? SelectedUser { get; set; }

        /*public async Task OnGet(int userId)
        {
            SelectedUser = await Service.ReadAsync(userId);
        }*/
        public async Task<IActionResult> OnGet()
        {
            SelectedUser = await Service.ReadAsync(UserId);
            if (SelectedUser == null)
                return NotFound();

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public int UserId { get; set; }

        public async Task<IActionResult> OnPost()
        {
            await Service.DeleteAsync(UserId);
            return RedirectToPage("./Index");
        }
    }
}
