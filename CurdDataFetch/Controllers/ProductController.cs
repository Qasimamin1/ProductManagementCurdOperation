using CurdDataFetch.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CurdDataFetch.Controllers
{
    public class ProductController : Controller
    {
        //Data Source=QASIMZAILDAR;Initial Catalog=MvcCrudDB;Integrated Security=True
        // GET: Product
        // SqlConnection sqlConnection1 = new SqlConnection("Data Source=QASIMZAILDAR;Initial Catalog=MvcCrudDB;Integrated Security=true;");

        [HttpGet]
        public ActionResult Index()
        {
            string connectionString = @"Data Source=QASIMZAILDAR;Initial Catalog=MvcCrudDB;Integrated Security=true";
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {

                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT* FROM Product", sqlCon);
                sqlDa.Fill(dtblProduct);

            }
            return View(dtblProduct);


        }


        // GET: Product/Create

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductModel());


        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            string connectionString = @"Data Source=QASIMZAILDAR;Initial Catalog=MvcCrudDB;Integrated Security=true";
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO Product VALUES(@ProductName, @Price,@Count)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("ProductName", productModel.ProductName);
                sqlCmd.Parameters.AddWithValue("Price", productModel.Price);
                sqlCmd.Parameters.AddWithValue("Count", productModel.Count);
                sqlCmd.ExecuteNonQuery();


            }
            return RedirectToAction("Index");

        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            string connectionString = @"Data Source=QASIMZAILDAR;Initial Catalog=MvcCrudDB;Integrated Security=true";

            ProductModel productModel = new ProductModel();
            DataTable dtblProduct = new DataTable();
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                string query = "SELECT* FROM Product Where ProductID= @ProductID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, SqlCon);

                sqlDa.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                sqlDa.Fill(dtblProduct);

            }

            if (dtblProduct.Rows.Count == 1)
            {
                productModel.ProductID = Convert.ToInt32(dtblProduct.Rows[0][0].ToString());
                productModel.ProductName = dtblProduct.Rows[0][1].ToString();
                productModel.Price = Convert.ToDecimal(dtblProduct.Rows[0][2].ToString());
                productModel.Count = Convert.ToInt32(dtblProduct.Rows[0][3].ToString());

                return View(productModel);
            }
            else
                return View("Index");
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productModel)


        {
            string connectionString = @"Data Source=QASIMZAILDAR;Initial Catalog=MvcCrudDB;Integrated Security=true";
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE Product SET ProductName= @ProductName, Price= @Price, Count=@Count Where ProductID = @ProductID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("ProductID", productModel.ProductID);
                sqlCmd.Parameters.AddWithValue("ProductName", productModel.ProductName);
                sqlCmd.Parameters.AddWithValue("Price", productModel.Price);
                sqlCmd.Parameters.AddWithValue("Count", productModel.Count);
                sqlCmd.ExecuteNonQuery();



            }

            return RedirectToAction("Index");

        }
        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {

            string connectionString = @"Data Source=QASIMZAILDAR;Initial Catalog=MvcCrudDB;Integrated Security=true";
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM Product Where ProductID = @ProductID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProductID", id);

                sqlCmd.ExecuteNonQuery();



            }


            
                return RedirectToAction("Index");
            }



        }
    
}