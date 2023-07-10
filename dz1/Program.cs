using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Schema;

namespace dz1
{
    internal class Program
    {
        // создание соединения с БД
        static SqlConnection connection;
        static void Main(string[] args)
        {
            // выделяем память для соединения
            connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=DESKTOP-7GU49OD\SQLEXPRESS;
                                            Initial Catalog = FruitsVegetables;
                                            Integrated Security = true;";
            // проверка, что соединение работает без ошибок
            try
            {
                // открываем соединение с БД
                connection.Open();
                Console.WriteLine("Соединение успешно открыто.");
                ShowAllInfo(); 
                Console.WriteLine("---------------------------------------");
                ShowNames();
                Console.WriteLine("---------------------------------------");
                ShowColor();
                Console.WriteLine("---------------------------------------");
                ShowMaxCalory();
                Console.WriteLine("---------------------------------------");
                ShowMinCalory();
                Console.WriteLine("---------------------------------------");
                ShowAvgCalory();
                Console.WriteLine("---------------------------------------");
                ShowVegCount(); 
                Console.WriteLine("---------------------------------------");
                ShowFrCount();
                Console.WriteLine("---------------------------------------");
                CountThisColor("желтый");
                Console.WriteLine("---------------------------------------");
                CountAllColors();
                Console.WriteLine("---------------------------------------");
                ShowCalLessThen(125);
                Console.WriteLine("---------------------------------------");
                ShowCalMoreThen(125);
                Console.WriteLine("---------------------------------------");
                ShowCalBetween(100, 125);
                Console.WriteLine("---------------------------------------");
                ShowRedYellow();
            }
            finally
            {
                // закрываем соединение с БД
                connection.Close();
                Console.WriteLine("Соединение успешно закрыто.");
            }
        }

        // Отображение всей информации из таблицы с овощами и фруктами;
        static void ShowAllInfo()
        {
            SqlCommand command = new SqlCommand();  // создание объекта-запроса для выборки данных
            command.Connection = connection;        // инициализация соединения

            command.CommandText = "select * from FruitsVegs";  // запрос

            // выполняем команду и помещаем в reader полученный данные для дальнейшего чтения
            SqlDataReader rd = command.ExecuteReader();

            Console.WriteLine("Отображение всей информации из таблицы с овощами и фруктами:");
            Console.WriteLine($"{rd.GetName(0)} {rd.GetName(1)} {rd.GetName(2)} {rd.GetName(3)} {rd.GetName(4)}");
            while (rd.Read())  // считываем полученные данные
            {
                Console.WriteLine($"{rd[0]} {rd[1]} {rd[2]} {rd[3]} {rd[4]}");
            }
            rd.Close();  // закрываем поток
        }

        // Отображение всех названий овощей и фруктов;
        static void ShowNames()
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "select Name from FruitsVegs";
            SqlDataReader rd = command.ExecuteReader();
            Console.WriteLine("Отображение всех названий овощей и фруктов:");
            while (rd.Read())
            {
                Console.WriteLine($"{rd["Name"]}");
            }
            rd.Close();
        }

        // Отображение всех цветов;
        static void ShowColor()
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "select Color from FruitsVegs group by Color";
            SqlDataReader rd = command.ExecuteReader();
            Console.WriteLine("Отображение всех цветов:");
            while (rd.Read())
            {
                Console.WriteLine($"{rd["Color"]}");
            }
            rd.Close();
        }

        //Показать максимальную калорийность;
        static void ShowMaxCalory()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = "select max(Calory) from FruitsVegs";
            
            object maxCalory = command.ExecuteScalar();
            Console.WriteLine($"Показать максимальную калорийность: {maxCalory}");
        }

        // Показать минимальную калорийность
        static void ShowMinCalory()
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "select min(Calory) from FruitsVegs";
            object minCalory = command.ExecuteScalar();
            Console.WriteLine($"Показать минимальную калорийность: {minCalory}");
        }

        // Показать среднюю калорийность
        static void ShowAvgCalory()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select avg(Calory) from FruitsVegs";
            object avgCalory = cmd.ExecuteScalar();
            Console.WriteLine($"Показать среднюю калорийность: {avgCalory}");
        }

        // Показать количество овощей;
        static void ShowVegCount()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select count(*) from FruitsVegs where Type='овощ'";
            object vegCount = cmd.ExecuteScalar();
            Console.WriteLine($"Показать количество овощей: {vegCount}");
        }

        // Показать количество фруктов;
        static void ShowFrCount()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select count(*) from FruitsVegs where Type='фрукт'";
            object frCount = cmd.ExecuteScalar();
            Console.WriteLine($"Показать количество фруктов: {frCount}");
        }

        // Показать количество овощей и фруктов заданного цвета;
        static void CountThisColor(string myColor)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"select count(*) from FruitsVegs where Color = '{myColor}'";
            object colorCount = cmd.ExecuteScalar();
            Console.WriteLine($"Показать количество фруктов с цветом {myColor}: {colorCount}");
        }

        // Показать количество овощей фруктов каждого цвета;
        static void CountAllColors()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select count(*), Color from FruitsVegs group by Color";
            
            SqlDataReader rd = cmd.ExecuteReader();
            Console.WriteLine("Показать количество овощей фруктов каждого цвета:");
            while (rd.Read())
            {
                Console.WriteLine($"{rd[1]} {rd[0]}");
            }
            rd.Close();
        }

        // Показать овощи и фрукты с калорийностью ниже указанной
        static void ShowCalLessThen(float myCalory)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"select Name, Calory from FruitsVegs where Calory<'{myCalory}'";

            SqlDataReader rd = cmd.ExecuteReader();
            Console.WriteLine($"Показать овощи и фрукты с калорийностью ниже {myCalory}");
            while (rd.Read())
            {
                Console.WriteLine($"{rd["Name"]} {rd["Calory"]}");
            }
            rd.Close();
        }

        // Показать овощи и фрукты с калорийностью выше указанной;
        static void ShowCalMoreThen(float myCalory)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"select Name, Calory from FruitsVegs where Calory>'{myCalory}'";

            SqlDataReader rd = cmd.ExecuteReader();
            Console.WriteLine($"Показать овощи и фрукты с калорийностью выше {myCalory}");
            while (rd.Read())
            {
                Console.WriteLine($"{rd["Name"]} {rd["Calory"]}");
            }
            rd.Close();
        }

        // Показать овощи и фрукты с калорийностью в указанном диапазоне;
        static void ShowCalBetween(float myCalory1, float myCalory2)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"select Name, Calory from FruitsVegs where Calory between '{myCalory1}' and '{myCalory2}'";

            SqlDataReader rd = cmd.ExecuteReader();
            Console.WriteLine($"Показать овощи и фрукты с калорийностью от {myCalory1} до {myCalory2}");
            while (rd.Read())
            {
                Console.WriteLine($"{rd["Name"]} {rd["Calory"]}");
            }
            rd.Close();
        }

        // Показать все овощи и фрукты, у которых цвет желтый или красный.
        static void ShowRedYellow()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select Name, Color from FruitsVegs where Color = 'красный' or Color='желтый'";
            SqlDataReader rd = cmd.ExecuteReader();
            Console.WriteLine("Показать все овощи и фрукты, у которых цвет желтый или красный:");
            while (rd.Read())
            {
                Console.WriteLine($"{rd[0]} {rd[1]}");
            }
            rd.Close();
        }
    }
}
