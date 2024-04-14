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

        static void Main(string[] args)
        {
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1 - Cadastrar Evento");
                Console.WriteLine("2 - Listar Eventos");
                Console.WriteLine("3 - Pesquisar Evento por uma Data Específica");
                Console.WriteLine("4 - Editar Informações de um Evento");
                Console.WriteLine("5 - Pesquisar Contato Cadastrado");
                Console.WriteLine("6 - Excluir Evento Pelo ID");
                Console.WriteLine("7 - Salvar Informações do Evento em um Arquivo de Texto");
                Console.WriteLine("8 - Sair");

                int opcao;

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Comando Não Reconhecido, Tente Novamente.");
                    continue;
                }

                try
                {
                    switch (opcao)
                    {
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
                            PesquisarContato();
                            break;

                        case 6:
                            ExcluirEvento();
                            break;

                        case 7:
                            SalvarEmTXT();
                            break;

                        case 8:
                            sair = true;
                            break;

                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
        }

        public static void CadastrarEvento()
        {
            Evento evento = new Evento();

            evento.Id = GerarIdEvento();

            Console.WriteLine($"O ID do evento é: {evento.Id}");

            Console.WriteLine("Informe o Título do Evento:");
            evento.TituloEvento = Console.ReadLine();

            Console.WriteLine("Informe a Data e Hora Inicial do Evento (dd/MM/yyyy HH:mm):");
            evento.DataInicioEvento = LerDataHora();

            Console.WriteLine("Informe a Data e Hora de Término do Evento (dd/MM/yyyy HH:mm):");
            evento.DataTerminoEvento = LerDataHora();

            Console.WriteLine("Informe a Descrição do Evento:");
            evento.Descricao = Console.ReadLine();

            Console.WriteLine("Digite a Quantidade de Pessoas:");
            evento.QuantidadeDePessoas = int.Parse(Console.ReadLine());

            Console.WriteLine("Informe o Público Alvo do Evento:");
            evento.PublicoAlvo = Console.ReadLine();

            Contato contato = new Contato();

            contato.Id = GerarIdContato();

            Console.WriteLine($"O ID do contato é: {contato.Id}");

            Console.WriteLine("Digite o Nome do Contato Responsável:");
            contato.Nome = Console.ReadLine();

            Console.WriteLine("Digite o Telefone do Contato Responsável:");
            contato.Telefone = Console.ReadLine();

            Console.WriteLine("Digite o Email do Contato Responsável:");
            contato.Email = Console.ReadLine();

            // Adicionar o contato à lista de contatos
            contatos.Add(contato);

            evento.IdContatoResponsavel = contato.Id;

            eventos.Add(evento);

            Console.WriteLine("Evento cadastrado com sucesso!");
        }


        public static void ListarEventos()
        {
            if (eventos.Count == 0)
            {
                Console.WriteLine("Nenhum evento cadastrado.");
                return;
            }

            foreach (Evento evento in eventos)
            {
                Console.WriteLine($"ID: {evento.Id}");
                Console.WriteLine($"Título: {evento.TituloEvento}");
                Console.WriteLine($"Data Inicial: {evento.DataInicioEvento}");
                Console.WriteLine($"Data de Término: {evento.DataTerminoEvento}");
                Console.WriteLine($"Descrição: {evento.Descricao}");
                Console.WriteLine($"Quantidade de Pessoas: {evento.QuantidadeDePessoas}");
                Console.WriteLine($"Público Alvo: {evento.PublicoAlvo}");
                Console.WriteLine($"ID do Contato Responsável: {evento.IdContatoResponsavel}");
                Console.WriteLine();
            }
        }

        public static void PesquisarEventoPorData()
        {
            Console.WriteLine("Digite a data para pesquisar eventos (dd/MM/yyyy):");
            DateTime dataPesquisa = LerData();

            var eventosNaData = eventos.Where(e => e.DataInicioEvento.Date == dataPesquisa.Date || e.DataTerminoEvento.Date == dataPesquisa.Date);

            if (eventosNaData.Any())
            {
                Console.WriteLine($"Eventos encontrados na data {dataPesquisa.ToShortDateString()}:");
                foreach (var evento in eventosNaData)
                {
                    Console.WriteLine($"ID: {evento.Id}");
                    Console.WriteLine($"Título: {evento.TituloEvento}");
                    Console.WriteLine($"Data Inicial: {evento.DataInicioEvento}");
                    Console.WriteLine($"Data de Término: {evento.DataTerminoEvento}");
                    Console.WriteLine($"Descrição: {evento.Descricao}");
                    Console.WriteLine($"Quantidade de Pessoas: {evento.QuantidadeDePessoas}");
                    Console.WriteLine($"Público Alvo: {evento.PublicoAlvo}");
                    Console.WriteLine($"ID do Contato Responsável: {evento.IdContatoResponsavel}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"Nenhum evento encontrado na data {dataPesquisa.ToShortDateString()}.");
            }
        }

        public static void EditarEvento()
        {
            Console.WriteLine("Digite o ID do evento que você deseja editar:");
            string idEventoEditar = Console.ReadLine();

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

        public static void PesquisarContato()
        {
            Console.WriteLine("Digite o ID do contato que você deseja pesquisar:");
            string idContatoPesquisa = Console.ReadLine();

            Contato contato = contatos.FirstOrDefault(c => c.Id.Equals(idContatoPesquisa));

            if (contato != null)
            {
                Console.WriteLine($"Contato encontrado. ID: {contato.Id}, Nome: {contato.Nome}");
                Console.WriteLine($"Telefone: {contato.Telefone}, Email: {contato.Email}");
            }
            else
            {
                Console.WriteLine("Contato não encontrado.");
            }
        }

        public static void ExcluirEvento()
        {
            Console.WriteLine("Digite o ID do evento que você deseja excluir:");
            string idEventoExcluir = Console.ReadLine();

            Evento eventoParaExcluir = eventos.FirstOrDefault(e => e.Id.Equals(idEventoExcluir));

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

        public static void SalvarEmTXT()
        {
            string pastaDestino = @"C:\Eventos";
            string nomeArquivo = "Eventos.txt";
            string caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);

            try
            { Console.WriteLine("OBSEVAÇÃO: crei a pasta Eventos no seu dico local C, não temos orcamento pra fazer mais que isso");
                using (StreamWriter sw = new StreamWriter(caminhoCompleto))
                {
                    foreach (Evento evento in eventos)
                    {
                        sw.WriteLine($"ID: {evento.Id}");
                        sw.WriteLine($"Título: {evento.TituloEvento}");
                        sw.WriteLine($"Data Inicial: {evento.DataInicioEvento}");
                        sw.WriteLine($"Data de Término: {evento.DataTerminoEvento}");
                        sw.WriteLine($"Descrição: {evento.Descricao}");
                        sw.WriteLine($"Quantidade de Pessoas: {evento.QuantidadeDePessoas}");
                        sw.WriteLine($"Público Alvo: {evento.PublicoAlvo}");
                        sw.WriteLine($"ID do Contato Responsável: {evento.IdContatoResponsavel}");
                        sw.WriteLine();
                    }
                }

                Console.WriteLine("Informações salvas com sucesso no arquivo de texto.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar informações em arquivo de texto: " + ex.Message);
            }
        }

        public static string GerarIdEvento()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GerarIdContato()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static DateTime LerDataHora()
        {
            DateTime dataHora;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataHora))
            {
                Console.WriteLine("Formato de data e hora inválido. Por favor, insira no formato correto (dd/MM/yyyy HH:mm):");
            }
            return dataHora;
        }

        public static DateTime LerData()
        {
            DateTime data;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
            {
                Console.WriteLine("Data inválida. Por favor, insira a data no formato correto (dd/MM/yyyy):");
            }
            return data;
        }


    }
}
