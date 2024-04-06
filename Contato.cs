/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace CalendarioDeEventos
{//classe para inserir informações do responsavel do evento
    public class Contato:Evento
    {
        public string Nome { get; set; }
        public int Telefone { get; set; }
        public string Email { get; set; }
    }
}
