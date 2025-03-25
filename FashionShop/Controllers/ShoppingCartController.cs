using FashionShop.Models;
using FashionShop.Models.common;
using FashionShop.Models.EF;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ShoppingCartController()
        {
        }

        public ShoppingCartController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: ShoppingCart
        [AllowAnonymous]
        public ActionResult Index()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult CheckOutSuccess()
        {
            
            return View();
        }

        [AllowAnonymous]
        public ActionResult Partial_Item_Payment()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                return PartialView(cart.items);
            }
            return PartialView();
        }

        [AllowAnonymous]
        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                return PartialView(cart.items);
            }   
            return PartialView();
        }

        [AllowAnonymous]
        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart != null)
            {
                return Json(new { Count = cart.items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json( new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult Partial_CheckOut()
        {
            var user = UserManager.FindByNameAsync(User.Identity.Name).Result;
            if(user != null)
            {
                ViewBag.User = user;
            }
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel req)
        {
            var code = new { Success = false, Code = -1 };
            if(ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null)
                {
                    Order order = new Order();
                    order.CustomerName = req.CustomerName;
                    order.Phone = req.Phone;
                    order.Address = req.Address;
                    order.Email = req.Email;
                    cart.items.ForEach(x => order.orderDetails.Add(new OrderDetail { 
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price,
                        Size = x.Size
                    }));
                    foreach(var item in cart.items)
                    {
                        var product = db.Product.FirstOrDefault(p => p.Id == item.ProductId);
                        if(product != null)
                        {
                            if(item.Size == "XS")
                            {
                                product.SizeXS -= item.Quantity;
                            }
                            if (item.Size == "S")
                            {
                                product.SizeS -= item.Quantity;
                            }
                            if (item.Size == "M")
                            {
                                product.SizeM -= item.Quantity;
                            }
                            if (item.Size == "L")
                            {
                                product.SizeL -= item.Quantity;
                            }
                            if (item.Size == "XL")
                            {
                                product.SizeXL -= item.Quantity;
                            }
                            if (item.Size == "XXL")
                            {
                                product.SizeXXL -= item.Quantity;
                            }
                            if (item.Size == "XXXL")
                            {
                                product.SizeXXXL -= item.Quantity;
                            }
                            product.Quantity = product.SizeXS + product.SizeS + product.SizeM + product.SizeL + product.SizeXL + product.SizeXXL + product.SizeXXXL;
                        }
                        db.SaveChanges();
                    }
                    
                    order.TotalAmount = cart.items.Sum(x => (x.Price * x.Quantity));
                    order.TypePayment = req.TypePayment;
                    order.CreatedDate = DateTime.Now;
                    order.ModifiedDate = DateTime.Now;
                    order.CreatedBy = req.Phone;
                    Random rd = new Random();
                    order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    db.Order.Add(order);
                    db.SaveChanges();


                    var strProducts = "";
                    var thanhtien = decimal.Zero;
                    var tongtien = decimal.Zero;
                    foreach (var sp in cart.items)
                    {
                        strProducts += "<tr>";
                        strProducts += "<td>" + sp.ProductName + "</td>";
                        strProducts += "<td>" + sp.Size + "</td>";
                        strProducts += "<td>" + sp.Quantity + "</td>";
                        strProducts += "<td>" + FashionShop.Models.common.Common.FormatNumber(sp.TotalPrice, 0) + "</td>";
                        strProducts += "</tr>";
                        thanhtien += sp.Price * sp.Quantity;
                    }

                    tongtien = thanhtien;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/content/templates/send2.html"));
                    contentCustomer = contentCustomer.Replace("{{CodeOrder}}", order.Code);
                    contentCustomer = contentCustomer.Replace("{{Products}}", strProducts);
                    contentCustomer = contentCustomer.Replace("{{DateOrder}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentCustomer = contentCustomer.Replace("{{CustomerName}}", order.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{Phone}}", order.Phone);
                    contentCustomer = contentCustomer.Replace("{{Email}}", req.Email);
                    contentCustomer = contentCustomer.Replace("{{Address}}", order.Address);
                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}", FashionShop.Models.common.Common.FormatNumber(thanhtien, 0));
                    contentCustomer = contentCustomer.Replace("{{TongTien}}", FashionShop.Models.common.Common.FormatNumber(tongtien, 0));
                    FashionShop.Models.common.Common.SendMail("Sheepo", "Order #" + order.Code, contentCustomer.ToString(), req.Email);


                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/content/templates/send1.html"));
                    contentAdmin = contentAdmin.Replace("{{CodeOrder}}", order.Code);
                    contentAdmin = contentAdmin.Replace("{{Products}}", strProducts);
                    contentAdmin = contentAdmin.Replace("{{DateOrder}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentAdmin = contentAdmin.Replace("{{CustomerName}}", order.CustomerName);
                    contentAdmin = contentAdmin.Replace("{{Phone}}", order.Phone);
                    contentAdmin = contentAdmin.Replace("{{Email}}", req.Email);
                    contentAdmin = contentAdmin.Replace("{{Address}}", order.Address);
                    contentAdmin = contentAdmin.Replace("{{ThanhTien}}", FashionShop.Models.common.Common.FormatNumber(thanhtien, 0));
                    contentAdmin = contentAdmin.Replace("{{TongTien}}", FashionShop.Models.common.Common.FormatNumber(tongtien, 0));
                    FashionShop.Models.common.Common.SendMail("Sheepo", "Order #" + order.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);
                    cart.ClearCart();
                    code = new { Success = true, Code = 1 };
                    //return RedirectToAction("CheckOutSuccess");
                    //order.Email = req.CustomerName;
                }
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity, string size)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.Product.FirstOrDefault(x => x.Id == id);
            if(checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];

                if(cart == null)
                {
                    cart = new ShoppingCart();
                }

                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    CategoryName = checkProduct.ProductCategory.Title,
                    Alias = checkProduct.Alias,
                    Size = size,
                    Quantity = quantity
                };
                if (checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                {
                    item.ProductImg = checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault).Image;
                }
                item.Price = checkProduct.Price;
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = (decimal)checkProduct.PriceSale;
                }
                item.TotalPrice = item.Quantity * item.Price;
                //------------------------------------Moi nhat------------------------------------------------------
                var ItemHientai = cart.items.FirstOrDefault(x => x.ProductId == id && x.Size == size);
                int QuantityinCart = ItemHientai != null ? ItemHientai.Quantity : 0;

                int trongkho = 0;
                if (size == "S")
                {
                    trongkho = checkProduct.SizeS;
                }
                if (size == "M")
                {
                    trongkho = checkProduct.SizeM;
                }
                if (size == "L")
                {
                    trongkho = checkProduct.SizeL;
                }
                if (size == "XL")
                {
                    trongkho = checkProduct.SizeXL;
                }
                if (size == "XXL")
                {
                    trongkho = checkProduct.SizeXXL;
                }
                if (size == "XXXL")
                {
                    trongkho = checkProduct.SizeXXXL;
                }
                if (size == "XS")
                {
                    trongkho = checkProduct.SizeXS;
                }

                if (quantity == 0)
                {
                    code = new { Success = false, msg = "", code = -1, Count = cart.items.Count };
                    return Json(code);
                }

                if (QuantityinCart + quantity > trongkho)
                {
                    code = new { Success = true, msg = "Tổng số lượng sản phẩm trong giỏ hàng và số lượng bạn muốn thêm đã nhiều hơn số lượng trong kho chúng tôi có!", code = 1, Count = cart.items.Count };
                    return Json(code);
                }
                //-----------------------------------------------------------------------------------------------
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm vào giỏ hàng thành công!", code = 1, Count = cart.items.Count };
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult Delete (int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];

            if (cart != null)
            {
                var checkProduct = cart.items.FirstOrDefault( x => x.ProductId == id);
                if(checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = 1, Count = cart.items.Count };
                }
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart != null)
            {
                cart.ClearCart();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
    }
}