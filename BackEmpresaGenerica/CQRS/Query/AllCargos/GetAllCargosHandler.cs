using MediatR;
using EmpresaGenericaAPI.CQRS.Query.AllEmpleados;
using EmpresaGenericaAPI.Dtos.Respuestas;
using EmpresaGenericaAPI.Dtos;
using AutoMapper;
using EmpresaGenericaAPI.Data;
using Microsoft.EntityFrameworkCore;
using EmpresaGenericaAPI.Models;

namespace EmpresaGenericaAPI.CQRS.Query.AllCargos
{
    public class ListaCargos : RespuestaBase
    {
        public List<CargoDTO>? listaCargos { get; set; }
    }

    public class GetAllCargosHandler : IRequestHandler<GetAllCargosQuery, ListaCargos>
    {
        private readonly EfcodeFirstContext _context;
        private readonly IMapper _mapper;

        public GetAllCargosHandler(EfcodeFirstContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaCargos> Handle(GetAllCargosQuery request, CancellationToken cancellationToken)
        {
            ListaCargos retorno = new ListaCargos();

            try
            {
                var lst_Cargos = await _context.Cargos.ToListAsync();

                var lst_CargosDTO = _mapper.Map<List<CargoDTO>>(lst_Cargos);

                retorno.listaCargos = lst_CargosDTO;
                retorno.StatusCode = 200;
                retorno.MessageError = "Los cargos se obtuvieron correctamente";
                retorno.IsSuccess = true;

                return retorno;

            }
            catch(Exception e)
            {
                retorno.StatusCode = 500;
                retorno.MessageError = "Error al obtener los cargos " + "\n" + e.Message;
                retorno.IsSuccess = false;

                return retorno;
            }
        }
    }
}
