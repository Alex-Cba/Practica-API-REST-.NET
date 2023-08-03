using MediatR;
using Microsoft.AspNetCore.Mvc;
using EmpresaGenericaAPI.CQRS.Commands.EmpleadoDelete;
using EmpresaGenericaAPI.CQRS.Commands.EmpleadoPost;
using EmpresaGenericaAPI.CQRS.Commands.EmpleadoPut;
using EmpresaGenericaAPI.CQRS.Query;
using EmpresaGenericaAPI.CQRS.Query.AllCargos;
using EmpresaGenericaAPI.CQRS.Query.AllCiudades;
using EmpresaGenericaAPI.CQRS.Query.AllEmpleados;
using EmpresaGenericaAPI.CQRS.Query.AllJefes;
using EmpresaGenericaAPI.CQRS.Query.AllSucursales;
using EmpresaGenericaAPI.CQRS.Query.GetEmpleadoFiltro;
using EmpresaGenericaAPI.Dtos;
using EmpresaGenericaAPI.Dtos.Respuestas;

namespace EmpresaGenericaAPIFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllControlleres : Controller
    {
        private readonly IMediator _mediator;

        public AllControlleres(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/GetAllEmpleados")]
        public async Task<ListaEmpleados> GetEmpleados()
        {
            return await _mediator.Send(new GetEmpleadoAllEmpleadoQuery());
        }

        [HttpGet]
        [Route("/GetAllCargos")]
        public async Task<ListaCargos> GetCargos()
        {
            return await _mediator.Send(new GetAllCargosQuery());
        }

        [HttpGet]
        [Route("/GetAllSucursales")]
        public async Task<ListaSucursales> GetSucursales()
        {
            return await _mediator.Send(new GetAllSucursalesQuery());
        }

        [HttpGet]
        [Route("/GetAllCiudades")]
        public async Task<ListaCiudades> GetCiudades()
        {
            return await _mediator.Send(new GetAllCiudadesQuery());
        }

        [HttpGet]
        [Route("/GetAllJefes")]
        public async Task<ListaJefes> GetJefes()
        {
            return await _mediator.Send(new GetAllJefesQuery());
        }

        [HttpGet]
        [Route("/GetEmpleadoFiltro")]
        public async Task<RespuestaEmpleadoDTOFiltro> GetEmpleadoConFiltro([FromQuery] GetEmpleadoQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        [Route("/PostEmpleado")]
        public async Task<ActionResult> PostEmpleadoDTO([FromBody] PostEmpleadoCommand command)
        {
            try
            {
                var respuesta = await _mediator.Send(command);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("/PutEmpleado")]
        public async Task<ActionResult> PutEmpleadoDTO([FromBody] PutEmpleadoCommand command)
        {
            try
            {
                var respuesta = await _mediator.Send(command);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/DeleteEmpleado")]
        public async Task<ActionResult> DeletePersona([FromQuery] DeleteEmpleadoCommand command)
        {
            try
            {
                var respuesta = await _mediator.Send(command);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
