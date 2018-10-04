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
    public class UsuarioController : Controller
    {
        public UsuarioController(IUsuarioService usuarioService,
                                 IDepartamentoService departamentoService,
                                 IGrupoDeSegurancaService grupoDeSegurancaService,
                                 IHostingEnvironment hostingEnvironment)
        {
            this.usuarioService = usuarioService;
            this.hostingEnvironment = hostingEnvironment;
            this.departamentoService = departamentoService;
            this.grupoDeSegurancaService = grupoDeSegurancaService;
        }

        readonly IUsuarioService usuarioService;
        readonly IDepartamentoService departamentoService;
        readonly IGrupoDeSegurancaService grupoDeSegurancaService;
        readonly IHostingEnvironment hostingEnvironment;

        [Authorize(Policy="PermiteListarUsuario")]
        public IActionResult Index()
        {
            if (TempData["mensagemIndex"] != null)
            {
                ViewBag.Sucesso = TempData["mensagemIndex"];
            }

            return View(this.usuarioService.ObterTodos());
        }

        [Authorize(Policy="PermiteCriarUsuario")]
        public IActionResult Criar()
        {
            AtualizarListas();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Usuario dados)
        {
            try
            {
                this.usuarioService.Adicionar(dados);
                TempData["mensagemIndex"] = "Usuário inserido com sucesso.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar atualizar o usuário:{0}", ex.Message));
                AtualizarListas();
                return View(dados);
            }
        }

        public IActionResult ImagemUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImagemUsuario(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            if (size <= 0)
            {
                TempData["mensagem"] = "Selecione um arquivo!!!";
                return View();
            }


            var filePath = Path.GetTempFileName();

            try
            {
                foreach (var formFile in files)
                {
                    string novoArquivo = FileHelper.GetUniqueFileName(formFile.FileName);
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    var fullPath = Path.Combine(uploads, novoArquivo);
                    formFile.CopyTo(new FileStream(fullPath, FileMode.Create));

                    this.usuarioService.AtualizarFoto(User.GetUserID(), novoArquivo);
                }

                TempData["mensagem"] = "Arquivo enviado com sucesso.";
                return RedirectToAction("ImagemUsuario");
            }
            catch (Exception ex)
            {
                TempData["mensagem"] = string.Format("Ocorreu um erro:{0}", ex.Message);
                return View();
            }
        }

        [Authorize(Policy="PermiteAlterarUsuario")]
        public IActionResult Alterar(int id)
        {
            if (TempData["mensagemEdicao"] != null)
            {
                ViewBag.Sucesso = TempData["mensagemEdicao"];
            }

            AtualizarListas();
            return View(this.usuarioService.ObterPorID(id));
        }

        [HttpPost]
        public IActionResult Alterar(Usuario dados)
        {
            try
            {
                this.usuarioService.Atualizar(dados);
                TempData["mensagemEdicao"] = "Usuário atualizado com sucesso.";
                return RedirectToAction("Alterar", dados.UsuarioID);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar atualizar o usuário:{0}", ex.Message));
                AtualizarListas();
                return View(dados);
            }
        }

        void AtualizarListas()
        {
            ViewBag.ListarDepartamentos = this.departamentoService.ObterTodos();
            ViewBag.ListarGruposDeSeguranca = this.grupoDeSegurancaService.ObterTodos();
        }

        [Authorize(Policy="PermiteExcluirUsuario")]
        public IActionResult Excluir(int id)
        {
            return View(this.usuarioService.ObterPorID(id));
        }

        [HttpPost]
        public ActionResult Excluir(Usuario dados)
        {
            try
            {
                this.usuarioService.Excluir(dados.UsuarioID);
                TempData["mensagemIndex"] = "Usuário excluído com sucesso.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar atualizar o usuário:{0}", ex.Message));
                return View(dados);
            }
        }

        public IActionResult NovaSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NovaSenha(NovaSenha novaSenha)
        {
            try
            {
                this.usuarioService.AtualizarSenha(User.GetUserID(), novaSenha.SenhaAnterior, novaSenha.Senha);
                ViewBag.Sucesso = "Senha atualizada com sucesso.";
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar atualizar a senha:{0}", ex.Message));
                return View();
            }
        }

        [Authorize(Policy="PermiteAlterarSenhaViaAdminUsuario")]
        public IActionResult AlterarSenhaViaAdmin(int id)
        {
            var usuario = new NovaSenhaAlteradaPorAdmin(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult AlterarSenhaViaAdmin(NovaSenhaAlteradaPorAdmin novaSenha)
        {
            try
            {
                this.usuarioService.AtualizarSenha(novaSenha.UserID, novaSenha.Senha);
                ViewBag.Sucesso = "Senha atualizada com sucesso.";
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Ocorreu um erro ao tentar atualizar a senha:{0}", ex.Message));
                return View();
            }
        }
    }
}
