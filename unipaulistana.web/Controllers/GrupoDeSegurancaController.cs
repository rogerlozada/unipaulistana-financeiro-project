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
    public class GrupoDeSegurancaController : Controller
    {
        public GrupoDeSegurancaController(IGrupoDeSegurancaService grupoDeSegurancaService, IDiretivaSegurancaService diretivaSegurancaService)
        {
            this.grupoDeSegurancaService = grupoDeSegurancaService;
            this.diretivaSegurancaService = diretivaSegurancaService;
        }

        readonly IGrupoDeSegurancaService grupoDeSegurancaService;
        readonly IDiretivaSegurancaService diretivaSegurancaService;

        [Authorize(Policy="PermiteListarGrupoDeSeguranca")]
        public IActionResult Index()
        { 
            if(TempData["mensagemIndex"] != null)
            {
                ViewBag.Sucesso = TempData["mensagemIndex"];
            } 

            return View(this.grupoDeSegurancaService.ObterTodos());
        }
        
        [Authorize(Policy="PermiteCriarGrupoDeSeguranca")]
        public IActionResult Criar()
        {
             return View();
        }

        [HttpPost]
        public IActionResult Criar(GrupoDeSeguranca dados)
        {
            if (!ModelState.RemoveKeyModelState("GrupoDeSegurancaID").IsValid)
                return View(dados);

            try
            {
                this.grupoDeSegurancaService.Adicionar(dados);
                TempData["mensagemIndex"] = "Grupo inserido com sucesso."; 
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar atualizar o grupo:{0}", ex.Message));
                return View(dados);
            }
        }

        [Authorize(Policy="PermiteAlterarGrupoDeSeguranca")]
        public IActionResult Alterar(int id)
        {
            if(TempData["mensagemEdicao"] != null)
            {
                ViewBag.Sucesso = TempData["mensagemEdicao"];
            } 

            return View(this.grupoDeSegurancaService.ObterPorID(id));
        }

        [HttpPost]
        public IActionResult Alterar(GrupoDeSeguranca dados)
        {
            if (!ModelState.IsValid)
                return View(dados);

            try
            {
                this.grupoDeSegurancaService.Atualizar(dados);
                TempData["mensagemEdicao"] = "Grupo atualizado com sucesso."; 
                return RedirectToAction("Alterar", dados.GrupoDeSegurancaID);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar atualizar o grupo:{0}", ex.Message));
                return View(dados);
            }
        }

        [Authorize(Policy="PermiteExcluirGrupoDeSeguranca")]
        public IActionResult Excluir(int id)
        {
            return View(this.grupoDeSegurancaService.ObterPorID(id));
        }

        [HttpPost]
        public ActionResult Excluir(GrupoDeSeguranca dados)
        {
            try
            {
                this.grupoDeSegurancaService.Excluir(dados.GrupoDeSegurancaID);
                TempData["mensagemIndex"] = "Grupo excluído com sucesso."; 
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar excluir o grupo:{0}", ex.Message));
                return View(dados);
            }
        }

        public IActionResult Permissoes(int id)
        {
            if(TempData["mensagemIndex"] != null)
            {
                ViewBag.Sucesso = TempData["mensagemIndex"];
            } 

            InicializarViewBagsDeDiretivasDeSeguranca(id);
            return View(new DiretivaSeguranca(id)); 
        }

        [HttpPost]
        public IActionResult Permissoes(DiretivaSeguranca dados)
        {   
            try
            {
                this.diretivaSegurancaService.AdicionarPermissao(dados);
                TempData["mensagemIndex"] = "Permissão adicionada com sucesso."; 
                return RedirectToAction("Permissoes", new { id=dados.GrupoSegurancaID });
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar adicionar uma permissão:{0}", ex.Message));
                InicializarViewBagsDeDiretivasDeSeguranca(dados.GrupoSegurancaID);
                return View(dados);
            }
        }

        public IActionResult RemoverPermissao(int grupoID, int diretivaID)
        {
            try
            {
                this.diretivaSegurancaService.RemoverPermissao(new DiretivaSeguranca(grupoID, diretivaID));
                TempData["mensagemIndex"] = "Permissão excluída com sucesso."; 
                return RedirectToAction("Permissoes", new { id=grupoID } );
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar adicionar uma permissão:{0}", ex.Message));
                return RedirectToAction("Permissoes", new { id=grupoID });
            }
        }

        void InicializarViewBagsDeDiretivasDeSeguranca(int id)
        {
            ViewBag.listaDeDiretivasAssociadasAoGrupo = this.diretivaSegurancaService.ObterDiretivasAssociadasGrupo(id);
            ViewBag.listaDeDiretivasNaoAssociadasAoGrupo = this.diretivaSegurancaService.ObterDiretivasNaoAssociadasGrupo(id);
            ViewBag.NomeDoGrupo = this.grupoDeSegurancaService.ObterPorID(id).Nome;
        }
    }
}

