using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int ReadFromConsole(string message)
            {
                int a = 0;
                while (a == 0)
                {
                    Console.Write(message);
                    try
                    {
                        a = Convert.ToInt32(Console.ReadLine());
                        if (a < 1 || a > 8)
                        {
                            Console.Write("Некорректный ввод.\n" + message);
                            a = 0;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Write("Некорректный ввод.\n" + message);
                        a = 0;
                    }
                   
                }
                return a;
            }
            
            int k, l, m, n;
            void questionA()
            {
                string q = "не";
                if ((k + l) % 2 == (m + n) % 2) q = "";
                Console.WriteLine("а) Данные поля " + q + " являются полями одного цвета. ");
            }
            void questionB()
            {
                string q = "не";
                if (DiagonalCheck() || (l == n) || (k == m)) q = "";
                Console.WriteLine("б) Ферзь со второго поля " + q + " угрожает первому полю. ");
            }
            void questionC()
            {
                string q = "не";
                int[] MovesX = new int[] { 1, 2, 1, 2, -1, -2, -1, -2 };
                int[] MovesY = new int[] { 2, 1,-2,-1,  2,  1, -2, -1 };
                for(int i = 0; i < 8; i++)
                {
                    if(k+MovesX[i]==m || n + MovesY[i] == l)
                    {
                        q = "";
                        break;
                    }
                }
                Console.WriteLine("в) Конь со второго поля " + q + " угрожает первому полю. ");
               
            }
            void questionD()
            {
                string q = "";
                string f = "";
                if (!((l == n) || (k == m)))
                {
                    q = "не";
                    f = "\n Ладья может совершить ход на поле (" + k + "," + n + "), оттуда на первое поле";
                }
                Console.WriteLine("г) Ладья со второго поля " + q + " может попасть на первое. "+f);
            }
            void questionE()
            {
                string q = "";
                string f = "";
                if (!(DiagonalCheck() || (l == n) || (k == m)))
                {
                    q = "не";
                    f = "\n Ферзь может совершить ход на поле (" + k + "," + n + "), оттуда на первое поле";
                }
                Console.WriteLine("д) Ферзь со второго поля " + q + " может попасть на первое. " + f);
            }
            void questionF()
            {
                string q = "";
                
                if (!(DiagonalCheck()))
                {
                    if ((k + l) % 2 != (m + n) % 2) q = " не сможет попасть  никогда, тк поля разного цвета";
                    else
                    {
                        q = "";
                        //Ищем диагональ
                        int x = k; int y = l;
                        while ((k > 0) && (l > 0))
                        {
                            k--; l--;
                            if (DiagonalCheck())
                            {
                                q = "сможет попасть через промежуточное поле(" + k + "," + l + ")";
                                break;
                            }
                        }
                        if (q == "") while ((k < 8) && (l > 0))
                            {
                                k++; l--;
                                if (DiagonalCheck())
                                {
                                    q = "сможет попасть через промежуточное поле(" + k + "," + l + ")";
                                    break;
                                }
                            }
                        if (q == "") while ((k < 8) && (l < 8))
                            {
                                k--; l--;
                                if (DiagonalCheck())
                                {
                                    q = "сможет попасть  через промежуточное поле(" + k + "," + l + ")";
                                    break;
                                }
                            }
                        if (q == "") while ((k > 0) && (l < 8))
                            {
                                k--; l--;
                                if (DiagonalCheck())
                                {
                                    q = "сможет попасть через промежуточное поле(" + k + "," + l + ")";
                                    break;
                                }
                            }



                    }
                }
                else q = "может попасть за один ход";
                Console.WriteLine("е) Слон с первого на второе "+q);
            }
            bool DiagonalCheck(){
                
                return (Math.Abs(k - m) == Math.Abs(l - n));
            }
            
            k = ReadFromConsole("Введите номер вертикали первого поля:");
            l = ReadFromConsole("Введите номер горизонтали первого поля:");
            m = ReadFromConsole("Введите номер вертикали второго поля:");
            n = ReadFromConsole("Введите номер горизонтали второго поля:");

            Console.Clear();
            Console.WriteLine("Получилось поле ("+k+','+l+ ") и поле(" + m + ',' + n + ")");
            questionA();
            questionB();
            questionC();
            questionD();
            questionE();
            questionF();
            Console.Read();


        }
    }
}
