using MediatR;
using EmpresaGenericaAPI.Dtos.Respuestas;

namespace EmpresaGenericaAPI.CQRS.Commands.EmpleadoDelete
{
    public class DeleteEmpleadoCommand : IRequest<RespuestaDeleteEmpleado>
    {
        public Guid Id { get; set; }
    }
    
}
