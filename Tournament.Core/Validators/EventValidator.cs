using System;
using FluentValidation;
using FluentValidation.Results;
using Tournament.Core.Models;

namespace Tournament.Core.Validators
{
    public class EventValidator : AbstractValidator<EventRequest>
    {
        public EventValidator()
        {
            DateTime eventDateTime = DateTime.Now;
            
            RuleFor(x => x.TournamentId)
                .NotEmpty()
                .WithMessage("Tournament is required.")
                .OverridePropertyName("tournamentId");

            RuleFor(x => x.EventName)
                .MaximumLength(100)
                .NotEmpty()
                .WithMessage("Event name is required.")
                .OverridePropertyName("eventName");

            RuleFor(x => x.EventNumber)
                .NotEmpty()
                .WithMessage("Event number is required.")
                .OverridePropertyName("eventNumber");

            RuleFor(x => x.EventDateTime)
                .NotEmpty()
                .WithMessage("Event date is required.")
                .OverridePropertyName("eventDateTime");

            RuleFor(x => x.EventDateTime)
                .Custom((d, context) =>
                {
                    eventDateTime = d;
                    if (d.Date <= DateTime.Now.Date.AddDays(-1))
                        context.AddFailure(new ValidationFailure("eventDateTime", $"Event date cannot be before today"));
                });

            RuleFor(x => x.EventEndDateTime)
                .NotEmpty()
                .WithMessage("Event end date is required.")
                .OverridePropertyName("eventEndDateTime");

            RuleFor(x => x.EventDateTime)
                .Custom((d, context) =>
                {
                    
                    if (d.Date <= eventDateTime.Date)
                        context.AddFailure(new ValidationFailure("eventEndDateTime", $"Event end date  cannot be before the eventDateTime"));
                });

            RuleFor(x => x.AutoClose)
                .NotEmpty()
                .WithMessage("Auto close is required.")
                .OverridePropertyName("autoClose");
        }
    }
}