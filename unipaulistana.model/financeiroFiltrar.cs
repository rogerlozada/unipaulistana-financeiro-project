namespace unipaulistana.model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FinanceiroFiltrar
    { 
        [Display(Name="Status")]
        public StatusTitulo Status { get; set; }
    }
}

