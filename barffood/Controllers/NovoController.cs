using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using barffood.Models;
using Rotativa;

namespace barffood.Controllers
{
    public class NovoController : Controller
    {
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                using (barffoodEntities2 db = new barffoodEntities2())
                {
                    Food este = db.Foods.Find(id ?? 0);
                    if (este != null)
                    {
                        return View(este);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Novo", new { msg = "Id não existe" });
                    }
                }
            }
            catch (Exception erro)
            {

                return RedirectToAction("Index", "Novo", new { msg = erro.Message });
            }
        }

        [HttpPost]
        public ActionResult Edit(Food editado, HttpPostedFileBase fich)
        {
            try
            {
                using (barffoodEntities2 db = new barffoodEntities2())
                {
                    Food este = db.Foods.Find(editado.idfood);
                    if (este != null)
                    {
                        este.nome = editado.nome;
                        este.url = editado.url;
                        este.data_validade = editado.data_validade;
                        if (fich != null && fich.ContentLength > 0 && fich.ContentType.Contains("image"))
                        {

                            string caminho = "~/fotos/" + este.idfood.ToString() + System.IO.Path.GetExtension(fich.FileName);
                            if (System.IO.File.Exists(Server.MapPath(caminho))) System.IO.File.Delete(Server.MapPath(caminho));
                            este.foto = caminho;
                            fich.SaveAs(Server.MapPath(caminho));

                        }
                        db.SaveChanges();
                        return RedirectToAction("Index", "Novo", new { msg = "Ok Registo editado com sucesso" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Novo", new { msg = "Id não existe" });

                    }

                }
            }
            catch (Exception erro)
            {

                return RedirectToAction("Index", "Novo", new { msg = erro.Message });
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            try
            {
                using (barffoodEntities2 db = new barffoodEntities2())
                {
                    Food este = db.Foods.Find(id ?? 0);
                    if (este != null)
                    {
                        return View(este);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Novo", new { msg = "Cliente não existe" });
                    }
                }
            }
            catch (Exception erro)
            {

                return RedirectToAction("Index", "Novo", new { msg = erro.Message });
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult DeleteFood(int? id)
        {
            try
            {
                using (barffoodEntities2 db = new barffoodEntities2())
                {
                    Food este = db.Foods.Find(id ?? 0);
                    if (este != null)
                    {
                        db.Foods.Remove(este);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Novo", new { msg = "Apagado com sucesso" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Novo", new { msg = "Del: id não existe" });
                    }
                }
            }
            catch (Exception erro)
            {

                return RedirectToAction("Index", "Novo", new { msg = erro.Message });
            }

        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddFood()
        {
            try
            {
                using (barffoodEntities2 db = new barffoodEntities2())
                {
                    List<animal> animals = db.animals.ToList();
                    ViewBag.animals = new SelectList(animals, "idanimal", "idanimal");
                    List<Race> race = db.Races.ToList();
                    ViewBag.races = new SelectList(race, "idrace", "idrace");
                    Food food = new Food();
                    return View(food);
                }

            }
            catch (Exception erro)
            {

                throw;
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddFood(Food novo, HttpPostedFileBase fich)
        {
            try
            {
                using (barffoodEntities2 db = new barffoodEntities2())
                {

                    db.Foods.Add(novo);
                    db.SaveChanges();
                    if (fich.ContentLength > 0 && fich.ContentType.Contains("image"))
                    {
                        string caminho = "~/fotos/" + novo.idfood.ToString() + System.IO.Path.GetExtension(fich.FileName);
                        novo.foto = caminho;
                        fich.SaveAs(Server.MapPath(caminho));
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", "Novo", new { msg = "Registo inserido com sucesso" });
                }
            }
            catch (Exception erro)
            {

                return RedirectToAction("Index", "Novo", new { msg = erro.Message });
            }

        }

        // GET: Novo
        public ActionResult Index()
        {

            using (barffoodEntities2 db = new barffoodEntities2())
            {
                List<Food> comida = db.Foods.ToList();

                return View(comida);
            }

        }
        public ActionResult Contact()
        {
            return View();
        }
        //Convert Index Page as PDF
        public ActionResult PrintViewToPdf()
        {
            var report = new ActionAsPdf("Index");
            return report;
        }

        //Convert partial Page as PDF
        public ActionResult PrintPartialViewToPdf(int id)
        {
            using (barffoodEntities2 db = new barffoodEntities2())
            {
                Food employee = db.Foods.FirstOrDefault(e => e.idfood == id);
                var report = new PartialViewAsPdf("~/Views/Novo/Details.cshtml", employee);
                return report;
            }
        }
        
    }
}