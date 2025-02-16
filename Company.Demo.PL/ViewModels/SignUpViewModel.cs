using System.ComponentModel.DataAnnotations;

namespace Company.Demo.PL.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "User Name is required")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "First Name is required")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name is required")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email Format")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Confirm Password is required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password doesn't match Password")]
		public string ConfirmPassword { get; set; }
		public bool IsAgreed { get; set; }
    }
}
