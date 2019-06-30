using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CidadesGrafo
{
    class Distancias
    {
        int NUM_CIDADES=5;
        string[,] cidades;
        string[] nomeCidades;

        public void inicializa(int numCidades) {
            Random random = new Random();
            nomeCidades = new string[] {"A", "B", "C", "D", "E","F","G","H","I","J","K","L","M","N","O"};
            if(numCidades>=5 && numCidades <= 15)
            {
                NUM_CIDADES = numCidades;
            }
            cidades = new string[NUM_CIDADES,NUM_CIDADES];
            for(int x=0;x< NUM_CIDADES; x++)
            {
                for (int y = 0; y < NUM_CIDADES; y++)
                {
                    if(x == 0 &&  y > 0)
                    {
                        cidades[x, y] = nomeCidades[y-1];
                    }
                    else if (y == 0 && x > 0 )
                    {
                        cidades[x, y] = nomeCidades[x-1];
                    }
                    else if (x == y)
                    {

                      cidades[x, y] = string.Format("{0}", 0);

                    }
                    else
                    {
                        cidades[x, y] = string.Format("{0}", random.Next(50));
                        cidades[y, x] = cidades[x, y];
                    }
                }
            }
        }
        public void mostra() {

            for (int x = 0; x < NUM_CIDADES; x++)
            {
                for (int y = 0; y < NUM_CIDADES; y++)
                {
                    Console.Write(cidades[x,y] + "\t");
                }
                Console.Write("\n");
            }
        }
        public void gravaCSV(String nomeArquivo) {
            StreamWriter arquivo = new StreamWriter(
                nomeArquivo, true, Encoding.ASCII);
           
            for (int x = 0; x < NUM_CIDADES; x++)
            {
                for (int y = 0; y < NUM_CIDADES; y++)
                {
                    if(y == NUM_CIDADES - 1)
                    {
                        arquivo.Write(cidades[x,y]);
                    }
                    else
                    {
                        arquivo.Write(cidades[x, y] + ";");
                    }
                }
                arquivo.Write("\n");
            }
            arquivo.Close();
            MessageBox.Show("Gravado com sucesso");
        }

    }
}
