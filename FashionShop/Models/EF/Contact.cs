using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FashionShop.Models.EF
{
    [Table("Tb_Contact")]
    public class Contact: CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name cannot be blank")]
        [StringLength(150, ErrorMessage = "Must not exceed 150 characters")]
        public string Name { get; set; }
        [StringLength(150, ErrorMessage = "Must not exceed 150 characters")]
        public string Email { get; set; }
        public string Website { get; set; }
        [StringLength(4000)]
        public string Message { get; set; }
        public bool Isread { get; set; }

    }
}