using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EmpresaGenericaAPI.CQRS.Commands.EmpleadoPost;
using EmpresaGenericaAPI.Data;
using EmpresaGenericaAPI.Dtos;
using EmpresaGenericaAPI.Dtos.Respuestas;
using EmpresaGenericaAPI.Models;

namespace EmpresaGenericaAPI.CQRS.Commands.EmpleadoDelete
{
    public class DeleteEmpleadoCommandHandler : IRequestHandler<DeleteEmpleadoCommand, RespuestaDeleteEmpleado>
    {
        private readonly EfcodeFirstContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<DeleteEmpleadoCommand> _validator;

        public DeleteEmpleadoCommandHandler(EfcodeFirstContext context, IMapper mapper, IValidator<DeleteEmpleadoCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<RespuestaDeleteEmpleado> Handle(DeleteEmpleadoCommand request, CancellationToken cancellationToken)
        {
            RespuestaDeleteEmpleado respuestaDelete = new RespuestaDeleteEmpleado();

            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    throw new Exception(validationResult.ToString());
                }
                else
                {
                    var ConsultarEmpleado = await _context.Empleados
                        .FirstOrDefaultAsync(d => d.Id == request.Id);

                    _context.Empleados.Remove(ConsultarEmpleado);
                    await _context.SaveChangesAsync();

                    respuestaDelete = _mapper.Map<RespuestaDeleteEmpleado>(ConsultarEmpleado);

                    respuestaDelete.StatusCode = 200;
                    respuestaDelete.MessageError = "Se elimino correctamente el empleado";
                    respuestaDelete.IsSuccess = true;

                    return respuestaDelete;
                }
            }
            catch(Exception e)
            {
                respuestaDelete.StatusCode = 500;
                respuestaDelete.MessageError = "Error al eliminar el empleado" + "\n" + e.Message;
                respuestaDelete.IsSuccess = false;

                return respuestaDelete;
            }
        }
    }
}
