using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EmpresaGenericaAPI.CQRS.Query;
using EmpresaGenericaAPI.Data;
using EmpresaGenericaAPI.Dtos;
using EmpresaGenericaAPI.Dtos.Respuestas;
using EmpresaGenericaAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace EmpresaGenericaAPI.CQRS.Commands.EmpleadoPost
{
    public class PostEmpleadoCommandHandler : IRequestHandler<PostEmpleadoCommand, RespuestaEmpleadoDTO>
    {
        private readonly EfcodeFirstContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<PostEmpleadoCommand> _validator;

        public PostEmpleadoCommandHandler(EfcodeFirstContext context, IMapper mapper, IValidator<PostEmpleadoCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<RespuestaEmpleadoDTO> Handle(PostEmpleadoCommand request, CancellationToken cancellationToken)
        {
            RespuestaEmpleadoDTO response = new RespuestaEmpleadoDTO();

            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    throw new Exception(validationResult.ToString());
                }
                else
                {
                    var empleado = _mapper.Map<Empleado>(request);
                    empleado.Id = Guid.NewGuid();
                    empleado.FechaAlta = DateTime.Now.ToUniversalTime();
                    await _context.AddAsync(empleado);
                    await _context.SaveChangesAsync();

                    var ConsultarEmpleado = await _context.Empleados
                        .Include(x => x.Cargo)
                        .Include(x => x.Sucursal)
                        .ThenInclude(s => s.Ciudad)
                        .FirstOrDefaultAsync(d => d.Id == empleado.Id);

                    var empleadoDTO = _mapper.Map<EmpleadoDTO>(ConsultarEmpleado);

                    response = _mapper.Map<RespuestaEmpleadoDTO>(empleadoDTO);

                    response.StatusCode = 200;
                    response.MessageError = "Se guardo correctamente el empleado";
                    response.IsSuccess = true;

                    return response;
                }
            }
            catch (Exception e)
            {
                response.StatusCode = 500;
                response.MessageError = "Error al guardar el empleado" + "\n" + e.Message;
                response.IsSuccess = false;

                return response;
            }
        }
    }
}
