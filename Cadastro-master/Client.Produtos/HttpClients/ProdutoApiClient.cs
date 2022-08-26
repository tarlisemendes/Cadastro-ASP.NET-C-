using Client.Produtos.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Produtos.HttpClients
{
    public class ProdutoApiClient
    {
            private readonly HttpClient _httpClient;
            private readonly IHttpContextAccessor _acessor;

            public ProdutoApiClient(HttpClient client, IHttpContextAccessor accessor)
            {
                _httpClient = client;
                _acessor = accessor;
            }

        public async Task<ProdutoView> GetAsync(int id)
        {
            
            var resposta = await _httpClient.GetAsync($"produto/{id}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<ProdutoView>();
        }

        public async Task DeleteAsync(int id)
        {
            
            var resposta = await _httpClient.DeleteAsync($"produto/{id}");
            resposta.EnsureSuccessStatusCode();
            if (resposta.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                throw new InvalidOperationException("Código de Status Http 204 esperado!");
            }
        }

        public async Task PostAsync(ProdutoView produto)
        {
            var jsonInString = JsonConvert.SerializeObject(produto);
            var resposta = await _httpClient.PostAsync("produto", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            resposta.EnsureSuccessStatusCode();
            if (resposta.StatusCode != System.Net.HttpStatusCode.Created)
            {
                throw new InvalidOperationException("Código de Status Http 201 esperado!");
            }
        }

        public async Task PutAsync(ProdutoView produto)
        {
            var jsonInString = JsonConvert.SerializeObject(produto);
            var resposta = await _httpClient.PutAsync($"produto/{produto.Id}", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            resposta.EnsureSuccessStatusCode();
            if (resposta.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new InvalidOperationException("Código de Status Http 200 esperado!");
            }

        }

        public async Task<PaginacaoView<ProdutoView>> GetAsync()
        {

            var resposta = await _httpClient.GetAsync($"produto");
            resposta.EnsureSuccessStatusCode();
            
            return await resposta.Content.ReadAsAsync<PaginacaoView<ProdutoView>>();
        }


        

    }
}

