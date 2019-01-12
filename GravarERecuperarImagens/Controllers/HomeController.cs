using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GravarRecuperarImagens.Models;

namespace GravarRecuperarImagens.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repositorio repo;

        public HomeController()
        {
            repo = new Repositorio();
        }

        public ActionResult ListarImagens()
        {
            var resultado = repo.GetImagens();
            if (resultado != null)
            {
                return View(resultado);

            }
            return View();

        }

        public ActionResult ImportarImagem()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ImportarImagem(HttpPostedFileBase request)
        {
            if (Request.Files[0].ContentLength > 0)
            {
                foreach (string upload in Request.Files)
                {
                    if (!(Request.Files[upload] != null && Request.Files[upload].ContentLength > 0)) continue;

                    var imagem = new Imagem(Request.Files[upload]);

                    if (!string.IsNullOrEmpty(Imagem.MsgErro))
                    {
                        ModelState.AddModelError("Upload", Imagem.MsgErro);
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = "Arquivo salvo com sucesso!!";
                        repo.Add(imagem.Img);
                    }
                }
            }
            else
                ModelState.AddModelError("Upload", "Importe seus arquivos!!");
            return View();
        }

        //Salvar a imagem na pasta do servidor
        //var fileName = Path.GetFileName(file.FileName);
        //var path = Path.Combine(Server.MapPath("~/Content/Upload"), fileName);
        //file.SaveAs(path);
        //ModelState.Clear();
    }
}