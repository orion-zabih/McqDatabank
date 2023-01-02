using KTS_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace KTS_Testing_System.Classes
{
    public class CustomPrincipal : IPrincipal
    {
        public decimal Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
        public string Designation { get; set; }
        public string DepartmentName { get; set; }
        public decimal DepartmentCode { get; set; }

        public CustomPrincipal(string UserId)
        {
            this.Identity = new GenericIdentity(UserId);
        }

        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            //int rol = int.Parse(role);
            ////if (roles.Any(r => role.Contains(rol)))
            ////if (Roles.RoleExists(role))
            //if(roles.Contains(rol))
            //{
            //    return true;
            //}
            //else
            //{
            //string[] roleArr = role.Split(':');

            //if (roles.Any(s => s.Contains(role)))
            try
            {
                if (string.IsNullOrEmpty(role))
                    return false;

                using (var context = new Kts_dataEntities())
                {
                    var dbUser = context.Users.FirstOrDefault(x => x.user_Id == Id);
                    if (dbUser != null)
                    {
                        if (dbUser.status.Trim().ToLower().Equals("banned") || dbUser.status.Trim().ToLower().Equals("deleted"))
                        {
                            return false;
                        }
                    }
                    var roleCode = role.Split(':')[0];

                    var RoleMap = dbUser.Roles.ToList();
                    var RolesDb = context.Roles.ToList();
                    var Roles = from r in RolesDb
                                join m in RoleMap on r.role_id equals m.role_id
                                select r;
                    if (Roles.Any(s => s.description == roleCode))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.Write("CustomPrincipal", "IsInRole", ex.Message.ToString(), ex.StackTrace.ToString());
                return false;
            }
        }


      
    }

}