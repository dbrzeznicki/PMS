﻿using PMS.DAL;
using PMS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PMS
{
    public class AdminValidation
    {
        bool FirstNameValidation(string firstName)
        {
            if (Regex.IsMatch(firstName, @"[a-zA-Z]"))
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect first name! Only uppercase and lowercase letters are required");
                er.ShowDialog();
                return false;
            }
        }

        bool LastNameValidation(string lastName)
        {

            if (Regex.IsMatch(lastName, @"[a-zA-Z]"))
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect last name! Only uppercase and lowercase letters are required");
                er.ShowDialog();
                return false;
            }

        }

        bool LoginValidation(string login)
        {

            PMSContext dbContext = new PMSContext();
            List<User> users = dbContext.User.ToList();
            var check = users.Where(x => x.Login == login).SingleOrDefault();

            if (login.Length >= 6 && check == null)
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect login! Login already exists or has less than 6 characters.");
                er.ShowDialog();
                return false;
            }
        }

        bool PasswordValidation(string password)
        {

            if (password.Length >= 6)
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect password! Password has less than 6 characters.");
                er.ShowDialog();
                return false;
            }
        }


        bool SalaryValidation(double salary)
        {

            if (salary > 0)
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect salary! The salary must be greater than 0.");
                er.ShowDialog();
                return false;
            }
        }

        bool PhoneNumberValidation(string phoneNumber)
        {
            if (Regex.IsMatch(phoneNumber, @"\d{3}-\d{3}-\d{3}"))
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect phone number! Required format xxx-xxx-xxx");
                er.ShowDialog();
                return false;
            }
        }

        bool EmailValidation(string email)
        {
            if (email != null && email != "")
            {
                try
                {
                    MailAddress MA = new MailAddress(email);
                    return true;
                }
                catch (FormatException)
                {
                    ErrorMessage er = new ErrorMessage("Incorrect email!");
                    er.ShowDialog();
                    return false;
                }
            }
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect email!");
                er.ShowDialog();
                return false;
            }

        }


        bool AddUserTeamValidation(string team, string userRole)
        {

            if ((team != null && userRole != "Admin") || (team == null && userRole == "Admin"))
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect team!");
                er.ShowDialog();
                return false;
            }
        }

        /// <summary>
        /// sprawdzam czy user ma jeszce jakies subtaski
        /// </summary>
        /// <param name="user"></param>
        /// <param name="teamParam"></param>
        /// <returns></returns>
        bool EditUserTeamValidation(User user, string teamParam)
        {
            PMSContext dbContext = new PMSContext();

            if (user.UserRole.Name != "Admin")
            {
                if (user.Team.Name == teamParam)
                    return true;
                else
                {
                    int _allTask = dbContext.Subtask.Where(x => (x.UserID == user.UserID) && ((x.SubtaskStatus.Name == "New") || (x.SubtaskStatus.Name == "In progress"))).Count();
                    if (_allTask == 0)
                    {
                        return true;
                    }
                    else
                    {
                        ErrorMessage er = new ErrorMessage("User has new or in progress task!");
                        er.ShowDialog();
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }

        }






        bool FiredDateValidation(DateTime firedDate, DateTime hiredDate)
        {

            if (firedDate.CompareTo(hiredDate) > 0)
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect fired date!");
                er.ShowDialog();
                return false;
            }
        }



        public bool AddUserValidation(string firstName, string lastName, string login, string password, double salary,
            string phoneNumber, string email, string userRole, DateTime firedDate, DateTime hiredDate)
        {
            if (FirstNameValidation(firstName) && LastNameValidation(lastName) && LoginValidation(login) &&
                PasswordValidation(password) && SalaryValidation(salary) && PhoneNumberValidation(phoneNumber) &&
                EmailValidation(email) && FiredDateValidation(firedDate, hiredDate))
                return true;
            else
                return false;
        }







        public bool EditUserValidation(string firstName, string lastName, string password, double salary,
            string phoneNumber, string email, User user, DateTime firedDate, DateTime hiredDate)
        {
            if (FirstNameValidation(firstName) && LastNameValidation(lastName) &&
                PasswordValidation(password) && SalaryValidation(salary) && PhoneNumberValidation(phoneNumber) &&
                EmailValidation(email) && FiredDateValidation(firedDate, hiredDate))
                return true;
            else
                return false;
        }



        bool UrlValidation(string url)
        {
            if (Regex.IsMatch(url, @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$"))
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect url!");
                er.ShowDialog();
                return false;
            }
        }

        bool DescriptionUrlValidation(string description)
        {
            if (description.Length >= 20)
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect description! Description has less than 20 characters.");
                er.ShowDialog();
                return false;
            }
        }


        public bool WikiValidation(string url, string description)
        {
            if (UrlValidation(url) && DescriptionUrlValidation(description))
                return true;
            else
                return false;
        }







        bool NIPValidation(string NIP)
        {
            if (Regex.IsMatch(NIP, @"\d{10}"))
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect NIP! Required 10 digits");
                er.ShowDialog();
                return false;
            }
        }

        bool REGONValidation(string REGON)
        {
            if (Regex.IsMatch(REGON, @"\d{9}") || Regex.IsMatch(REGON, @"\d{14}"))
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect REGON! Required 9 or 14 digits");
                er.ShowDialog();
                return false;
            }
        }

        bool CompanyNameValidation(string companyName)
        {
            if (companyName != null && companyName != "")
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect company name!");
                er.ShowDialog();
                return false;
            }
        }

        public bool ClientValidation(string firstName, string lastName, string companyName, string NIP, string REGON)
        {
            if (FirstNameValidation(firstName) && LastNameValidation(lastName) && CompanyNameValidation(companyName) &&
                NIPValidation(NIP) && REGONValidation(REGON))
                return true;
            else
                return false;
        }


        bool TeamNameValidation(string teamName)
        {

            PMSContext dbContext = new PMSContext();
            List<Team> teams = dbContext.Team.ToList();
            var check = teams.Where(x => x.Name == teamName).SingleOrDefault();

            if (teamName.Length >= 5 && check == null)
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect team name! Team name has less than 5 characters.");
                er.ShowDialog();
                return false;
            }
        }




        bool UserListAddTeamValidation(ObservableCollection<User> userList)
        {

            foreach (var user in userList)
            {
                if (user.UserRole.Name == "Manager" && userList.Count() >= 2)
                {
                    return true;
                }
            }

            ErrorMessage er = new ErrorMessage("The team must consist of at least 2 people and have a manager.");
            er.ShowDialog();
            return false;
        }



        public bool AddTeamValidation(string TeamName, ObservableCollection<User> userList)
        {
            if (TeamNameValidation(TeamName) && UserListAddTeamValidation(userList))
                return true;
            else
                return false;
        }


        bool TeamNameEditValidation(string teamName)
        {

            if (teamName.Length >= 5)
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect team name! Team name has less than 5 characters.");
                er.ShowDialog();
                return false;
            }
        }

        public bool EditTeamValidation(string TeamName, ObservableCollection<User> userList)
        {
            if (TeamNameEditValidation(TeamName) && UserListAddTeamValidation(userList))
                return true;
            else
                return false;
        }





        bool TeamEditRemoveUserCheckSUbtaskValidation(User user)
        {

            PMSContext dbContext = new PMSContext();

            int _allTask = dbContext.Subtask.Where(x => (x.UserID == user.UserID) && ((x.SubtaskStatus.Name == "New") || (x.SubtaskStatus.Name == "In progress"))).Count();
            if (_allTask == 0)
            {
                return true;
            }
            else
            {
                ErrorMessage er = new ErrorMessage("User has new or in progress task!");
                er.ShowDialog();
                return false;
            }
        }

        public bool EditTeamRemoveUserValidation(User user)
        {
            if (TeamEditRemoveUserCheckSUbtaskValidation(user))
                return true;
            else
                return false;
        }


        //Usuniecie teamu

        bool RemoveTeamCheckSUbtaskValidation(Team team)
        {

            PMSContext dbContext = new PMSContext();

            foreach(var user in team.Users)
            {
                int _allTask = dbContext.Subtask.Where(x => (x.UserID == user.UserID) && ((x.SubtaskStatus.Name == "New") || (x.SubtaskStatus.Name == "In progress"))).Count();
                
                if (_allTask > 0)
                {        
                    ErrorMessage er = new ErrorMessage("Users have new or in progress task!");
                    er.ShowDialog();
                    return false;
                }
            }

            return true;
        }

        public bool RemoveTeamValidation(Team team)
        {
            if (RemoveTeamCheckSUbtaskValidation(team))
                return true;
            else
                return false;
        }
    }
}
