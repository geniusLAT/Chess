﻿using System;
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
            int ReadFromConsole(string message)//Метод чтения координат с консоли
            {
                int a = 0;//Сюда пишем значение
                while (a == 0)//Пока оно ноль продолжаем требовать ввод
                {
                    Console.Write(message);//Пишем, что мы хотим от юзера
                    try
                    {
                        a = Convert.ToInt32(Console.ReadLine());//Берём ввод с клавиатуры и пытаемся сделать цифрой
                        if (a < 1 || a > 8)//Есть ли такие координаты на шахматной доске
                        {
                            Console.Write("Некорректный ввод.\n" + message);//Выводим просьбу
                            a = 0;//Чтобы цикл продолжился
                        }
                    }
                    catch (Exception e)//Сделать цифрой не получилось
                    {
                        Console.Write("Некорректный ввод.\n" + message);//Выводим просьбу
                        a = 0;//Чтобы цикл продолжился
                    }
                   
                }
                return a;//Возвращаем
            }
            
            int k, l, m, n;//Переменные
            //Функции отвечающие за ответ на соответвующий вопрос из списка заданий
            void questionA()
            {
                string q = "не";//Вспоминаем DRY, чтобы не писать дважды предложение просто будем вставлять "не" или ""
                if ((k + l) % 2 == (m + n) % 2) q = "";//Если остатки от деления на 2 равны, то цвет один.
                Console.WriteLine("а) Данные поля " + q + " являются полями одного цвета. ");//Генерируем ответ с "не" или без
            }
            void questionB()
            {
                string q = "не";//Опять схема с "не", думаю, что я один раз её комментировал, дальше аналогично
                if (DiagonalCheck() || (l == n) || (k == m)) q = "";//Ферзь ходит по диагоналям, и по основным осям. Проверяем.
                Console.WriteLine("б) Ферзь со второго поля " + q + " угрожает первому полю. ");
            }
            void questionC()
            {
                string q = "не";
                //Я не придумал нормальную формулу хождения коня и просто сделал два массива его хождений
                int[] MovesX = new int[] { 1, 2, 1, 2, -1, -2, -1, -2 };
                int[] MovesY = new int[] { 2, 1,-2,-1,  2,  1, -2, -1 };
                for(int i = 0; i < 8; i++)//Проверяем все возможные ходы коня на предмет совпадения с первым полем из дано
                {
                    if(k+MovesX[i]==m && n + MovesY[i] == l)//Совпало с первым полем
                    {
                        q = "";//Ответ не содержит "не"
                        break;//Выходим из цикла ради оптимизации
                    }
                }
                Console.WriteLine("в) Конь со второго поля " + q + " угрожает первому полю. ");
               
            }
            void questionD()
            {
                string q = "";
                string f = "";
                if (!((l == n) || (k == m)))//Если ладья не может
                {
                    q = "не";
                    f = "\n Ладья может совершить ход на поле (" + k + "," + n + "), оттуда на первое поле";//Обозначаем промежуточное поле
                }
                Console.WriteLine("г) Ладья со второго поля " + q + " может попасть на первое. "+f);
            }
            void questionE()
            {
                string q = "";
                string f = "";
                if (!(DiagonalCheck() || (l == n) || (k == m)))//Если не получилось за один ход
                {
                    q = "не";
                    f = "\n Ферзь может совершить ход на поле (" + k + "," + n + "), оттуда на первое поле";//Скопировал с ладьи
                }
                Console.WriteLine("д) Ферзь со второго поля " + q + " может попасть на первое. " + f);
            }
            void questionF()
            {
                string q = "";
                
                if (!(DiagonalCheck()))//Если слон и поле на одной диагонали, то можно за один ход
                {
                    //Слон не может сменить цвет клетки
                    if ((k + l) % 2 != (m + n) % 2) q = " не сможет попасть  никогда, тк поля разного цвета";
                    else
                    {
                        q = "";
                        //Ищем диагональ
                        int x = k; int y = l;//Потенциально мы могли бы не менять k,l, но задание последнее, а функция DiagonalCheck()уже работает под них
                        //Тут мы проходимся
                        while ((k > 0) && (l > 0))
                        {
                            k--; l--;//Проходим все клеточки по диагонали, пока не упрёмся в край доски (верхний или нижний
                            if (DiagonalCheck())//Если с нашей клеточки до желанной можно пройти по диагонали, то наша промежуточная
                            {
                                q = "сможет попасть через промежуточное поле(" + k + "," + l + ")";
                                break;
                            }
                        }
                        //Сознательно не ставлю else, чтобы при наличии двух путей, указались оба
                        k = x;l = y;//Восстанавливаем сохранённые значения как в  дано.
                        //Ниже всё аналогично, просто для разных направлений
                        if (q == "") while ((k < 8) && (l > 0))
                            {
                                k++; l--;
                                if (DiagonalCheck())
                                {
                                    q = "сможет попасть через промежуточное поле(" + k + "," + l + ")";
                                    break;
                                }
                            }
                        k = x; l = y;
                        if (q == "") while ((k < 8) && (l < 8))
                            {
                                k--; l--;
                                if (DiagonalCheck())
                                {
                                    q = "сможет попасть  через промежуточное поле(" + k + "," + l + ")";
                                    break;
                                }
                            }
                        k = x; l = y;
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
            bool DiagonalCheck(){//Проверка, на одной ли диагонали поля
                
                return (Math.Abs(k - m) == Math.Abs(l - n));//Если отход по одной оси равен отходу по другой, они на одной диагонали
            }
            //Вводим все координаты
            k = ReadFromConsole("Введите номер вертикали первого поля:");
            l = ReadFromConsole("Введите номер горизонтали первого поля:");
            m = ReadFromConsole("Введите номер вертикали второго поля:");
            n = ReadFromConsole("Введите номер горизонтали второго поля:");

            Console.Clear();//Очистили от просьб о введении кординат
            Console.WriteLine("Получилось поле ("+k+','+l+ ") и поле(" + m + ',' + n + ")");//Чтобы юзер мог вспомнить
            //Вызываем по очереди вопросы
            questionA();
            questionB();
            questionC();
            questionD();
            questionE();
            questionF();
            Console.Read();//Чтобы консоль не закрылась сразу, а только по нажатию любой кнопки.


        }
    }
}
