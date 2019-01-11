using System.Linq;
using System.Web;

namespace GravarRecuperarImagens.Models
{
    public class Imagem
    {
        public int Id { get; set; }
        public byte[] Img { get; set; }
        public string Path { get; set; }
        public static string MsgErro { get; set; }

        protected Imagem()
        {

        }

        public Imagem(HttpPostedFileBase file)
        {
            if (ValidarImagem(file) == "")
                Img = CriarImagem(file);
        }

        public byte[] CriarImagem(HttpPostedFileBase file)
        {
            byte[] image = new byte[file.ContentLength];
            file.InputStream.Read(image, 0, image.Length);
            return image;
        }

        private static string ValidarImagem(HttpPostedFileBase file)
        {
            int MaxContentLength = 1024 * 1024 * 3; //3 MB
            string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

            if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                return MsgErro = "Por favor, carregue arquivos do tipo: " + string.Join(", ", AllowedFileExtensions);
            }

            else if (file.ContentLength > MaxContentLength)
            {
                return MsgErro = "Seu arquivo é muito grande, o tamanho máximo permitido é: " + MaxContentLength + " MB";
            }

            return "";

        }
    }
}