using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SON.Models;
using SON.DAO;

namespace SON.Controllers
{
    public class MCVshoppingController : Controller
    {
        //
        // GET: /MCVshopping/
        MCVshoppingDAO dbo = new MCVshoppingDAO();
        DatabaseMVCshoppingEntities entity = new DatabaseMVCshoppingEntities();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(dbo.Showcategories());
        }
        public ActionResult ProductLists(int id)
        {
            return View(dbo.GetProductLists(id));
        }
        public ActionResult insertproduct()
        {
            ViewBag.list = Selectcategorise();
            return View();
        }
        [HttpPost]
        public ActionResult insertproduct(products product)
        {
            dbo.InsertProduct(product);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UpdateProductID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            ViewBag.list = Selectcategorise();
            products productID = dbo.Getproduct(id);
            return View(productID);
        }
        [HttpPost]
        public ActionResult EditProduct(products product)
        {
            dbo.Update(product);
            return RedirectToAction("ProductLists", "MCVshopping");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Editmember(int id)
        {
            ViewBag.list = Selectcategorise();
            customers customerID = dbo.Getcustomer(id);
            return View(customerID);
        }
        [HttpPost]
        public ActionResult Editmember(customers customer)
        {
            dbo.Updatemember(customer);
            return RedirectToAction("ProductLists", "MCVshopping");
        }

        public List<SelectListItem> Selectcategorise()
        {
            List<SelectListItem> listCategory = new List<SelectListItem>();
            List<categories> list = dbo.Getcategories();
            foreach (categories cd in list)
            {
                SelectListItem item = new SelectListItem();
                item.Text = cd.CategoryName;
                item.Value = cd.CategoryID.ToString();
                listCategory.Add(item);
            }
            return listCategory;
        }
     
        public ActionResult Login()
        {
            Session["CustomerID"] = null;
            Session["ContactName"] = null;
            Session["Status"] = null;

            return View();
        }
        [HttpPost]
        public ActionResult Login(customers Customers)
        {
            
            var chk = entity.customers.Where(x => x.ContactName == Customers.ContactName && x.Password == Customers.Password).FirstOrDefault();
            if (chk != null)
            {
                Session["CustomerID"] = chk.CustomerID.ToString();
                Session["ContactName"] = chk.ContactName.ToString();
                Session["Status"] = chk.Status;
                return RedirectToAction("Index");
            }
            return View(Customers);
        }

        [HttpGet]
        public ActionResult Regiters()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Regiters(customers Member)
        {
            dbo.save(Member);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Showproducts(int id)
        {
            return View(dbo.Showwheyproduct(id));
        }
        public ActionResult showmember()
        {
            return View(dbo.ShowMemberAll());
        }
        public ActionResult Showbays()
        {
            return View(dbo.ShowbaysAll());
        }
        [HttpGet]
        public ActionResult CartMember(int id)
        {
            return View(dbo.ShowbaysMember(id));
        }
        [HttpGet]
        public ActionResult CartProduct()
        {
            string date = Request["OrderDate"];
            int id = Convert.ToInt32(Request["CustomerID"]);
            string status = Request["status"];
            int ProductID = Convert.ToInt32(Request["ProductID"]);
            orders order = new orders();
            order.OrderDate = date;
            order.CustomerID = id;
            order.status = status;
            order.ProductID = ProductID;
            dbo.AddProductMember(order);
            return RedirectToAction("CartMember", new { id = Session["CustomerID"] });
        }
        [HttpGet]
        public ActionResult DelCart(int id)
        {
            dbo.DeleteCart(id);
            return RedirectToAction("CartMember", new { id = Session["CustomerID"] });
        }
        [HttpGet]
        public ActionResult DelCartHistory(int id)
        {
            dbo.DeleteCartHistorys(id);
            return RedirectToAction("History", new { id = Session["CustomerID"] });
        }
        [HttpGet]
        public ActionResult Delmem(int id)
        {
            dbo.Deletemember(id);
            return RedirectToAction("showmember", new { id = Session["CustomerID"] });
        }
        [HttpGet]
        public ActionResult Del(int id)
        {
            dbo.Delete(id);
            return RedirectToAction("ProductLists", new { id = Session["CustomerID"] });
        }
        public ActionResult Pay()
        {
            int count = entity.orders.Count(x => x.status == "0");
            for (int i = 0; i < count; i++)
            {
                dbo.Pay();
            }
            return RedirectToAction("History", new { id = Session["CustomerID"] });
        }
        [HttpGet]
        public ActionResult History(int id)
        {
            return View(dbo.History(id));
        }
        [HttpGet]
        public ActionResult SendMember(int id)
        {
            dbo.Send(id);
            return RedirectToAction("Showbays");
        }
        
	}
}