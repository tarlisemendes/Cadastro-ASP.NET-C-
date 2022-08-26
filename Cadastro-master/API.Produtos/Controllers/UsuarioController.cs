using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Produtos.DAL;
using API.Produtos.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Produtos.Controllers
{
    [Produces("application/json")]
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IRepository<Usuario> _repository;


        public UsuarioController(IRepository<Usuario> repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(UsuarioPaginado))]
        [ProducesResponseType(statusCode: 500)]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Get([FromQuery] Paginacao paginacao)
        {

            var lista = _repository.All;

            var listaPaginada = UsuarioPaginado.From(paginacao, lista);

            if (listaPaginada.Resultado.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaPaginada);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(Usuario))]
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
        public IActionResult Post([FromBody]Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var _usuario = new Usuario
                {
                    Email = usuario.Email,
                    Nome = usuario.Nome,
                    Telefone = usuario.Telefone,
                    Endereco = usuario.Endereco,
                    CPF = usuario.CPF
                };

                _repository.Incluir(_usuario);

                //var uri = Url.Action("Recuperar", new { id = _produto.Id });
                return Created("201", _usuario); //201
            }
            return BadRequest();

        }

        [HttpPut("{id}")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 500)]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Put(int id, [FromBody]Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var data = _repository.Find(id);
                if (data == null)
                {
                    return NotFound();
                }
                data.Nome = usuario.Nome;
                data.Email = usuario.Email;
                data.Telefone = usuario.Telefone;
                data.Endereco = usuario.Endereco;
                data.CPF = usuario.CPF;

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
