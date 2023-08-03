using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EmpresaGenericaAPI.CQRS.Query.AllCargos;
using EmpresaGenericaAPI.Data;
using EmpresaGenericaAPI.Dtos;
using EmpresaGenericaAPI.Dtos.Respuestas;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmpresaGenericaAPI.CQRS.Query.GetEmpleadoFiltro
{
    public class GetEmpleadoHandler : IRequestHandler<GetEmpleadoQuery, RespuestaEmpleadoDTOFiltro>
    {
        private readonly EfcodeFirstContext _context;
        private readonly IMapper _mapper;

        public GetEmpleadoHandler(EfcodeFirstContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RespuestaEmpleadoDTOFiltro> Handle(GetEmpleadoQuery request, CancellationToken cancellationToken)
        {
            RespuestaEmpleadoDTOFiltro retorno = new RespuestaEmpleadoDTOFiltro();

            try
            {
                bool CheckEmpleado = await _context.Empleados.AnyAsync(p => p.Id == request.Id);

                if (!CheckEmpleado)
                {
                    throw new InvalidOperationException($"No se encontró ningún empleado con el ID: {request.Id}");
                }
                else
                {
                    var empleadoFiltrado = await _context.Empleados
                        .Include(x => x.Cargo)
                        .Include(x => x.Sucursal)
                        .ThenInclude(s => s.Ciudad)
                        .FirstOrDefaultAsync(d => d.Id == request.Id);

                    retorno = _mapper.Map<RespuestaEmpleadoDTOFiltro>(empleadoFiltrado);

                    retorno.StatusCode = 200;
                    retorno.MessageError = "El empleado se obtuvo correctamente";
                    retorno.IsSuccess = true;

                    return retorno;
                }
            }
            catch (Exception e)
            {
                retorno.StatusCode = 500;
                retorno.MessageError = "Error al obtener el empleado " + "\n" + e.Message;
                retorno.IsSuccess = false;

                return retorno;
            }
        }
    }
}
