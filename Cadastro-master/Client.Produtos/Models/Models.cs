using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Produtos.Models
{
  
    public class ProdutoView
    {

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        public decimal SaldoEstoque { get; set; }
       


        public List<FornecedorView> Fonecedores { get; private set; } = new List<FornecedorView>();
    }

    public class FornecedorView
    {

        public int Id { get;  set; }

        public ProdutoView Produto { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "Preencha o Identificador do produto")]
        public int ProdutoId { get; set; }

    }

    public class UsuarioView
    {
        public int Id { get;  set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string CPF { get; set; }

        //public List<Produto> Produtos { get; private set; } = new List<Produto>();
        //public List<Fornecedor> Fonecedores { get; private set; } = new List<Fornecedor>();

    }


    public class PaginacaoView<T>  : IResultado<T> where T : class
    {

        public int Total { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanhoPagina { get; set; }
        public int NumeroPagina { get; set; }
        public IList<T> Resultado { get; set; }
        public string Anterior { get; set; }
        public string Proximo { get; set; }
    }

    public interface IResultado<T> where T : class
    {
        IList<T> Resultado { get; set; }
        
    }

}
