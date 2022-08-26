using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Produtos.Models
{
    public class Paginacao
    {
        private readonly int TAMANHO_PADRAO = 25;
        private int _pagina = 0;
        private int _tamanho = 0;

        public int Pagina
        {
            get
            {
                return (_pagina <= 0) ? 1 : _pagina;
            }
            set
            {
                _pagina = value;
            }
        }
        public int Tamanho
        {
            get
            {
                return (_tamanho <= 0) ? TAMANHO_PADRAO : _tamanho;
            }
            set
            {
                _tamanho = value;
            }
        }

        public int QtdeParaDescartar => Pagina > 0 ? Tamanho * (Pagina - 1) : Tamanho;
    }

    public class ProdutoPaginado
    {
        public int Total { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanhoPagina { get; set; }
        public int NumeroPagina { get; set; }
        public IList<Produto> Resultado { get; set; }
        public string Anterior { get; set; }
        public string Proximo { get; set; }

        public static ProdutoPaginado From(Paginacao parametros, IQueryable<Produto> origem)
        {
            if (parametros == null)
            {
                parametros = new Paginacao();
            }
            int totalItens = origem.Count();
            //260 itens / 25 itens por página >> 10,4 e seu teto é 11.
            int totalPaginas = (int)Math.Ceiling(totalItens / (double)parametros.Tamanho);
            bool temPaginaAnterior = (parametros.Pagina > 1);
            bool temProximaPagina = (parametros.Pagina < totalPaginas);
            return new ProdutoPaginado
            {
                Total = totalItens,
                TotalPaginas = totalPaginas,
                TamanhoPagina = parametros.Tamanho,
                NumeroPagina = parametros.Pagina,
                Resultado = origem.Skip(parametros.QtdeParaDescartar).Take(parametros.Tamanho).ToList(),
                Anterior = temPaginaAnterior
                    ? $"produto?pagina={parametros.Pagina - 1}&tamanho={parametros.Tamanho}"
                    : "",
                Proximo = temProximaPagina
                    ? $"produto?pagina={parametros.Pagina + 1}&tamanho={parametros.Tamanho}"
                    : ""
            };
        }
    }

    public class FornecedorPaginado
    {
        public int Total { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanhoPagina { get; set; }
        public int NumeroPagina { get; set; }
        public IList<Fornecedor> Resultado { get; set; }
        public string Anterior { get; set; }
        public string Proximo { get; set; }

        public static FornecedorPaginado From(Paginacao parametros, IQueryable<Fornecedor> origem)
        {
            if (parametros == null)
            {
                parametros = new Paginacao();
            }
            int totalItens = origem.Count();
            //260 itens / 25 itens por página >> 10,4 e seu teto é 11.
            int totalPaginas = (int)Math.Ceiling(totalItens / (double)parametros.Tamanho);
            bool temPaginaAnterior = (parametros.Pagina > 1);
            bool temProximaPagina = (parametros.Pagina < totalPaginas);
            return new FornecedorPaginado
            {
                Total = totalItens,
                TotalPaginas = totalPaginas,
                TamanhoPagina = parametros.Tamanho,
                NumeroPagina = parametros.Pagina,
                Resultado = origem.Skip(parametros.QtdeParaDescartar).Take(parametros.Tamanho).ToList(),
                Anterior = temPaginaAnterior
                    ? $"fornecedor?pagina={parametros.Pagina - 1}&tamanho={parametros.Tamanho}"
                    : "",
                Proximo = temProximaPagina
                    ? $"fornecedor?pagina={parametros.Pagina + 1}&tamanho={parametros.Tamanho}"
                    : ""
            };
        }
    }

    public class UsuarioPaginado
    {
        public int Total { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanhoPagina { get; set; }
        public int NumeroPagina { get; set; }
        public IList<Usuario> Resultado { get; set; }
        public string Anterior { get; set; }
        public string Proximo { get; set; }

        public static UsuarioPaginado From(Paginacao parametros, IQueryable<Usuario> origem)
        {
            if (parametros == null)
            {
                parametros = new Paginacao();
            }
            int totalItens = origem.Count();
            //260 itens / 25 itens por página >> 10,4 e seu teto é 11.
            int totalPaginas = (int)Math.Ceiling(totalItens / (double)parametros.Tamanho);
            bool temPaginaAnterior = (parametros.Pagina > 1);
            bool temProximaPagina = (parametros.Pagina < totalPaginas);
            return new UsuarioPaginado
            {
                Total = totalItens,
                TotalPaginas = totalPaginas,
                TamanhoPagina = parametros.Tamanho,
                NumeroPagina = parametros.Pagina,
                Resultado = origem.Skip(parametros.QtdeParaDescartar).Take(parametros.Tamanho).ToList(),
                Anterior = temPaginaAnterior
                    ? $"usuario?pagina={parametros.Pagina - 1}&tamanho={parametros.Tamanho}"
                    : "",
                Proximo = temProximaPagina
                    ? $"usuario?pagina={parametros.Pagina + 1}&tamanho={parametros.Tamanho}"
                    : ""
            };
        }
    }
}
