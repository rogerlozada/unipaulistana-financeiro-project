namespace unipaulistana.web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using unipaulistana.model;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class FinanceiroController : Controller
    {
        public FinanceiroController(IFinanceiroService financeiroService, IClienteService clienteService)
        {
            this.financeiroService = financeiroService;
            this.clienteService = clienteService;
        }

        readonly IFinanceiroService financeiroService;
        readonly IClienteService clienteService;

        [Authorize(Policy="PermiteListarFinanceiro")]
        public IActionResult Index()
        { 
            if(TempData["mensagemIndex"] != null)
            {
                ViewBag.Sucesso = TempData["mensagemIndex"];
            } 

            return View(this.financeiroService.ObterTodos());
        }
        
        [Authorize(Policy="PermiteCriarFinanceiro")]
        public IActionResult Criar()
        {
             AtualizarListas();
             return View();
        }

        [HttpPost]
        public IActionResult Criar(Financeiro dados)
        {
            if (!ModelState.RemoveKeyModelState("FinanceiroID").IsValid)
                return View(dados);

            try
            {
                this.financeiroService.Adicionar(dados);
                TempData["mensagemIndex"] = "Título inserido com sucesso."; 
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar criar o título:{0}", ex.Message));
                return View(dados);
            }
        }

        [Authorize(Policy="PermiteAlterarFinanceiro")]
        public IActionResult Alterar(int id)
        {
            if(TempData["mensagemEdicao"] != null)
            {
                ViewBag.Sucesso = TempData["mensagemEdicao"];
            } 

            return View(this.financeiroService.ObterPorID(id));
        }

        [HttpPost]
        public IActionResult Alterar(Financeiro dados)
        {
            if (!ModelState.IsValid)
                return View(dados);

            try
            {
                this.financeiroService.Atualizar(dados);
                TempData["mensagemEdicao"] = "Financeiro atualizado com sucesso."; 
                return RedirectToAction("Alterar", dados.FinanceiroID);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar atualizar o título:{0}", ex.Message));
                return View(dados);
            }
        }

        void AtualizarListas()
        {
            ViewBag.ListarCliente = this.clienteService.ObterTodos();
        }

        [Authorize(Policy="PermiteConcluirFinanceiro")]
        public IActionResult Excluir(int id)
        {
            return View(this.financeiroService.ObterPorID(id));
        }

        [HttpPost]
        public ActionResult Excluir(Financeiro dados)
        {
            try
            {
                this.financeiroService.Excluir(dados.FinanceiroID);
                TempData["mensagemIndex"] = "Título excluído com sucesso."; 
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("error", string.Format("Ocorreu um erro ao tentar excluir o título:{0}", ex.Message));
                return View(dados);
            }
        }
    }
}
