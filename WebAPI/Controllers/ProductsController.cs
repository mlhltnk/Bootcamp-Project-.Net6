using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entitities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //IOC Container(Inversion of Control) --> WEPAPI'de singleton ile bunu hallettik.

        IProductService _productService;          //loosely coupled;gevşek bağımlılık=dependecy injection

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            //IProductService productService = new ProductManager(new EfProductDal());  -->Bu kısım yerine SOLID için loosely coupled yapıldı.
            var result = _productService.GetAll();
            if(result.Success)
            {
                return Ok(result);         //code:200 datayı,mesaj verir
            }

            return BadRequest(result);   //code:400 data ve mesaj verir
        }


        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
