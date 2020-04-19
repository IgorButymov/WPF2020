using System;//пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows;//пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls;//Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Input; //Предоставляет типы для поддержки системы ввода  WPF.
using System.Windows.Media; //Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео) в приложениях  WPF.
using System.Windows.Shapes; //Предоставляет доступ к библиотеке фигур, которые могут использоваться в XAML или в коде.
namespace Spiral
{
    public class Spiral : Window
    {
        const int revs = 20;
        const int numpts = 1000 * revs;
        Polyline poly; //объект, рисующий последовательность соед. прямых линий
        [STAThread] //Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main()
        {
            Application app = new Application(); //создание нового приложения 
            app.Run(new Spiral()); //Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        }
        public Spiral()
        {
            Title = "Spiral";             //заголовок
            Canvas canv = new Canvas();//  область, в кот. можно явно расположить дочерние элементы.  
            canv.SizeChanged += CanvasOnSizeChanged;
            Content = canv;              //содержимое - область, в кот. можно явно расположить дочерние элементы. 
            poly = new Polyline();// создание объекта, рисующего последовательность соед. прямых линий.
            poly.Stroke = SystemColors .WindowTextBrush; //опр. способ рисования контура
            canv.Children.Add(poly);          //добавление поли в children к канвасу   
            Point[] pts = new Point[numpts];//инициализация массива  координат X, Y
            for (int i = 0; i < numpts; i++) //заполнение массива координат
            {
                double angle = i * 2 * Math.PI /  (numpts / revs);
                double scale = 250 * (1 - (double)  i / numpts);
                pts[i].X = scale * Math.Cos(angle);
                pts[i].Y = scale * Math.Sin(angle);
            }
            poly.Points = new PointCollection(pts);//рисование спирали по координатам
        }
        void CanvasOnSizeChanged(object sender,  SizeChangedEventArgs args)
        {
            Canvas.SetLeft(poly, args.NewSize .Width / 2); //опр. области, где можно расположить дочерние элементы
            Canvas.SetTop(poly, args.NewSize .Height / 2);
        }
    }
} 