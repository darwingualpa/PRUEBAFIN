using Datos.ModelosNuevos;
using Datos.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRUEBAFINAL.Controllers
{
    [Authorize]
    public class TipoVehiculosController : Controller
    {
        private readonly EjercicioEvaluacionContext _context;
        public TipoVehiculosController(EjercicioEvaluacionContext context)
        {
            _context = context;


        }
        public void Combox()
        {

            ViewData["CodigoVehiculo"] = new SelectList(_context.Vehiculos.Select(D => new ViewmodelTipovehiculo
            {
                Codigo = D.Codigo,
                Nombres = $"{D.Nombre}",
                Estado = D.Estado,

            }).Where(F => F.Estado == 1).ToList(), "Codigo", "Nombres");

        }

        [Authorize(Roles = "Rey,Peon")]
        // GET: TipoVehiculosController
        public ActionResult Index()
        {
            //List<TipoVehiculo> ltstipovehiculos = _context.TipoVehiculos.ToList();

            List<ViewmodelTipovehiculo> ltstipovehiculos = _context.TipoVehiculos.Select(D => new ViewmodelTipovehiculo
            {
                CodigoVehiculo = D.Codigo,
                DescripcionVehiculo = D.Descripcion,
                Nombres = $"{D.CodigoVehiculoNavigation.Nombre}",
                Estado = D.Estado,

            }).ToList();



            return View(ltstipovehiculos);
        }
        [Authorize(Roles = "Rey,Peon")]
        // GET: TipoVehiculosController/Details/5
        public ActionResult Details(int id)
        {
            TipoVehiculo tipovehiculo = _context.TipoVehiculos.Where(D => D.CodigoVehiculo == id).FirstOrDefault();

            return View(tipovehiculo);
        }

        // GET: VehiculosController/Create
        public ActionResult Create()
        {
            Combox();
            return View();
        }
        [Authorize(Roles = "Rey")]
        // POST: VehiculosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoVehiculo tipovehiculo)
        {
            try
            {
                tipovehiculo.Estado = 1;
                _context.Add(tipovehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View(tipovehiculo);
            }
        }
        [Authorize(Roles = "Rey")]
        // GET: VehiculosController/Edit/5
        public ActionResult Edit(int id)
        {
            Combox();
            TipoVehiculo tipovehiculo = _context.TipoVehiculos.Where(D => D.CodigoVehiculo == id).FirstOrDefault();
            return View();
        }
        [Authorize(Roles = "Rey")]
        // POST: VehiculosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TipoVehiculo tipovehiculo)
        {
            if (id != tipovehiculo.CodigoVehiculo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.Update(tipovehiculo);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View(tipovehiculo);
            }
        }



        // POST: VehiculosController/Delete/5
        [Authorize(Roles = "Rey")]
        public ActionResult Activar(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(D => D.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 1;
            _context.Update(vehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Rey")]
        public ActionResult Desactivar(int id)
        {
            TipoVehiculo tipovehiculo = _context.TipoVehiculos.Where(D => D.CodigoVehiculo == id).FirstOrDefault();
            tipovehiculo.Estado = 0;
            _context.Update(tipovehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }

}

