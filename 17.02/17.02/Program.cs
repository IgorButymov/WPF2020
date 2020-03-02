using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace _17._02
{
    class Program
    {
        class Moving
        {
            public double g = 9.8;
            public double x0 = 0;
            public double y0 = 0;
            public double v0;
            public double t;
            public double angle;
            public Moving()
            {
                File.WriteAllText("test.txt", "");
                Console.WriteLine("Введите начальную скорость");
                v0 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите угол");
                angle = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите x0");
                x0 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите y0");
                y0 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите время");
                t = Convert.ToDouble(Console.ReadLine());
                for (int k = 0; k < t; k++)
                {
                    double XCord = (x0 + v0 * k * Math.Cos(angle));
                    double YCord = (y0 + v0 * k * Math.Sin(angle) - ((g * k * k) / 2));
                    if (YCord < 0)
                    {
                        YCord = 0;
                    }
                    Console.WriteLine("x cord = {0}; y cord = {1}", XCord, YCord);
                    double[] myArray = new double[3] { XCord, YCord, k };
                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine(myArray[i]);
                    }
                    string str = "";
                    for (int j = 0; j < 3; j++)
                    {
                        str += Convert.ToString(myArray[j]);
                        str += "                 ";
                    }
                    //File.WriteAllText("test.txt", "");
                    File.AppendAllText("test.txt", "X координата        Y координата            Время(c)" + Environment.NewLine);
                    File.AppendAllText("test.txt", str + Environment.NewLine);
                }

            }
        }
        static void Main(string[] args)
        {
            Moving p = new Moving();




        }
    }
}
