using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BookShopModel;
using BookShopModel.Interfaces;
using BookShopModel.ContainerEF;
using BookShopModel.Model;

namespace BookShopWeb.Models
{
    /// <summary>
    /// Custom RoleProvider
    /// </summary>
    public class BookShopRoleProvider : RoleProvider
    {
        Repository repo;
        public BookShopRoleProvider()
        {
            repo = new Repository(new ModelContainerEF());
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> roles = new List<string>();
            User user = repo.GetUser(username);
            if (user != null)
            {
              roles.Add("User");
              if (user.IsAdmin) roles.Add("Admin");
            }
            return roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}