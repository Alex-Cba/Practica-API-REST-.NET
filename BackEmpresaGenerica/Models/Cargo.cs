using System;
using System.Collections.Generic;

namespace EmpresaGenericaAPI.Models;

public partial class Cargo
{
    public Guid Id { get; set; }
    public string? Nombre { get; set; }
    public virtual ICollection<Empleado>? Empleados { get; set; }
}
