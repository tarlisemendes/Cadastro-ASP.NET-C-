using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Produtos.DAL;
using API.Produtos.Models;
using Microsoft.AspNetCore.Mvc;


namespace API.Produtos.Controllers
{
    [Produces("application/json")]
    [Route("api/fornecedor")]
    [ApiController]
    public class FornecedorController : Controller
    {
        private readonly IRepository<Fornecedor> _repository;


        public FornecedorController(IRepository<Fornecedor> repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(FornecedorPaginado))]
        [ProducesResponseType(statusCode: 500)]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Get([FromQuery] Paginacao paginacao)
        {

            var lista = _repository.All;

            var listaPaginada = FornecedorPaginado.From(paginacao, lista);

            if (listaPaginada.Resultado.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaPaginada);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(Fornecedor))]
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
        public IActionResult Post([FromBody]Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                var _fornecedor = new Fornecedor
                {
                    Email = fornecedor.Email,
                    Nome = fornecedor.Nome,
                    Telefone = fornecedor.Telefone,
                    Endereco = fornecedor.Endereco,
                    CNPJ = fornecedor.CNPJ,
                    ProdutoId = fornecedor.ProdutoId
                };

                _repository.Incluir(_fornecedor);

                //var uri = Url.Action("Recuperar", new { id = _produto.Id });
                return Created("201", _fornecedor); //201
            }
            return BadRequest();

        }

        [HttpPut("{id}")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 500)]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Put(int id, [FromBody]Fornecedor produto)
        {
            if (ModelState.IsValid)
            {
                var data = _repository.Find(id);
                if (data == null)
                {
                    return NotFound();
                }
                data.Nome = produto.Nome;
                data.Email = produto.Email;
                data.Telefone = produto.Telefone;
                data.Endereco = produto.Endereco;
                data.CNPJ = produto.CNPJ;

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
