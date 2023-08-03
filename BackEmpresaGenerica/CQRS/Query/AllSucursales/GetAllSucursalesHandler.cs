using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EmpresaGenericaAPI.Data;
using EmpresaGenericaAPI.Dtos;
using EmpresaGenericaAPI.Dtos.Respuestas;

namespace EmpresaGenericaAPI.CQRS.Query.AllSucursales
{
    public class ListaSucursales : RespuestaBase
    {
        public List<RespuestaSucursalesDTO>? listSucursales { get; set; }
    }
    public class GetAllSucursalesHandler : IRequestHandler<GetAllSucursalesQuery, ListaSucursales>
    {
        private readonly EfcodeFirstContext _context;
        private readonly IMapper _mapper;

        public GetAllSucursalesHandler(EfcodeFirstContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaSucursales> Handle(GetAllSucursalesQuery request, CancellationToken cancellationToken)
        {
            ListaSucursales retorno = new ListaSucursales();

            try
            {
                var lst_sucursales = await _context.Sucursales.ToListAsync();

                var lst_sucursalesDTO = _mapper.Map<List<RespuestaSucursalesDTO>>(lst_sucursales);

                retorno.listSucursales = lst_sucursalesDTO;
                retorno.StatusCode = 200;
                retorno.MessageError = "Las sucursales se obtuvieron correctamente";
                retorno.IsSuccess = true;

                return retorno;
            }
            catch(Exception e)
            {
                retorno.StatusCode = 500;
                retorno.MessageError = "Error al obtener las sucursales " + "\n" + e.Message;
                retorno.IsSuccess = false;

                return retorno;
            }
        }
    }
}
