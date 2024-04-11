/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace CalendarioDeEventos
{//classe para inserir informações do responsavel do evento
    public class Contato:Evento
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string IdDoContato { get; set; }
        public List<string> EventosRelacionados { get; set; }

        

        
    }
}
