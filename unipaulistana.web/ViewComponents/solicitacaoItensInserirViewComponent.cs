namespace LoginViewComponent
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using unipaulistana.model;
    using System.Linq;

    public class SolicitacaoItensInserirViewComponent : ViewComponent
    {
        public SolicitacaoItensInserirViewComponent(ISolicitacaoService solicitacaoService)
        {
            this.solicitacaoService = solicitacaoService;
        }

        readonly ISolicitacaoService solicitacaoService;

        public async Task<IViewComponentResult> InvokeAsync(int solicitacaoID)
        {
           return View("_solicitacaoItensInserir", new SolicitacaoItem(solicitacaoID));
        }
    }
}