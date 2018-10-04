namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public interface IUsuarioService
    {
        IEnumerable<Usuario> ObterTodos();
        Usuario ObterPorID(int usuarioID);
        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Excluir(int usuarioID);
        Usuario Login(string email, string senha);
        void AtualizarFoto(int usuarioID, string nomeDaImagem);
        void AtualizarSenha(int usuarioID, string novaSenha);
        void AtualizarSenha(int usuarioID, string senhaAnterior, string novaSenha);
    } 
}
