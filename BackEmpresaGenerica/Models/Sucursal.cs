using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpresaGenericaAPI.Models;

public partial class Sucursal
{
    public Guid Id { get; set; }
    public string? Nombre { get; set; }

    [ForeignKey("Ciudad")]
    public Guid CiudadId { get; set; }
    public Ciudad? Ciudad { get; set; }
    public virtual ICollection<Empleado>? Empleados { get; set; }
}
