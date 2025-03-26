using Microsoft.AspNetCore.Mvc;
using MvcCoreApiCrudDepartamentos.Models;
using MvcCoreApiCrudDepartamentos.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MvcCoreApiCrudDepartamentos.Controllers
{
    public class DepartamentosController : Controller
    {
        private ServiceDepartamentos service;
        public DepartamentosController(ServiceDepartamentos service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Departamento> data = await this.service.GetDepartamentosAsync();
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            Departamento data = await this.service.FindDepartamentoAsync(id);
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Departamento dept)
        {
            await this.service.InsertDepartamentoAysnc(dept.IdDepartamento, dept.Nombre, dept.Localidad);
            return RedirectToAction("Index");
        }

        public IActionResult Cliente()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            Departamento data = await this.service.FindDepartamentoAsync(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Departamento dept)
        {
            await this.service.UpdateDepartamentoAysnc(dept.IdDepartamento, dept.Nombre, dept.Localidad);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteDepartamentoAsync(id);
            return RedirectToAction("Index");
        }
    }
}
