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
    public class DepartamentoController : Controller
    {
        public DepartamentoController(IDepartamentoService departamentoService)
        {
            this.departamentoService = departamentoService;
        }

        readonly IDepartamentoService departamentoService;

        [Authorize(Policy="PermiteListarDepartamento")]
        public IActionResult Index()
        { 
            if(TempData["mensagemIndex"] != null)
            {
                ViewBag.Sucesso = TempData["mensagemIndex"];
            } 

            return View(this.departamentoService.ObterTodos());
        }
        
        [Authorize(Policy="PermiteCriarDepartamento")]
        public IActionResult Criar()
        {
             return View();
        }

        [HttpPost]
        public IActionResult Criar(Departamento dados)
        {
            if (!ModelState.RemoveKeyModelState("DepartamentoID").IsValid)
                return View(dados);

            try
            {
                this.departamentoService.Adicionar(dados);
                TempData["mensagemIndex"] = "Departamento inserido com sucesso."; 
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar atualizar o departamento:", ex.Message));
                return View(dados);
            }
        }

        [Authorize(Policy="PermiteAlterarDepartamento")]
        public IActionResult Alterar(int id)
        {
            if(TempData["mensagemEdicao"] != null)
            {
                ViewBag.Sucesso = TempData["mensagemEdicao"];
            } 

            return View(this.departamentoService.ObterPorID(id));
        }

        [HttpPost]
        public IActionResult Alterar(Departamento dados)
        {
            if (!ModelState.IsValid)
                return View(dados);

            try
            {
                this.departamentoService.Atualizar(dados);
                TempData["mensagemEdicao"] = "Departamento atualizado com sucesso."; 
                return RedirectToAction("Alterar", dados.DepartamentoID);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar atualizar o departamento:{0}", ex.Message));
                return View(dados);
            }
        }

        [Authorize(Policy="PermiteExcluirDepartamento")]
        public IActionResult Excluir(int id)
        {
            return View(this.departamentoService.ObterPorID(id));
        }

        [HttpPost]
        public ActionResult Excluir(Departamento dados)
        {
            try
            {
                this.departamentoService.Excluir(dados.DepartamentoID);
                TempData["mensagemIndex"] = "Departamento excluído com sucesso."; 
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar excluir o departamento:{0}", ex.Message));
                return View(dados);
            }
        }
    }
}
