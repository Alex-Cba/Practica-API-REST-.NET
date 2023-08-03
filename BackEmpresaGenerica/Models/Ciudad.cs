using System;
using System.Collections.Generic;

namespace EmpresaGenericaAPI.Models;

public partial class Ciudad
{
    public Guid Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Sucursal>? Sucursales { get; set; }
}
