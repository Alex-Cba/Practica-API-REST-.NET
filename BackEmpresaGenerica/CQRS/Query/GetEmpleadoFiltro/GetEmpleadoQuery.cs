using MediatR;
using EmpresaGenericaAPI.Dtos;
using EmpresaGenericaAPI.Dtos.Respuestas;

namespace EmpresaGenericaAPI.CQRS.Query.GetEmpleadoFiltro
{
    public class GetEmpleadoQuery : IRequest<RespuestaEmpleadoDTOFiltro>
    {
        public Guid Id { get; set; }
    }
}
