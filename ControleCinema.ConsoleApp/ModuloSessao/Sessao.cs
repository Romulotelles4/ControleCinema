using ControleCinema.ConsoleApp.Compartilhado;
using System;

namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class Sessao : EntidadeBase
    {
        public int IdSessao { get; set; }   
        public string TituloSessao{ get; set; }
        public string GeneroSessao { get; set; }
        public int DuracaoSessao { get; set; }

        public int HorarioSessao { get; set; }

        public Sessao( string tituloSessao, string generoSessao, int horarioSessao, int duracaoSessao)
        {
          
            TituloSessao = tituloSessao;
            GeneroSessao = generoSessao;
            HorarioSessao = horarioSessao;
            DuracaoSessao = duracaoSessao;
           
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Título da Sessão: " + TituloSessao + Environment.NewLine +
                "Gênero da Sessão: " + GeneroSessao + Environment.NewLine +
                "Horário da Sessão: " + HorarioSessao + Environment.NewLine +
                "Duração da Sessão: " + DuracaoSessao + Environment.NewLine;

        }

    }
}
