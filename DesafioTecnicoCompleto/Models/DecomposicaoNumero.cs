using System.Collections.Generic;

namespace ProjetoWeb.Models
{
    public class DecomposicaoNumero
    {
        public int NumeroEntrada { get; set; }
        public List<int> NumerosDivisores { get; set; }
        public List<int> NumerosDivisoresPrimos { get; set; }
    }
}
