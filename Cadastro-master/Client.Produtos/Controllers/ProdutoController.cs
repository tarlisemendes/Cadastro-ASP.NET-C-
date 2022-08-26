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
    public class ProdutoController : Controller
    {
        private readonly ProdutoApiClient _api;

        public ProdutoController(ProdutoApiClient api)
        {
            _api = api;
        } 

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _api.PostAsync(model);
                }
                catch (System.Exception e)
                {
          
                    System.Console.WriteLine($"Erro: {e.Message}");
                    
                    throw;
                }
                return RedirectToAction("ReadAll", "Produto");
            }
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Read(int id)
        {
            var model = await _api.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll()
        {
            var model = await _api.GetAsync();
            foreach (var item in model.Resultado.ToList())
            {

            }


            return View(model.Resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _api.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProdutoView model)
        {
            if (ModelState.IsValid)
            {
                await _api.PutAsync(model);
                return RedirectToAction("ReadAll", "Produto");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProdutoView model)
        {

            if (ModelState.IsValid)
            {
                await _api.DeleteAsync(model.Id);
                return RedirectToAction("ReadAll", "Produto");
            }
            return View(model);
        }



    }
}
