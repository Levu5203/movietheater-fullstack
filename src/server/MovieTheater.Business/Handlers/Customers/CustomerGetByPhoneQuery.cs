using System;
using MediatR;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Handlers.Customers;

public class CustomerGetByPhoneQuery : IRequest<User>
{
    public string? phoneNumber {get; set;}
}
