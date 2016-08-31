﻿using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class OrdersController : ApiController
    {
        private NORTHWNDEntities _db;

        public OrdersController()
        {
            _db = new NORTHWNDEntities();
        }

        //GET: api/orders/between/01.01.1997/12.31.1997
        [HttpGet, Route("api/orders/between/{startDate}/{endDate}"), ResponseType(typeof(IQueryable<Order>))]
        public IHttpActionResult GetOrdersBetween(DateTime startDate, DateTime endDate)
        {
            var result = _db.Orders.Where(o => o.RequiredDate >= startDate && o.RequiredDate <= endDate && o.Freight < 100);

            return Ok(result);
           // throw new NotImplementedException("Write a query to return all orders with required dates between Jan 1, 1997 
           //and Dec 31, 1997 with freight under 100 units.");
        }

        //GET: api/orders/reports/purchase
        [HttpGet, Route("api/orders/reports/purchase"), ResponseType(typeof(IQueryable<object>))]
        public IHttpActionResult PurchaseReport()
        {
            var result = from product in _db.Products

                         select new
                         {
                             ProductName = product.ProductName,
                             QuantityPurchased = product.Order_Details.Sum(od => od.Quantity)
                         } into anon
                         orderby anon.QuantityPurchased descending
                         select anon;

            return Ok(result);
                            

            // See this blog post for more information about projecting to anonymous objects. https://blogs.msdn.microsoft.com/swiss_dpe_team/2008/01/25/using-your-own-defined-type-in-a-linq-query-expression/
            //throw new NotImplementedException("Write a query to return an array of anonymous objects that have two properties. A Product property
            //and the quantity ordered for that product labelled as 'QuantityPurchased' ordered by QuantityPurchased in descending order.");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}