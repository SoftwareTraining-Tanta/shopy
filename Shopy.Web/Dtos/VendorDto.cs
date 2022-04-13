using Shopy.Web.Models;

namespace Shopy.Web.Dtos;

    #nullable disable

    public class VendorDto
    {

     
        public string Username { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
 
        public string VerificationCode { get; set; }

        public bool IsVerified { get; set; }
        public virtual ICollection<Model> Models { get; set; }

    }

