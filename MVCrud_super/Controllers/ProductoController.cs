using Microsoft.AspNetCore.Mvc;
using MVCrud_super.Servicios;
using MVCrud_super.Models;

namespace MVCrud_super.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IRepositorioProducto repositorioProducto;
        public ProductoController(IRepositorioProducto repositorioProducto)
        {
            this.repositorioProducto = repositorioProducto;
        }

        public async Task<IActionResult> Index()
        {
            var producto = await repositorioProducto.obtenerProductos();
            return View(producto);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost] //Se debe utilizar con un task
        public async Task<IActionResult> Crear(ProductoViewModel producto)
        {

            if (!ModelState.IsValid)
            {
                return View(producto);
            }

            await repositorioProducto.crear(producto);
            return RedirectToAction("Index");
        }

        [HttpGet] //Obtiene los datos mediante la url
        public async Task<IActionResult> Editar(int id)
        {
            var producto = await repositorioProducto.obtenerPorId(id);

            if (producto is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(producto);
        }

        [HttpPost]

        public async Task<IActionResult> Editar(ProductoViewModel producto)
        {
            var productoExiste = await repositorioProducto.obtenerPorId(producto.GsIdProducto);

            if (productoExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioProducto.actualizar(producto);
            return RedirectToAction("Index");
        }
        
        [HttpGet]

        public async Task<IActionResult> Borrar(int id)
        {
            var producto = await repositorioProducto.obtenerPorId(id);

            if (producto is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(producto);
        }

        [HttpPost]

        public async Task<IActionResult> Eliminar(ProductoViewModel libro)
        {
            var productoExiste = await repositorioProducto.obtenerPorId(libro.GsIdProducto);

            if (productoExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioProducto.borrar(libro.GsIdProducto);
            return RedirectToAction("Index");
        }
    }
}
