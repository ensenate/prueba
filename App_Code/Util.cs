using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Security.Cryptography;
using AdamTibi.Web.Security;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Net;
using System.Xml.Linq;
using MySql.Data.MySqlClient;


    public class Util
    {


        public static void CreateEncryptedCookie(bool remember)
        {
            HttpCookie cookie = new HttpCookie(cookiesNombre.cookieNombre);
            if (remember) cookie.Expires = DateTime.Now.AddDays(30);

            HttpCookie encodedCookie = HttpSecureCookie.Encode(cookie, CookieProtection.Encryption);
            HttpContext.Current.Response.AppendCookie(encodedCookie);
        }

        public static void AddCookieValue(string key, string value)
        {
            HttpCookie decodedCookie = null;
            HttpCookie encodedCookie = HttpContext.Current.Request.Cookies[cookiesNombre.cookieNombre];
            try
            {
                decodedCookie = HttpSecureCookie.Decode(encodedCookie, CookieProtection.Encryption);
            }
            catch
            {
                return;
            }
            decodedCookie.Values.Set(key, value);

            encodedCookie = HttpSecureCookie.Encode(decodedCookie, CookieProtection.Encryption);
            HttpContext.Current.Response.Cookies.Set(encodedCookie);
        }

        public static void AddCookieValues(Dictionary<string, string> keyValues)
        {
            HttpCookie decodedCookie = null;
            HttpCookie encodedCookie = HttpContext.Current.Request.Cookies[cookiesNombre.cookieNombre];
            try
            {
                decodedCookie = HttpSecureCookie.Decode(encodedCookie, CookieProtection.Encryption);
            }
            catch
            {
                return;
            }

            foreach (KeyValuePair<string, string> entry in keyValues)
            {
                decodedCookie.Values.Set(entry.Key, entry.Value);
            }

            encodedCookie = HttpSecureCookie.Encode(decodedCookie, CookieProtection.Encryption);
            HttpContext.Current.Response.Cookies.Set(encodedCookie);
        }

        public static string GetCookieValue(string key)
        {
            HttpCookie decodedCookie = null;
            HttpCookie encodedCookie = HttpContext.Current.Request.Cookies[cookiesNombre.cookieNombre];
            try
            {
                decodedCookie = HttpSecureCookie.Decode(encodedCookie, CookieProtection.Encryption);
            }
            catch
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Session.Abandon();
                //HttpContext.Current.Response.Redirect("~/");
            }

            if (decodedCookie[key] == null) return null;
            return decodedCookie[key].ToString();
        }

        public static void redirect(String role)
        {
            switch (role)
            {
                case "admin":
                    HttpContext.Current.Response.Redirect("Administracion", false);
                    break;
                case "consultor":
                    HttpContext.Current.Response.Redirect("Reportes", false);
                    break;
                case "usuario":
                    HttpContext.Current.Response.Redirect("Inventario", false);
                    break;
                case "superusuario":
                    HttpContext.Current.Response.Redirect("Inventario", false);
                    break;
                default:
                    break;
            }
        }

        public static String formatiarControl(String id,String prefijo)
        {
            String control = id;
            int max = 0;

            String sql = "select maxPrefijo from configuracion";
            try
            {
                using (MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString()))
                {
                    conex.Open();
                    MySqlCommand comando = new MySqlCommand(sql, conex);
                    Object obj = comando.ExecuteScalar();
                    if (obj != null)
                    {
                        max = Convert.ToInt32(obj);
                    }
                }
            }
            catch
            {
                control = "";
            }


            int ope = 10 - id.Length - prefijo.Length;

            for (int i = 0; i < ope;i++ )
            {
                control = "0"+control;
            }
            control = prefijo + control;

            return control;
        }    


    }