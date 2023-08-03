namespace EmpresaGenericaAPI.Dtos.Respuestas
{
    public class RespuestaEmpleadoDTOFiltro : RespuestaBase
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public DateTime FechaAlta { get; set; }
        public Guid SucursalId { get; set; }
        public Guid CargoId { get; set; }
        public Guid? JefeId { get; set; }
    }
}
