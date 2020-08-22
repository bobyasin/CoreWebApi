using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
namespace Commander.Controllers
{
    //[Route("api/[controller]")] => bu şekilde de yapabiliriz
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;
        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commands = _repository.GetAllCommands();
            if (commands.Count() > 0)
            {
                return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var command = _repository.GetCommandById(id);
            if (command != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(command));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost(Name = "CreateCommand")]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto cmd)
        {
            var commandModel = _mapper.Map<Command>(cmd);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();
            var commanReadModel = _mapper.Map<CommandReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commanReadModel.Id }, cmd);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpateDto cmd)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            //var model = _mapper.Map<Command>(cmd);
            _mapper.Map(cmd, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpateDto> patchDocument)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpateDto>(commandModelFromRepo);

            patchDocument.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE  api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}