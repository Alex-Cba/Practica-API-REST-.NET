using FluentValidation;
using Microsoft.EntityFrameworkCore;
using EmpresaGenericaAPI.CQRS.Commands.EmpleadoPost;
using EmpresaGenericaAPI.Data;

namespace EmpresaGenericaAPI.CQRS.Commands.EmpleadoDelete
{
    public class DeleteEmpleadoCommandValidator : AbstractValidator<DeleteEmpleadoCommand>
    {
        private readonly EfcodeFirstContext _context;

        public DeleteEmpleadoCommandValidator(EfcodeFirstContext context)
        {
            RuleFor(rule => rule.Id)
                .NotEmpty().WithMessage("Id del empleado no puede ser vacio");
            RuleFor(rule => rule)
                .MustAsync(CheckEmpleado).WithMessage("No se encontro ningún empleado con ese Id");
            RuleFor(rule => rule)
               .MustAsync(CheckRelacionJefe).WithMessage("No se puede eliminar este empleado, porque es jefe de otros empleados");


            _context = context;
        }

        private async Task<bool> CheckRelacionJefe(DeleteEmpleadoCommand command, CancellationToken Token)
        {
            bool tieneRelacionConOtrosEmpleados = await _context.Empleados.AnyAsync(e => e.JefeId == command.Id);

            if (tieneRelacionConOtrosEmpleados)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> CheckEmpleado(DeleteEmpleadoCommand command, CancellationToken Token)
        {
            bool CheckEmpleado = await _context.Empleados.AnyAsync(p => p.Id == command.Id);
            return CheckEmpleado;
        }
    }
}
