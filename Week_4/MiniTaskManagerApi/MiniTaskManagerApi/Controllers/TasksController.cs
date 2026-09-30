using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniTaskManagerApi.DTOs.Tasks;
using MiniTaskManagerApi.Repositories;

namespace MiniTaskManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<TaskCreateDto> _validator;

        public TasksController(ITaskRepository taskRepository, IMapper mapper, IValidator<TaskCreateDto> validator)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult GetMyTasks()
        {
            string username = User.Identity!.Name!;
            var tasks = _taskRepository.GetByOwner(username);
            var response = _mapper.Map<List<TaskResponseDto>>(tasks);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            string username = User.Identity!.Name!;
            var task = _taskRepository.GetById(id);

            if(task == null)
            {
                return NotFound();
            }
            if(task.OwnerUsername != username)
            {
                return Forbid();
            }
            var response = _mapper.Map<TaskResponseDto>(task);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create(TaskCreateDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            string username = User.Identity!.Name!;
            var task = _mapper.Map<Models.TaskItem>(dto);
            task.OwnerUsername = username;
            task.CreatedAt = DateTime.UtcNow;
            task.IsCompleted = false;
            _taskRepository.Add(task);

            var response = _mapper.Map<TaskResponseDto>(task);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskUpdateDto dto)
        {
            string username = User.Identity!.Name!;
            var task = _taskRepository.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            if (task.OwnerUsername != username)
            {
                return Forbid();
            }
            
            task.Title = dto.Title;
            task.Description = dto.Description;
            task.IsCompleted = dto.IsCompleted;
            _taskRepository.Update(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string username = User.Identity!.Name!;
            var task = _taskRepository.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            if (task.OwnerUsername != username)
            {
                return Forbid();
            }
            _taskRepository.Delete(id);
            return NoContent();
        }

        [HttpGet("all")]
        public IActionResult GetAllTasks()
        {
            var tasks = _taskRepository.GetAll();
            var response = _mapper.Map<List<TaskResponseDto>>(tasks);
            return Ok(response);
        }
    }
}
