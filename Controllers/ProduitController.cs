using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Models.ViewModels;

namespace WebApplication2.Controllers
{
    public class ProduitController : Controller
    {
      
            private readonly ApplicationDbContext _db;
            private readonly IWebHostEnvironment _webHostEnvironment;
            public ProduitController(ApplicationDbContext db, IWebHostEnvironment webHost)
            {
                _db = db;
                _webHostEnvironment = webHost;
            }
            public IActionResult Index()
            {
                 List<Produit> ProduitList = _db.Produits.ToList();
                return View(ProduitList);
            }
            public IActionResult Create()
            {
                ProduitVM produitVM = new()
                {
                    categorieListe = _db.Categories.ToList().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }),
                    produit = new Produit()
                };
                return View(produitVM);
            }
            [HttpPost]
            public IActionResult Create(ProduitVM obj, IFormFile file)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//donner un nom aléatoire
                    string filePath = Path.Combine(wwwRootPath, @"Images");
                    using (var fileStrem = new FileStream(Path.Combine(filePath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStrem);
                    }
                    obj.produit.imageURL = @"Images\" + fileName;
                }
                if (ModelState.IsValid)
                {
                    _db.Produits.Add(obj.produit);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else return View();
            }
            public IActionResult Edit(int? id)
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                Produit obj = _db.Produits.Find(id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
            [HttpPost]
            //[ValidateAntiForgeryToken]
            public IActionResult Edit(Produit obj)
            {
                if (ModelState.IsValid)
                {
                    _db.Produits.Update(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else return View();
            }


            public IActionResult Delete(int? id)
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                Produit obj = _db.Produits.Find(id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
            [HttpPost, ActionName("Delete")]
            public IActionResult DeletePost(int? id)
            {
                Produit obj = _db.Produits.Find(id);
                if (obj == null)
                    return NotFound();
                _db.Produits.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

        }
}
