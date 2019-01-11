using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace GravarRecuperarImagens.Models
{
    public class Repositorio : IDisposable
    {
        private readonly SqlConnection cn;

        public Repositorio()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        }

        public void Add(byte[] img)
        {
            cn.Open();
            var cmd = new SqlCommand($@"INSERT INTO TB_IMAGENS (BLOB) values(@img)", cn);

            cmd.Parameters.Add("@img", SqlDbType.VarBinary, int.MaxValue).Value = img;

            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public List<Imagem> GetImagens()
        {
            try
            {
                return cn.Query<Imagem>("SELECT BLOB as Img, Id from TB_IMAGENS").AsQueryable().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            cn.Dispose();
        }
    }
}