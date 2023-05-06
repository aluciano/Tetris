using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Util
{
    public class ReadConsolePosition
    {
        //[DllImport("kernel32", SetLastError = true)]
        //static extern IntPtr GetStdHandle(int num);

        //[DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        //[return: MarshalAs(UnmanagedType.Bool)] //   ̲┌──────────────────^
        //static extern bool ReadConsoleOutputCharacterA(
        //IntPtr hStdout,   // result of 'GetStdHandle(-11)'
        //out byte ch,      // A̲N̲S̲I̲ character result
        //uint c_in,        // (set to '1')
        //uint coord_XY,    // screen location to read, X:loword, Y:hiword
        //out uint c_out);  // (unwanted, discard)

        //[DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        //[return: MarshalAs(UnmanagedType.Bool)] //   ̲┌───────────────────^
        //static extern bool ReadConsoleOutputCharacterW(
        //    IntPtr hStdout,   // result of 'GetStdHandle(-11)'
        //    out Char ch,      // U̲n̲i̲c̲o̲d̲e̲ character result
        //    uint c_in,        // (set to '1')
        //    uint coord_XY,    // screen location to read, X:loword, Y:hiword
        //    out uint c_out);  // (unwanted, discard)


        //public static char ObterCaracter(uint esquerda, uint topo)
        //{
        //    var stdout = GetStdHandle(-11);

        //    uint coord = esquerda;
        //    coord |= 2 << 16; //topo

        //    if (!ReadConsoleOutputCharacterA(
        //        stdout,
        //        out byte chAnsi,    // result: single ANSI char
        //        1,                  // # of chars to read
        //        coord,              // (X,Y) screen location to read (see above)
        //        out _))             // result: actual # of chars (unwanted)
        //        throw new Win32Exception();

        //    return (char)chAnsi;
        //}


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadConsoleOutputCharacter(
            IntPtr hConsoleOutput,
            [Out] StringBuilder lpCharacter,
            uint length,
            COORD bufferCoord,
            out uint lpNumberOfCharactersRead);

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;
        }

        public static char ObterCaracterNaPosicao(int x, int y)
        {
            IntPtr consoleHandle = GetStdHandle(-11);
            if (consoleHandle == IntPtr.Zero)
            {
                return '\0';
            }
            COORD position = new COORD
            {
                X = (short)x,
                Y = (short)y
            };
            StringBuilder result = new StringBuilder(1);
            uint read = 0;
            if (ReadConsoleOutputCharacter(consoleHandle, result, 1, position, out read))
            {
                return result[0];
            }
            else
            {
                return '\0';
            }
        }

    }
}
