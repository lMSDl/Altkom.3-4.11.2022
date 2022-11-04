using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
	public class User : Entity
	{
		[DisplayName("Username")]
		[Required]
		public string? Name { get; set; }
		[MinLength(8)]
        [Required]
        public string? Password { get; set; }
    }
}