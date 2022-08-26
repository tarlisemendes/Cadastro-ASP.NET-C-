using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Produtos.Models;
using API.Produtos.DAL;
using Microsoft.AspNetCore.Mvc;
using API.Produtos.Services;


namespace API.Produtos.Controllers
{
    [Produces("application/json")]
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : Controller
    {
        //private readonly IService<Produto> _service;

        private readonly IRepository<Produto> _repository;


        public ProdutoController(IRepository<Produto> repository)
        {
            _repository = repository;
        }

        
        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(ProdutoPaginado))]
        [ProducesResponseType(statusCode: 500)]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Get([FromQuery] Paginacao paginacao)
        {

            var lista =  _repository.All;

            var listaPaginada = ProdutoPaginado.From(paginacao, lista);

            if (listaPaginada.Resultado.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaPaginada);
            
        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(Produto))]
        [ProducesResponseType(statusCode: 500)]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Get(int id)
        {
            var model = _repository.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return Json(model);
        }


        [HttpPost]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 500)]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Post([FromBody]Produto produto)
        {
            if (ModelState.IsValid)
            {
                var _produto = new Produto
                {
                    Codigo = produto.Codigo,
                    Nome = produto.Nome,
                    Valor = produto.Valor,
                    Quantidade = produto.Quantidade
                };

                _repository.Incluir(_produto);

                
                return Created("201", _produto); 
            }
            return BadRequest();

        }

        [HttpPut("{id}")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 500)]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Put(int id, [FromBody]Produto produto)
        {
            if (ModelState.IsValid)
            {
                var data = _repository.Find(id);
                if (data == null)
                {
                    return NotFound();
                }
                data.Nome = produto.Nome;
                data.Codigo = produto.Codigo;
                data.Valor = produto.Valor;
                data.Quantidade = produto.Quantidade;
                
                _repository.Alterar(data);
                return Ok(); //200
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 500)]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Delete(int id)
        {
            var model = _repository.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            _repository.Excluir(model);
            return NoContent(); //204
        }
    }
}
