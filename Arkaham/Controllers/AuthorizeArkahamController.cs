using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data.SqlClient;
using Arkaham.Models;

namespace Arkaham.Controllers
{
    public class AuthorizeArkaham : AuthorizeAttribute
    {
        private AspNetEntities db = new AspNetEntities();
        private readonly string[] allowedroles;

        public AuthorizeArkaham(params string[] roles)
        {
            this.allowedroles = roles[0].Split(',');
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            //var identityName = System.Web.HttpContext.Current.User.Identity.Name.ToString();
            ////var GID = identityName.Substring(8).Trim();
            //var GID = identityName;
            var GID = System.Web.HttpContext.Current.User.Identity.Name.ToString();

            Users UserInfo = new Users();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "MXSCT010011SRV";            // update me
            builder.UserID = "FactDBUser";                          // update me
            builder.Password = "Siemens2018";                   // update me
            builder.InitialCatalog = "AspNet";   // update me

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {

                connection.Open();

                string sqlStatement =
                    $"SELECT [GID], [GID] " +
                    $"FROM [dbo].[Users] " +
                    $"WHERE [GID] = '{GID}'";

                using (SqlCommand command = new SqlCommand(sqlStatement, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserInfo.GID = reader.GetString(0);
                            UserInfo.GID = reader.GetString(1).Split(' ')[0]; //Remove empty spaces after role string
                        }
                    }
                }
               

                /*
                 * Buscar Comparar Role con cualquiera de los allowed roles
                 */

                foreach (var role in allowedroles)
                {
                    if (UserInfo.GID != null && UserInfo.GID == role)
                    {
                        authorize = true;
                        break;
                    }
                }

                //connection.Close();

            }
            return authorize;
        }
    }
}




