using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulador_de_Rotas
{
    public class GeraRotas
    {
        //Matriz para guardar o arquivo lido no sistema (em .CSV)
        private string[,] Arquivo { get; set; }

        //Vetor das Cidadess
        public string[] Cidades { get; set; }

        //Matriz de distâncias
        public int[,] MatrizDistancia { get; set; }

        //Lista contentedo os dois atributos
        public IDictionary<int, Rotas> Rotas;




        public GeraRotas(string nomeDoArquivo)
        {
            //Metodo para ler o arquivo .CSV
            LeArquivo(nomeDoArquivo);
            GeraListaCidades(Arquivo);
            GeraMatrizDistancia(Arquivo);
            GeradorDeRotas();


        }


        private void LeArquivo(string nomeArquivo)
        {
            string linha = "";
            StreamReader sr = new StreamReader(nomeArquivo);
            try
            {

                for (int i = 0; (linha = sr.ReadLine()) != null; i++)
                {
                    string[] vetSplit = linha.Split(';');

                    if (i == 0)
                    {
                        Arquivo = new string[vetSplit.Length, vetSplit.Length];
                    }
                    for (int j = 0; j < Arquivo.GetLength(0); j++)
                    {
                        Arquivo[i, j] = vetSplit[j];
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao ler o arquivo");
            }
            //Fechar metodo de leitura
            sr.Close();
        }

        //Primeira linha da matriz será utilizada para formar vetor de Cidadea
        private void GeraListaCidades(string[,] Arquivo)
        {
            int tamanho = Arquivo.GetLength(0);
            Cidades = new string[tamanho - 1];
            int j = 0;

            for (int i = 0; i < Arquivo.GetLength(0); i++)
            {
                if (Arquivo[0, i] != null && Arquivo[0, i] != "0")
                {
                    Cidades[j] = Arquivo[0, i];
                    //para se mover no vetor de cidades
                    j++;
                }

            }

        }

        //Gera uma matriz de inteiros por meio do arquivo CSV
        private void GeraMatrizDistancia(string[,] Arquivo)
        {
            //Tamanho da matraz, subtraido a primeira coluna que contém as cidades.
            int tamanho = Arquivo.GetLength(0) - 1;
            MatrizDistancia = new int[tamanho, tamanho];

            try
            {
                //Índice i = Percorre as Linhas do Arquivo, começa na segunda, pois na primeira estam as cidades;
                //Índice j = Percorre as Colunas do Arquivo, começa na segunda, pois na primeira estam as cidades;

                //Índice m = Percorre as linhas da Matriz Distância;
                //Índice k = Percorre as colunas da Matriz Distância;

                for (int i = 1, m = 0; i < Arquivo.GetLength(0); i++, m++)
                {
                    for (int j = 1, k = 0; j < Arquivo.GetLength(1); j++, k++)
                    {
                        int aux = Int32.Parse(Arquivo[i, j]);
                        MatrizDistancia[m, k] = aux;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro no Cast String para Int");
            }

        }

        //Gera numero de Arranjos possíveis como numero total de cidades;
        //Ex: 3 cidades = 3*2*1 = 6 combinações possiveis entre as cidades;
        //[ABC][ACB][BAC][BCA][CAB][CBA]
        private int Arranjo(int tam)
        {
            int arranjo = 1;
            for (int i = tam; i > 0; i--)
            {
                arranjo *= i;
            }
            return arranjo;
        }

        //Gerador de rotas
        private void GeradorDeRotas()
        {

            long numRotas = Arranjo(Cidades.Length);
            Rotas = new Dictionary<int, Rotas>();
            int _distancia = 0;
            string _caminho = "";
            //For para movimentar para a direita a posição do vetor;
            for (int i = 0; i < Cidades.Length; i++)
            {

                //Sempre que i "andar", diminui o espaço o numero de cidades possíveis
                //Exemplo: preenchendo a primeira posição: [i,_,_,_] = Arranjo = 4*3*2 = 24 combinações entre cidades difrentes
                //Quando incrementamos o i, para popular a segunda posição: [X, i,_,_] = Arranjo = 3*2 = 6 combinações entre cidades difrentes
                int novoAranjo = Arranjo(Cidades.Length - i);

                int k = 0;

                for (int j = 0; j < numRotas; j++)
                {


                    //Teste para verificar valor de k, deve ser menor que o tamanho do vetor Cidades, pois este representa as Cidadess.
                    if (j > 0 && j % (novoAranjo / (Cidades.Length - i)) == 0)
                        k++;

                    //Grante que o índice k, referente às Cidadess, tenha valor menor que o tamanho do vetor;
                    k = k > 0 && k > (Cidades.Length - 1) ? 0 : k;


                    if (i > 0)
                    {
                        //confere se tem alguma Cidades igual na rota, caso haja, incrementa-se k, até achar uma Cidades que não está na rota
                        while (Rotas[j].Caminho.Contains(Cidades[k]))
                        {
                            k++;
                            k = k > 0 && k > (Cidades.Length - 1) ? 0 : k;
                        }

                        string elemento = Rotas[j].Caminho;
                        int ultimo = elemento.Length - 1;
                        //Encontra a distância na matriz na posição [origem, k];
                        int origem = EncontraOrigem(string.Format("{0}", elemento[ultimo]));
                        //Somatório da Distancia;
                        Rotas[j].Distancia += MatrizDistancia[origem, k];
                        Rotas[j].Caminho += Cidades[k];

                    }
                    else
                    {
                        //Como é a primeira i == 0, estamos observando a primeira posição da esquerda para direita
                        // é necessário inicializar o dicionario.
                        Rotas.Add(j, new Rotas(Cidades[k], 0));
                    }
                }

            }

        }

        private int EncontraOrigem(string origem)
        {

            for (int i = 0; i < Cidades.Length; i++)
            {
                if (Cidades[i].Equals(origem))
                    return i;
            }
            return -1;
        }

        public List<Rotas> GetLista(string origem, string destino)
        {
            //Transformando o dicionario em lista, ordenada pelo valor da distancia
            List<Rotas> listaResult = Rotas.Values.OrderBy(x => x.Distancia).ToList();
           
            //Sabe-se a origem e o destino
            if (origem != "Todas as cidades" && destino != "Todas as cidades" && origem != "" && destino != "")
            {
                var lista = listaResult.Where(x => x.Caminho[0].ToString() == origem &&
                                              x.Caminho[Cidades.Length - 1].ToString() == destino)
                                       .OrderBy(x => x.Distancia);

                return lista.ToList();
            }
            //sabe-se apenas a origem;
            else if (origem != "Todas as cidades" && origem != "")
            {
                var lista = listaResult.Where(x => x.Caminho[0].ToString() == origem)
                                       .OrderBy(x => x.Distancia);
                return lista.ToList();
            }
            //sabe-se apenas o destino
            else if (destino != "Todas as cidades" && destino != "")
            {
                var lista = listaResult.Where(x => x.Caminho[Cidades.Length - 1].ToString() == destino)
                                      .OrderBy(x => x.Distancia);
                return lista.ToList();
            }


            return listaResult;
        }

    }

}

