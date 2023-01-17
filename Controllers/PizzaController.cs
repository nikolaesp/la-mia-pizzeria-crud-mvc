using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;

namespace LaMiaPizzeria.Controllers
{
    public class PizzaController : Controller
    {


        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> listaPizza = db.Pizza.ToList<Pizza>();
                return View("Index", listaPizza);
            }
        }

        public IActionResult Details(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza pizzaTrovato = db.Pizza
                    .Where(SingoloPizzaNelDb => SingoloPizzaNelDb.Id == id)
                    .FirstOrDefault();

                if (pizzaTrovato != null)
                {
                    return View(pizzaTrovato);
                }
                return NotFound("Non ci sono pizze presenti");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", formData);
            }

            using (PizzaContext db = new PizzaContext())
            {
                db.Pizza.Add(formData);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza pizzaupdate = db.Pizza.Where(pizzas => pizzas.Id == id).FirstOrDefault();

                if (pizzaupdate == null)
                {
                    return NotFound("Il post non è stato trovato");
                }

                return View("Update", pizzaupdate);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Pizza formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", formData);
            }

            using (PizzaContext db = new PizzaContext())
            {
                Pizza pizzaupdate = db.Pizza.Where(articolo => articolo.Id == formData.Id).FirstOrDefault();

                if (pizzaupdate != null)
                {
                    pizzaupdate.Title = formData.Title;
                    pizzaupdate.Description = formData.Description;
                    pizzaupdate.Image = formData.Image;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("Il post che volevi modificare non è stato trovato!");
                }
            }

        }

        //[HttpDelete] // se qui metti metodo delete devi mettere metodo delete anche nella view. Oppure usi metodo post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza pizzadelete = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzadelete != null)
                {
                    db.Pizza.Remove(pizzadelete);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("Il post da eliminare non è stato trovato!");
                }
            }
        }
    }

}


    

