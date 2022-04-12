using ControleCinema.ConsoleApp.Compartilhado;
using System;

namespace ControleCinema.ConsoleApp.ModuloFilme
{
    public class Filme : EntidadeBase
    {
        public string TituloFilme { get; set; }
        public string GeneroFilme { get; set; }
        public int DuracaoFilme { get; set; }

        public Filme(string tituloFilme, string generoFilme, int duracaoFilme)
        {
            TituloFilme = tituloFilme;
            GeneroFilme = generoFilme;
            DuracaoFilme= duracaoFilme;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine + 
                "Título do Filme: " + TituloFilme + Environment.NewLine +
                "Gênero do Filme: " + GeneroFilme + Environment.NewLine +
                "Duração do Filme: " + DuracaoFilme + Environment.NewLine;
        }
    }
}
