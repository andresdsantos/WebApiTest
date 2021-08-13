using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiJwt.Models
{
    public class UserViewModel
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public List<AddressVM> addresses { get; set; }
    }
}