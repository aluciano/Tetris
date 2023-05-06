using System;
using Tetris.Entity;
using Tetris.Enumerators;
using Tetris.Util;

namespace Tetris
{
    public class Tetris
    {
        private Peca PecaAtual { get; set; }

        public Tetris()
        {
            Console.CursorVisible = false;
            Console.WindowWidth = 100;
            Console.WindowHeight = 53;
            PecaAtual = new Peca(ObterPecaAleatoria());
            DesenharAreaJogo();
            EscreverPecaAtual();

            DateTime tempo = DateTime.Now;
            ConsoleKeyInfo tecla = default;

            bool fimDeJogo = false;
            while (!fimDeJogo)
            {
                if (DateTime.Now > tempo.AddSeconds(2))
                {
                    fimDeJogo = MoverPecaAtualParaBaixo();
                    tempo = DateTime.Now;
                }

                if (!fimDeJogo)
                {
                    AplicarMovimentoJogador(tecla);
                }
            }

            Console.SetCursorPosition(65, 10);
            Console.Write("FIM DE JOGO");
        }

        private void AplicarMovimentoJogador(ConsoleKeyInfo tecla)
        {
            if (Console.KeyAvailable)
            {
                tecla = Console.ReadKey();

                switch (tecla.Key)
                {
                    case ConsoleKey.LeftArrow:
                        MoverPecaAtualParaEsquerda();
                        break;
                    case ConsoleKey.UpArrow:
                        break;
                    case ConsoleKey.RightArrow:
                        MoverPecaAtualParaDireita();
                        break;
                    case ConsoleKey.DownArrow:
                        MoverPecaAtualParaBaixo();
                        break;
                    default:
                        break;
                }
            }
        }

        private void MoverPecaAtualParaEsquerda()
        {
            if (!EspacoPreenchidoEsquerdaPecaAtual())
            {
                ApagarPecaAtual();
                PecaAtual.Esquerda -= 2;
                EscreverPecaAtual();
            }
        }

        private bool EspacoPreenchidoEsquerdaPecaAtual()
        {
            //se já existir um espaço preenchido nos caracteres à esquerda da peça atual,
            //não move a peça
            int primeiraColunaPecaAtual = PecaAtual.Esquerda;

            int colunaEsquerdaPecaAtual = primeiraColunaPecaAtual - 1;
            int primeiraLinhaPecaAtual = PecaAtual.Topo;
            int ultimaLinhaPecaAtual = primeiraLinhaPecaAtual + PecaAtual.Altura - 1;

            for (int i = primeiraLinhaPecaAtual; i <= ultimaLinhaPecaAtual; i++)
            {
                char partePeca = ReadConsolePosition.ObterCaracterNaPosicao(colunaEsquerdaPecaAtual, i);

                if (partePeca != ' ')
                {
                    return true;
                }
            }

            return false;
        }

        private PecaEnum ObterPecaAleatoria()
        {
            Array values = Enum.GetValues(typeof(PecaEnum));
            Random random = new Random();
            return (PecaEnum)values.GetValue(random.Next(values.Length));
        }

        public bool MoverPecaAtualParaBaixo()
        {
            if (EspacoPreenchidoAbaixoPecaAtual())
            {
                PecaAtual = new Peca(ObterPecaAleatoria());
                //PecaAtual = new PosicaoPeca(4 + 25 - (PecaAtual.Largura / 2), 3);
                EscreverPecaAtual();

                return EspacoPreenchidoAbaixoPecaAtual();
            }

            ApagarPecaAtual();
            PecaAtual.Topo++;
            EscreverPecaAtual();

            return false;
        }

        private bool EspacoPreenchidoAbaixoPecaAtual()
        {
            //se já existir um espaço preenchido nos caracteres abaixo da peça atual,
            //não move a peça e gera uma nova peça no topo
            int primeiraLinhaPecaAtual = PecaAtual.Topo;
            int ultimaLinhaPecaAtual = primeiraLinhaPecaAtual + PecaAtual.Altura - 1;

            int linhaAbaixoPecaAtual = ultimaLinhaPecaAtual + 1;
            int primeiraColunaPecaAtual = PecaAtual.Esquerda;
            int ultimaColunaPecaAtual = primeiraColunaPecaAtual + PecaAtual.Largura - 1;

            for (int i = primeiraColunaPecaAtual; i <= ultimaColunaPecaAtual; i++)
            {
                char partePeca = ReadConsolePosition.ObterCaracterNaPosicao(i, linhaAbaixoPecaAtual);

                if (partePeca != ' ')
                {
                    return true;
                }
            }

            return false;
        }

        public void MoverPecaAtualParaDireita()
        {
            if (!EspacoPreenchidoDireitaPecaAtual())
            {
                ApagarPecaAtual();
                PecaAtual.Esquerda += 2;
                EscreverPecaAtual();
            }
        }

        private bool EspacoPreenchidoDireitaPecaAtual()
        {
            //se já existir um espaço preenchido nos caracteres à direita da peça atual,
            //não move a peça
            int primeiraColunaPecaAtual = PecaAtual.Esquerda;
            int ultimaColunaPecaAtual = primeiraColunaPecaAtual + PecaAtual.Largura - 1;

            int colunaDireitaPecaAtual = ultimaColunaPecaAtual + 1;
            int primeiraLinhaPecaAtual = PecaAtual.Topo;
            int ultimaLinhaPecaAtual = primeiraLinhaPecaAtual + PecaAtual.Altura - 1;

            for (int i = primeiraLinhaPecaAtual; i <= ultimaLinhaPecaAtual; i++)
            {
                char partePeca = ReadConsolePosition.ObterCaracterNaPosicao(colunaDireitaPecaAtual, i);

                if (partePeca != ' ')
                {
                    return true;
                }
            }

            return false;
        }

        private void DesenharAreaJogo()
        {
            Console.SetCursorPosition(2, 2);

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("0".PadRight(54, '0'));

            for (int i = 1; i <= 46; i++)
            {
                Console.SetCursorPosition(2, 2 + i);

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("00");
                Console.ResetColor();
                Console.Write("".PadRight(50));
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("00");
            }

            Console.SetCursorPosition(2, 49);

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("0".PadRight(54, '0'));
            Console.ResetColor();
        }

        private void EscreverPecaAtual(bool apagar = false)
        {
            Console.SetCursorPosition(PecaAtual.Esquerda,
                                      PecaAtual.Topo);

            char texto = char.Parse(((byte)PecaAtual.Tipo).ToString());

            if (apagar)
            {
                texto = ' ';
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = PecaAtual.Cor;
                Console.ForegroundColor = PecaAtual.Cor;
            }

            for (int i = 1; i <= PecaAtual.Altura; i++)
            {
                Console.Write($"{texto}".PadRight(PecaAtual.Largura, texto));

                Console.SetCursorPosition(PecaAtual.Esquerda,
                                          PecaAtual.Topo + i);
            }

            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
        }

        private void ApagarPecaAtual()
        {
            EscreverPecaAtual(true);
        }
    }
}
