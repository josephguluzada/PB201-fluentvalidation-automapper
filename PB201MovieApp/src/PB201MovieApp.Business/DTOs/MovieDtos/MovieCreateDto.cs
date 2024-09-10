using FluentValidation;

namespace PB201MovieApp.Business.DTOs.MovieDtos;

public record MovieCreateDto(string Title, string Desc, bool isDeleted);

public class MovieCreateDtoValidator : AbstractValidator<MovieCreateDto>
{
    public MovieCreateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Not empty")
            .NotNull().WithMessage("Not null")
            .MinimumLength(2).WithMessage("Min length must be 1")
            .MaximumLength(200).WithMessage("Length must be less than 200");

        RuleFor(x => x.Desc)
            .NotNull().When(x => !x.isDeleted).WithMessage("If movie is active desc cannot be null")
            .MaximumLength(800).WithMessage("Length must be less than 800");

        RuleFor(x => x.isDeleted).NotNull();
    }
}