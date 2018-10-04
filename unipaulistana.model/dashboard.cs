namespace unipaulistana.model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Dashboard
    {
        public Dashboard(){}
        public Dashboard(int totalUsuario, int totalCliente)
        {
            this.TotalUsuario = totalUsuario;
            this.TotalCliente = totalCliente;
        }

        public int TotalUsuario { get; private set; }
        public int TotalCliente { get; private set; }
    }
}
