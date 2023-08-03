using FluentValidation;
using Microsoft.EntityFrameworkCore;
using EmpresaGenericaAPI.CQRS.Commands.EmpleadoPost;
using EmpresaGenericaAPI.Data;

namespace EmpresaGenericaAPI.CQRS.Commands.EmpleadoPut
{
    public class PutEmpleadoCommandValidator : AbstractValidator<PutEmpleadoCommand>
    {

        private readonly EfcodeFirstContext _context;

        public PutEmpleadoCommandValidator(EfcodeFirstContext context)
        {
            RuleFor(rule => rule.Nombre)
                .NotEmpty().WithMessage("Nombre del empleado no puede ser vacio");
            RuleFor(rule => rule.Apellido)
                .NotEmpty().WithMessage("Apellido no puede ser vacio");
            RuleFor(rule => rule.DNI)
                .NotEmpty().WithMessage("El DNI no puede ser vacio")
                .Must(ContieneSoloNumeros).WithMessage("El campo DNI debe contener solo números.");
            RuleFor(rule => rule.SucursalId)
                .NotEmpty().WithMessage("El empleado debe tener una sucursal asignada");
            RuleFor(rule => rule.CargoId)
                .NotEmpty().WithMessage("El empleado debe tener un cargo");
            RuleFor(rule => rule)
                .MustAsync(CheckEmpleado).WithMessage("El empleado no existe");
            RuleFor(rule => rule)
                .MustAsync(CheckCargos).WithMessage("No existe el cargo");
            RuleFor(rule => rule)
                .MustAsync(CheckSucursales).WithMessage("No existe la sucursal");
            RuleFor(rule => rule)
                .MustAsync(CheckJefes).WithMessage("No existe el jefe que trata de asignarle al empleado")
                .When(rule => rule.JefeId != Guid.Empty && rule.JefeId != null);

            _context = context;
        }

        private bool ContieneSoloNumeros(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            foreach (char c in value)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        private async Task<bool> CheckEmpleado(PutEmpleadoCommand command, CancellationToken Token)
        {
            bool CheckEmpleado = await _context.Empleados.AnyAsync(p => p.DNI == command.DNI);
            return CheckEmpleado;
        }

        private async Task<bool> CheckCargos(PutEmpleadoCommand command, CancellationToken Token)
        {
            bool CheckCargos = await _context.Cargos.AnyAsync(p => p.Id == command.CargoId);
            return CheckCargos;
        }

        private async Task<bool> CheckSucursales(PutEmpleadoCommand command, CancellationToken Token)
        {
            bool CheckSucursales = await _context.Sucursales.AnyAsync(p => p.Id == command.SucursalId);
            return CheckSucursales;
        }

        private async Task<bool> CheckJefes(PutEmpleadoCommand command, CancellationToken Token)
        {
            bool CheckJefes = await _context.Empleados.AnyAsync(p => p.Id == command.JefeId);
            return CheckJefes;
        }
    }
}
