using System;
using System.ComponentModel.DataAnnotations;

namespace MicroblogServer.Entity 
{ 

   public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Image { get; set; }
        
        public string Country { get; set; }

    }
}
