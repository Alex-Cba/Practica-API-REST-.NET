using MediatR;
using EmpresaGenericaAPI.Dtos.Respuestas;

namespace EmpresaGenericaAPI.CQRS.Commands.EmpleadoPut
{
    public class PutEmpleadoCommand : IRequest<RespuestaEmpleadoDTO>
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Guid CargoId { get; set; }
        public Guid SucursalId { get; set; }
        public string DNI { get; set; }
        public Guid? JefeId { get; set; }
    }
}
