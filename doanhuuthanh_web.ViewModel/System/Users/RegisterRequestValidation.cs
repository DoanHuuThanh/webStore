using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doanhuuthanh_web.ViewModel.System.Users
{
    public class RegisterRequestValidation:AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidation() { 
            RuleFor(x=>x.FirstName).NotEmpty().WithMessage("FirstName is required").MaximumLength(200).WithMessage("FirstName can not over 200 characters");
            RuleFor(x=>x.LastName).NotEmpty().WithMessage("LastName is required").MaximumLength(200).WithMessage("LastName can not over 200 characters");
            RuleFor(x=>x.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Birthday cannot greaterthan 100 years");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").Matches("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$").WithMessage("Email format not match");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is reqired");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User is reqired");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").MinimumLength(6).WithMessage("Password is at least 6 characters");
            RuleFor(x => x).Custom((request, context) => //request là đối tượng kiểm tra (các thành phần của registerrequest),context là đưa ra tông báo
            {
                if(request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Confirm password is not match");
                }
            }
            );
        }
    }
}
