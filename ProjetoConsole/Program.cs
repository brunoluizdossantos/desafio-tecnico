using System;
using System.Collections.Generic;

namespace ProjetoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Digite um número: ");

            int valorEntrada = int.Parse(Console.ReadLine());

            ResultadoFinal(valorEntrada);
        }

        public static void ResultadoFinal(int numero)
        {
            Console.WriteLine();
            Console.WriteLine("Número de Entrada: " + numero);

            CalculaNumerosDivisoresEPrimos(numero);

            Console.WriteLine();
            Console.WriteLine("Aperte 'Enter' para finalizar...");

            while (Console.ReadKey(false).Key != ConsoleKey.Enter) ;
        }

        public static void CalculaNumerosDivisoresEPrimos(int valor)
        {
            List<int> arrayDivisores = new List<int>();

            for (var i = 1; i <= valor; i++)
            {
                if (valor % i == 0)
                {
                    arrayDivisores.Add(i);
                }
            }

            Console.Write("Números Divisores: ");

            foreach (var i in arrayDivisores)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();

            CalculaDivisoresPrimos(arrayDivisores);
        }

        public static void CalculaDivisoresPrimos(List<int> array)
        {
            List<int> arrayDivisoresPrimos = new List<int>
            {
                array[0]
            };

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

            Console.Write("Divisores Primos: ");

            foreach (var i in arrayDivisoresPrimos)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
        }
    }
}
