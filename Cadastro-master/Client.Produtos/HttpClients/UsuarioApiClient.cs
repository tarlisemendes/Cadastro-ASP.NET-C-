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
    public class UsuarioApiClient
    {
       
            private readonly HttpClient _httpClient;
            private readonly IHttpContextAccessor _acessor;

            public UsuarioApiClient(HttpClient client, IHttpContextAccessor accessor)
            {
                _httpClient = client;
                _acessor = accessor;
            }

        
            public async Task<UsuarioView> GetAsync(int id)
            {
                
                var resposta = await _httpClient.GetAsync($"usuario/{id}");
                resposta.EnsureSuccessStatusCode();
                return await resposta.Content.ReadAsAsync<UsuarioView>();
            }

            public async Task DeleteAsync(int id)
            {
                
                var resposta = await _httpClient.DeleteAsync($"usuario/{id}");
                resposta.EnsureSuccessStatusCode();
                if (resposta.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    throw new InvalidOperationException("Código de Status Http 204 esperado!");
                }
            }

            public async Task PostAsync(UsuarioView usuario)
            {
                var jsonInString = JsonConvert.SerializeObject(usuario);
                var resposta = await _httpClient.PostAsync("usuario", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                resposta.EnsureSuccessStatusCode();
                if (resposta.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new InvalidOperationException("Código de Status Http 201 esperado!");
                }
            }

            public async Task PutAsync(UsuarioView usuario)
            {
                var jsonInString = JsonConvert.SerializeObject(usuario);
                var resposta = await _httpClient.PutAsync($"usuario/{usuario.Id}", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                resposta.EnsureSuccessStatusCode();
                if (resposta.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new InvalidOperationException("Código de Status Http 200 esperado!");
                }

            }

        public async Task<PaginacaoView<UsuarioView>> GetAsync()
        {
         

            var resposta = await _httpClient.GetAsync($"usuario");
            resposta.EnsureSuccessStatusCode();

            return await resposta.Content.ReadAsAsync<PaginacaoView<UsuarioView>>();
        }



    }
}
