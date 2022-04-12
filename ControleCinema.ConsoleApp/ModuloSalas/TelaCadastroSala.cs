using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloSalas;
using System;
using System.Collections.Generic;

namespace ControleCinema.ConsoleApp.ModuloSalas
{
     public class TelaCadastroSala : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Sala> _repositorioSala;
        private readonly Notificador _notificador;

        public TelaCadastroSala(IRepositorio<Sala> repositorioSala, Notificador notificador)
            : base("Cadastro de Salas de Filmes")
        {
            _repositorioSala = repositorioSala;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Salas");

            Sala novaSala = ObterSala();

            _repositorioSala.Inserir(novaSala);

            _notificador.ApresentarMensagem("Sala cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Sala de Filme");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma Sala de filme cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroSala = ObterNumeroRegistro();

            Sala salaAtualizada = ObterSala();

            bool conseguiuEditar = _repositorioSala.Editar(numeroSala, salaAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sala de Filme editada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Sala de Filme");

            bool temSalasRegistradas = VisualizarRegistros("Pesquisando");

            if (temSalasRegistradas == false)
            {
                _notificador.ApresentarMensagem("Nenhuma Sala de filme cadastrada para ser excluida.", TipoMensagem.Atencao);
                return;
            }

            int numeroSala = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioSala.Excluir(numeroSala);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sala de Filme excluída com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Salas de Filmes");

            List<Sala> salas = _repositorioSala.SelecionarTodos();

            if (salas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma sala de filme disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sala sala in salas)
                Console.WriteLine(sala.ToString());

            Console.ReadLine();

            return true;
        }

         private Sala ObterSala()
        {
            Console.Write("Digite o nome da Sala: ");
            string nome = Console.ReadLine();

            Console.Write("Digite a capacidade da sala: ");
            int capacidade = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o número de assentos: ");
            int numeroAssentos = Convert.ToInt32(Console.ReadLine());

            return new Sala(nome, capacidade, numeroAssentos);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o id sala de filme que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioSala.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("Sala de filme não foi encontrada, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
