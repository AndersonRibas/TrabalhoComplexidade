using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Simulador_de_Rotas
{
    public partial class Form1 : Form
    {
        private OpenFileDialog openFile;
        private string line;
        private DataTable tabela;
        private string Destino;
        private string Origem;
        private GeraRotas rotas;



        public Form1()
        {
            InitializeComponent();
            tabela = new DataTable();
            openFile = new OpenFileDialog();
            Destino = "";
            Origem = "";
            line = "";



        }
        /*
         Botão responsável por abrir encontrar o arquivo a ser lido
         */
        private void OpenFileBtt_Click(object sender, EventArgs e)
        {
            tabela.Clear();

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                label1.Text += openFile.FileName;
                GeraRotabt.Enabled = true;
                comboBox1.Enabled = true;
                comboBox1.Items.Clear();

                rotas = new GeraRotas(openFile.FileName);
                comboBox1.Items.Add("Todas as cidades");

                for (int i = 0; i < rotas.Cidades.Length; i++)
                {
                    comboBox1.Items.Add(rotas.Cidades[i]);
                }
            }
         }

        private void Form1_Load(object sender, EventArgs e)
        {
            GeraRotabt.Enabled = false;
            label1.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;

            //Configuara os campos da tabela
            tabela.Columns.Add("Rota", typeof(string));
            tabela.Columns.Add("Distância", typeof(int));

           //Configuração da classe que permite encontrar o arquivo a ser selecionado
           //openFile.Filter = "Arquivos txt (*.txt)|*.txt|Arquivos CSV(*.cvs*)|*.cvs*";
            openFile.Title = "Abrir Arquivo";
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
        private void GeraRotabt_Click(object sender, EventArgs e)
        {
            var lista = rotas.GetLista(comboBox1.Text, comboBox2.Text);
            tabela.Clear();
            foreach (Rotas elemento in lista)
            {
               tabela.Rows.Add(elemento.Caminho, elemento.Distancia);
            }
            dataGridView1.DataSource = tabela;
            
        }

        private void Label2_Click(object sender, EventArgs e)
        {
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            comboBox2.Enabled = true;
            comboBox2.Items.Clear();
            comboBox2.Items.Add("Todas as cidades");
            for (int i = 0; i < rotas.Cidades.Length; i++)
            {
                if(comboBox1.Text != rotas.Cidades[i] )
                    comboBox2.Items.Add(rotas.Cidades[i]);
            }

        }
    }
}
