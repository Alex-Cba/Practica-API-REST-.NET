using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using EmpresaGenericaAPI.Models;

namespace EmpresaGenericaAPI.Data;

public partial class EfcodeFirstContext : DbContext
{
    public EfcodeFirstContext(DbContextOptions<EfcodeFirstContext> options) : base(options)
    {

    }

    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Sucursal> Sucursales { get; set; }
    public DbSet<Ciudad> Ciudades { get; set; }
    public DbSet<Cargo> Cargos { get; set; }
}
