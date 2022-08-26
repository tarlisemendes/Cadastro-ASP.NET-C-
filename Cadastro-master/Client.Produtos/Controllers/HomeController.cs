using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Produtos.HttpClients;
using Client.Produtos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace Client.Produtos.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly FornecedorApiClient _apiFornecedor;
        private readonly ProdutoApiClient _apiProduto;
        public HomeController(FornecedorApiClient apiFornecedor, ProdutoApiClient apiProduto)
        {
            _apiFornecedor = apiFornecedor;
            _apiProduto = apiProduto;
        }

        public async Task<IActionResult> Index()
        {
            var fornecedor = await _apiFornecedor.GetAsync();
            var produto = await _apiProduto.GetAsync();

            IList<FornecedorView> listafornecedor = fornecedor.Resultado;
            IList<ProdutoView> listaproduto = produto.Resultado;

            IList<FornecedorView> result = (from p in listaproduto
                                            join f in listafornecedor
                                                on p.Id equals f.ProdutoId
                                           select new FornecedorView()
                                           {
                                               Produto = p,
                                               Nome = f.Nome,
                                               Email = f.Email,
                                               Telefone = f.Telefone,
                                               CNPJ = f.CNPJ,
                                               Endereco = f.Endereco 
                                           }).ToList();



            return View(result);
        }


    }
    }
