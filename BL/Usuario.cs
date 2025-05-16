using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DL.AppDbContext;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll(ApplicationDbContext context)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = context.UsuarioGets
                .FromSqlRaw(@"
                    SELECT 
                        u.IdUsuario,
                        u.UserName,
                        u.Nombre,
                        u.ApellidoPaterno,
                        u.ApellidoMaterno,
                        l.Email,
                        l.Password,
                        u.FechaNacimiento,
                        u.Sexo,
                        u.Telefono,
                        u.Celular,
                        u.Estatus,
                        u.CURP
                    FROM Usuarios u
                    INNER JOIN Logins l ON u.IdLogin = l.IdLogin
                ").ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var objBD in query)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Login = new ML.Login();
                        usuario.IdUsuario = objBD.IdUsuario;
                        usuario.UserName = objBD.UserName;
                        usuario.Nombre = objBD.Nombre;
                        usuario.ApellidoPaterno = objBD.ApellidoPaterno;
                        usuario.ApellidoMaterno = objBD.ApellidoMaterno;
                        usuario.Login.Email = objBD.Email;
                        usuario.Login.Password = objBD.Password;
                        usuario.FechaNacimiento = objBD.FechaNacimiento.ToString(("dd/MM/yyyy"));
                        usuario.Sexo = objBD.Sexo;
                        usuario.Telefono = objBD.Telefono;
                        usuario.Celular = objBD.Celular;
                        usuario.Estatus = objBD.Estatus;
                        usuario.CURP = objBD.CURP;

                        result.Objects.Add(usuario);
                        result.Correct = true;
                    }
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No hay registros de usuarios";
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

        public static ML.Result Delete(int idUsuario, ApplicationDbContext context)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var usuario = (from usuarioDB in context.Usuarios
                                       where usuarioDB.IdUsuario == idUsuario
                                       select usuarioDB).SingleOrDefault();

                        var login = (from loginDB in context.Logins
                                     where loginDB.IdLogin == usuario.IdLogin
                                     select loginDB).SingleOrDefault();

                        context.Usuarios.Remove(usuario);
                        context.Logins.Remove(login);

                        int filasAfectadas = context.SaveChanges();

                        if (filasAfectadas == 2)
                        {
                            transaction.Commit();
                            result.Correct = true;
                        }
                        else
                        {
                            transaction.Rollback();
                            result.Correct = false;
                            result.ErrorMessage = "No se eliminaron ambos los registros";
                        }
                    }
                    catch (Exception exTransaccion)
                    {
                        transaction.Rollback();
                        result.Correct = false;
                        result.ErrorMessage = "Hubo un error en la transacción.";
                        result.Ex = exTransaccion;
                    }
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


        public static ML.Result Add(ML.Usuario usuario, ApplicationDbContext context)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        DL.Usuario usuarioBD = new DL.Usuario();
                        usuarioBD.Login = new DL.Login();

                        usuarioBD.Login.Email = usuario.Login.Email;
                        usuarioBD.Login.Password = usuario.Login.Password;

                        usuarioBD.UserName = usuario.UserName;
                        usuarioBD.Nombre = usuario.Nombre;
                        usuarioBD.ApellidoPaterno = usuario.ApellidoPaterno;
                        usuarioBD.ApellidoMaterno = usuario.ApellidoMaterno;
                        usuarioBD.FechaNacimiento = DateTime.Parse(usuario.FechaNacimiento.ToString());
                        usuarioBD.Sexo = usuario.Sexo;
                        usuarioBD.Telefono = usuario.Telefono;
                        usuarioBD.Celular = usuario.Celular;
                        usuarioBD.Estatus = usuario.Estatus;
                        usuarioBD.CURP = usuario.CURP;

                        context.Usuarios.Add(usuarioBD);

                        int filasAfectadas = context.SaveChanges();

                        if (filasAfectadas == 2)
                        {
                            transaction.Commit();
                            result.Correct = true;
                        }
                        else
                        {
                            transaction.Rollback();
                            result.Correct = false;
                            result.ErrorMessage = "No se insertaron los registros";
                        }
                    }
                    catch (Exception exTransaccion)
                    {
                        transaction.Rollback();
                        result.Correct = false;
                        result.ErrorMessage = "Hubo un error al insertar la transaccion";
                        result.Ex = exTransaccion;
                    }
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


        public static ML.Result GetById(int idUsuario, ApplicationDbContext context)
        {
            ML.Result result = new ML.Result();

            try
            {

                var usuarioDB = (from usuario in context.Usuarios
                                 where usuario.IdUsuario == idUsuario
                                 select usuario).SingleOrDefault();

                if (usuarioDB != null)
                {
                    var loginDB = (from login in context.Logins
                                   where login.IdLogin == usuarioDB.IdLogin
                                   select login).SingleOrDefault();

                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Login = new ML.Login();

                    usuario.IdUsuario = usuarioDB.IdUsuario;
                    usuario.UserName = usuarioDB.UserName;
                    usuario.Nombre = usuarioDB.Nombre;
                    usuario.ApellidoPaterno = usuarioDB.ApellidoPaterno;
                    usuario.ApellidoMaterno = usuarioDB.ApellidoMaterno;
                    usuario.Login.Email = loginDB.Email;
                    usuario.Login.Password = loginDB.Password;
                    usuario.FechaNacimiento = usuarioDB.FechaNacimiento.ToString("dd/MM/yyyy");
                    usuario.Sexo = usuarioDB.Sexo;
                    usuario.Telefono = usuarioDB.Telefono;
                    usuario.Celular = usuarioDB.Celular;
                    usuario.Estatus = usuarioDB.Estatus;
                    usuario.CURP = usuarioDB.CURP;

                    result.Object = usuario;
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No existe el usuario";
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


        public static ML.Result Update(ML.Usuario usuario, ApplicationDbContext context)
        {
            ML.Result result = new ML.Result();

            try
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = (from usuarioSelect in context.Usuarios
                                     where usuarioSelect.IdUsuario == usuario.IdUsuario
                                     select usuarioSelect).SingleOrDefault();

                        if (query != null)
                        {

                            query.Login = new DL.Login();

                            query.IdUsuario = usuario.IdUsuario;
                            query.Nombre = usuario.Nombre;
                            query.UserName = usuario.UserName;
                            query.ApellidoPaterno = usuario.ApellidoPaterno;
                            query.ApellidoMaterno = usuario.ApellidoMaterno;
                            query.Login.Email = usuario.Login.Email;
                            query.Login.Password = usuario.Login.Password;
                            query.FechaNacimiento = DateTime.Parse(usuario.FechaNacimiento.ToString());
                            query.Sexo = usuario.Sexo;
                            query.Telefono = usuario.Telefono;
                            query.Celular = usuario.Celular;
                            query.Estatus = usuario.Estatus;
                            query.CURP = usuario.CURP;

                            int filasAfectadas = context.SaveChanges();

                            if (filasAfectadas == 2)
                            {
                                transaction.Commit();
                                result.Correct = true;
                            }
                            else
                            {
                                transaction.Rollback();
                                result.Correct = false;
                                result.ErrorMessage = "No se pudieron actualizar ambos registros (usuario y login).";
                            }
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontró el usuario para actualizar.";
                        }
                    }
                    catch (Exception exTransaccion)
                    {
                        transaction.Rollback();
                        result.Correct = false;
                        result.ErrorMessage = "Hubo un error al actualizar ";
                        result.Ex = exTransaccion;
                    }
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
