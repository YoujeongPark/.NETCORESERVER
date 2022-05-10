using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    // api/Product/ ... 로 시작 
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller
    {
        List<MockProduct> products;
        MockProduct product;
        public ProductController(){
            products = new List<MockProduct>();
            product = new MockProduct();
        }

        [HttpGet]
        public List<MockProduct> GetProductList()
        {
            //List<MockProduct> mockProducts = new List<MockProduct>();   
            // MockProduct product = new MockProduct();
            product.ID = 1;
            product.ProductName = "Product 1";
            products.Add(product);  
            return products;
        }
        

        [HttpPost]
        public IActionResult UpdateProduct([FromBody] MockProduct mockProduct)
        {   
            products.Add(mockProduct);
            return Ok();
        }

    }

    public class MockProduct
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
    }
}
