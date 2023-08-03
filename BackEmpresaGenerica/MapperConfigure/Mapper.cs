using AutoMapper;
using EmpresaGenericaAPI.CQRS.Commands.EmpleadoPost;
using EmpresaGenericaAPI.Dtos;
using EmpresaGenericaAPI.Dtos.Respuestas;
using EmpresaGenericaAPI.Models;

namespace EmpresaGenericaAPI.MapperConfigure
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Empleado, EmpleadoDTO>().ReverseMap();
            CreateMap<Empleado, PostEmpleadoCommand>().ReverseMap();
            CreateMap<Ciudad, CiudadDTO>().ReverseMap();
            CreateMap<Cargo, CargoDTO>().ReverseMap();
            CreateMap<Sucursal, SucursalDTO>().ReverseMap();
            CreateMap<RespuestaSucursalesDTO, Sucursal>().ReverseMap();
            CreateMap<RespuestaJefe, Empleado>().ReverseMap();
            CreateMap<RespuestaEmpleadoDTOFiltro, Empleado>().ReverseMap();
            CreateMap<RespuestaGetAllEmpleadoDTO, Empleado>().ReverseMap();
            CreateMap<RespuestaDeleteEmpleado, Empleado>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.EmpleadoNombre))
                .ReverseMap();
            CreateMap<EmpleadoDTO, RespuestaEmpleadoDTO>()
                .ForMember(dest => dest.CiudadNombre, opt => opt.MapFrom(src => src.Sucursal.Ciudad.Nombre))
                .ReverseMap();
        }
    }
}
