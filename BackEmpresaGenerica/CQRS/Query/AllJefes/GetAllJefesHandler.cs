using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EmpresaGenericaAPI.CQRS.Query.AllCargos;
using EmpresaGenericaAPI.Data;
using EmpresaGenericaAPI.Dtos;
using EmpresaGenericaAPI.Dtos.Respuestas;

namespace EmpresaGenericaAPI.CQRS.Query.AllJefes
{
    public class ListaJefes : RespuestaBase
    {
        public List<RespuestaJefe>? listaJefes { get; set; }
    }
    public class GetAllJefesHandler : IRequestHandler<GetAllJefesQuery, ListaJefes>
    {
        private readonly EfcodeFirstContext _context;
        private readonly IMapper _mapper;

        public GetAllJefesHandler(EfcodeFirstContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaJefes> Handle(GetAllJefesQuery request, CancellationToken cancellationToken)
        {
            ListaJefes retorno = new ListaJefes();

            try
            {
                var lst_Jefes = await _context.Empleados
                    .Include(x => x.Cargo)
                    .Where(x => x.Cargo.Nombre == "Jefe")
                    .ToListAsync();

                var lst_JefesDTO = _mapper.Map<List<RespuestaJefe>>(lst_Jefes);

                retorno.listaJefes = lst_JefesDTO;
                retorno.StatusCode = 200;
                retorno.MessageError = "Los jefes se obtuvieron correctamente";
                retorno.IsSuccess = true;

                return retorno;

            }
            catch (Exception e)
            {
                retorno.StatusCode = 500;
                retorno.MessageError = "Error al obtener los jefes " + "\n" + e.Message;
                retorno.IsSuccess = false;

                return retorno;
            }
        }
    }
}
