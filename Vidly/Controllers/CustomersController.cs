using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        readonly Customers _customers = new Customers
        {
            //CustomerList = new List<Customer>
            //    {
            //        new Customer {Name = "John Smith", Id = 1},
            //        new Customer {Name = "Mary Williams", Id = 2}
            //    }
            CustomerList = new List<Customer>()
        };

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        [Route("Customers/Details/{id}")]
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).ToList().SingleOrDefault(i => i.Id == id);
            return customer == null ? (ActionResult) HttpNotFound() : View(customer);
        }
    }
}