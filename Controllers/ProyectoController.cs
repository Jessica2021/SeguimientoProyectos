using ProyectoS.Models;
using ProyectoS.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoS.Controllers
{
    public class ProyectoController : Controller
    {
        // GET: Proyecto
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
        
        public ActionResult Proyecto()
        {
            ViewBag.ListaProyectos = db.vw_ListaProyectos.ToList();
            return View();
        }
        public ActionResult NuevoProyecto()
        {

            return View();
        }

        [HttpPost]
        public JsonResult Guardar_Proyecto()
        {
            #region :: Definición de variables ::

            string nombre = "";
            if (!string.IsNullOrEmpty(Request.Params["nombre"].Trim()))
            {
                nombre = Request.Params["nombre"];
            }
            string Observaciones = "";
            if (!string.IsNullOrEmpty(Request.Params["Observaciones"].Trim()))
            {
                Observaciones = Request.Params["Observaciones"];
            }

            DateTime Fecha = DateTime.Now;
            #endregion

            try
            {
                //Guarda 
                ViewBag.proyecto = db.Sp_Proyecto_Create(nombre, Observaciones, "Administrador", Fecha);

            }
            catch (Exception)
            {
                throw;
            }
            return Json(new { Guardar = "Hecho" }, JsonRequestBehavior.AllowGet);
            // return Json(new { Usuario = Usuario, Contraseña = Contraseña });

        }

        public ActionResult Historia()
        {
            ViewBag.ListaHistorias = db.vw_ListaProyectos.ToList();
            return View();
        }
        public ActionResult ListaHistoria(int RowID)
        {
            ViewBag.ListaHistorias = db.vw_ListaHistorias.Where(f=>f.RowIDProyecto==RowID).ToList();
            return View();
        }
        public ActionResult NuevaHistoria(int RowID)
        {
            vw_ListaProyectos obj = new vw_ListaProyectos();

            if (RowID > 0 )
            {
                obj = db.vw_ListaProyectos.Where(f => f.RowID == RowID).FirstOrDefault();
                
            }


            return View(obj);
        }


     
        [HttpPost]
        public JsonResult Guardar_Historia()
        {
            #region :: Definición de variables ::
            int RowIDProyecto = 0;

            if (!string.IsNullOrEmpty(Request.Params["RowIDProyecto"].Trim()))
            {
                RowIDProyecto = int.Parse(Request.Params["RowIDProyecto"]);
            }

            string nombre = "";
            if (!string.IsNullOrEmpty(Request.Params["nombre"].Trim()))
            {
                nombre = Request.Params["nombre"];
            }
            string Observaciones = "";
            if (!string.IsNullOrEmpty(Request.Params["Observaciones"].Trim()))
            {
                Observaciones = Request.Params["Observaciones"];
            }

            DateTime Fecha = DateTime.Now;
            #endregion

            try
            {
                //Guarda 
                ViewBag.proyecto = db.Sp_Historias_Create(nombre, Observaciones,"Administrador", Fecha, RowIDProyecto);

            }
            catch (Exception )
            {
                throw ;
            }
            return Json(new { Guardar = "Hecho"}, JsonRequestBehavior.AllowGet);
            // return Json(new { Usuario = Usuario, Contraseña = Contraseña });

        }
        public ActionResult Tiket()
        {
            ViewBag.ListaHistorias = db.vw_ListaHistorias.ToList();
            return View();
        }
        public ActionResult ListaTikets(int RowID)
        {
            ViewBag.ListaTikets = db.vw_ListaTikets.Where(f=>f.RowIDHistorias==RowID).ToList();
            return View();
        }
        public ActionResult NuevoTiket(int RowID)
        {
            vw_ListaHistorias obj = new vw_ListaHistorias();

            if (RowID > 0 )
            {
                obj = db.vw_ListaHistorias.Where(f => f.RowID == RowID).FirstOrDefault();
                
            }


            return View(obj);
        }


     
        [HttpPost]
        public JsonResult Guardar_Tiket()
        {
            #region :: Definición de variables ::
            int RowIDHistoria = 0;

            if (!string.IsNullOrEmpty(Request.Params["RowIDHistoria"].Trim()))
            {
                RowIDHistoria = int.Parse(Request.Params["RowIDHistoria"]);
            }

            string nombre = "";
            if (!string.IsNullOrEmpty(Request.Params["nombre"].Trim()))
            {
                nombre = Request.Params["nombre"];
            }
            string Observaciones = "";
            if (!string.IsNullOrEmpty(Request.Params["Observaciones"].Trim()))
            {
                Observaciones = Request.Params["Observaciones"];
            }
            string Estado = "";
            if (!string.IsNullOrEmpty(Request.Params["Estado"].Trim()))
            {
                Estado = Request.Params["Estado"];
            }
            string Activo = "";
            if (!string.IsNullOrEmpty(Request.Params["Activo"].Trim()))
            {
                Activo = Request.Params["Activo"];
            }

            DateTime Fecha = DateTime.Now;
            #endregion

            try
            {
                //Guarda 
                ViewBag.proyecto = db.Sp_Tikets_Create(nombre, Observaciones, Estado, Activo, "Administrador", Fecha, RowIDHistoria);

            }
            catch (Exception )
            {
                throw ;
            }
            return Json(new { Guardar = "Hecho"}, JsonRequestBehavior.AllowGet);
            // return Json(new { Usuario = Usuario, Contraseña = Contraseña });

        }
    }
}