using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SON.Models;

namespace SON.DAO
{
    public class MCVshoppingDAO
    {
        DatabaseMVCshoppingEntities entity = new DatabaseMVCshoppingEntities();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        public void save(customers customer)
        {
            entity.customers.Add(customer);
            entity.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buys"></param>
        public void Insertbuys(orders buys)
        {
            entity.orders.Add(buys);
            entity.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="insertproduct"></param>
        public void InsertProduct(products insertproduct)
        {
            entity.products.Add(insertproduct);
            entity.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editProducts"></param>
        public void Update(products editProducts)
        {
            var productDB = entity.products.Where(x => x.ProuctID == editProducts.ProuctID).FirstOrDefault();
            productDB.CategoryID = editProducts.CategoryID;
            productDB.ProductName = editProducts.ProductName;
            productDB.UnitsInStock = editProducts.UnitsInStock;
            productDB.Price = editProducts.Price;

            entity.Entry(productDB).State = System.Data.EntityState.Modified;
            entity.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deleteProductID"></param>
        public void Deleteproducts(int deleteProductID)
        {
            var del = entity.products.Where(x => x.ProuctID == deleteProductID).FirstOrDefault();
            entity.Entry(del).State = System.Data.EntityState.Deleted;
            entity.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeleteCustomerID"></param>
        public void Deletemember(int DeleteCustomerID)
        {
            var del = entity.customers.Where(x => x.CustomerID == DeleteCustomerID).FirstOrDefault();
            entity.Entry(del).State = System.Data.EntityState.Deleted;
            entity.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeleteOrderID"></param>
        public void DeleteOrder(int DeleteOrderID)
        {
            var dels = entity.orders.Where(x => x.OrderID == DeleteOrderID).FirstOrDefault();
            entity.Entry(dels).State = System.Data.EntityState.Deleted;
            entity.SaveChanges();
        }
        

        public List<categories> Getcategories()
        {
            List<categories> list = new List<categories>();
            list = entity.categories.ToList();
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Productid"></param>
        /// <returns></returns>
        public products Getproduct(int Productid)
        {
            return entity.products.Where(x => x.ProuctID == Productid).FirstOrDefault();
        }


        public List<categories> Showcategories()
        {
            List<categories> list = new List<categories>();
            list = entity.categories.ToList();
            return list;
        }


        public List<products> Showwheyproduct(int productid)
        {
            List<products> list = new List<products>();
            list = entity.products.Where(x => x.CategoryID == productid).ToList();
            return list;
        }
        public List<customers> ShowMemberAll()
        {
            List<customers> list = new List<customers>();
            list = entity.customers.ToList();
            return list;
        }
        public List<orders> ShowbaysAll()
        {
            List<orders> list = new List<orders>();
            list = entity.orders.Where(x => x.status != "0").ToList();
            return list;
        }
        public List<orders> ShowbaysMember(int id)
        {
            List<orders> list = new List<orders>();
            list = entity.orders.Where(x => x.status == "0" & x.CustomerID == id).ToList();
            return list;
        }
        public void AddProductMember(orders order)
        {
            entity.orders.Add(order);
            entity.SaveChanges();
        }
        public void DeleteCart(int id)
        {
            var dels = entity.orders.Where(x => x.OrderID == id).FirstOrDefault();
            entity.Entry(dels).State = System.Data.EntityState.Deleted;
            entity.SaveChanges();
        }
        public List<orders> History(int id)
        {
            List<orders> list = new List<orders>();
            list = entity.orders.Where(x => x.status != "0" & x.CustomerID == id).ToList();
            return list;
        }
        public void Pay()
        {
            orders order = new orders();
            var productDB = entity.orders.Where(x => x.status == "0").FirstOrDefault();
            productDB.status = "1";
            entity.Entry(productDB).State = System.Data.EntityState.Modified;
            entity.SaveChanges();
        }
        public void Send(int id)
        {
            orders order = new orders();
            var productDB = entity.orders.Where(x => x.OrderID == id).FirstOrDefault();
            productDB.status = "2";
            entity.Entry(productDB).State = System.Data.EntityState.Modified;
            entity.SaveChanges();
        }
    }
}