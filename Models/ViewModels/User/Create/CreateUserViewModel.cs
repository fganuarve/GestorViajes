namespace GestorViajes.Models.ViewModels.User.Create
{
    public class CreateUserViewModel
    {
        /*Step 1*/
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? LastName2 { get; set; }
        public string NationalId { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? Email { get; set; }
        /*Step 2*/
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int ZipCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Notes { get; set; }
        /*Step 3*/
        public string SelectedRole { get; set; } = string.Empty;
    }
}
