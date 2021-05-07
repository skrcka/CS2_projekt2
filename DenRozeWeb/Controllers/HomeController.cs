using DenRozeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DenRozeWeb.Services;
using System.Text;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DenRozeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ItemService itemService;
        private OrderService orderService;
        private BillItemService billItemService;
        private UserService userService;
        private UserModel? activeUser;

        public HomeController(ILogger<HomeController> logger, UserService userService, ItemService itemService, OrderService orderService, BillItemService billItemService)
        {
            _logger = logger;
            this.itemService = itemService;
            this.orderService = orderService;
            this.billItemService = billItemService;
            this.userService = userService; 
        }

        public IActionResult Index()
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public bool CheckLogin()
        {
            if (activeUser != null)
                return true;
            return false;
        }

        public IActionResult Login()
        {
            if (CheckLogin())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            activeUser = null;
            var data = Encoding.UTF8.GetBytes("");
            this.HttpContext.Session.Set("user", data);
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public IActionResult Login(UserModel um)
        {
            var users = userService.GetAllUsers();
            foreach (var user in users)
            {
                if (um.Login == user.Login)
                {
                    if (um.Password == user.Password)
                    {
                        activeUser = user;
                        var data = Encoding.UTF8.GetBytes($"{um.Login},{um.Password}");
                        this.HttpContext.Session.Set("user", data);
                    }
                }
            }
            if (activeUser != null)
                um = activeUser;
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return Login();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult OrderList()
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Orders = orderService.GetOrderByUserId(activeUser.UID);
            return View();
        }

        public IActionResult OrderDetail(int id)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login", "Home");
            }
            OrderDetailModel orm = new OrderDetailModel();
            orm.Order = orderService.GetOrderByOrderId(id)[0];
            orm.BillItems = billItemService.GetBillItemByOrder(id);
            orm.Items = new ObservableCollection<ItemModel>();
            foreach(var bi in orm.BillItems)
            {
                var items = itemService.GetItemById(bi.IID);
                if (items.Count > 0)
                    orm.Items.Add(itemService.GetItemById(bi.IID)[0]);
                else
                    orm.Items.Add(itemService.GetAllItems()[0]);
            }
            ViewBag.Order = orm;
            return View();
        }

        public IActionResult ItemList()
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Items = itemService.GetAllItems();
            return View();
        }

        [HttpPost]
        public IActionResult ItemList(string? Search)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login", "Home");
            }
            if(Search == "" ||  Search == null)
            {
                return ItemList();
            }
            ViewBag.Items = itemService.GetAllItems().Where(x => x.Name.Contains(Search));
            return View();
        }

        public IActionResult ItemDetail(int id)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Item = itemService.GetItemById(id)[0];
            return View();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ObservableCollection<ItemModel> items = new ObservableCollection<ItemModel>();
            if (this.HttpContext.Session.TryGetValue("basket", out byte[] data))
            {
                List<int> list = new List<int>();
                string txt = Encoding.UTF8.GetString(data);
                if(txt != "")
                    list = txt.Split(',').Select(x => int.Parse(x)).ToList();
                foreach (var i in list)
                {
                    items.Add(itemService.GetItemById(i)[0]);
                }
            }
            ViewBag.Basket = items;
            
            if (this.HttpContext.Session.TryGetValue("user", out byte[] data2))
            {
                List<string> list = new List<string>();
                string txt = Encoding.UTF8.GetString(data2);
                if (txt != "")
                {
                    list = txt.Split(',').ToList();
                    var users = userService.GetAllUsers();
                    foreach (var user in users)
                    {
                        if (list[0] == user.Login && list[1] == user.Password)
                        {
                            activeUser = user;
                        }
                    }
                }    
            }

            if (activeUser != null)
            {
                ViewBag.ActiveUser = activeUser.Full_name;
            }
            else
            {
                ViewBag.ActiveUser = "Login";
            }
            base.OnActionExecuting(context);
        }

        public IActionResult Basket()
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login", "Home");
            }
            var model = new BasketModel();
            model.BillItems = new ObservableCollection<BillItemModel>();
            model.User = activeUser;
            for (int i = 0; i < ViewBag.Basket.Count; i++)
            {
                model.BillItems.Add(new BillItemModel { BIID = 0, IID = ViewBag.Basket[i].IID, Count = 1 });
            }
            return View(model);
        }

        public IActionResult UserDetail()
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login", "Home");
            }
            var editUser = activeUser;
            editUser.Password = "";
            return View(editUser);
        }

        [HttpPost]
        public IActionResult UserDetail(UserModel um)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                if (um.Password == activeUser.Password)
                    userService.UpdateUser(um.UID, um.Password, um.Full_name, um.Phone, um.Email, um.Address);
                return RedirectToAction("UserDetail", "Home");
            }
            return View();
        }

        public IActionResult OrderSuccess(int id)
        {
            ViewBag.SuccessOID = id;
            return View();
        }

        [HttpPost]
        public IActionResult Basket(BasketModel bm)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login", "Home");
            }
            bm.User = activeUser;
            bm.Order.Created_at = DateTime.Now;
            if(bm.BillItems.Count < 1)
            {
                ModelState.AddModelError("No Items", "No Items in Order");
            }
            if (ModelState.IsValid) {
                int newOrderID = decimal.ToInt32(orderService.InsertOrder(bm.Order.Note, bm.Order.Created_at, DateTime.Now, bm.User.UID));
                foreach (var bi in bm.BillItems)
                {
                    billItemService.InsertBillItem(bi.Count, bi.IID, null, newOrderID);
                }
                var data = Encoding.UTF8.GetBytes("");
                this.HttpContext.Session.Set("basket", data);
                return RedirectToAction("OrderSuccess", "Home", new { id = newOrderID });
            }

            return View();
        }

        public IActionResult AddToBasket(int id)
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login", "Home");
            }
            List<int> list;
            if (this.HttpContext.Session.TryGetValue("basket", out byte[] data))
            {
                string txt = Encoding.UTF8.GetString(data);
                list = txt.Split(',').Select(x => int.Parse(x)).ToList();
            }
            else
            {
                list = new List<int>();
            }
            list.Add(id);
            data = Encoding.UTF8.GetBytes(string.Join(',', list));
            this.HttpContext.Session.Set("basket", data);
            return RedirectToAction("ItemList", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
