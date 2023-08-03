using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EmpresaGenericaAPI.CQRS.Commands.EmpleadoPost;
using EmpresaGenericaAPI.Data;
using EmpresaGenericaAPI.Dtos;
using EmpresaGenericaAPI.Dtos.Respuestas;
using EmpresaGenericaAPI.Models;

namespace EmpresaGenericaAPI.CQRS.Commands.EmpleadoPut
{
    public class PutEmpleadoCommandHandler : IRequestHandler<PutEmpleadoCommand, RespuestaEmpleadoDTO>
    {
        private readonly EfcodeFirstContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<PutEmpleadoCommand> _validator;

        public PutEmpleadoCommandHandler(EfcodeFirstContext context, IMapper mapper, IValidator<PutEmpleadoCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<RespuestaEmpleadoDTO> Handle(PutEmpleadoCommand request, CancellationToken cancellationToken)
        {
            RespuestaEmpleadoDTO retorno = new RespuestaEmpleadoDTO();

            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    throw new Exception(validationResult.ToString());
                }
                else
                {
                    var EmpParaActualizar = await _context.Empleados.FirstOrDefaultAsync(x => x.Id == request.Id);

                    EmpParaActualizar.Nombre = request.Nombre;
                    EmpParaActualizar.Apellido = request.Apellido;
                    EmpParaActualizar.CargoId = request.CargoId;
                    EmpParaActualizar.SucursalId = request.SucursalId;
                    EmpParaActualizar.DNI = request.DNI;
                    EmpParaActualizar.JefeId = request.JefeId;

                    await _context.SaveChangesAsync();

                    var EmpleadoActualizado = await _context.Empleados
                        .Include(x => x.Cargo)
                        .Include(x => x.Sucursal)
                        .ThenInclude(s => s.Ciudad)
                        .FirstOrDefaultAsync(x => x.Id == request.Id);

                    var EmpActualizadoDTO = _mapper.Map<EmpleadoDTO>(EmpleadoActualizado);

                    retorno = _mapper.Map<RespuestaEmpleadoDTO>(EmpActualizadoDTO);

                    retorno.StatusCode = 200;
                    retorno.MessageError = "Se actualizo correctamente el empleado";
                    retorno.IsSuccess = true;

                    return retorno;
                }
            }
            catch(Exception e)
            {
                retorno.StatusCode = 500;
                retorno.MessageError = "Error al actualizar el empleado" + "\n" + e.Message;
                retorno.IsSuccess = false;

                return retorno;
            }
        }
    }
}
