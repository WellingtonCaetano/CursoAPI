namespace curso.api.Configurations
{
    public interface IAuthenticationService
    {
        string GerarToken(Controllers.UsuarioViewModelOutput usuarioViewModelOutput);
    }
}
