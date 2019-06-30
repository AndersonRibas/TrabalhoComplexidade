using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            //    Console.WriteLine("___________________________________________");
            //    Console.WriteLine();
            //    Console.WriteLine("Aplicação de Exemplo MSDN - Ler Arquivo CSV");
            //    Console.WriteLine();
            //    Console.WriteLine("___________________________________________");
            //    try
            //    {
            //        //Declaro o StreamReader para o caminho onde se encontra o arquivo 
            //        StreamReader rd = new StreamReader(@"C:\T.csv");
            //        //Declaro uma string que será utilizada para receber a linha completa do arquivo 
            //        string linha = null;
            //        //Declaro um array do tipo string que será utilizado para adicionar o conteudo da linha separado 
            //        string[] linhaseparada = null;
            //        bool primera = true;
            //        //realizo o while para ler o conteudo da linha 
            //        while ((linha = rd.ReadLine()) != null)
            //        {
            //            if (primera == true)
            //            {
            //                Console.WriteLine("linha inteira" + linha);
            //                //com o split adiciono a string 'quebrada' dentro do array 
            //                linhaseparada = linha.Split(";");
            //                //aqui incluo o método necessário para continuar o trabalho 
            //                for (int i = 0; i < linhaseparada.Length; i++)
            //                {
            //                    Console.WriteLine("Vetor de String" + linhaseparada[i].ToString());
            //                }
            //                primera = false;
            //            }

            //        }
            //        rd.Close();
            //    }
            //    catch
            //    {
            //        Console.WriteLine("Erro ao executar Leitura do Arquivo");
            //    }


            //}
            ////static void Main(string[] args)
            ////{
            ////    Hashtable clientes = new Hashtable();
            ////    string elemento = "AB";

            ////    clientes.Add(1, elemento);
            ////    clientes.Add(2, "Maria");
            ////    clientes.Add(3, "José");


            ////        Console.WriteLine(clientes[1]);

            ////    elemento = "TTTT";

            ////    clientes[1] = elemento;

            ////    Console.WriteLine(clientes[1]);

            ////    string numero = "1234";
            ////    int x = Int32.Parse(numero);

            ////    x += 1000;

            ////    Console.WriteLine(x);

            string[] cidade = new string[] { "-A-", "-B-", "-C-"};
            int variacao = cidade.Length - 1;
            int nRotas = 1;

            for (int u = cidade.Length; u > 0; u--)
            {
                nRotas *= u;
            }
            string[] rotas = new string[nRotas];
            int k = 0;


            //For para movimentar para a direita a posição do vetor;
            for (int i = 0; i < cidade.Length; i++)
            {

                //ver números de arranjos...
                int novoAranjo = 1;
                for (int h = cidade.Length - i; h > 0; h--)
                {
                    novoAranjo *= h;

                }
                k = 0;

                for (int j = 0; j < rotas.Length; j++)
                {  
                   
                    //Teste para verificar valor de k, deve ser menor que o tamanho do vetor cidade, pois este representa as cidades.
                    if (j > 0 && j % (novoAranjo / (cidade.Length - i)) == 0)
                        k++;

                    //Grante que o índice k, referente às cidades, tenha valor menor que o tamanho do vetor;
                    k = k > 0 && k > (cidade.Length - 1) ? 0 : k;

                    if (i > 0)
                    {
                        //confere se tem alguma cidade igual na rota, caso haja, incrementa-se k, até achar uma cidade que não está na rota
                        while (rotas[j].Contains(cidade[k]))
                        {
                            k++;
                            k = k > 0 && k > (cidade.Length - 1) ? 0 : k;
                        }

                    }

                    //Agrega mais uma cidade a Rota na posição j
                    rotas[j] += cidade[k];

                }

            }

            foreach (string e in rotas)
            {
                Console.WriteLine(e);

            }
        }



    }
}
