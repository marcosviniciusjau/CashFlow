using AutoMapper;
using CashFlow.App.Validations.Users;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.App.Validations.Users.Register;
public class RegisterUserValidation : IRegisterUserValidation
{
    private readonly IExpensesWrite _repo;
    private readonly IUnitOfWork _unityOfWork;
    private readonly IMapper _mapper;
    public RegisterUserValidation(
        IExpensesWrite repo,
        IUnitOfWork unityOfWork,
        IMapper mapper
        )
    {
        _repo = repo;
        _unityOfWork = unityOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredUser> Execute(RequestUser request)
    {
        Validate(request);

        var user = _mapper.Map<User>(request);
        return new ResponseRegisteredUser
        {
            Name = user.Name
        };
    }

    private void Validate(RequestExpenses request)
    {
        var validator = new UserValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidation(errorMessages);
        }
    }
}