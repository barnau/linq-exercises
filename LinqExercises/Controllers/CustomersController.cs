using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class CustomersController : ApiController
    {
        private NORTHWNDEntities _db;

        public CustomersController()
        {
            _db = new NORTHWNDEntities();
        }

        // GET: api/customers/city/London
        [HttpGet, Route("api/customers/city/{city}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAll(string city)
        {
            var resultSet = from cust in _db.Customers
                            where cust.City.Equals(city)
                            select cust;

            return Ok(resultSet);
        }

        // GET: api/customers/mexicoSwedenGermany
        [HttpGet, Route("api/customers/mexicoSwedenGermany"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAllFromMexicoSwedenGermany()
        {
            var resultSet = from cust in _db.Customers
                            where cust.Country.Equals("Mexico")
                            || cust.Country.Equals("Sweden")
                            || cust.Country.Equals("Germany")
                            select cust;
            Console.WriteLine(resultSet);
            return Ok(resultSet);
        }

        // GET: api/customers/shippedUsing/Speedy Express
        [HttpGet, Route("api/customers/shippedUsing/{shipperName}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersThatShipWith(string shipperName)
        {


            var resultSet = _db.Customers.Where(c => c.Orders.Any(o => o.Shipper.CompanyName == shipperName));

           


            return Ok(resultSet);





            throw new NotImplementedException("Write a query to return all customers with orders that shipped using the given shipperName.");
        }

        // GET: api/customers/withoutOrders
        [HttpGet, Route("api/customers/withoutOrders"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersWithoutOrders()
        {
            
            var resultSet = _db.Customers.Where(c => c.Orders.Count() == 0 );
            return Ok(resultSet);
            
           // throw new NotImplementedException("Write a query to return all customers with no orders in the Orders table.");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
