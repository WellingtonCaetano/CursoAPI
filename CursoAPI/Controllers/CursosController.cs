using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace curso.api.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize]
    public class CursosController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursosController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        /// <summary>
        /// Este serviço permite autenticar um curso para o usuário autenticado.
        /// </summary>
        /// <returns>Retorna o status 201 e dados do curso e usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar um curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CusroViewModelIntput cusroViewModelIntput)
        {
            Curso curso = new Curso();
            curso.Nome = cusroViewModelIntput.Nome;
            curso.Descricao = cusroViewModelIntput.Descricao;

            var codigoUsusario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            curso.CodigoUsuario = codigoUsusario;
            _cursoRepository.Adicionar(curso);
            _cursoRepository.Commit();

            return Created("", cusroViewModelIntput);
        }

        /// <summary>
        /// Este serviço permite obeter todos os curso ativos do usuário.
        /// </summary>
        /// <returns>Retorna o status ok e dados do curso e usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao obter lista de curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {

            var codigoUsusario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var cursos = _cursoRepository.ObterPorUsuario(codigoUsusario)
                .Select(s => new CusroViewModelOutput()
                {
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    Login = s.Usuario.Login
                });

            return Ok(cursos);
        }
    }
}