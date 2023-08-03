using MediatR;
using EmpresaGenericaAPI.Dtos;
using EmpresaGenericaAPI.Dtos.Respuestas;

namespace EmpresaGenericaAPI.CQRS.Commands.EmpleadoPost
{
    public class PostEmpleadoCommand : IRequest<RespuestaEmpleadoDTO>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Guid CargoId { get; set; }
        public Guid SucursalId { get; set; }
        public string DNI { get; set; }
        public Guid? JefeId { get; set; }
    }
}
