﻿namespace HTTPServer.ByTheCake.Controllers
{
    using HTTPServer.ByTheCake.Data;
    using HTTPServer.ByTheCake.Helpers;
    using HTTPServer.ByTheCake.Models;
    using HTTPServer.ByTheCake.Models.Cakes;
    using HTTPServer.Server.Http.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CakesController : Controller
    {
        private readonly CakesData cakesData;

        public CakesController()
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse Add()
        {
            this.ViewData["showResult"] = "none";

            return this.FileViewResponse(@"Cakes\Add");
        }

        public IHttpResponse Add(string name, string price)
        {
            var cake = new Cake
            {
                Name = name,
                Price = decimal.Parse(price)
            };

            this.cakesData.Add(name, price);

            this.ViewData["name"] = name;
            this.ViewData["price"] = price;
            this.ViewData["showResult"] = "block";

            return this.FileViewResponse(@"Cakes\Add");
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            var urlParameters = req.UrlParameters;

            this.ViewData["results"] = string.Empty;
            this.ViewData["search"] = string.Empty;

            if (urlParameters.ContainsKey("search"))
            {
                var searchTerm = urlParameters["search"];

                this.ViewData["search"] = searchTerm;

                var savedCakesDivs = this.cakesData
                    .All()
                    .Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()))
                    .Select(c => $"<div>{c.Name} - ${c.Price:f2} <a href=/Shopping/Add/{c.Id}?SearchTerm={searchTerm}>Order</a></div>");

                var results = string.Join(Environment.NewLine, savedCakesDivs);

                this.ViewData["results"] = results;
            }

            this.ViewData["showCart"] = "none";

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (shoppingCart.Orders.Any())
            {
                var totalProducts = shoppingCart.Orders.Count;
                var totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";
            }

            return this.FileViewResponse(@"Cakes\Search");
        }
    }
}
