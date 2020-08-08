//Справочник автомобилей
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Butymov.CarsDirectory
{
    public class Car
    {
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public string YearOfIssue { get; set; }
        public string Kilometrage { get; set; }
    }

    public class Directory : Car
    {
        public const string jsonPath = @"C:\Users\pc\source\repos\Butymov.CarsDirectory\Car.json";
        public const string DirectoryPath = @"C:\Users\pc\source\repos\Butymov.CarsDirectory\Directory.txt";
        public const int NumOfParameters = 5;

        //Проверка существования файла базы (вызывается в начале каждой функции)
        //Если файла по заданному пути не существует, создается новый пустой файл с именем Directory.txt
        public void IsDBExist()
        {
            if (!File.Exists(DirectoryPath))
            {
                var DirectoryFile = File.Create(DirectoryPath);
                DirectoryFile.Close();
                const string ValueForDataIfDirectoryFileDoNotExist = "";
                File.AppendAllText(DirectoryPath, ValueForDataIfDirectoryFileDoNotExist);
                Console.WriteLine("Предупреждение. Исходный файл не был найден, он был заменён на новый.");
            }
        }

        //Проверка существования номера в базе
        public bool IsLPExist(string LP)
        {
            string CheckLine;
            string[] Parameters;
            using (FileStream FS = new FileStream(DirectoryPath, FileMode.Open))
            {
                using (StreamReader SR = new StreamReader(FS, Encoding.UTF8))
                {
                    while ((CheckLine = SR.ReadLine()) != null)
                    {
                        if (CheckLine.Contains(LP))
                        {
                            Parameters = CheckLine.Split(' ');
                            if (Parameters[0] == LP)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Вывод списка
        public void Output()
        {
            IsDBExist();
            string OutputLine;
            string[] OutputLineArray;
            Console.WriteLine("\nСписок автомобилей: \n");
            using (FileStream FS = new FileStream(DirectoryPath, FileMode.Open))
            {
                using (StreamReader SR = new StreamReader(FS, Encoding.UTF8))
                {
                    while ((OutputLine = SR.ReadLine()) != null)
                    {
                        OutputLineArray = OutputLine.Split(' ');
                        for (int i = 0; i < OutputLineArray.Count(); i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    Console.Write("Гос. номер: ");
                                    break;
                                case 1:
                                    Console.Write("Марка: ");
                                    break;
                                case 2:
                                    Console.Write("Цвет: ");
                                    break;
                                case 3:
                                    Console.Write("Год выпуска: ");
                                    break;
                                case 4:
                                    Console.Write("Пробег(км): ");
                                    break;
                            }
                            Console.WriteLine(OutputLineArray[i]);
                        }
                    }
                }
            }
        }//Вывод списка

        //Перегрузка вывода списка 
        //Если Param = 1 - вывод списка будет отсортирован по году выпуска по возрастанию
        //Если Param = 2 - вывод списка будет отсортирован по пробегу по возрастанию
        public int Output(int Param)
        {
            string OutputLine;
            string[] OutputLineArray;
            List<string> OutputList = new List<string>();

            string[] FinalOutputArray;
            string[] TempOutputArray = new string[5];

            switch (Param)
            {
                case 1:
                    Console.WriteLine("\nСписок автомобилей (отсортирован по году выпуска): \n");
                    using (FileStream FS = new FileStream(DirectoryPath, FileMode.Open))
                    {
                        using (StreamReader SR = new StreamReader(FS, Encoding.UTF8))
                        {
                            while ((OutputLine = SR.ReadLine()) != null)
                            {
                                OutputLineArray = OutputLine.Split(' ');
                                for (int i = 0; i < OutputLineArray.Count(); i++)
                                {
                                    if (OutputLineArray[i] != "")
                                    {
                                        OutputList.Add(OutputLineArray[i]);
                                    }
                                }
                            }
                        }
                    }

                    FinalOutputArray = OutputList.ToArray();

                    //Сортировка пузырьком
                    for (int i = 3; i < FinalOutputArray.Count() - 1; i += 5)
                    {
                        for (int j = 3; j < FinalOutputArray.Count() - i - 1; j += 5)
                        {
                            if (System.Convert.ToInt16(FinalOutputArray[j]) > System.Convert.ToInt16(FinalOutputArray[j + 5]))
                            {
                                TempOutputArray[0] = FinalOutputArray[j - 3];
                                TempOutputArray[1] = FinalOutputArray[j - 2];
                                TempOutputArray[2] = FinalOutputArray[j - 1];
                                TempOutputArray[3] = FinalOutputArray[j];
                                TempOutputArray[4] = FinalOutputArray[j + 1];

                                FinalOutputArray[j - 3] = FinalOutputArray[j + 5 - 3];
                                FinalOutputArray[j - 2] = FinalOutputArray[j + 5 - 2];
                                FinalOutputArray[j - 1] = FinalOutputArray[j + 5 - 1];
                                FinalOutputArray[j] = FinalOutputArray[j + 5];
                                FinalOutputArray[j + 1] = FinalOutputArray[j + 5 + 1];

                                FinalOutputArray[j + 5 - 3] = TempOutputArray[0];
                                FinalOutputArray[j + 5 - 2] = TempOutputArray[1];
                                FinalOutputArray[j + 5 - 1] = TempOutputArray[2];
                                FinalOutputArray[j + 5] = TempOutputArray[3];
                                FinalOutputArray[j + 5 + 1] = TempOutputArray[4];
                            }
                        }
                    }//Сортировка пузырьком

                    //Вывод
                    for (int i = 0; i < FinalOutputArray.Count(); i += 5)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            switch (j)
                            {
                                case 0:
                                    Console.Write("Гос. номер: ");
                                    Console.WriteLine(FinalOutputArray[i]);
                                    break;
                                case 1:
                                    Console.Write("Марка: ");
                                    Console.WriteLine(FinalOutputArray[i + 1]);
                                    break;
                                case 2:
                                    Console.Write("Цвет: ");
                                    Console.WriteLine(FinalOutputArray[i + 2]);
                                    break;
                                case 3:
                                    Console.Write("Год выпуска: ");
                                    Console.WriteLine(FinalOutputArray[i + 3]);
                                    break;
                                case 4:
                                    Console.Write("Пробег(км): ");
                                    Console.WriteLine(FinalOutputArray[i + 4] + "\n");
                                    break;
                            }
                        }
                    }//Вывод

                    return 0;
                case 2:
                    Console.WriteLine("\nСписок автомобилей (отсортирован по пробегу): \n");
                    using (FileStream FS = new FileStream(DirectoryPath, FileMode.Open))
                    {
                        using (StreamReader SR = new StreamReader(FS, Encoding.UTF8))
                        {
                            while ((OutputLine = SR.ReadLine()) != null)
                            {
                                OutputLineArray = OutputLine.Split(' ');
                                for (int i = 0; i < OutputLineArray.Count(); i++)
                                {
                                    if (OutputLineArray[i] != "")
                                    {
                                        OutputList.Add(OutputLineArray[i]);
                                    }
                                }
                            }
                        }
                    }

                    FinalOutputArray = OutputList.ToArray();

                    //Сортировка пузырьком
                    for (int i = 4; i < FinalOutputArray.Count() - 1; i += 5)
                    {
                        for (int j = 4; j < FinalOutputArray.Count() - i - 1; j += 5)
                        {
                            if (System.Convert.ToInt64(FinalOutputArray[j]) > System.Convert.ToInt64(FinalOutputArray[j + 5]))
                            {
                                TempOutputArray[0] = FinalOutputArray[j - 4];
                                TempOutputArray[1] = FinalOutputArray[j - 3];
                                TempOutputArray[2] = FinalOutputArray[j - 2];
                                TempOutputArray[3] = FinalOutputArray[j - 1];
                                TempOutputArray[4] = FinalOutputArray[j];

                                FinalOutputArray[j - 4] = FinalOutputArray[j + 5 - 4];
                                FinalOutputArray[j - 3] = FinalOutputArray[j + 5 - 3];
                                FinalOutputArray[j - 2] = FinalOutputArray[j + 5 - 2];
                                FinalOutputArray[j - 1] = FinalOutputArray[j + 5 - 1];
                                FinalOutputArray[j] = FinalOutputArray[j + 5];

                                FinalOutputArray[j + 5 - 4] = TempOutputArray[0];
                                FinalOutputArray[j + 5 - 3] = TempOutputArray[1];
                                FinalOutputArray[j + 5 - 2] = TempOutputArray[2];
                                FinalOutputArray[j + 5 - 1] = TempOutputArray[3];
                                FinalOutputArray[j + 5] = TempOutputArray[4];
                            }
                        }
                    }//Сортировка пузырьком

                    //Вывод
                    for (int i = 0; i < FinalOutputArray.Count(); i += 5)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            switch (j)
                            {
                                case 0:
                                    Console.Write("Гос. номер: ");
                                    Console.WriteLine(FinalOutputArray[i]);
                                    break;
                                case 1:
                                    Console.Write("Марка: ");
                                    Console.WriteLine(FinalOutputArray[i + 1]);
                                    break;
                                case 2:
                                    Console.Write("Цвет: ");
                                    Console.WriteLine(FinalOutputArray[i + 2]);
                                    break;
                                case 3:
                                    Console.Write("Год выпуска: ");
                                    Console.WriteLine(FinalOutputArray[i + 3]);
                                    break;
                                case 4:
                                    Console.Write("Пробег(км): ");
                                    Console.WriteLine(FinalOutputArray[i + 4] + "\n");
                                    break;
                            }
                        }
                    }//Вывод

                    return 0;
                default:
                    Console.WriteLine("Ошибка. Недопустимый параметр.");
                    return 1;
            }
        }//Перегрузка вывода списка

        //Добавление автомобиля
        public int AddCar(JToken json)
        {
            IsDBExist();
            string CheckLP = "";

            string[] a = new string[NumOfParameters];
            string JSON;
            using (StreamReader SR = new StreamReader(jsonPath))
            {
                JSON = SR.ReadToEnd();
            }
            dynamic array = JsonConvert.DeserializeObject(JSON);

            foreach (var item in array)
            {
                CheckLP = item.LicensePlate;
            }

            //Проверка на существование номера в базе 
            if (IsLPExist(CheckLP) == true)
            {
                Console.WriteLine("Ошибка. Автомобиль с регистрационным знаком " + CheckLP + " уже существует в базе.");
                return 1;
            }

            //Проверка на правильный формат номера (данные регионов на 2020 год)
            //Обычный номер
            Regex NormalLP = new Regex(@"(А|В|Е|К|М|Н|О|Р|С|Т|У|Х){1}\d{3}(А|В|Е|К|М|Н|О|Р|С|Т|У|Х){2}(01|02|03|04|05|06|07|08|09|10|11|12|13|14|15|16|17|18|19|20|21|22|23|24|25|26|27|28|29|30|31|32|33|34|35|36|37|38|39|40|41|42|43|44|45|46|47|48|49|50|51|52|53|54|55|56|57|58|59|60|61|62|63|64|65|66|67|68|69|70|71|72|73|74|75|76|77|78|79|80|81|82|83|84|85|86|87|88|89|90|91|92|93|94|95|96|97|98|99|102|113|116|121|122|123|124|125|126|134|136|138|142|147|150|152|154|156|159|161|163|164|173|174|177|178|186|190|193|196|197|198|199|702|750|716|761|763|774|777|790|797|799){1}$");
            //Транзитный номер
            Regex TransitLP = new Regex(@"(А|В|Е|К|М|Н|О|Р|С|Т|У|Х){2}\d{3}(А|В|Е|К|М|Н|О|Р|С|Т|У|Х){1}(01|02|03|04|05|06|07|08|09|10|11|12|13|14|15|16|17|18|19|20|21|22|23|24|25|26|27|28|29|30|31|32|33|34|35|36|37|38|39|40|41|42|43|44|45|46|47|48|49|50|51|52|53|54|55|56|57|58|59|60|61|62|63|64|65|66|67|68|69|70|71|72|73|74|75|76|77|78|79|80|81|82|83|84|85|86|87|88|89|90|91|92|93|94|95|96|97|98|99|102|113|116|121|122|123|124|125|126|134|136|138|142|147|150|152|154|156|159|161|163|164|173|174|177|178|186|190|193|196|197|198|199|702|750|716|761|763|774|777|790|797|799){1}$");
            //Обычный номер с 000 (исключение)
            Regex ExceptionNormalLP = new Regex(@"(А|В|Е|К|М|Н|О|Р|С|Т|У|Х){1}000(А|В|Е|К|М|Н|О|Р|С|Т|У|Х){2}(01|02|03|04|05|06|07|08|09|10|11|12|13|14|15|16|17|18|19|20|21|22|23|24|25|26|27|28|29|30|31|32|33|34|35|36|37|38|39|40|41|42|43|44|45|46|47|48|49|50|51|52|53|54|55|56|57|58|59|60|61|62|63|64|65|66|67|68|69|70|71|72|73|74|75|76|77|78|79|80|81|82|83|84|85|86|87|88|89|90|91|92|93|94|95|96|97|98|99|102|113|116|121|122|123|124|125|126|134|136|138|142|147|150|152|154|156|159|161|163|164|173|174|177|178|186|190|193|196|197|198|199|702|750|716|761|763|774|777|790|797|799){1}$");
            //Транзитный номер с 000 (исключение)
            Regex ExceptionTransitLP = new Regex(@"(А|В|Е|К|М|Н|О|Р|С|Т|У|Х){2}000(А|В|Е|К|М|Н|О|Р|С|Т|У|Х){1}(01|02|03|04|05|06|07|08|09|10|11|12|13|14|15|16|17|18|19|20|21|22|23|24|25|26|27|28|29|30|31|32|33|34|35|36|37|38|39|40|41|42|43|44|45|46|47|48|49|50|51|52|53|54|55|56|57|58|59|60|61|62|63|64|65|66|67|68|69|70|71|72|73|74|75|76|77|78|79|80|81|82|83|84|85|86|87|88|89|90|91|92|93|94|95|96|97|98|99|102|113|116|121|122|123|124|125|126|134|136|138|142|147|150|152|154|156|159|161|163|164|173|174|177|178|186|190|193|196|197|198|199|702|750|716|761|763|774|777|790|797|799){1}$");

            MatchCollection MatchesNormalLP = NormalLP.Matches(CheckLP);
            MatchCollection MatchesTransitLP = TransitLP.Matches(CheckLP);
            MatchCollection MatchesExceptionNormalLP = ExceptionNormalLP.Matches(CheckLP);
            MatchCollection MatchesExceptionTransitLP = ExceptionTransitLP.Matches(CheckLP);

            if (MatchesExceptionNormalLP.Count > 0)
            {
                Console.WriteLine("Ошибка. Неверный формат номерного знака.");
                return 1;
            }

            if (MatchesExceptionTransitLP.Count > 0)
            {
                Console.WriteLine("Ошибка. Неверный формат номерного знака.");
                return 1;
            }

            if ((MatchesNormalLP.Count > 0) || (MatchesTransitLP.Count > 0))
            {
                foreach (var item in array)
                {
                    a[0] = item.LicensePlate;
                    a[1] = item.Model;
                    a[2] = item.Colour;
                    a[3] = item.YearOfIssue;
                    a[4] = item.Kilometrage;
                }
                for (int i = 0; i < a.Count(); i++)
                {
                    File.AppendAllText(DirectoryPath, a[i]);
                    File.AppendAllText(DirectoryPath, " ");
                }
                File.AppendAllText(DirectoryPath, "\n");
                Console.WriteLine("Успешно. Автомобиль с регистрационным знаком " + CheckLP + " добавлен в базу.");
                return 0;
            }
            Console.WriteLine("Ошибка. Неверный формат номерного знака.");
            return 1;
        }//Добавление автомобиля

        //Удаление автомобиля
        public int DeleteCar(JToken LP)
        {
            IsDBExist();

            //Проверка на существование номера в базе 
            if (IsLPExist(System.Convert.ToString(LP)) == true)
            {
                File.WriteAllLines(DirectoryPath, File.ReadAllLines(DirectoryPath).Where(v => v.Trim().IndexOf(System.Convert.ToString(LP)) == -1).ToArray(), Encoding.UTF8);
                Console.WriteLine("Успешно. Автомобиль с регистрационным знаком " + System.Convert.ToString(LP) + " удалён из базы.");
                return 0;
            }

            Console.WriteLine("Ошибка. Автомобиль с регистрационным знаком " + LP + " отсутсвует в базе.");
            return 1;
        }//Удаление автомобиля

        //Статистика базы
        public void Statistic()
        {
            IsDBExist();
            Console.WriteLine("\nСтатистика: ");
            int NumberOfRows = System.IO.File.ReadAllLines(DirectoryPath).Length;
            Console.WriteLine("Количество записей: " + NumberOfRows);
            DateTime LastEdit = File.GetLastWriteTime(DirectoryPath);
            Console.WriteLine("Последнее обращение к базе: " + LastEdit);
            DateTime CreationDate = File.GetCreationTime(DirectoryPath);
            Console.WriteLine("Дата создания базы: " + CreationDate);
        }//Статистика базы
    }

    class Program : Directory
    {
        static void Main(string[] args)
        {
            Directory Directory = new Directory();

            //Directory.AddCar(jsonPath);
            //Directory.DeleteCar("К502КК77");
            //Directory.Output();
            //Directory.Statistic();

            //Directory.Output(1);
            //Directory.Output(2);
        }
    }
}
