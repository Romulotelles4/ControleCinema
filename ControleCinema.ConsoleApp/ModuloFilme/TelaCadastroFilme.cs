using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloSalas;
using System;
using System.Collections.Generic;

namespace ControleCinema.ConsoleApp.ModuloFilme
{
    public class TelaCadastroFilme : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Filme> _repositorioFilme;
        private readonly Notificador _notificador;

        public TelaCadastroFilme(IRepositorio<Filme> repositorioFilme, Notificador notificador)
            : base("Cadastro de Filmes")
        {
            _repositorioFilme = repositorioFilme;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Filmes");

            Filme novoFilme = ObterFilme();

            _repositorioFilme.Inserir(novoFilme);

            _notificador.ApresentarMensagem("Filme cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Filme");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Filme cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroFilme = ObterNumeroRegistro();

            Filme filmeAtualizado = ObterFilme();

            bool conseguiuEditar = _repositorioFilme.Editar(numeroFilme, filmeAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Filme editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Filme");

            bool temFilmesRegistrados= VisualizarRegistros("Pesquisando");

            if (temFilmesRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Filme cadastrado para ser excluido.", TipoMensagem.Atencao);
                return;
            }

            int numeroFilme = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioFilme.Excluir(numeroFilme);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Filme excluído com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Filmes");

            List<Filme> filmes = _repositorioFilme.SelecionarTodos();

            if (filmes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum Filme disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Filme filme in filmes)
                Console.WriteLine(filme.ToString());

            Console.ReadLine();

            return true;
        }

        private Filme ObterFilme()
        {
            Console.Write("Digite o Título do Filme: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite o Gênero do Filme: ");
            string genero = Console.ReadLine();

            Console.Write("Digite a duração do Filme: ");
            int duracao = Convert.ToInt32(Console.ReadLine());

            return new Filme(titulo, genero, duracao);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do gênero de filme que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioFilme.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do gênero de filme não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
