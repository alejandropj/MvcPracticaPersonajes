using Microsoft.AspNetCore.Mvc;
using MvcPracticaPersonajes.Models;
using MvcPracticaPersonajes.Services;

namespace MvcPracticaPersonajes.Controllers
{
    public class PersonajesSeriesController : Controller
    {
        private ServiceApiPersonajes service;
        public PersonajesSeriesController(ServiceApiPersonajes service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<PersonajesSeries> personajes = 
                await this.service.GetPersonajesSeriesAsync();
            List<string> series = await this.service.GetSeriesAsync();

            ViewData["SERIES"] = series;
            return View(personajes);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string serie)
        {
            List<PersonajesSeries> personajes = 
                await this.service.FindPersonajesBySerieAsync(serie);
            List<string> series = await this.service.GetSeriesAsync();

            ViewData["SERIES"] = series;
            return View(personajes);
        }
        public async Task<IActionResult> Details(int id)
        {
            PersonajesSeries personaje =
                await this.service.FindPersonajeSerieAsync(id);
            return View(personaje);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PersonajesSeries personajesSeries)
        {
            await this.service.CreatePersonajeSerieAsync
                (personajesSeries.IdPersonaje, personajesSeries.Nombre,
                personajesSeries.Imagen, personajesSeries.Serie);
            return RedirectToAction("Index");
        }        
        public async Task<IActionResult> Edit(int id)
        {
            PersonajesSeries personaje = await this.service.FindPersonajeSerieAsync(id);
            return View(personaje);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PersonajesSeries personajesSeries)
        {
            await this.service.UpdatePersonajeSerieAsync
                (personajesSeries.IdPersonaje, personajesSeries.Nombre,
                personajesSeries.Imagen, personajesSeries.Serie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajeSerieAsync(id);
            return RedirectToAction("Index");
        }
    }
}
