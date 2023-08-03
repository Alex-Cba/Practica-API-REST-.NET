using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EmpresaGenericaAPI.Data;
using EmpresaGenericaAPI.Dtos;
using EmpresaGenericaAPI.Dtos.Respuestas;

namespace EmpresaGenericaAPI.CQRS.Query.AllCiudades
{
    public class ListaCiudades : RespuestaBase
    {
        public List<CiudadDTO>? listaCiudades { get; set; }
    }
    public class GetAllCiudadesHandler : IRequestHandler<GetAllCiudadesQuery, ListaCiudades>
    {
        private readonly EfcodeFirstContext _context;
        private readonly IMapper _mapper;

        public GetAllCiudadesHandler(EfcodeFirstContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaCiudades> Handle(GetAllCiudadesQuery request, CancellationToken cancellationToken)
        {
            ListaCiudades retorno = new ListaCiudades();

            try
            {
                var lst_Ciudades = await _context.Ciudades.ToListAsync();

                var lst_CiudadesDTO = _mapper.Map<List<CiudadDTO>>(lst_Ciudades);

                retorno.listaCiudades = lst_CiudadesDTO;
                retorno.StatusCode = 200;
                retorno.MessageError = "Las ciudades se obtuvieron correctamente";
                retorno.IsSuccess = true;

                return retorno;
            }
            catch(Exception e)
            {
                retorno.StatusCode = 500;
                retorno.MessageError = "Error al obtener las ciudades " + "\n" + e.Message;
                retorno.IsSuccess = false;

                return retorno;
            }
        }
    }
}
