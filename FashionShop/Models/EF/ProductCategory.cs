﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FashionShop.Models.EF
{
    [Table("Tb_ProductCategory")]
    public class ProductCategory: CommonAbstract
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [StringLength(150)]
        public string Title { get; set; }
        [StringLength(150)]
        public string Alias { get; set; }
        public string Description { get; set; }
        [StringLength(250)]

        public string Icon { get; set; }
        [StringLength(250)]

        public string SeoTitle { get; set; }
        [StringLength(250)]

        public string SeoDescription { get; set; }
        [StringLength(500)]

        public string SeoKeywords { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}