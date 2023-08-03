namespace EmpresaGenericaAPI.Dtos.Respuestas
{
    public class RespuestaEmpleadoDTO : RespuestaBase
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public DateTime FechaAlta { get; set; }
        public string SucursalNombre { get; set; }
        public string CiudadNombre { get; set; }
        public string CargoNombre { get; set; }
        public string? JefeNombre { get; set; }
    }
}
