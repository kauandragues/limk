using Limk.VolleyBall.Backend;

namespace Limk.VolleyBall.User_Constrols
{
    // O formulário principal informa o usuário antes de exibir qualquer tela.
    public interface ITelaUsuario
    {
        void DefinirUsuario(UsuarioAutenticado usuario);
    }
}
