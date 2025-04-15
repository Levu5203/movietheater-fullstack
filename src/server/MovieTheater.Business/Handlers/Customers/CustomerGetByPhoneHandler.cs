using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Handlers.Customers;

public class CustomerGetByPhoneHandler(IUnitOfWork unitOfWork, IMapper mapper): BaseHandler(unitOfWork, mapper), IRequestHandler<CustomerGetByPhoneQuery, User>
{
    public async Task<User> Handle(CustomerGetByPhoneQuery request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.UserRepository.GetQuery().Where(c => c.PhoneNumber == request.phoneNumber).FirstOrDefaultAsync(cancellationToken);
        if (customer == null) throw new Exception("Customer not found");
        return customer;
    }
}
