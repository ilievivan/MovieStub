using MovieStub.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieStub.Web.Controllers
{
    public class ShoppingCardController : Controller
    {

        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCardController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._shoppingCartService.getShoppingCartInfo(userId));
        }

        public IActionResult DeleteFromShoppingCart(Guid id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.deleteMovieFromSoppingCart(userId, id);
            
            if(result)
            {
                return RedirectToAction("Index", "ShoppingCard");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCard");
            }
        }

        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.order(userId);

            if(result)
            {
                return RedirectToAction("Index", "ShoppingCard");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCard");
            }
        }
    }
}
