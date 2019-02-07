using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using APIProdutos.Models;
using APIProdutos.Repository;

namespace APIProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            return ProdutosRepository.Listar();
        }

        private JsonResult GetJsonProdutos()
        {
            return new JsonResult(
                ProdutosRepository.Listar(),
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );
        }

        [HttpGet("sem-nulos")]
        public IActionResult GetRemocaoNulls()
        {
            return GetJsonProdutos();
        }

        [HttpGet("comprimir")]
        [MiddlewareFilter(typeof(CompressaoBrotliPipeline))]
        public IActionResult GetComCompressao()
        {
            return GetJsonProdutos();
        }
    }
}