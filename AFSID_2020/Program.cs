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

            double fi10 = 1;
            double fi1f = 1;

            double Rf = 1; //должно быть равно единице
            double deltapsif = 1;
            double psif = fi1f + deltapsif;

            //углы
            double O2O1P1 = deltapsif;
            double ABS_O2P1 = Math.Sqrt(Math.Pow(Rf, 2) + Math.Pow(e2, 2) - 2 * Rf * e2 * Math.Cos(deltapsif));
            double O1O2P1 = Math.Acos((Math.Pow(Rf, 2) - Math.Pow(e2, 2) - Math.Pow(ABS_O2P1, 2)) / (2 * e2 * ABS_O2P1));
            double O3O2P1 = Math.Acos((Math.Pow(R3, 2) - Math.Pow(e3, 2) - Math.Pow(ABS_O2P1, 2)) / (2 * e3 * ABS_O2P1));
            double O2O3P1 = Math.Acos((Math.Pow(ABS_O2P1, 2) - Math.Pow(R3, 2) - Math.Pow(e3, 2)) / (2 * e3 * R3));
            //нужно изменять
            double fi1 = 30; //угол поворота большой пробки
            double fi2 = 30; //угол поворота средней пробки по отношению к большой пробке
            double fi3 = 30; //угол поворота малой пробки по отношению к средней пробке
            double m2 = 30;  //масса средней пробки с вырезом
            double m3 = 30;  //масса малой пробки
            double a = 30;   //a = |O2 C2|
            double alpha = 30; //угол(О3 О2 С2)

            Console.Write("O2O1P1 = ");
            Console.WriteLine(O2O1P1);
            Console.Write("модуль O2P1 = ");
            Console.WriteLine(ABS_O2P1);
            Console.Write("O1O2P1 = ");
            Console.WriteLine(O1O2P1);
            Console.Write("O3O2P1 = ");
            Console.WriteLine(O3O2P1);
            Console.Write("O2O3P1 = ");
            Console.WriteLine(O2O3P1);

            

            //производняа по времени
            double TimeDerivative(double fi)
            {
                return 0;
            }

            //уравнения Лагранжа второго рода
            /*double u1 = (J1 * TimeDerivative(TimeDerivative(fi1))) + (J2 * (TimeDerivative(TimeDerivative(fi1)) + TimeDerivative(TimeDerivative(fi2)))) + (J3 * (TimeDerivative(TimeDerivative(fi1)) + TimeDerivative(TimeDerivative(fi2)) + TimeDerivative(TimeDerivative(fi3)))) + (m2 * (Math.Pow(e2, 2) * TimeDerivative(TimeDerivative(fi1)) + (e2 * a * Math.Cos(fi2 + alpha) * (2 * TimeDerivative(TimeDerivative(fi1)) + TimeDerivative(TimeDerivative(fi2)))) - (e2 * a * Math.Sin(fi2 + alpha) * (2 * TimeDerivative(fi1) + TimeDerivative(fi2) * TimeDerivative(fi2))))) + (m3 * (Math.Pow(e2, 2) * TimeDerivative(TimeDerivative(fi1)) + Math.Pow(e3, 2) * (TimeDerivative(TimeDerivative(fi1)) + TimeDerivative(TimeDerivative(fi2))) + e2 * e3 * Math.Cos(fi2) * (2 * TimeDerivative(TimeDerivative(fi1)) + TimeDerivative(TimeDerivative(fi2))) - e2 * e3 * Math.Sin(fi2) * (2 * TimeDerivative(fi1) + TimeDerivative(fi2)) * TimeDerivative(fi2)));
            double u2 = J2 * (TimeDerivative(TimeDerivative(fi1)) + TimeDerivative(TimeDerivative(fi2))) + J3 * (TimeDerivative(TimeDerivative(fi1)) + TimeDerivative(TimeDerivative(fi2)) + TimeDerivative(TimeDerivative(fi3))) + m2 * e2 * a * (Math.Cos(fi2 + alpha) * TimeDerivative(TimeDerivative(fi1)) + Math.Sin(fi2 + alpha) * Math.Pow(TimeDerivative(fi1), 2)) + m3 * (Math.Pow(e3, 2) * (TimeDerivative(TimeDerivative(fi1)) + TimeDerivative(TimeDerivative(fi2))) + e2 * e3 * (Math.Cos(fi2) * TimeDerivative(TimeDerivative(fi1)) + Math.Sin(fi2) * Math.Pow(TimeDerivative(fi1), 2)));
            double u3 = J3 * (TimeDerivative(TimeDerivative(fi1)) + TimeDerivative(TimeDerivative(fi2)) + TimeDerivative(TimeDerivative(fi3)));
           
            Console.Write("u1 = ");
            Console.WriteLine(u1);
            Console.Write("u2 = ");
            Console.WriteLine(u2);
            Console.Write("u3 = ");
            Console.WriteLine(u2);*/

            //динамика средней и малой пробок 
            //fi2 ??? 
            //fi3 ??? a3 ??? 
            double a1 = (1) / (J2 + m3 * Math.Pow(e3, 2));
            double a2 = (J2 + J3 + m3 * Math.Pow(e3, 2)) / (J3 * (J2 + m3 * Math.Pow(e3, 2)));

            double XResult;
            double FiResult;

            double X(double v)
            {
                if (v == 0)
                {
                    return 0;
                }
                else
                {
                    XResult = fi2 + (a1 / a2 * fi3);
                    return XResult;
                }
            }

            double Fi(double f)
            {
                if (f == 0)
                {
                    return 0;
                }
                else
                {
                    FiResult = fi2 + fi3;
                    return FiResult;
                }
            }

            double u1;
            double u2;

            
            double fi20 = 1;
            double fi2f = Math.PI - O1O2P1 - O3O2P1;
            double fi30 = 1;
            double fi3f = Math.PI - O2O3P1;
            double t = 20;
            double deltafi = fi2f + fi3f - fi20 - fi30;

            

            

            double V1 = 1; //оптимальное время разворота в первом случае
            double V2 = 1; //оптимальное время разворота во втором случае
            double V1_X = 1;
            double V2_X = 1;
            double V = 1;
            double deltaX = fi1f + (a1 * fi2f / a2) - fi10 - (a1 * fi20 / a2);
            

            if ((a2 * Math.Abs(deltaX)) >= (mu1 * a1 * deltafi / mu2))
            {
                Console.WriteLine("Больше");
                V1 = 2 * Math.Sqrt((a2 * Math.Abs(X(V1_X) - X(0))) / (a1 * mu1 * (a2 - a1)));
                u1 = mu1 * Math.Sign(X(V1_X) - X(0)) * Math.Sign(V1 / 2 - t);
                u2 = (4 * deltafi) / ((a1 - a2) * Math.Pow(V1, 2)) * (Math.Sign(V1 / 2 - t));
                Console.Write("V1 = ");
                Console.WriteLine(V1);
                Console.Write("u1 = ");
                Console.WriteLine(u1);
                Console.Write("u2 = ");
                Console.WriteLine(u2);
            }
            else if ((a2 * Math.Abs(deltaX)) <= (mu1 * a1 * deltafi / mu2))
            {
                Console.WriteLine("Меньше");
                V2 = 2 * Math.Sqrt((Math.Abs(Fi(V) - Fi(0)) / (mu2 * (a2 - a1))));
            }
            

            double J1Star = J1 + J2 + J3 + m2 * Math.Pow(e2, 2) + 2 * e2 * a * Math.Cos(fi2 + alpha) + m3 * (Math.Pow(e2, 2) + Math.Pow(e3, 2) + 2 * e2 * e3 * Math.Cos(fi2));
            double deltafi1 = 10; //нет знаечния
            double V3 = 2 * Math.Sqrt(J1Star / mu1 * Math.Abs(deltafi1));
            double T; //суммарное время, необходимое на перемещение
            if ((a2 * Math.Abs(deltaX)) >= (mu1 * a1 * deltafi / mu2))
            {
                Console.WriteLine("Больше");
                T = V1 + V3;
                Console.Write("T = ");
                Console.WriteLine(T);
            }
            else if ((a2 * Math.Abs(deltaX)) <= (mu1 * a1 * deltafi / mu2))
            {
                Console.WriteLine("Меньше");
                T = V2 + V3;
                Console.Write("T = ");
                Console.WriteLine(T);
            }
        }
    }
}
