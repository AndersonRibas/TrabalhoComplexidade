using System;

namespace Simulador_de_Rotas
{
    public class Rotas : IComparable<Rotas>
    {
        public string Caminho { get; set; }
        public int Distancia { get; set; }

        public Rotas(string caminho, int distancia)
        {
            Caminho = caminho;
            Distancia = distancia;
        }

       
        //Dessa forma é possível usar os métodos para ordenar a lista, neste caso conforme a Distancia
        public int CompareTo(Rotas other)
        {
            return Distancia.CompareTo(other.Distancia);
        }
    }
}