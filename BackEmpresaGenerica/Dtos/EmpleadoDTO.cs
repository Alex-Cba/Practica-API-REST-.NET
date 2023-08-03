using EmpresaGenericaAPI.Dtos.Respuestas;
using EmpresaGenericaAPI.Models;
using System.Text.Json.Serialization;

namespace EmpresaGenericaAPI.Dtos
{
    public class EmpleadoDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public DateTime FechaAlta { get; set; }
        public SucursalDTO Sucursal { get; set; }
        public CargoDTO Cargo { get; set; }
        public EmpleadoDTO? Jefe { get; set; }
    }
}
