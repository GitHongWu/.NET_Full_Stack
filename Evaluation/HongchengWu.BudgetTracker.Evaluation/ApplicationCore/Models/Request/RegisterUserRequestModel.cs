using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Request
{
    public class RegisterUserRequestModel
    {
        public RegisterUserRequestModel()
        {
            JoinedOn = DateTime.Now;
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime? JoinedOn { get; set; }
    }
}
