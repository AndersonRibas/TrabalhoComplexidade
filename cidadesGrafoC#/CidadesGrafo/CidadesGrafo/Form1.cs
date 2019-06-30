using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CidadesGrafo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            
            Distancias d = new Distancias();
            d.inicializa(Convert.ToUInt16(comboBox1.Text));
            d.mostra();
            d.gravaCSV("Matriz_Distancia.csv");
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text != "")
            {
                button1.Enabled = true;
            }
        }
    }
}
