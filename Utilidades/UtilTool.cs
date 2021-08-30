using System;
using System.Collections.Generic;
using System.Linq;
using ProyectoS.Models;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;
using System.Web.Mvc;

namespace ProyectoS.Utilidades
{
    public class UtilTool
    {
        #region ::: MÉTODOS GENERALES :::        
        public static string OnValidateNullOrEmpty(string String_value, string Default_Value = "0")
        {
            string Result = Default_Value;
            try
            {
                Result = !String.IsNullOrEmpty(String_value) ? String_value : Default_Value;
            }
            catch (Exception)
            {
            }
            return Result;
        }

        public static ReponseMethod JsonResult(bool success = false, string type = "error", string message = "Mensaje informativo", string result = "Resultado informativo", dynamic data = null) {
            ReponseMethod reponseMethod = new ReponseMethod();
            reponseMethod.success = success;
            reponseMethod.type = type;
            reponseMethod.message = message;
            reponseMethod.result = result;
            reponseMethod.data = data != null ? data : "";
            return reponseMethod;
            //return Json(new { success = false, type = "error", message = $"{ex.Message.ToString()}", data = RowID }, JsonRequestBehavior.AllowGet);
        }

        public static DateTime GetDateTime()
        {
            try
            {
                TimeZoneInfo zona = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                
                return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, zona);
            }
            catch
            {
                return DateTime.Now;
            }
        }
        public static string CryptPasswd(string data, string cryptKey)
        {
            if (string.IsNullOrEmpty(data))
                return "";


            Crypto cpt = new Crypto(Crypto.SymmProvEnum.DES);
            return cpt.Encrypting(data, cryptKey).Trim();

        }
        #endregion

        public static class Constantes
        {
            public const int Take = 250;
        }
        #region LISTADOS
        public class ReponseMethod
        {
            public bool success { get; set; } = false;
            public string type { get; set; } = "error";
            public string message { get; set; } = "Mensaje informativo";
            public string result { get; set; } = "Resultado informativo";
            public dynamic data { get; set; }          
        }
        public class DatoValorGrafica
        {
            public string Dato { get; set; }
            public double Valor { get; set; }
            public DateTime? DatoFecha { get; set; }
            public Int32 Seq { get; set; }
            public string Persona { get; set; }
        }
        public class GraficaOportunidades
        {
            public string name { get; set; }
            public List<int> data { get; set; }
        }
        public class GraficaOportunidadesInfo
        {
            public int mes { get; set; }
            public int cantidad { get; set; }
        }
        #endregion

    }
}