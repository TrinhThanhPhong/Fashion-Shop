using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Models.EF
{
    [Table("Tb_Product")]
    public class Product:CommonAbstract
    {
        public Product()
        {
            this.ProductImage = new HashSet<ProductImage>();
            this.OrderDetail = new HashSet<OrderDetail>();
            this.Reviews = new HashSet<ReviewProduct>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [StringLength(250)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập loại sản phẩm")]
        public string ProductType { get; set; }
        [StringLength(250)]
        public string Alias { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã sản phẩm")]
        [StringLength(50)]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thương hiệu sản phẩm")]
        public string ProductBrand { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng sản phẩm")]
        public int SizeXS { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng sản phẩm")]
        public int SizeS { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng sản phẩm")]
        public int SizeM { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng sản phẩm")]
        public int SizeL { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng sản phẩm")]
        public int SizeXL { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng sản phẩm")]
        public int SizeXXL { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng sản phẩm")]
        public int SizeXXXL { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        [StringLength(250)]

        //[Required(ErrorMessage = "Vui lòng thêm ảnh sản phẩm")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá gốc sản phẩm")]
        public decimal OriginalPrice { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá khuyến mãi sản phẩm")]
        public decimal PriceSale { get; set; }
        public int Quantity { get; set; }
        public int ViewCount { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsFeature { get; set; }
        public bool IsHot { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục sản phẩm")]
        public int ProductCategoryId { get; set; }
        [StringLength(250)]
        public string SeoTitle { get; set; }
        [StringLength(500)]
        public string SeoDescription { get; set; }
        [StringLength(250)]
        public string SeoKeywords { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<ReviewProduct> Reviews { get; set; }
    }
}