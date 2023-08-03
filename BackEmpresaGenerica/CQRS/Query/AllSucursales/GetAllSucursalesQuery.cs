using MediatR;

namespace EmpresaGenericaAPI.CQRS.Query.AllSucursales
{
    public class GetAllSucursalesQuery : IRequest<ListaSucursales>
    {
    }
}