﻿using System.Collections.Generic;
using DataAccessLayer;
using DataTypeObject;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using BusinessLogicalLayer.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace BusinessLogicalLayer
{
    public class UserBLL : IUSERCRUD
    {
        List<ErrorField> errors = new List<ErrorField>();
        private readonly BatteryCollectorDbContext userDbContext;
        public UserBLL(BatteryCollectorDbContext _userDbContext)
        {
            userDbContext = _userDbContext;
            userDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add(User user)
        {

            validateEmail(user.Email);
            user.Email = user.Email.ToLower();
            //adicionar outros métodos de validação e implementá-los

            userDbContext.Add(user);
            userDbContext.SaveChanges();
        }

        public List<RankingData> GetRankingData() {
            string[] array = new string[5];
            var ranking = userDbContext.Users.
            OrderByDescending(x => x.TotalPoints).Select(x => new RankingData(x.Name, x.TotalPoints)).ToList();
            return ranking;
        }

        public User Find(int _Id)
        {
            return userDbContext.Users.Find(_Id);
        }

        public void UpdatePoints ( User user, int pointsToAdd) {
            user.TotalPoints += pointsToAdd;
            Update(user);
        }

        public IEnumerable<User> GetAll()
        {
            //return userDbContext.Users.;
            return userDbContext.Users.ToList();
        }

        public void Remove(int Id)
        {
            var userFound = userDbContext.Users.Find(Id);
            userDbContext.Users.Remove(userFound);
            userDbContext.SaveChanges();
        }

        public User Update(User user)
        {
            int totalPoints = userDbContext.Users.Where(x => x.Id == user.Id).Select(x => x.TotalPoints).ToList()[0];
            user.TotalPoints = totalPoints;
            EntityEntry<User> response = userDbContext.Update(user);
            userDbContext.SaveChanges();
            return user;
            throw new Exception();
        }

        public User Authenticate(string username, string password)
        {
                validateEmail(username);
                validatePasswordString(password);
                if (errors.Count > 0)
                {
                    return null;
                }
                return userDbContext.Users.FirstOrDefault(u => u.Email == username && u.Password == password);
        }

        private void validatePasswordString(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                errors.Add(new ErrorField());

            }
            else if (password.Length < 8)
            {
                errors.Add(new ErrorField());

            }
        }


        private void validateEmail(string Email)
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorField error = new ErrorField()
                {
                    Error = "Email deve ser informado.",
                    PropertyName = "Email"
                };
                errors.Add(error);
            }
            else if (!Regex.IsMatch(Email, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$"))
            {
                ErrorField error = new ErrorField()
                {
                    Error = "Email inválido.",
                    PropertyName = "Email"
                };
                errors.Add(error);
            }
        }

        private void validateCellphone(string CelphoneNumber)
        {
            if (string.IsNullOrWhiteSpace(CelphoneNumber))
            {
                ErrorField error = new ErrorField()
                {
                    Error = "Telefone deve ser informado.",
                    PropertyName = "Telefone"
                };
                errors.Add(error);
            }
            else if (CelphoneNumber.Length < 7 && CelphoneNumber.Length > 15)
            {
                ErrorField error = new ErrorField()
                {
                    Error = "Telefone deve conter entre 7 e 15 caracteres.",
                    PropertyName = "Telefone"
                };
                errors.Add(error);
            }
        }

        
    }
}
