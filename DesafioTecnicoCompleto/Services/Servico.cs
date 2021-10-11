using ProjetoWeb.Models;
using System.Collections.Generic;

namespace ProjetoWeb.Services
{
    public class Servico : IServico
    {
        public DecomposicaoNumero CalculaNumerosDivisoresEPrimos(int numero)
        {
            List<int> arrayDivisores = new();

            for (var i = 1; i <= numero; i++)
            {
                if (numero % i == 0)
                {
                    arrayDivisores.Add(i);
                }
            }

            var resultado = new DecomposicaoNumero
            {
                NumeroEntrada = numero,
                NumerosDivisores = arrayDivisores,
                NumerosDivisoresPrimos = CalculaDivisoresPrimos(arrayDivisores)
            };

            return resultado;
        }

        private static List<int> CalculaDivisoresPrimos(List<int> array)
        {
            List<int> arrayDivisoresPrimos = new();

            arrayDivisoresPrimos.Add(array[0]);

            foreach (var valor in array)
            {
                int i = (int)valor;

                for (int j = 2; j <= i; j++)
                {
                    for (int k = 2; k <= i; k++)
                    {
                        if (j == k && j == i)
                        {
                            arrayDivisoresPrimos.Add(j);
                            break;
                        }
                        else if (j % k == 0)
                        {
                            break;
                        }
                    }
                }
            }

            return arrayDivisoresPrimos;
        }
    }
}
