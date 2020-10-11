using FluentValidation;
using Tournament.Core.Models;

namespace Tournament.Core.Validators
{
    public class TournamentValidator : AbstractValidator<TournamentRequest>
    {
        public TournamentValidator()
        {
            RuleFor(x => x.TournamentName)
                .MaximumLength(200)
                .NotEmpty()
                .WithMessage("tournament name is required.")
                .OverridePropertyName("tournamentName");
        }
    }
}
