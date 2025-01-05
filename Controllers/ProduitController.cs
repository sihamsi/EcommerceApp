using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
                categorieListe = _db.Categories.Select(u => new SelectListItem
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
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(wwwRootPath, @"Images");
                using (var fileStrem = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
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
            else
            {
                obj.categorieListe = _db.Categories.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(obj);
            }
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

            ProduitVM produitVM = new()
            {
                produit = obj,
                categorieListe = _db.Categories.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                    Selected = (u.Id == obj.categorieId)
                })
            };

            return View(produitVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProduitVM obj, IFormFile file)
        {
            if (id != obj.produit.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var existingProduct = _db.Produits.Find(obj.produit.Id);

                if (existingProduct == null)
                    return NotFound();



                existingProduct.Name = obj.produit.Name;
                existingProduct.Code = obj.produit.Code;
                existingProduct.PrixProduit = obj.produit.PrixProduit;
                existingProduct.Description = obj.produit.Description;
                existingProduct.categorieId = obj.produit.categorieId;


                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(wwwRootPath, "Images");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    existingProduct.imageURL = @"Images/" + fileName;
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            obj.categorieListe = _db.Categories.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
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