using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Limk.VolleyBall
{
    internal class ConexaoBD
    {
        public static MySqlConnection ObterConexao()
        {
            string servidor = Environment.GetEnvironmentVariable("DB_SERVER");
            string porta = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
            string banco = Environment.GetEnvironmentVariable("DB_NAME");
            string usuario = Environment.GetEnvironmentVariable("DB_USER");
            string senha = Environment.GetEnvironmentVariable("DB_PASSWORD");

            string stringConexao = $"Server={servidor};Port={porta};Database={banco};Uid = {usuario};Pwd={senha};SslMode=none;";

            MySqlConnection conexao = new MySqlConnection(stringConexao);
            try
            {
                conexao.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar ao MySQL: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return conexao;
        }
    }
}
