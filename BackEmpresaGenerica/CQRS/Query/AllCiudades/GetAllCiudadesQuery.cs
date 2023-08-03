using MediatR;

namespace EmpresaGenericaAPI.CQRS.Query.AllCiudades
{
    public class GetAllCiudadesQuery : IRequest<ListaCiudades>
    {
    }
}
