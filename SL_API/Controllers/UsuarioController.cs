using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static DL.AppDbContext;

namespace SL_API.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Add(usuario, _context);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Usuario.GetAll(_context);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            ML.Result result = BL.Usuario.GetById(id, _context);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Update(usuario, _context);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            ML.Result result = BL.Usuario.Delete(id, _context);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

    }
}
