namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public interface IDiretivaSegurancaService
    {
        IEnumerable<DiretivaSeguranca> ObterTodos();
        DiretivaSeguranca ObterPorID(int diretivaSegurancaID);
        IEnumerable<DiretivaSeguranca> ObterDiretivasNaoAssociadasAoGrupoDoUsuario(int usuarioID);
        IEnumerable<DiretivaSeguranca> ObterDiretivasAssociadasGrupoDoUsuario(int usuarioID);
        IEnumerable<DiretivaSeguranca> ObterDiretivasNaoAssociadasGrupo(int grupoID);
        IEnumerable<DiretivaSeguranca> ObterDiretivasAssociadasGrupo(int grupoID);
        void AdicionarPermissao(DiretivaSeguranca diretivaSeguranca);
        void RemoverPermissao(DiretivaSeguranca diretivaSeguranca);
    }
}
