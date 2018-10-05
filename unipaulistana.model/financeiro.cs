namespace unipaulistana.model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Financeiro
    { 
        public Financeiro(){}

        public Financeiro(int financeiroID, DateTime dataDeVencimento, double valor, StatusTitulo status, int clienteID)
        {
            this.FinanceiroID = financeiroID;
            this.DataDeVencimento = dataDeVencimento;
            this.Valor = valor;
            this.Status = status;
            this.ClienteID = clienteID;
        }

        public Financeiro(int financeiroID, DateTime dataDeVencimento, double valor, StatusTitulo status, int clienteID, string nomeCliente)
        {
            this.FinanceiroID = financeiroID;
            this.DataDeVencimento = dataDeVencimento;
            this.Valor = valor;
            this.Status = status;
            this.ClienteID = clienteID;
            this.Cliente = new Cliente(clienteID, nomeCliente);
        }

        [Display(Name="Número do título")]
        public int FinanceiroID { get; set; }

       
        [Required(ErrorMessage="O campo data de vencimento é obrigatório")]
        public DateTime DataDeVencimento { get; set; }

        
        [Required(ErrorMessage="O campo valor é obrigatório")]
        public Double Valor { 
            get{
                return this.valor;
            }
            set{
                this.valor =value;
            }
        }

        double valor = 0;

        
        [Required(ErrorMessage="O campo status é obrigatório")]
        public StatusTitulo Status { get; set; }


        [Display(Name="Cliente")]
        public int ClienteID { get; set; }
        public Cliente Cliente { get; set; }
    }

    public enum StatusTitulo
    {
        em_aberto = 1,
        pago = 2,
        vencida = 3
    }
}

