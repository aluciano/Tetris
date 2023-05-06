using System;
using Tetris.Enumerators;

namespace Tetris.Entity
{
    public class Peca
    {
        public Peca(PecaEnum tipo)
        {
            Tipo = tipo;
            Configurar();
        }

        private void Configurar()
        {
            switch (Tipo)
            {
                case PecaEnum.Quadrado1:
                    Largura = 2;
                    Altura = 1;
                    Cor = ConsoleColor.Blue;
                    break;
                case PecaEnum.Quadrado4:
                    Largura = 4;
                    Altura = 2;
                    Cor = ConsoleColor.Magenta;
                    break;
                case PecaEnum.Quadrado6:
                    Largura = 6;
                    Altura = 3;
                    Cor = ConsoleColor.Green;
                    break;
                case PecaEnum.Linha2:
                    Largura = 4;
                    Altura = 1;
                    Cor = ConsoleColor.Yellow;
                    break;
                case PecaEnum.Linha3:
                    Largura = 6;
                    Altura = 1;
                    Cor = ConsoleColor.Red;
                    break;
                case PecaEnum.Linha4:
                    Largura = 8;
                    Altura = 1;
                    Cor = ConsoleColor.Cyan;
                    break;
                default:
                    break;
            }

            int metadeLargura = (Largura / 2);
            bool metadeLarguraEhPar = metadeLargura % 2 == 0;

            if (!metadeLarguraEhPar)
                metadeLargura++;
            
            Esquerda = 3 + 25 - metadeLargura;
            Topo = 3;
        }

        public PecaEnum Tipo { get; set; }
        public int Largura { get; set; }
        public int Altura { get; set; }
        public int Esquerda { get; set; }
        public int Topo { get; set; }
        public ConsoleColor Cor { get; set; }
    }
}
