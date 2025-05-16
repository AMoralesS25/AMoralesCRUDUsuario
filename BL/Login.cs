using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DL.AppDbContext;

namespace BL
{
    public class Login
    {
        public static ML.Result GetByEmailAndPassword(string email, string password, ApplicationDbContext context)
        {
            ML.Result result = new ML.Result();

            try
            {

                var loginQuery = context.Logins
                    .Where(login => login.Email == email && login.Password == password)
                    .Select(login => new
                    {
                        login.IdLogin,
                        login.Email,
                        login.Password,
                        Usuario = context.Usuarios.FirstOrDefault(usuario => usuario.IdLogin == login.IdLogin)
                    })
                    .FirstOrDefault();

                if (loginQuery != null && loginQuery.Usuario != null)
                {
                    ML.Usuario usuario = new ML.Usuario
                    {
                        IdUsuario = loginQuery.Usuario.IdUsuario,
                        UserName = loginQuery.Usuario.UserName,
                        Nombre = loginQuery.Usuario.Nombre,
                        ApellidoPaterno = loginQuery.Usuario.ApellidoPaterno,
                        ApellidoMaterno = loginQuery.Usuario.ApellidoMaterno,
                        FechaNacimiento = loginQuery.Usuario.FechaNacimiento.ToString("dd/MM/yyyy"),
                        Sexo = loginQuery.Usuario.Sexo,
                        Telefono = loginQuery.Usuario.Telefono,
                        Celular = loginQuery.Usuario.Celular,
                        Estatus = loginQuery.Usuario.Estatus,
                        CURP = loginQuery.Usuario.CURP,
                        Login = new ML.Login
                        {
                            Email = loginQuery.Email,
                            Password = loginQuery.Password
                        }
                    };

                    result.Object = usuario;
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Usuario o contraseña incorrectos";
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}
