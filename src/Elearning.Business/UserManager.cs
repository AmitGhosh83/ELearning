﻿using Elearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elearning.Business
{
    public interface IUserManager
    {
        UserModel Login(string email, string password);
        UserModel Register(string email, string password);
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public UserModel Login(string email, string password)
        {
            var user = userRepository.Login(email, password);
            if(user== null)
            {
                return null;
            }
            return new UserModel { Id = user.Id, Name = user.Name };
        }

        public UserModel Register(string email, string password)
        {
            var user = userRepository.Register(email, password);
            
            if(user!= null)
            {
                return new UserModel { Id = user.Id, Name = user.Name };
            }
            return null;
        }
    }
}
