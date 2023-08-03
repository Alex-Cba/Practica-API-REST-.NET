using MediatR;
using EmpresaGenericaAPI.Dtos;

namespace EmpresaGenericaAPI.CQRS.Query.AllCargos
{
    public class GetAllCargosQuery : IRequest<ListaCargos>
    {
    }
}
