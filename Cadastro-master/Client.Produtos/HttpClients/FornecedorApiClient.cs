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
    public class FornecedorApiClient
    {
            private readonly HttpClient _httpClient;
            private readonly IHttpContextAccessor _acessor;

            public FornecedorApiClient(HttpClient client, IHttpContextAccessor accessor)
            {
                _httpClient = client;
                _acessor = accessor;
            }

            public async Task<FornecedorView> GetAsync(int id)
            {
               
                var resposta = await _httpClient.GetAsync($"fornecedor/{id}");
                resposta.EnsureSuccessStatusCode();
                return await resposta.Content.ReadAsAsync<FornecedorView>();
            }

            public async Task DeleteAsync(int id)
            {
               
                var resposta = await _httpClient.DeleteAsync($"fornecedor/{id}");
                resposta.EnsureSuccessStatusCode();
                if (resposta.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    throw new InvalidOperationException("Código de Status Http 204 esperado!");
                }
            }

            public async Task PostAsync(FornecedorView fornecedor)
            {
                var jsonInString = JsonConvert.SerializeObject(fornecedor);
                var resposta = await _httpClient.PostAsync("fornecedor", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                resposta.EnsureSuccessStatusCode();
                if (resposta.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new InvalidOperationException("Código de Status Http 201 esperado!");
                }
            }

            public async Task PutAsync(FornecedorView fornecedor)
            {
                var jsonInString = JsonConvert.SerializeObject(fornecedor);
                var resposta = await _httpClient.PutAsync($"fornecedor/{fornecedor.Id}", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                resposta.EnsureSuccessStatusCode();
                if (resposta.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new InvalidOperationException("Código de Status Http 200 esperado!");
                }

            }
        

        public async Task<PaginacaoView<FornecedorView>> GetAsync()
        {
            

            var resposta = await _httpClient.GetAsync($"fornecedor");
            resposta.EnsureSuccessStatusCode();

            return await resposta.Content.ReadAsAsync<PaginacaoView<FornecedorView>>();
        }


    }
}
