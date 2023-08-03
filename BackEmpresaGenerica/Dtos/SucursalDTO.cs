using EmpresaGenericaAPI.Models;

namespace EmpresaGenericaAPI.Dtos
{
    public class SucursalDTO
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public CiudadDTO Ciudad { get; set; }
    }
}
