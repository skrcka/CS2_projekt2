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
    public class ContentController : Controller
    {
        private ItemService itemService;
        private OrderService orderService;
        private BillItemService billItemService;
        private UserService userService;
        private UserModel? activeUser;

        public ContentController(UserService userService, ItemService itemService, OrderService orderService, BillItemService billItemService)
        {
            this.itemService = itemService;
            this.orderService = orderService;
            this.billItemService = billItemService;
            this.userService = userService; 
        }

        public IActionResult GetJson()
        {
            return new JsonResult(itemService.GetAllItems());
        }
    }
}
