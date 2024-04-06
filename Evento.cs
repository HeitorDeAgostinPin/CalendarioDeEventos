/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace CalendarioDeEventos
{// classe para atribuir caracteristicas do evento
    public class Evento
    {
        public string Id { get; set; }
        public string TituloEvento { get; set; }
        public DateTime DataInicioEvento { get; set; }
        public DateTime DataTerminoEvento { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeDePessoas { get; set; }
        public string PublicoAlvo { get; set; }
        public Contato ContatoDoResponsavel { get; set; }

    }
}
