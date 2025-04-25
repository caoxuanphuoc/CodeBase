using FluentValidation;
using CodeBase.Service.Handlers.V1.Student.Dto;

namespace CodeBase.Service.Handlers.V1.Student.Validators
{
    public class UpdateStudentDtoValidator : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(50).WithMessage("Tên không được vượt quá 50 ký tự");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Họ không được để trống")
                .MaximumLength(50).WithMessage("Họ không được vượt quá 50 ký tự");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không hợp lệ")
                .MaximumLength(100).WithMessage("Email không được vượt quá 100 ký tự");

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Số điện thoại không được vượt quá 20 ký tự");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Ngày sinh không được để trống");

            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("Địa chỉ không được vượt quá 200 ký tự");
        }
    }
}