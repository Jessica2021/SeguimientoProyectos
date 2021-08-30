using ProyectoS.Utilidades;
using ProyectoS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoS.Controllers
{
    public class CuentaController : Controller
    {
        // GET: Cuenta
        public ActionResult Index()
        {
            return View();
        }

        ProyectosEntities db = new ProyectosEntities();
        #region Utilidades
     
        public string RenderViewToString(string viewName, object model) //metodo especial para renderisar view to string
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        public JsonResult _Json(bool success = false, string type = "error", string message = "Mensaje informativo", string result = "Resultado informativo", dynamic data = null)
        {
            return Json(UtilTool.JsonResult(success, type, message, result, data), JsonRequestBehavior.AllowGet);
        }

        private FormCollection DeSerialize(FormCollection FormData)
        {
            FormCollection collection = new FormCollection();
            //un-encode, and add spaces back in
            string querystring = Uri.UnescapeDataString(FormData["FormData"]).Replace("+", " ");
            var split = querystring.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> items = new Dictionary<string, string>();
            foreach (string s in split)
            {
                string text = s.Substring(0, s.IndexOf("="));
                string value = s.Substring(s.IndexOf("=") + 1);

                if (items.Keys.Contains(text))
                    items[text] = items[text] + "," + value;
                else
                    items.Add(text, value);
            }
            foreach (var i in items)
            {
                collection.Add(i.Key, i.Value);
            }
            return collection;
        }

        #endregion

        public ActionResult Login()
        {
            
            return View();
        }
       



        [HttpPost]
        public JsonResult Guardar_Usuario()
        {
            #region :: Definición de variables ::

            string Nombre = "";
            if (!string.IsNullOrEmpty(Request.Params["Nombre"].Trim()))
            {
                Nombre = Request.Params["Nombre"];
            }
            string Usuario = "";
            if (!string.IsNullOrEmpty(Request.Params["Usuario"].Trim()))
            {
                Usuario = Request.Params["Usuario"];
            }

            string Contraseña = "";
            if (!string.IsNullOrEmpty(Request.Params["Contraseña"].Trim()))
            {
                Contraseña = Request.Params["Contraseña"];
            }

           
            #endregion

            try
            {
                //Guarda 
                ViewBag.Usuario = db.Sp_Usuario_Create(Nombre, Usuario, Contraseña);
      
            }
            catch (Exception)
            {
                throw;
            }
            return Json(new { Usuario = Usuario, Contraseña = Contraseña }, JsonRequestBehavior.AllowGet);
           // return Json(new { Usuario = Usuario, Contraseña = Contraseña });

        } 
        
        [HttpPost]
        public JsonResult Iniciar_Sesion()
        {
            #region :: Definición de variables ::

            string Usuario = "";
            if (!string.IsNullOrEmpty(Request.Params["UsuarioSs"].Trim()))
            {
                Usuario = Request.Params["UsuarioSs"];
            }

            string Contraseña = "";
            if (!string.IsNullOrEmpty(Request.Params["ContraseñaSs"].Trim()))
            {
                Contraseña = Request.Params["ContraseñaSs"];
            }

           
            #endregion

            try
            {
                //Guarda 
                ViewBag.Usuario = db.vw_ListaUsuarios.Where(f=>f.Usuario == Usuario && f.Contraseña == Contraseña).ToList();

                if (ViewBag.Usuario.Count > 0)
                {
                    vw_ListaUsuarios LisUsuarios = new vw_ListaUsuarios();
                    LisUsuarios = db.vw_ListaUsuarios.Where(f => f.Usuario == Usuario && f.Contraseña == Contraseña).FirstOrDefault();

                    return Json(new { Guardar = "Hecho" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Guardar = "No" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { Guardar = "No" }, JsonRequestBehavior.AllowGet);
            }
           
           

        }

        public ActionResult Usuarios()
        {
            ViewBag.ListaUsuarios = db.vw_ListaUsuarios.ToList();
            return View();
        }

    }
}