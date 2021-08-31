using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithTokenAntiForgery30Aug21.DTOs.Test.Requests
{
    public class TestRequest
    {
        [Required]
        public int Transaction { get; set; }
    }
}
