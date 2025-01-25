using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionShop.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Empty!!!")]
        public string CustomerName { get; set; }


        [Required(ErrorMessage = "Empty!!!")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Empty!!!")]
        public string Address { get; set; }
        public string Email { get; set; }
        public int TypePayment { get; set; }

    }
}