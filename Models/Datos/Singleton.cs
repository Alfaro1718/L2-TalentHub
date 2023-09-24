using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaGenerica;

namespace L1_TalentHub.Models.Datos
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();

        public List<Registros> ListaRegistros;
        public List<Registros> RegistrosEncontrados;
        public List<List<Registros>> ListaDeListas;
        public AVLRepetidos<Registros> AVLNombres;
        public ArbolAVL<Registros> AVLDPI;

        public List<Huffman<Registros>> ListaHuffman;
        public Huffman<Registros> huffman;

        private Singleton()
        {
            ListaRegistros = new List<Registros>();
            RegistrosEncontrados = new List<Registros>();
            ListaDeListas = new List<List<Registros>>();
            AVLNombres = new AVLRepetidos<Registros>();
            AVLDPI = new ArbolAVL<Registros>();
            ListaHuffman = new List<Huffman<Registros>>();
            huffman = new Huffman<Registros>();

        }


        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
