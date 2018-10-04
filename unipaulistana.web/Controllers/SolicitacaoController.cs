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
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using unipaulistana.web.Helper;
    using Microsoft.AspNetCore.Hosting;
    using unipaulistana.web.extensions;

    [Authorize]
    public class SolicitacaoController : Controller
    {
        public SolicitacaoController(ISolicitacaoService solicitacaoService,
                                    IClienteService clienteService,
                                    IUsuarioService usuarioService,
                                    IDepartamentoService departamentoService,
                                    IHostingEnvironment hostingEnvironment)
        {
            this.solicitacaoService = solicitacaoService;
            this.clienteService = clienteService;
            this.usuarioService = usuarioService;
            this.hostingEnvironment = hostingEnvironment;
            this.departamentoService = departamentoService;
        }

        readonly ISolicitacaoService solicitacaoService;
        readonly IClienteService clienteService;
        readonly IUsuarioService usuarioService;
        readonly IDepartamentoService departamentoService;
        readonly IHostingEnvironment hostingEnvironment;

        [Authorize(Policy = "PermiteListarSolicitacao")]
        public IActionResult Index()
        {
            if (TempData["mensagemIndex"] != null)
            {
                ViewBag.Sucesso = TempData["mensagemIndex"];
            }

            ViewBag.ListarSolicitacoes = this.solicitacaoService.ObterStatusEmAbertoPorUsuario(User.GetUserID());
            return View();
        }


        [HttpPost]
        public IActionResult Index(SolicitacaoPesquisar solicitacaoPesquisar)
        {
            ViewBag.ListarSolicitacoes = this.solicitacaoService.Filtrar(solicitacaoPesquisar);
            return View();
        }

        [Authorize(Policy = "PermiteCriarSolicitacao")]
        public IActionResult Criar()
        {
            AtualizarListas();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Solicitacao dados)
        {
            try
            {
                this.solicitacaoService.Adicionar(dados, User.GetUserID());
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar atualizar o usuário:{0}", ex.Message));
                AtualizarListas();
                return View(dados);
            }
        }

        [Authorize(Policy = "PermiteAlterarSolicitacao")]
        public IActionResult Alterar(int id)
        {
            if (TempData["mensagemEdicao"] != null)
            {
                ViewBag.Sucesso = TempData["mensagemEdicao"];
            }

            AtualizarListas();
            return View(this.solicitacaoService.ObterPorID(id));
        }

        [HttpPost]
        public IActionResult Alterar(Solicitacao dados)
        {
            try
            {
                this.solicitacaoService.Atualizar(dados);
                TempData["mensagemEdicao"] = "Solicitação atualizada com sucesso.";
                return RedirectToAction("Alterar", dados.SolicitacaoID);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar atualizar a solicitação:{0}", ex.Message));
                AtualizarListas();
                return View(dados);
            }
        }

        void AtualizarListas()
        {
            ViewBag.ListarDepartamentos = this.departamentoService.ObterTodos();
            ViewBag.ListarCliente = this.clienteService.ObterTodos();
            ViewBag.ListarUsuarios = this.usuarioService.ObterTodos();
        }

        [Authorize(Policy = "PermiteConcluirSolicitacao")]
        public IActionResult ConcluirSolicitacao(int id)
        {
            return View();
        }


        [HttpPost]
        [Authorize(Policy = "PermiteAlterarSolicitacao")]
        public IActionResult AdicionarItem(SolicitacaoItem solicitacaoItem)
        {
            if (!string.IsNullOrEmpty(solicitacaoItem.Descricao))
                this.solicitacaoService.AdicionarItem(solicitacaoItem, User.GetUserID());
                
            return RedirectToAction("Alterar", new { id = solicitacaoItem.SolicitacaoID });
        }
    }
}
