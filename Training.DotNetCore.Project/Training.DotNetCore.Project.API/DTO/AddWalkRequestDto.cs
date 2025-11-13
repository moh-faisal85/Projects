using System.ComponentModel.DataAnnotations;

namespace Training.DotNetCore.Project.API.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(minimum:0,maximum:50)]
        public double LengthInKM { get; set; }//Nullable
        public string? WalkImageUrl { get; set; }

        [Required]
        [NotEmptyGuid]//Custom Validatator
        public Guid DifficultyId { get; set; }
        
        [Required]
        [NotEmptyGuid]//Custom Validatator

        public Guid RegionId { get; set; }
    }
    /// <summary>
    /// Custom Validatator for Guid Not Empty Logic Check
    /// </summary>
    public class NotEmptyGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value is Guid guid && guid == Guid.Empty)
            {
                return new ValidationResult($"{context.DisplayName} cannot be empty.");
            }
            return ValidationResult.Success;
        }
    }

    #region DataAnnotations Tutorial
    /*
Category	    Attribute	        Description	Example
Required Field	[Required]	        Ensures the property is not null or empty.	[Required(ErrorMessage = "Name is required")] public string Name { get; set; }
String Length	[StringLength(max)]	Sets max (and optional min) allowed string length.	[StringLength(50, MinimumLength = 3)] public string Username { get; set; }
Minimum Length	[MinLength]	        Minimum number of characters required.	[MinLength(5)] public string Password { get; set; }
Maximum Length	[MaxLength]	        Restricts string or array length.	[MaxLength(10)] public string PhoneNumber { get; set; }
Range Check	    [Range(min, max)]	Validates numeric range.	[Range(18, 60)] public int Age { get; set; }
Regular Expression	[RegularExpression("pattern")]	Validates format using regex.	[RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Name must start with a capital letter")]
Email Format	[EmailAddress]	    Ensures valid email address.	[EmailAddress(ErrorMessage = "Invalid email format")]
Phone Format	[Phone]	            Validates phone number format.	[Phone(ErrorMessage = "Invalid phone number")]
URL Format	    [Url]	            Validates URL format.	[Url(ErrorMessage = "Invalid website URL")]
Credit Card	    [CreditCard]	    Validates credit card format.	[CreditCard(ErrorMessage = "Invalid credit card number")]
Data Type Hint	[DataType(DataType.Password)]	Specifies data type (used for UI rendering).	[DataType(DataType.Password)] public string Password { get; set; }
Compare Fields	[Compare("OtherProperty")]	Ensures two fields have matching values.	[Compare("Password", ErrorMessage = "Passwords do not match")] public string ConfirmPassword { get; set; }
Display Name	[Display(Name = "Full Name")]	Defines how the field name appears in UI.	[Display(Name = "Date of Birth")] public DateTime DOB { get; set; }
Display Format	[DisplayFormat(DataFormatString = "...", ApplyFormatInEditMode = true)]	Controls display format for UI binding.	[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")] public DateTime DOB { get; set; }
Scaffold Control[ScaffoldColumn(false)]	Hides field from scaffolding in auto-generated views.	[ScaffoldColumn(false)] public string InternalCode { get; set; }
Timestamp	    [Timestamp]	Marks field as concurrency check column.	[Timestamp] public byte[] RowVersion { get; set; }
Concurrency Check [ConcurrencyCheck]	Ensures the value hasn’t changed in the database.	[ConcurrencyCheck] public decimal Price { get; set; }
Key	            [Key]	            Marks property as primary key (Entity Framework).	[Key] public int Id { get; set; }
Database Generated	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]	Defines identity/auto values for DB.	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
Custom Validation	[CustomValidation(typeof(Class), "MethodName")]	Uses custom logic for validation.	[CustomValidation(typeof(UserValidator), "ValidateAge")]
Remote Validation	[Remote("Action", "Controller")]	Calls server-side method via AJAX for validation.	[Remote("CheckEmail", "Account")] public string Email { get; set; }

💡 Example: Using Data Annotations in a Model
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(20, MinimumLength = 5)]
    public string Username { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords must match")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

    [Range(18, 60, ErrorMessage = "Age must be between 18 and 60")]
    public int Age { get; set; }

    [Url]
    public string Website { get; set; }

    [Remote("CheckEmailExists", "Account", ErrorMessage = "Email already registered")]
    public string RemoteEmail { get; set; }
}

🧠 Custom Validation Example

You can create your own validation attribute by inheriting from ValidationAttribute.

public class NotAdminNameAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        if (value != null && value.ToString().ToLower() == "admin")
        {
            return new ValidationResult("Username cannot be 'admin'.");
        }
        return ValidationResult.Success;
    }
}


Use it like:

[NotAdminName]
public string Username { get; set; }
     */
    #endregion
}
