namespace EmpresaGenericaAPI.Dtos.Respuestas
{
    public class RespuestaGetAllEmpleadoDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public DateTime FechaAlta { get; set; }
        public SucursalDTO Sucursal { get; set; }
        public CargoDTO Cargo { get; set; }
        public string? JefeNombre { get; set; }
    }
}
