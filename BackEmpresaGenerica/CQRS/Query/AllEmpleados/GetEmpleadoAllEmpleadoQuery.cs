using MediatR;
using EmpresaGenericaAPI.Dtos;

namespace EmpresaGenericaAPI.CQRS.Query.AllEmpleados
{
    public class GetEmpleadoAllEmpleadoQuery : IRequest<ListaEmpleados>
    {
    }
}
