using System.ComponentModel.DataAnnotations;

namespace Company.Demo.PL.ViewModels
{
	public class SignInViewModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email Format")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
