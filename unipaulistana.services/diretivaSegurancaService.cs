namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public class DiretivaSegurancaService : IDiretivaSegurancaService
    {
        public DiretivaSegurancaService(IDiretivaSegurancaRepository repository)
        {
            this.repository = repository;
        }

        readonly IDiretivaSegurancaRepository repository;

        public IEnumerable<DiretivaSeguranca> ObterTodos()
            => this.repository.ObterTodos();

        public DiretivaSeguranca ObterPorID(int diretivaSegurancaID)
            => this.repository.ObterPorID(diretivaSegurancaID);

        public IEnumerable<DiretivaSeguranca> ObterDiretivasNaoAssociadasAoGrupoDoUsuario(int usuarioID)
            => this.repository.ObterDiretivasNaoAssociadasAoGrupoDoUsuario(usuarioID);

        public IEnumerable<DiretivaSeguranca> ObterDiretivasAssociadasGrupoDoUsuario(int usuarioID)
            => this.repository.ObterDiretivasAssociadasGrupoDoUsuario(usuarioID);

        public IEnumerable<DiretivaSeguranca> ObterDiretivasNaoAssociadasGrupo(int grupoID)
            => this.repository.ObterDiretivasNaoAssociadasGrupo(grupoID);

        public IEnumerable<DiretivaSeguranca> ObterDiretivasAssociadasGrupo(int grupoID)
            => this.repository.ObterDiretivasAssociadasGrupo(grupoID);

        public void AdicionarPermissao(DiretivaSeguranca diretivaSeguranca)
        {
            this.repository.AdicionarPermissao(diretivaSeguranca);
        }

        public void RemoverPermissao(DiretivaSeguranca diretivaSeguranca)
        {
            this.repository.RemoverPermissao(diretivaSeguranca);
        }
    }
}
