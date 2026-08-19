using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Contracts.Services
{
    public interface IAuthService
    {
        User Login(string username, string password);
        void Register(string username, string password);
    }
}
