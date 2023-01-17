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
                PizzaCategory pizzatrovata = new PizzaCategory();
                    pizzatrovata.Pizza = db.Pizza
                    .Where(SingoloPizzaNelDb => SingoloPizzaNelDb.Id == id)
                    .FirstOrDefault();
                pizzatrovata.Categories = db.Categories.ToList();
               
                if (pizzatrovata.Pizza != null)
                {
                    return View(pizzatrovata);
                }
                return NotFound("Non ci sono pizze presenti");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Category> categoriesFromDb = db.Categories.ToList<Category>();

                PizzaCategory modelForView = new PizzaCategory();
                modelForView.Pizza = new Pizza();

                modelForView.Categories = categoriesFromDb;

                return View("Create", modelForView);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaCategory formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", formData);
            }

            using (PizzaContext db = new PizzaContext())
            {
                db.Pizza.Add(formData.Pizza);
                db.SaveChanges();
            }

            return RedirectToAction("Index",formData.Pizza);
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
                List<Category> categories = db.Categories.ToList<Category>();

                PizzaCategory modelForView = new PizzaCategory();
                modelForView.Pizza = pizzaupdate;
                modelForView.Categories = categories;

                return View("Update", modelForView);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id ,PizzaCategory formData)
        {
            if (!ModelState.IsValid)
            {
                using (PizzaContext db = new PizzaContext())
                {
                    List<Category> categories = db.Categories.ToList<Category>();

                    formData.Categories = categories;
                }
                return View("Update", formData);
            }

            using (PizzaContext db = new PizzaContext())
            {
                Pizza pizzaupdate = db.Pizza.Where(articolo => articolo.Id == id).FirstOrDefault();

                if (pizzaupdate != null)
                {
                    pizzaupdate.Title = formData.Pizza.Title;
                    pizzaupdate.Description = formData.Pizza.Description;
                    pizzaupdate.Image = formData.Pizza.Image;
                    pizzaupdate.Category = formData.Pizza.Category;
                    pizzaupdate.CategoryId = formData.Pizza.CategoryId;

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


    

