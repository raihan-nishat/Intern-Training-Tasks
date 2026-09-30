using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini_Task_Manager_API.DTOs.Tasks;
using Mini_Task_Manager_API.Models;
using Mini_Task_Manager_API.Repositories.Interfaces;
using System.Security.Claims;

namespace MiniTaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository taskRepository;
        private readonly IMapper mapper;

        public TasksController(ITaskRepository taskRepository, IMapper mapper)
        {
            this.taskRepository = taskRepository;
            this.mapper = mapper;
        }


        // get: api/tasks
        
        [HttpGet]
        public IActionResult GetMyTasks()
        {
            var username =
                User.FindFirstValue(
                    ClaimTypes.Name
                );

            var tasks =
                taskRepository.GetByOwner(username!);

            var result =
                mapper.Map<List<TaskResponseDto>>(tasks);

            return Ok(result);
        }


        // get: api/tasks/1
        
        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var username =
                User.FindFirstValue(
                    ClaimTypes.Name
                );

            var task =
                taskRepository.GetById(id);

            if (task == null)
            {
                return NotFound(
                    new
                    {
                        message = "task not found"
                    }
                );
            }

            if (task.ownerUsername != username)
            {
                return Forbid();
            }

            var result =
                mapper.Map<TaskResponseDto>(task);

            return Ok(result);
        }


        // post: api/tasks
        
        [HttpPost]
        public IActionResult CreateTask(
            TaskCreateDto dto)
        {
            var username =
                User.FindFirstValue(
                    ClaimTypes.Name
                );

            var task =
                mapper.Map<TaskItem>(dto);

            task.ownerUsername = username!;
            task.createdAt = DateTime.UtcNow;
            task.isCompleted = false;

            taskRepository.Add(task);

            var result =
                mapper.Map<TaskResponseDto>(task);

            return CreatedAtAction(
                nameof(GetTask),
                new { id = task.id },
                result
            );
        }


        // put: api/tasks/1
        
        [HttpPut("{id}")]
        public IActionResult UpdateTask(
            int id,
            TaskUpdateDto dto)
        {
            var username =
                User.FindFirstValue(
                    ClaimTypes.Name
                );

            var existingTask =
                taskRepository.GetById(id);

            if (existingTask == null)
            {
                return NotFound(
                    new
                    {
                        message = "task not found"
                    }
                );
            }

            if (existingTask.ownerUsername != username)
            {
                return Forbid();
            }

            existingTask.title = dto.title;
            existingTask.description = dto.description;
            existingTask.isCompleted = dto.isCompleted;

            taskRepository.Update(existingTask);

            var result =
                mapper.Map<TaskResponseDto>(
                    existingTask
                );

            return Ok(result);
        }


        // delete: api/tasks/1
        
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var username =
                User.FindFirstValue(
                    ClaimTypes.Name
                );

            var task =
                taskRepository.GetById(id);

            if (task == null)
            {
                return NotFound(
                    new
                    {
                        message = "task not found"
                    }
                );
            }

            if (task.ownerUsername != username)
            {
                return Forbid();
            }

            taskRepository.Delete(id);

            return Ok(
                new
                {
                    message = "task deleted successfully"
                }
            );
        }


        // get: api/tasks/all
        
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllTasks()
        {
            var tasks =
                taskRepository.GetAll();

            var result =
                mapper.Map<List<TaskResponseDto>>(
                    tasks
                );

            return Ok(result);
        }
    }
}