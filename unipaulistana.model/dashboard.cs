namespace unipaulistana.model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Dashboard
    {
        public Dashboard(){}
        public Dashboard(int totalUsuario, int totalCliente, int totalSolicitacaoEmAberto)
        {
            this.TotalUsuario = totalUsuario;
            this.TotalCliente = totalCliente;
            this.TotalSolicitacaoEmAberto = totalSolicitacaoEmAberto;
        }

        public int TotalUsuario { get; private set; }
        public int TotalCliente { get; private set; }
        public int TotalSolicitacaoEmAberto { get; private set; }
    }
}
