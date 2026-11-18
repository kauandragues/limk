using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Text;

namespace Limk.VolleyBall.Variaveis_de_Ambiente
{
    public static class VariaveisAmbiente
    {
        public static string DbHost => GetRequired("DB_HOST");
        public static string DbPort => GetRequired("DB_PORT");
        public static string DbUser => GetRequired("DB_USER");
        public static string DbPassword => GetRequired("DB_PASSWORD");
        public static string DbDatabase => GetRequired("DB_DATABASE");


        private static readonly string[] _obrigatorias = { "DB_HOST", "DB_PORT", "DB_USER", "DB_PASSWORD", "DB_DATABASE" };

        public static void Validar()
        {
            var Faltando = _obrigatorias.Where(nome => string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(nome))).ToList();

            if (Faltando.Count == 0)
            {
                return;
            }
            foreach (var nome in Faltando)
                Console.WriteLine($"[Faltando] {nome}");

            throw new ValidationException($"Faltam {Faltando.Count} variável(is): {string.Join(", ", Faltando)}");
        }

        private static GetRequired(string nome) => Environment.GetEnvironmentVariable(nome) ?? throw new ValidationException($"A variável {nome} não foi definida");
        private static GetOptional(string nome, string padrao) => Environment.GetEnvironmentVariable(nome) ?? padrao;
    }
}
