using ControleCinema.ConsoleApp.Compartilhado;
using System;

namespace ControleCinema.ConsoleApp.ModuloSalas
{
    public class Sala : EntidadeBase
    {
        public string NomeSala { get; set; }
        public int Capacidade { get; set; }
        public int NumeroAssentos { get; set; }  

        public Sala (string nomeSala, int capacidade, int numeroAssentos)
        {
            NomeSala = nomeSala;
            Capacidade = capacidade;
            NumeroAssentos = numeroAssentos;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine + 
                "Nome da sala: " + NomeSala + Environment.NewLine +
                "Capacidade: " + Capacidade + Environment.NewLine +
                "Número de assentos: " + NumeroAssentos + Environment.NewLine;
        }
    }
}
