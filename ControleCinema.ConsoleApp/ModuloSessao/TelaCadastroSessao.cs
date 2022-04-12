using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloSalas;
using System;
using System.Collections.Generic;

namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class TelaCadastroSessao : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Sessao> _repositorioSessao;
        private readonly Notificador _notificador;

        public TelaCadastroSessao(IRepositorio<Sessao> repositorioSessao, Notificador notificador)
            : base("Cadastro de Nova Sessão")
        {
            _repositorioSessao= repositorioSessao;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Nova Sessão");

            Sessao novaSessao = ObterSessao();

            _repositorioSessao.Inserir(novaSessao);

            _notificador.ApresentarMensagem("Sessão cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Sessão");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma Sessão cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroSessao = ObterNumeroRegistro();

            Sessao sessaoAtualizada = ObterSessao();

            bool conseguiuEditar = _repositorioSessao.Editar(numeroSessao, sessaoAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sessão editada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Sessão");

            bool temSessoesRegistradas = VisualizarRegistros("Pesquisando");

            if (temSessoesRegistradas == false)
            {
                _notificador.ApresentarMensagem("Nenhuma Sessão cadastrada para ser excluida.", TipoMensagem.Atencao);
                return;
            }

            int numeroSessao = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioSessao.Excluir(numeroSessao);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sessão excluída com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Sessões");

            List<Sessao> sessoes = _repositorioSessao.SelecionarTodos();

            if (sessoes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma Sessão disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sessao sessao in sessoes)
                Console.WriteLine(sessao.ToString());

            Console.ReadLine();

            return true;
        }

        private Sessao ObterSessao()
        {

            Console.Write("Digite o Título da Sessão: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite o Gênero da Sessão: ");
            string genero = Console.ReadLine();

            Console.WriteLine("Digite o horário da Sessão: ");
            int horario = Convert.ToInt32(Console.ReadLine());   

            Console.Write("Digite a duração da Sessão: ");
            int duracao = Convert.ToInt32(Console.ReadLine());

            return new Sessao( titulo, genero, horario, duracao);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o Id da Sessão que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioSessao.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("A Sessão não foi encontrada, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
