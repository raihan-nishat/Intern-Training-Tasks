using FluentValidation;
using Mini_Task_Manager_API.DTOs.Tasks;

namespace MiniTaskManager.Validators
{
    public class TaskCreateDtoValidator : AbstractValidator<TaskCreateDto>
    {
        public TaskCreateDtoValidator()
        {
            RuleFor(x => x.title)
                .NotEmpty()
                .WithMessage("title is required")
                .MaximumLength(100)
                .WithMessage("title cannot exceed 100 characters");

            RuleFor(x => x.description)
                .MaximumLength(500)
                .WithMessage("description cannot exceed 500 characters");
        }
    }
}