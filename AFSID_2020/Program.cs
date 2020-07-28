using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFSID_2020
{
    class Program
    {
        static void Main(string[] args)
        {
            const double R1 = 2;      //радиус большой пробки, м
            const double R2 = 1.6;    //радиус средней пробки, м
            const double R3 = 0.8;    //радиус малой пробки, м
            const double e2 = 0.7;    //расстояние e2, м; e2 = |O1 O2|
            const double e3 = 0.5;    //рaсстояние e3, м; e3 = |O2 O3|
            const double J1 = 60260;  //момент инерции большой пробки, кг*м^2
            const double J2 = 4500;   //момент инерции средней пробки, кг*м^2
            const double J3 = 300;    //момент инерции малой пробки, кг*м^2
            const double mu1 = 1200;  //момент управления большой пробки, Н*м
            const double mu2 = 800;   //момент управления средней пробки, Н*м
            const double mu3 = 500;   //момент управления малой пробки, Н*м
            const double m1 = 142000; //масса большой пробки, кг
            const double m2 = 81000;  //масса средней пробки с вырезом, кг
            const double m3 = 40000;  //масса малой пробки, кг
            const double a = 1 / (3 * R2); //a = |O2 C2|
            const double alpha = 0;        //угол(О3 О2 С2)

            const double Rf = 1; //Rf < |O1O2| + R2 = e2 + R2 = 0.7 + 1.6 = 2.3

            double a1 = (1) / (J2 + m3 * Math.Pow(e3, 2));
            double a2 = (J2 + J3 + m3 * Math.Pow(e3, 2)) / (J3 * (J2 + m3 * Math.Pow(e3, 2)));

            //ПЕРВАЯ ЧАСТЬ 

            double fi10 = 30 * 0.174;     //начальное знаечние угла fi1 (задаем сами) 
            double fi1 = fi10;   //величина угла fi1
            double fi1f = fi10;  //конечное значение угла fi1
            double deltafi1 = 0 * 0.174; //СПРОСИТЬ дельта fi1 = угол(P1 O1 Qf) СПРОСИТЬ

            double fi20 = 10 * 0.174;   //начальное знаечние угла fi2 (задаем сами)
            double fi2 = 0 * 0.174;     //величина угла fi2,
            double fi2f = 0 * 0.174;    //конечное значение угла fi2
            double fi2TMin = 0 * 0.174; //угол fi2, который был получен в результате поиска минимального времени

            double fi30 = 10 * 0.174;   //начальное знаечние угла fi3 (задаем сами) 
            double fi3 = 0 * 0.174;     //величина угла fi3
            double fi3f = 0 * 0.174;    //конечное значение угла fi3
            double fi3TMin = 0 * 0.174; //угол fi3, который был получен в результате поиска минимального времени

            double deltapsif = 30 * 0.174; //угол(О2 О1 Р1)
            double psif = fi10 + deltapsif; //равно нулю на первом этапе

            double deltaX;
            double deltafi;

            double V1; //оптимальное время разворота средней и малой пробок
            double V2; //оптимальное время разворота средней и малой пробок
            double V3;

            double J1Star;
            double T = 0;
            double TMin = -1;
            //углы
            double ABS_O2P1_POW2;
            double O1O2P1;
            double O3O2P1;
            double O2O3P1;

            for (double i = fi20; i < 360 * 0.174 - fi20; i++) //цикл по углу fi2
            {
                for (double j = fi30; j < 360 * 0.174 - fi30; j++) //цикл по углу fi3
                {
                    ABS_O2P1_POW2 = Math.Pow(Rf * Math.Cos(psif) - e2 * Math.Cos(fi10), 2) + Math.Pow(Rf * Math.Sin(psif) - e2 * Math.Sin(fi10), 2);
                    O1O2P1 = Math.Acos((Math.Pow(Rf, 2) - Math.Pow(e2, 2) - ABS_O2P1_POW2) / (2 * e2 * Math.Sqrt(ABS_O2P1_POW2)));
                    O3O2P1 = Math.Acos((Math.Pow(R3, 2) - Math.Pow(e3, 2) - ABS_O2P1_POW2) / (2 * e3 * Math.Sqrt(ABS_O2P1_POW2)));
                    O2O3P1 = Math.Acos((ABS_O2P1_POW2 - Math.Pow(R3, 2) - Math.Pow(e3, 2)) / (2 * e3 * R3));

                    fi2f = Math.PI - O1O2P1 - O3O2P1; //конечное значение угла fi2
                    fi3f = Math.PI - O2O3P1;          //конечное значение угла fi3

                    /*Console.Write("O1O2P1 = ");
                    Console.WriteLine(O1O2P1);
                    Console.Write("O3O2P1 = ");
                    Console.WriteLine(O3O2P1);*/

                    fi2 = fi20 - fi2f; //значение угла fi2 //не число из-за O3O2P1
                    fi3 = fi30 - fi3f; //значение угла fi3


                    deltaX = fi2f + a1 / a2 * fi3f - fi20 - a1 / a2 * fi30; //дельта Хи
                    deltafi = fi2f + fi3f - fi20 - fi30;                    //дельта fi

                    J1Star = J1 + J2 + J3 + m2 * Math.Pow(e2, 2) + 2 * e2 * a * Math.Cos(fi2 + alpha) + m3 * (Math.Pow(e2, 2) + Math.Pow(e3, 2) + 2 * e2 * e3 * Math.Cos(fi2));

                    V3 = 2 * Math.Sqrt(J1Star * Math.Abs(deltafi1) / mu1);

                    if ((a2 * Math.Abs(deltaX)) >= (mu1 * a1 * deltafi / mu2))
                    {
                        //Console.WriteLine("Больше");
                        V1 = 2 * Math.Sqrt((a2 * Math.Abs(deltaX)) / (a1 * mu1 * (a2 - a1))); //оптимальное время разворота средней и малой пробок
                        T = V1 + V3;
                        if (TMin < 0)
                        {
                            TMin = T;
                            fi2TMin = fi2;
                            fi3TMin = fi3;
                        }
                        if ((TMin > 0) && (TMin > T))
                        {
                            TMin = T;
                            fi2TMin = fi2;
                            fi3TMin = fi3;
                        }

                    }

                    if ((a2 * Math.Abs(deltaX)) <= (mu1 * a1 * deltafi / mu2))
                    {
                        //Console.WriteLine("Меньше");
                        V2 = 2 * Math.Sqrt((Math.Abs(deltafi) / (mu2 * (a2 - a1)))); //оптимальное время разворота средней и малой пробок
                        T = V2 + V3;
                        if (TMin < 0)
                        {
                            TMin = T;
                            fi2TMin = fi2;
                            fi3TMin = fi3;
                        }
                        if ((TMin > 0) && (TMin > T))
                        {
                            TMin = T;
                            fi2TMin = fi2;
                            fi3TMin = fi3;
                        }
                    }

                } //конец цикла по углу fi3
            } //конец цикла по углу fi2
            Console.Write("TMin = ");
            Console.WriteLine(TMin);

            Console.Write("fi2 при TMin = ");
            Console.WriteLine(fi2TMin);
            Console.Write("fi3 при TMin = ");
            Console.WriteLine(fi3TMin);

            //ВТОРАЯ ЧАСТЬ


            double gamma1 = fi10 + Math.Acos((Math.Pow(Rf, 2) + Math.Pow(e2, 2) - Math.Pow(R2, 2)) / (2 * e2 * Rf)); //угол, который ставится в соответствие точке M, отсчитывается от оси ОХ в направлении против часовой стрелки до точки M
            double gamma2 = fi10 - Math.Acos((Math.Pow(Rf, 2) + Math.Pow(e2, 2) - Math.Pow(R2, 2)) / (2 * e2 * Rf)); //угол, который ставится в соответствие точке N, отсчитывается от оси ОХ в направлении против часовой стрелки до точки N

            Console.Write("g1 = ");
            Console.WriteLine(gamma1);
            Console.Write("g2 = ");
            Console.WriteLine(gamma2);

            //координаты точки M
            double XM = Rf * Math.Cos(gamma1);
            double YM = Rf * Math.Sin(gamma1);

            //координаты точки N
            double XN = Rf * Math.Cos(gamma2);
            double YN = Rf * Math.Sin(gamma2);

            

            

            double Rounding(double gamma)
            {
                double RoundingResult;
                if (gamma < 0)
                {
                    RoundingResult = Math.Ceiling(gamma);
                    return RoundingResult;
                }
                else
                {
                    RoundingResult = Math.Floor(gamma);
                    return RoundingResult;
                }
            }

            double gamma1Rounded = Rounding(gamma1);
            double gamma2Rounded = Rounding(gamma2);

            Console.Write("g1R = ");
            Console.WriteLine(gamma1Rounded);
            Console.Write("g2R = ");
            Console.WriteLine(gamma2Rounded);

            double TMin2 = -1;
            double TMinFinal = 0;
            double fi1TMin = 0;

            List<double> Time = new List<double>();
            T = 0;
     
            if (gamma1 > gamma2) //выполняется
            {
                Console.WriteLine("Положения точки P1 на дуге MN (i): ");
                for (double i = gamma2Rounded; i <= gamma1Rounded; i++) //цикл P1 
                {
                    Console.WriteLine(i);
                    //psif++;
                    deltafi1++;
                    ABS_O2P1_POW2 = Math.Pow(Rf * Math.Cos(psif) - e2 * Math.Cos(fi10), 2) + Math.Pow(Rf * Math.Sin(psif) - e2 * Math.Sin(fi10), 2);
                    O1O2P1 = Math.Acos((Math.Pow(Rf, 2) - Math.Pow(e2, 2) - ABS_O2P1_POW2) / (2 * e2 * Math.Sqrt(ABS_O2P1_POW2)));
                    O3O2P1 = Math.Acos((Math.Pow(R3, 2) - Math.Pow(e3, 2) - ABS_O2P1_POW2) / (2 * e3 * Math.Sqrt(ABS_O2P1_POW2)));
                    O2O3P1 = Math.Acos((ABS_O2P1_POW2 - Math.Pow(R3, 2) - Math.Pow(e3, 2)) / (2 * e3 * R3));

                    fi2f = Math.PI - O1O2P1 - O3O2P1; //конечное значение угла fi2
                    fi3f = Math.PI - O2O3P1;          //конечное значение угла fi3
                    
                    fi2 = fi20 - fi2f; //значение угла fi2 //не число из-за O3O2P1
                    fi3 = fi30 - fi3f; //значение угла fi3


                    deltaX = fi2f + a1 / a2 * fi3f - fi20 - a1 / a2 * fi30; //дельта Хи
                    deltafi = fi2f + fi3f - fi20 - fi30;                    //дельта fi

                    J1Star = J1 + J2 + J3 + m2 * Math.Pow(e2, 2) + 2 * e2 * a * Math.Cos(fi2 + alpha) + m3 * (Math.Pow(e2, 2) + Math.Pow(e3, 2) + 2 * e2 * e3 * Math.Cos(fi2));

                    V3 = 2 * Math.Sqrt(J1Star * Math.Abs(deltafi1) / mu1);

                    if ((a2 * Math.Abs(deltaX)) >= (mu1 * a1 * deltafi / mu2))
                    {
  
                        V1 = 2 * Math.Sqrt((a2 * Math.Abs(deltaX)) / (a1 * mu1 * (a2 - a1))); //оптимальное время разворота средней и малой пробок
                        T = V1 + V3;
                        Console.Write("T = ");
                        Console.WriteLine(T);
                        if (TMin2 < 0)
                        {
                            TMin2 = T;
                            fi1TMin = i * 0.174;
                        }
                        if ((TMin2 > 0) && (TMin2 > T))
                        {
                            TMin2 = T;
                            fi1TMin = i * 0.174;
                        }

                    }

                    if ((a2 * Math.Abs(deltaX)) <= (mu1 * a1 * deltafi / mu2))
                    {
                        //Console.WriteLine("Меньше");
                        V2 = 2 * Math.Sqrt((Math.Abs(deltafi) / (mu2 * (a2 - a1)))); //оптимальное время разворота средней и малой пробок
                        T = V2 + V3;
                        Console.Write("T = ");
                        Console.WriteLine(T);
                        if (TMin2 < 0)
                        {
                            TMin2 = T;
                            fi1TMin = i * 0.174;
                        }
                        if ((TMin2 > 0) && (TMin2 > T))
                        {
                            TMin2 = T;
                            fi1TMin = i * 0.174;
                        }
                    }
                    Time.Add(T + TMin);

                    //psif++;
                } //конец цикла P1

                Console.WriteLine("TMin2 значения: ");
                for (int t = 0; t < Time.Count; t++)
                {
                    Console.WriteLine(Time[t]);
                }

                Console.Write("Time count = ");
                Console.WriteLine(Time.Count);

                TMinFinal = TMin + TMin2;
                

                Console.Write("TMin2 = ");
                Console.WriteLine(TMin2);
                Console.Write("fi1 при TMin2 = ");
                Console.WriteLine(fi1TMin);
                Console.Write("TMin сумма = ");
                Console.WriteLine(TMinFinal);
            } //конец условия (gamma1 > gamma2)

            if (gamma2 > gamma1)
            {
                for (double i = gamma1Rounded; i <= gamma2Rounded; i++) //цикл P1 
                {
                    psif++;
                    deltafi1++;
                    ABS_O2P1_POW2 = Math.Pow(Rf * Math.Cos(psif) - e2 * Math.Cos(fi10), 2) + Math.Pow(Rf * Math.Sin(psif) - e2 * Math.Sin(fi10), 2);
                    O1O2P1 = Math.Acos((Math.Pow(Rf, 2) - Math.Pow(e2, 2) - ABS_O2P1_POW2) / (2 * e2 * Math.Sqrt(ABS_O2P1_POW2)));
                    O3O2P1 = Math.Acos((Math.Pow(R3, 2) - Math.Pow(e3, 2) - ABS_O2P1_POW2) / (2 * e3 * Math.Sqrt(ABS_O2P1_POW2)));
                    O2O3P1 = Math.Acos((ABS_O2P1_POW2 - Math.Pow(R3, 2) - Math.Pow(e3, 2)) / (2 * e3 * R3));

                    fi2f = Math.PI - O1O2P1 - O3O2P1; //конечное значение угла fi2
                    fi3f = Math.PI - O2O3P1;          //конечное значение угла fi3

                    fi2 = fi20 - fi2f; //значение угла fi2 //не число из-за O3O2P1
                    fi3 = fi30 - fi3f; //значение угла fi3


                    deltaX = fi2f + a1 / a2 * fi3f - fi20 - a1 / a2 * fi30; //дельта Хи
                    deltafi = fi2f + fi3f - fi20 - fi30;                    //дельта fi

                    J1Star = J1 + J2 + J3 + m2 * Math.Pow(e2, 2) + 2 * e2 * a * Math.Cos(fi2 + alpha) + m3 * (Math.Pow(e2, 2) + Math.Pow(e3, 2) + 2 * e2 * e3 * Math.Cos(fi2));

                    V3 = 2 * Math.Sqrt(J1Star * Math.Abs(deltafi1) / mu1);

                    if ((a2 * Math.Abs(deltaX)) >= (mu1 * a1 * deltafi / mu2))
                    {
                        //Console.WriteLine("Больше");
                        V1 = 2 * Math.Sqrt((a2 * Math.Abs(deltaX)) / (a1 * mu1 * (a2 - a1))); //оптимальное время разворота средней и малой пробок
                        T = V1 + V3;
                        Time.Add(T);
                        if (TMin2 < 0)
                        {
                            TMin2 = T;
                            fi1TMin = i * 0.174;
                        }
                        if ((TMin2 > 0) && (TMin2 > T))
                        {
                            TMin2 = T;
                            fi1TMin = i * 0.174;
                        }

                    }

                    if ((a2 * Math.Abs(deltaX)) <= (mu1 * a1 * deltafi / mu2))
                    {
                        //Console.WriteLine("Меньше");
                        V2 = 2 * Math.Sqrt((Math.Abs(deltafi) / (mu2 * (a2 - a1)))); //оптимальное время разворота средней и малой пробок
                        T = V2 + V3;
                        Time.Add(T);
                        if (TMin2 < 0)
                        {
                            TMin2 = T;
                            fi1TMin = i * 0.174;
                        }
                        if ((TMin2 > 0) && (TMin2 > T))
                        {
                            TMin2 = T;
                            fi1TMin = i * 0.174;
                        }
                    }
                    //psif++;
                } //конец цикла P1

                TMinFinal = TMin + TMin2;

                Console.Write("TMin2 = ");
                Console.WriteLine(TMin2);
                Console.Write("fi1 при TMin2 = ");
                Console.WriteLine(fi1TMin);
                Console.Write("TMin сумма = ");
                Console.WriteLine(TMinFinal);
            } //конец условия gamma2 > gamma1




        }
    }
}
