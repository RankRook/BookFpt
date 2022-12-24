using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookFpt.Helpers;
using BookFpt.Models;
using BookFpt.Data;
using BookFpt.ViewModels;
using Microsoft.AspNetCore.Authorization;
using BookFpt.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace FptBookOke.Controllers
{
    public class CartController : Controller
    {
        private readonly SampleAppContext _context;
        private readonly UserManager<SampleAppUser> _userManager;

        public CartController(
            SampleAppContext context,
            UserManager<SampleAppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<CartItemVM> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItemVM>>("cart");
                if (data == null)
                {
                    data = new List<CartItemVM>();
                }
                return data;
            }
        }

        public IActionResult Index()
        {
            return View(Carts);
        }


        //[Authorize(Policy = "roleOwner")]
        //public HashSet<OrderDetail> AddOrderDetails(int Id)
        //{


        //    var carts = Carts;
        //    var orderDetails = new HashSet<OrderDetail>();
        //    foreach (var item in carts)
        //    {
        //        orderDetails.Add(new OrderDetail
        //        {
        //            OrderId = Id,
        //            BookID = item.BookId,
        //            Price = item.Price,
        //            Quantity = item.Quantity,
        //        });
        //    }
        //    return orderDetails;
        //}
        [Authorize(Policy = "roleUser")]
        public async Task<ActionResult> CheckOut(string address, string note)
        {
            var user = User.Claims.ToArray();
            Random rand = new Random(100);
            int id = rand.Next(000000000, 999999999);
            var order = new Order
            {
                //Id = id,
                UserId = user[0].Value,
                Date = DateTime.Now,
                FullName = user[5].Value,
                Status = 1,
                Note = note,
                Address= address,

            };
            var carts = Carts;
            foreach (var item in carts)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    BookID = item.BookId,
                    Price = item.Price,
                    Quantity = item.Quantity,
                });
            }

            await _context.Order.AddAsync(order);
            _context.SaveChanges();
            HttpContext.Session.Remove("cart");
            //HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }



        public IActionResult AddToCart(int id, int Quantity, string type)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.BookId == id);

            if (item == null)//chưa có
            {
                var book = _context.Book.SingleOrDefault(p => p.Id == id);
                item = new CartItemVM
                {
                    BookId = id,
                    BookName = book.Name,
                    Price = book.Price,
                    Quantity = Quantity,
                    Image = book.BookFileName
                };
                myCart.Add(item);
            }
            else
            {
                item.Quantity += Quantity;
            }
            HttpContext.Session.Set("cart", myCart);

            if (type == "ajax")
            {
                return Json(new
                {
                    Quantity = Carts.Sum(c => c.Quantity)
                });
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveInCart(int id, int Quantity)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.BookId == id);
            myCart.Remove(item);
            HttpContext.Session.Set("cart", myCart);


            return Json(new
            {
                quantity = Carts.Sum(c => c.Quantity) - Quantity,
            });
        }





        public IActionResult PlusCart(int id, int Quantity, string type = "Normal")
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.BookId == id);
            item.Quantity += Quantity;
            
            HttpContext.Session.Set("cart", myCart);

            if (type == "ajax")
            {
                return Json(new
                {
                    Cart = myCart,
                    itemQuantity = item.Quantity,
                    Quantity = Carts.Sum(c => c.Quantity),
                    Sum = item.Price * item.Quantity,
                    Total = Carts.Sum(c => c.Quantity * c.Price)

                });
            }
            return RedirectToAction("Index");
        }



        public IActionResult MinusCart(int id, int Quantity, string type)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.BookId == id);
            //if(Quantity <= 0)
            //{
            //    throw new Exception("No Book");
            //}
            item.Quantity -= Quantity;

            HttpContext.Session.Set("cart", myCart);

            if (type == "ajax")
            {
                return Json(new
                {
                    itemQuantity = item.Quantity,
                    Quantity = Carts.Sum(c => c.Quantity) - 1,
                    Sum = item.Price * item.Quantity,
                    Total = Carts.Sum(c => c.Quantity * c.Price)
                });
            }
            return RedirectToAction("Index");
        }
    }
}