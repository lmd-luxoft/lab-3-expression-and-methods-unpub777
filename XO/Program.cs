using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO
{
    class Program
    {
       static char win = '-';
       static string PlayerName1, PlayerName2;
       static char[] cells = new char[]{ '-', '-', '-', '-', '-', '-', '-', '-', '-' };

        static void show_cells()
        {
            Console.Clear();

            Console.WriteLine("Числа клеток:");
            Console.WriteLine("-1-|-2-|-3-");
            Console.WriteLine("-4-|-5-|-6-");
            Console.WriteLine("-7-|-8-|-9-");

            Console.WriteLine("Текущая ситуация (---пустой):");
            Console.WriteLine($"-{cells[0]}-|-{cells[1]}-|-{cells[2]}-");
            Console.WriteLine($"-{cells[3]}-|-{cells[4]}-|-{cells[5]}-");
            Console.WriteLine($"-{cells[6]}-|-{cells[7]}-|-{cells[8]}-");        
        }
        static void make_move(int num)
        {
            Console.Write(GetPlayerName(num));

            var cell = getCell(",введите номер ячейки,сделайте свой ход:");

            while (IsIncorrectPlace(cell))
            {
                cell = getCell("Введите номер правильного ( 1-9 ) или пустой ( --- ) клетки , чтобы сделать ход:");

                Console.WriteLine();
            }

            cells[cell - 1] = GetPlayerMark(num);
        }

        static string GetPlayerName(int num) => num == 1 ? PlayerName1 : PlayerName2;

        static char GetPlayerMark(int num)  => num == 1 ? 'X' : 'O';

        static int getCell(string message)
        {
            do
            {
                Console.Write(message);

                var raw_cell = Console.ReadLine();
                if (Int32.TryParse(raw_cell, out var cell))
                {
                    return cell;
                }
            }
            while (true);
        }

        static bool IsIncorrectPlace(int cell)
        {
            return (cell > 9 || cell < 1 || cells[cell - 1] == 'O' || cells[cell - 1] == 'X');
        }

        static char check()
        {
            for (int i = 0; i < 3; i++)
            {
                if (HasVictory(i))
                    return cells[i];
            }
            return '-';
        }

        static bool HasVictory(int index) => HasHorizontalLine(index) || HasVerticalLine(index) || HasDiagLine();

        static bool HasHorizontalLine(int index) => cells[index * 3] == cells[index * 3 + 1] && cells[index * 3 + 1] == cells[index * 3 + 2];
        static bool HasVerticalLine(int index) => cells[index] == cells[index + 3] && cells[index + 3] == cells[index + 6];
        static bool HasDiagLine() => (cells[2] == cells[4] && cells[4] == cells[6]) || (cells[0] == cells[4] && cells[4] == cells[8]);

        static void result()
        {
            if (win == 'X')
            {
                Console.WriteLine($"{PlayerName1} вы  выиграли поздравляем {PlayerName2} а вы проиграли...");
                return;
            }

            Console.WriteLine($"{PlayerName2} вы  выиграли поздравляем {PlayerName1} а вы проиграли...");
        }

        static void inputPlayers()
        {
            do
            {
                Console.Write("Введите имя первого игрока : ");
                PlayerName1 = Console.ReadLine();

                Console.Write("Введите имя второго игрока: ");
                PlayerName2 = Console.ReadLine();
                Console.WriteLine();
            } while (PlayerName1 == PlayerName2);
        }

        static int getActivePlayer(int move) => move % 2 != 0 ? 1 : 2;

        static void Main(string[] args)
        {
            inputPlayers();

            show_cells();

            for (int move = 1; move <= 9; move++)
            {
                make_move(getActivePlayer(move));

                show_cells();

                if (move >= 5)
                {
                    win = check();
                    if (win != '-')
                        break;
                }

            }

            result();
        }
    }
}
