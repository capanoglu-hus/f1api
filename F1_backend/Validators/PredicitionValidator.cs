using Azure.Core;
using f1api.Dtos;
using f1api.Models;
using FluentValidation;

namespace f1api.Validators
{
    public class PredicitionValidator: AbstractValidator<RacePre>
    {
        public PredicitionValidator()
        {
            RuleFor(x => x.RaceId).NotEmpty();

            // Özel kural: Sürücülerin hepsi birbirinden farklı olmalı
            RuleFor(x => x)
                .Must(request =>
                    request.FirstPlaceDriverId != request.SecondPlaceDriverId &&
                    request.FirstPlaceDriverId != request.ThirdPlaceDriverId &&
                    request.SecondPlaceDriverId != request.ThirdPlaceDriverId)
                .WithMessage("Aynı sürücüyü birden fazla sıra için seçemezsiniz.");

            // Alternatif olarak 0 id gelmesin diye de kontrol edebilirsin
            RuleFor(x => x.FirstPlaceDriverId).GreaterThan(0);
            RuleFor(x => x.SecondPlaceDriverId).GreaterThan(0);
            RuleFor(x => x.ThirdPlaceDriverId).GreaterThan(0);
        }
    }

    public class DriverVoteValidator : AbstractValidator<DriverVote>
    {
        public DriverVoteValidator()
        {
            

            // Özel kural: Sürücülerin hepsi birbirinden farklı olmalı
            RuleFor(x => x)
                .Must(request =>
                    request.FirstDriverId != request.SecondDriverId &&
                    request.FirstDriverId != request.ThirdDriverId &&
                    request.SecondDriverId != request.ThirdDriverId)
                .WithMessage("Aynı sürücüyü birden fazla sıra için seçemezsiniz.");

            // Alternatif olarak 0 id gelmesin diye de kontrol edebilirsin
            RuleFor(x => x.FirstDriverId).GreaterThan(0);
            RuleFor(x => x.SecondDriverId).GreaterThan(0);
            RuleFor(x => x.ThirdDriverId).GreaterThan(0);
        }
    }
}
