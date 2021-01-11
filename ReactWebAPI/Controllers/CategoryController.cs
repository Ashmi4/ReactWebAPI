using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReactWebAPI.Models;

namespace ReactWebAPI.Controllers
{
    
        [RoutePrefix("Api/Shop")]
        public class CategoryController : ApiController
        {
            ShopEntities objEntity = new ShopEntities();

            [HttpGet]
            [Route("GetCategory")]
            public IQueryable<Category> GetCategory()
            {
                try
                {
                    return objEntity.Categories;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            [HttpGet]
            [Route("GetCategoryDetailsById/{catId}")]
            public IHttpActionResult GetCategoryById(string catId)
            {
                Category objUser = new Category();
                int ID = Convert.ToInt32(catId);
                try
                {
                    objUser = objEntity.Categories.Find(ID);
                    if (objUser == null)
                    {
                        return NotFound();
                    }

                }
                catch (Exception)
                {
                    throw;
                }

                return Ok(objUser);
            }

            [HttpPost]
            [Route("InsertCategoryDetails")]
            public IHttpActionResult PostCategory(Category data)
            {
                string message = "";
                if (data != null)
                {

                    try
                    {
                        objEntity.Categories.Add(data);
                        int result = objEntity.SaveChanges();
                        if (result > 0)
                        {
                            message = "Category has been successfully added";
                        }
                        else
                        {
                            message = "faild";
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                return Ok(message);
            }

            [HttpPut]
            [Route("UpdateCategoryDetails")]
            public IHttpActionResult PutCategoryMaster(Category change)
            {
                string message = "";
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    Category objUser = new Category();
                    objUser = objEntity.Categories.Find(change.C_Id);
                    if (objUser != null)
                    {
                        objUser.C_Name = change.C_Name;
                        objUser.C_Description = change.C_Description;
                        objUser.C_Active = change.C_Active;

                    }

                    int result = objEntity.SaveChanges();
                    if (result > 0)
                    {
                        message = "Category has been successfully updated";
                    }
                    else
                    {
                        message = "faild";
                    }

                }
                catch (Exception)
                {
                    throw;
                }

                return Ok(message);
            }

            [HttpDelete]
            [Route("DeleteCategoryDetails/{id}")]
            public IHttpActionResult DeleteCategoryDelete(int id)
            {
                string message = "";
                Category data = objEntity.Categories.Find(id);
                if (data == null)
                {
                    return NotFound();
                }

                objEntity.Categories.Remove(data);
                int result = objEntity.SaveChanges();
                if (result > 0)
                {
                    message = "Category has been successfully deleted";
                }
                else
                {
                    message = "faild";
                }

                return Ok(message);
            }
        [HttpGet]
        [Route("GetShopDetails")]
        public IHttpActionResult GetShop()
        {

            List<Category> lcategorydetail = objEntity.Categories.ToList();
            List<Product> lproduct = objEntity.Products.ToList();
            var query = from c in lcategorydetail
                        join p in lproduct on c.C_Id equals p.C_Id into table
                        from p in table.DefaultIfEmpty()
                        select new JoinClass { getcategory = c, getproduct = p };
            return Ok(query);


        }

        [HttpGet]
        [Route("GetCategoryProduct/{catId}")]
        public IHttpActionResult GetCategoryProduct(string catId)
        {
            objEntity.Configuration.ProxyCreationEnabled = false;
            int ID = Convert.ToInt32(catId);
            List<Category> lcategorydetail = objEntity.Categories.ToList();
            List<Product> lproduct = objEntity.Products.ToList();
            var query = from p in lproduct
                        join c in lcategorydetail on p.C_Id equals c.C_Id into table
                        from c in table
                        where p.C_Id == ID
                        select new JoinClass { getcategory = c, getproduct = p };
            
            return Ok(query);

        }

        //Products

        [HttpGet]
        [Route("GetProduct")]
        public IQueryable<Product> GetProduct()
        {
            try
            {
                return objEntity.Products;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetProductDetailsById/{prodId}")]
        public IHttpActionResult GetProductById(string prodId)
        {
            Product objUser = new Product();
            int ID = Convert.ToInt32(prodId);
            try
            {
                objUser = objEntity.Products.Find(ID);
                if (objUser == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(objUser);
        }

        [HttpPost]
        [Route("InsertProductDetails")]
        public IHttpActionResult PostProduct(Product data)
        {
            string message = "";
            if (data != null)
            {

                try
                {
                    objEntity.Products.Add(data);
                    int result = objEntity.SaveChanges();
                    if (result > 0)
                    {
                        message = "Product has been successfully added";
                    }
                    else
                    {
                        message = "faild";
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return Ok(message);
        }

        [HttpPut]
        [Route("UpdateProductDetails")]
        public IHttpActionResult PutProductMaster(Product data)
        {
            string message = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Product objUser = new Product();
                objUser = objEntity.Products.Find(data.P_Id);
                if (objUser != null)
                {
                    objUser.P_Name = data.P_Name;
                    objUser.P_Cost = data.P_Cost;
                    objUser.P_Description = data.P_Description;
                    objUser.P_Active = data.P_Active;
                    objUser.C_Id = data.C_Id;

                }

                int result = objEntity.SaveChanges();
                if (result > 0)
                {
                    message = "Product has been successfully updated";
                }
                else
                {
                    message = "faild";
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(message);
        }

        [HttpDelete]
        [Route("DeleteProductDetails/{id}")]
        public IHttpActionResult DeleteProductDelete(int id)
        {
            string message = "";
            Product data = objEntity.Products.Find(id);
            if (data == null)
            {
                return NotFound();
            }

            objEntity.Products.Remove(data);
            int result = objEntity.SaveChanges();
            if (result > 0)
            {
                message = "Product has been successfully deleted";
            }
            else
            {
                message = "faild";
            }

            return Ok(message);
        }

    }
}

