using Datos.ModelosNuevos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRUEBAFINAL.Controllers
{
    [Authorize]
    public class VehiculosController : Controller
    {
        private readonly EjercicioEvaluacionContext _context;
        public VehiculosController(EjercicioEvaluacionContext context)
        {
            _context = context;


        }
        [Authorize(Roles ="Rey,Peon")]
        public ActionResult Index()
        {
            List<Vehiculo> ltsvehiculos = _context.Vehiculos.ToList();

            return View(ltsvehiculos);
        }
        [Authorize(Roles = "Rey,Peon")]
        // GET: VehiculosController/Details/5
        public ActionResult Details(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(D => D.Codigo == id).FirstOrDefault();

            return View(vehiculo);
        }

        // GET: VehiculosController/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Rey")]
        // POST: VehiculosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vehiculo vehiculo)
        {
            try
            {
                vehiculo.Estado = 1;
                _context.Add(vehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(vehiculo);
            }
        }
        [Authorize(Roles = "Rey")]
        // GET: VehiculosController/Edit/5
        public ActionResult Edit(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(D => D.Codigo == id).FirstOrDefault();
            return View();
        }
        [Authorize(Roles = "Rey")]
        // POST: VehiculosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Codigo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.Update(vehiculo);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
            Vehiculo vehiculo = _context.Vehiculos.Where(D => D.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 0;
            _context.Update(vehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }

}

