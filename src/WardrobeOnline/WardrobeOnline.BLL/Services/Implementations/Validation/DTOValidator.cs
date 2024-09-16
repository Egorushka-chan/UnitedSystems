using FluentValidation;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace WardrobeOnline.BLL.Services.Implementations.Validation
{
    public class DTOValidator : AbstractValidator<IEntityDTO>
    {
        public DTOValidator()
        {
            RuleFor(x => x.ID).GreaterThanOrEqualTo(1);
        }
    }
}
