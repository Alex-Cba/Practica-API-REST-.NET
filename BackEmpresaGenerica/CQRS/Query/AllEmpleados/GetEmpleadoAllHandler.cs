using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EmpresaGenericaAPI.Data;
using EmpresaGenericaAPI.Dtos;
using EmpresaGenericaAPI.Dtos.Respuestas;

namespace EmpresaGenericaAPI.CQRS.Query.AllEmpleados
{
    public class ListaEmpleados : RespuestaBase
    {
        public List<RespuestaGetAllEmpleadoDTO>? listaEmpleados { get; set; }
    }

    public class GetEmpleadoAllHandler : IRequestHandler<GetEmpleadoAllEmpleadoQuery, ListaEmpleados>
    {
        private readonly EfcodeFirstContext _context;
        private readonly IMapper _mapper;

        public GetEmpleadoAllHandler(EfcodeFirstContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaEmpleados> Handle(GetEmpleadoAllEmpleadoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var empleados = await _context.Empleados
                    .Include(x => x.Cargo)
                    .Include(x => x.Sucursal)
                    .ThenInclude(s => s.Ciudad)
                    .ToListAsync();


                var lst_empleadoDTO = _mapper.Map<List<RespuestaGetAllEmpleadoDTO>>(empleados);

                ListaEmpleados EmpleadosDTO = new ListaEmpleados();
                EmpleadosDTO.listaEmpleados = lst_empleadoDTO;
                EmpleadosDTO.StatusCode = 200;
                EmpleadosDTO.MessageError = "Los empleados se obtuvieron correctamente";
                EmpleadosDTO.IsSuccess = true;

                return EmpleadosDTO;
            }
            catch (Exception e)
            {

                ListaEmpleados RespuestaError = new ListaEmpleados();

                RespuestaError.StatusCode = 500;
                RespuestaError.MessageError = "Error al obtener empleados" + "\n" + e.Message;
                RespuestaError.IsSuccess = false;

                return RespuestaError;
            }
        }

    }
}

