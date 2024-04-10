using CalendarioDeEventos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CalendarioDeEventos
{
   

    class Program
    {
        private static List<Evento> eventos = new List<Evento>();
        private static List<Contato> contatos = new List<Contato>();
        private static IEnumerable<Evento> evento;

        static void Main(string[] args)
        {
            bool sair = false;

            while (!sair)
            {//menuzinho show aqui
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1 - Cadastrar Evento");
                Console.WriteLine("2 - Listar Eventos");
                Console.WriteLine("3 - Pesquisar Evento Por Uma Data Especifica");
                Console.WriteLine("4 - Editar Informações De Um Evento");
                Console.WriteLine("5 - Cadastrar Contato Do Responsavel Pelo Evento");
                Console.WriteLine("6 - Pesquisar Contato Cadastrado");
                Console.WriteLine("7 - Excluir Evento Pelo ID");
                Console.WriteLine("8 - Salvar Informações Do Evento Em Um Arquivo De Texto");
                Console.WriteLine("9 - Sair");

                int opcao;

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {//pega oque o usuario escreveu e confere se exite nas opções
                    Console.WriteLine("Comando Não Reconhecido, Tente Novamente.");
                    continue;
                }

                try
                {
                    switch (opcao)
                    {//aqui são os casos que vão cair dependendo doque vc digitar
                        case 1:
                            CadastrarEvento();
                            break;

                        case 2:
                            ListarEventos();
                            break;

                        case 3:
                            PesquisarEventoPorData();
                            break;

                        case 4:
                            EditarEvento();
                            break;

                        case 5:
                            CadastrarContatoResponsavel();
                            break;

                        case 6:
                            PesquisarContato();
                            break;

                        case 7:
                            ExcluirEvento();
                            break;

                        case 8:
                            SalvarEmTXT();
                            break;

                        case 9:
                            sair = true;
                            break;

                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
                catch (Exception ex)
                {//tratamento basiquinho de exessão aqui
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
        }

        static void CadastrarEvento()
        {   Evento evento = new Evento();
            //aqui onde se vão obter as informações do evento 
           
            //tb criei uma variavel local
            //recebendo as informções inseridas ai por voce e convertendo se necessario
            Console.WriteLine("O Sistema Ira Gerar Um Id Alfanumérico Para Seu Evento");
            evento.Id = Path.GetRandomFileName().Replace(".", "").Substring(0, 6);

            Console.WriteLine($" O ID De Seu Evento É : {evento.Id} ");

            Console.WriteLine("Me Diga O Nome Ou Titulo De Seu Evento");
            evento.TituloEvento = Console.ReadLine();

            Console.WriteLine("Informe A Data Inicial De Seu Evento");
            string dataInicioEventoString = Console.ReadLine();


            DateTime dataInicioEvento;
            bool isParseSuccessful = DateTime.TryParse(dataInicioEventoString, out dataInicioEvento);

            if (isParseSuccessful)
            {
                evento.DataInicioEvento = dataInicioEvento;
            }
            else
            {
                Console.WriteLine("Data inválida. Por favor, insira a data no formato correto (dd/MM/yyyy).");
            }

            Console.WriteLine("Informe A Data De Termino De Seu Evento");
            string dataTerminoEventoString = Console.ReadLine();

            DateTime dataTerminoEvento;
            bool ParseSuccessful = DateTime.TryParse(dataTerminoEventoString, out dataTerminoEvento);

            if (isParseSuccessful)
            {
                evento.DataTerminoEvento = dataTerminoEvento;
            }
            else
            {
                Console.WriteLine("Data inválida. Por favor, insira a data no formato correto (dd/MM/yyyy).");
            }

            Console.WriteLine("Por favor, Me Descreva Seu Evento");
            evento.Descricao = Console.ReadLine();

            Console.WriteLine("Digite a quantidade de pessoas:");
            string quantidadeDePessoasString = Console.ReadLine();
            evento.QuantidadeDePessoas = int.Parse(quantidadeDePessoasString);

            Console.WriteLine("Me Informe O Publico Alvo De Seu Evento:");
            evento.PublicoAlvo = Console.ReadLine();

            Console.WriteLine("Evento Cadastrado Com Sucesso! ");

            Console.WriteLine("<==========================================================================>");
            //aqui adiciona as informações na lista
            eventos.Add(evento);
        }

        static void ListarEventos()
        {
            foreach (Evento evento in eventos)
            {//percorre a lista eventos e exibe todos eventos cadastrados
                Console.WriteLine($"Id Do Evento : {evento.Id}");
                Console.WriteLine($"Titulo Do Evento :{evento.TituloEvento}");
                Console.WriteLine($"Data Inicial Do Evento :{evento.DataInicioEvento}");
                Console.WriteLine($"Data De Termino Do Evento : {evento.DataTerminoEvento}");
                Console.WriteLine($"Descreição Do Evento :{evento.Descricao}");
                Console.WriteLine($"Qauntidade De pessoas Que comparecerão No Evento :{evento.QuantidadeDePessoas}");
                Console.WriteLine($"Publico Alvo Do Evento :{evento.PublicoAlvo}");
                Console.WriteLine($"Dados De Contato Do Responsavel Pelo Evento :{evento.ContatoDoResponsavel}");

                Console.WriteLine("<==========================================================================>");
            }
        }

        static void PesquisarEventoPorId()
        {
            try
            {//le a informação que voce digitou e busca para ver se tem algum id igual ela
                Console.WriteLine("Digite O Id Do Evento Que Voçê Deseja Buscar");
                string Id = Console.ReadLine();

                foreach (Evento evento in eventos)
                {//se tiver vai exibir tudo daquele determinado evento
                    if (evento.Id.Equals(Id))
                    {
                        Console.WriteLine($"Id Do Evento : {evento.Id}");
                        Console.WriteLine($"Titulo Do Evento :{evento.TituloEvento}");
                        Console.WriteLine($"Data Inicial Do Evento :{evento.DataInicioEvento}");
                        Console.WriteLine($"Data De Termino Do Evento : {evento.DataTerminoEvento}");
                        Console.WriteLine($"Descreição Do Evento :{evento.Descricao}");
                        Console.WriteLine($"Qauntidade De pessoas Que comparecerão No Evento :{evento.QuantidadeDePessoas}");
                        Console.WriteLine($"Publico Alvo Do Evento :{evento.PublicoAlvo}");
                        Console.WriteLine($"Dados De Contato Do Responsavel Pelo Evento :{evento.ContatoDoResponsavel}");

                        Console.WriteLine("<==========================================================================>");
                        return;
                    }
                }
                Console.WriteLine("Evento Não Enontrado");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Id Não Encontrado, Tente Denovo" + ex.Message);
            }
        }

        static void EditarEvento()
        {
            try
            {
                Console.WriteLine("Digite o ID do evento que você deseja editar:");
                string idEventoEditar = Console.ReadLine();
                // pega o id que voce digitou e confere se essa informação com o primeiro que corresponde a ela

                Evento eventoParaEditar = eventos.FirstOrDefault(e => e.Id.Equals(idEventoEditar));

                if (eventoParaEditar != null)
                {
                    Console.WriteLine($"Evento encontrado. ID: {eventoParaEditar.Id}, Título: {eventoParaEditar.TituloEvento}");
                    Console.WriteLine("Digite o novo título do evento:");
                    eventoParaEditar.TituloEvento = Console.ReadLine();

                    

                    Console.WriteLine("Evento atualizado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Evento não encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao editar evento: " + ex.Message);
            }
        }

        static void CadastrarContatoResponsavel()
        {
            Contato contato = new Contato();

            try
            {
                Console.WriteLine("Informe O Nome Do Responsavel Pelo Evento :");
                contato.Nome = Console.ReadLine();

                Console.WriteLine("Informe O Numero Telefonico Do Responsavel Pelo Evento :");
                string telefoneString = Console.ReadLine();
                contato.Telefone = int.Parse(telefoneString);

                Console.WriteLine("Informe O Email Do Responsavel Pelo Evento :");
                contato.Email = Console.ReadLine();

                Console.WriteLine("Contato Cadastrado Com Sucesso! ");

                Console.WriteLine("<==========================================================================>");

                contatos.Add(contato);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha Ao Cadastrar Contato");
            }
        }

        static void PesquisarContato()
        {
            try
            {
                Console.WriteLine("Digite O Id Do Evento Para Visulaizar O Responsavel Pelo Respctivo Evento ");
                string Id = Console.ReadLine();

                foreach (Contato contato in contatos)
                {
                    if (contato.Id == Id)
                    {
                        Console.WriteLine($"Nome: {contato.Nome} ");
                        Console.WriteLine($"Telefone: {contato.Telefone} ");
                        Console.WriteLine($"Email: {contato.Email} ");

                        Console.WriteLine("<==========================================================================>");
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Informações De Contato Não Encontradas, Favor Verifique Se O Id Está Correto" + ex.Message);
            }
        }

        static void ExcluirEvento()
        {
            try
            {
                Console.WriteLine("Digite o ID do evento que você deseja excluir:");
                string id = Console.ReadLine();

                Evento eventoParaExcluir = eventos.FirstOrDefault(e => e.Id.Equals(id));

                if (eventoParaExcluir != null)
                {
                    eventos.Remove(eventoParaExcluir);
                    Console.WriteLine("Evento excluído com sucesso!");
                }
                else
                {
                    Console.WriteLine("Evento não encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir evento: " + ex.Message);
            }
        }

        static void SalvarEmTXT()
        {


            try
            {
                string pastaDestino = @"C:\Eventos";
                string nomeArquivo = "Eventos.txt";
                string caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);

                using (StreamWriter sw = new StreamWriter(caminhoCompleto))
                {
                    foreach (Evento evento in eventos)
                    {
                        sw.WriteLine($"Id Do Evento : {evento.Id}");
                        sw.WriteLine($"Titulo Do Evento : {evento.TituloEvento}");
                        sw.WriteLine($"Data Inicial Do Evento : {evento.DataInicioEvento}");
                        sw.WriteLine($"Data De Termino Do Evento : {evento.DataTerminoEvento}");
                        sw.WriteLine($"Descrição Do Evento : {evento.Descricao}");
                        sw.WriteLine($"Quantidade De Pessoas Que Comparecerão No Evento : {evento.QuantidadeDePessoas}");
                        sw.WriteLine($"Publico Alvo Do Evento : {evento.PublicoAlvo}");
                        if (evento.ContatoDoResponsavel != null)
                        {
                            sw.WriteLine($"Dados De Contato Do Responsável Pelo Evento : Nome: {evento.ContatoDoResponsavel.Nome}, Telefone: {evento.ContatoDoResponsavel.Telefone}, Email: {evento.ContatoDoResponsavel.Email}");
                        }
                        else
                        {
                            sw.WriteLine("Responsável pelo evento não cadastrado.");
                        }
                        sw.WriteLine("<==========================================================================>");
                    }
                }

                Console.WriteLine("Informações salvas com sucesso no arquivo de texto.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar informações em arquivo de texto: " + ex.Message);
            }
        }


        static void PesquisarEventoPorData()
        {
            try
            {
                Console.WriteLine("Digite a data para pesquisar eventos (dd/MM/yyyy):");
                DateTime dataPesquisa = LerData();

                var eventosNaData = eventos.Where(e => e.DataInicioEvento.Date == dataPesquisa.Date || e.DataTerminoEvento.Date == dataPesquisa.Date);

                if (eventosNaData.Any())
                {
                    Console.WriteLine($"Eventos encontrados na data {dataPesquisa.ToShortDateString()}:");
                    foreach (var evento in eventosNaData)
                    {
                        Console.WriteLine($"Id Do Evento : {evento.Id}");
                        Console.WriteLine($"Titulo Do Evento :{evento.TituloEvento}");
                        Console.WriteLine($"Data Inicial Do Evento :{evento.DataInicioEvento}");
                        Console.WriteLine($"Data De Termino Do Evento : {evento.DataTerminoEvento}");
                        Console.WriteLine($"Descreição Do Evento :{evento.Descricao}");
                        Console.WriteLine($"Qauntidade De pessoas Que comparecerão No Evento :{evento.QuantidadeDePessoas}");
                        Console.WriteLine($"Publico Alvo Do Evento :{evento.PublicoAlvo}");
                        Console.WriteLine($"Dados De Contato Do Responsavel Pelo Evento :{evento.ContatoDoResponsavel}");

                        Console.WriteLine("<==========================================================================>");
                    }
                }
                else
                {
                    Console.WriteLine($"Nenhum evento encontrado na data {dataPesquisa.ToShortDateString()}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao pesquisar eventos por data: " + ex.Message);
            }
        }

        static DateTime LerData()
        {
            DateTime data;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
            {
                Console.WriteLine("Data inválida. Por favor, insira a data no formato correto (dd/MM/yyyy).");
            }
            return data;
        }
    }
}
