using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    // api/Product/ ... 로 시작 
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Route("[action]")]
    public class ProductController : Controller
    {

        // List<MockProduct> products;
        // MockProduct product;
        Database db; 
        
        public ProductController(){
        //    products = new List<MockProduct>();
        //    product = new MockProduct();
            db = new Database();
        }


        //https://localhost:7130/api/Product/GetProductList 
        [HttpGet]
        public List<MockProduct> GetProductList()
        {
            //List<MockProduct> mockProducts = new List<MockProduct>();   
            MockProduct product = new MockProduct();
            
            product.ID = 1;
            product.ProductName = "Product 1";
            //products.Add(product); 
            db.Add(product);
            //return products;
            return db.Print();
        }


        //http://localhost:7130/api/Product/UpdateProduct
         [HttpPost]
        public IActionResult UpdateProduct([FromBody] MockProduct mockProduct)
        {
            //products.Add(mockProduct);
            db.Add(mockProduct);
            //return Ok();
            return Ok();
        }



        //[HttpPost]
        //public MockProduct UpdateProduct([FromBody] MockProduct mockProduct)
        //{
        //    //products.Add(mockProduct);
        //    db.Add(mockProduct);
        //    //return Ok();
        //    return mockProduct;
        //}


    }



}
