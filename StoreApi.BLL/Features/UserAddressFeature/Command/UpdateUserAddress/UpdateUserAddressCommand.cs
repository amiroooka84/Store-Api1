﻿using MediatR;
using StoreApi.Entity._Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.BLL.Features.UserAddressFeature.Command.UpdateUserAddress
{
    public class UpdateUserAddressCommand : IRequest<Address>
    {
        public Address Address { get; set; } 

    }
}
