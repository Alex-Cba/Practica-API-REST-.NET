namespace EmpresaGenericaAPI.Dtos.Respuestas
{
    public class RespuestaBase
    {
        public int StatusCode { get; set; }
        public string? MessageError { get; set; }
        public bool IsSuccess { get; set; }
    }
}
