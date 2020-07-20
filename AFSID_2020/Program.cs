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
            const double R1 = 2;     //радиус большой пробки, м
            const double R2 = 1.6;   //радиус средней пробки, м
            const double R3 = 0.8;   //радиус малой пробки, м
            const double e2 = 0.7;   //расстояние e2, м; e2 = |O1 O2|
            const double e3 = 0.5;   //рaсстояние e3, м; e3 = |O2 O3|
            const double J1 = 60260; //момент инерции большой пробки, кг*м^2
            const double J2 = 4500;  //момент инерции средней пробки, кг*м^2
            const double J3 = 300;   //момент инерции малой пробки, кг*м^2
            const double mu1 = 1200; //момент управления большой пробки, Н*м
            const double mu2 = 800;  //момент управления средней пробки, Н*м
            const double mu3 = 500;  //момент управления малой пробки, Н*м



            int fi10 = 1;  //начальное значение угла fi1 (большая пробка)
            int fi20 = 25; //начальное значение угла fi2 (средняя пробка)
            int fi30 = 25; //начальное значение угла fi3 (малая пробка)
            double Rf = 1; //Rf < |O1O2| + R2 = e2 + R2 = 0.7 + 1.6 = 2.3
            double fi1f = 1; //конечное значение угла fi1
            double deltapsif = 1; //дельта угла psif
            double psif = fi10 + deltapsif; //угол psif


            double m1 = 142000;      //масса большой пробки, кг
            double m2 = 81000;       //масса средней пробки с вырезом, кг
            double m3 = 40000;       //масса малой пробки, кг
            double a = 1 / (3 * R2); //a = |O2 C2|
            double alpha = 0;        //угол(О3 О2 С2)

            double u1;
            double u2;
            double u3;

            double t = 0; 

            double V1 = 1; //оптимальное время разворота в первом случае
            double V2 = 1; //оптимальное время разворота во втором случае

            double V = 1;
            double fi2f = 0;
            double fi3f = 0;
            double TMin = 0; //минимальное суммарное время, необходимое на перемещение

            //задаем значения сами 
            double fi1 = 0; //угол поворота большой пробки
            double fi2 = 0; //угол поворота средней пробки по отношению к большой пробке
            double fi3 = 0; //угол поворота малой пробки по отношению к средней пробке


            double a1 = (1) / (J2 + m3 * Math.Pow(e3, 2));
            double a2 = (J2 + J3 + m3 * Math.Pow(e3, 2)) / (J3 * (J2 + m3 * Math.Pow(e3, 2)));

            double deltaX = 0;
            double deltafi = 0;
            for (double i = fi2; i < 360 - fi2; i++) //цикл по fi2
            {

                //углы
                //double O2O1P1 = deltapsif;
                //double ABS_O2P1 = Math.Sqrt(Math.Pow(Rf, 2) + Math.Pow(e2, 2) - 2 * Rf * e2 * Math.Cos(deltapsif));
                double ABS_O2P1_POW2 = Math.Pow(Rf * Math.Cos(psif) - e2 * Math.Cos(fi10), 2) + Math.Pow(Rf * Math.Sin(psif) - e2 * Math.Sin(fi10), 2);
                double O1O2P1 = Math.Acos((Math.Pow(Rf, 2) - Math.Pow(e2, 2) - ABS_O2P1_POW2) / (2 * e2 * Math.Sqrt(ABS_O2P1_POW2)));
                double O3O2P1 = Math.Acos((Math.Pow(R3, 2) - Math.Pow(e3, 2) - ABS_O2P1_POW2) / (2 * e3 * Math.Sqrt(ABS_O2P1_POW2)));
                double O2O3P1 = Math.Acos((ABS_O2P1_POW2 - Math.Pow(R3, 2) - Math.Pow(e3, 2)) / (2 * e3 * R3));




                fi2f = Math.PI - O1O2P1 - O3O2P1; //конечное значение угла fi2
                fi3f = Math.PI - O2O3P1; //конечное значение угла fi3

                
                

                deltafi = fi2f + fi3f - fi20 - fi30; //дельта fi
                deltaX = fi2f + a1 / a2 * fi3f - fi20 - a1 / a2 * fi30; //дельта Хи


                if ((a2 * Math.Abs(deltaX)) >= (mu1 * a1 * deltafi / mu2))
                {
                    Console.WriteLine("Больше");
                    V1 = 2 * Math.Sqrt((a2 * Math.Abs(deltaX)) / (a1 * mu1 * (a2 - a1)));
                    u2 = mu1 * Math.Sign(deltaX) * Math.Sign(V1 / 2 - t);
                    u3 = (4 * deltafi) / ((a1 - a2) * Math.Pow(V1, 2)) * (Math.Sign(V1 / 2 - t));

                }
                else if ((a2 * Math.Abs(deltaX)) <= (mu1 * a1 * deltafi / mu2))
                {
                    Console.WriteLine("Меньше");
                    V2 = 2 * Math.Sqrt((Math.Abs(deltafi) / (mu2 * (a2 - a1))));
                }
            } //конец цикла по fi2







            //ВТОРАЯ ЧАСТЬ

            double J1Star = J1 + J2 + J3 + m2 * Math.Pow(e2, 2) + 2 * e2 * a * Math.Cos(fi2 + alpha) + m3 * (Math.Pow(e2, 2) + Math.Pow(e3, 2) + 2 * e2 * e3 * Math.Cos(fi2));
            double deltafi1 = 10; //нет знаечния //угол(P1 O1 Qf)
            double V3 = 2 * Math.Sqrt(J1Star / mu1 * Math.Abs(deltafi1));
            double T = 0; //суммарное время, необходимое на перемещение
                    
            if ((a2 * Math.Abs(deltaX)) >= (mu1 * a1 * deltafi / mu2))
            {          
                 Console.WriteLine("Больше");
                 T = V1 + V3;
                
                /*Console.Write("T = ");
                Console.WriteLine(T);*/
            }
            else if ((a2 * Math.Abs(deltaX)) <= (mu1 * a1 * deltafi / mu2))
            {
                 Console.WriteLine("Меньше");
                 T = V2 + V3;
                 /*Console.Write("T = ");
                 Console.WriteLine(T);*/
            }
            for (int i = 0; i < 360; i++)
            {
                //координаты точек M и N
                double x = Rf * Math.Cos(i); //i = гамма i-тое
                double y = Rf * Math.Sin(i);
                //уравнение окружности радиуса R2 с центром в точке O2
                double R22 = Math.Pow(x - e2 * Math.Cos(fi1f), 2) + Math.Pow(y - e2 * Math.Sin(fi1f), 2);
                
                double gamma1 = fi1f + Math.Acos((Math.Pow(Rf, 2) + Math.Pow(e2, 2) - R22) / (2 * Math.Pow(e2, 2) * Rf));
                double gamma2 = fi1f - Math.Acos((Math.Pow(Rf, 2) + Math.Pow(e2, 2) - R22) / (2 * Math.Pow(e2, 2) * Rf));
            }
        }
    }
}
