using System;//System - пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows; //пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls;//Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Input; //Предоставляет типы для поддержки системы ввода Windows Presentation Foundation (WPF) Сюда входят классы абстрагирования устройств для устройств мыши, клавиатуры и пера, часто используемые классы диспетчера ввода, поддержка для команд и пользовательских команд, а также различные служебные классы.
using System.Windows.Media; //Пространство имен Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео)
using System.Windows.Shapes; //Предоставляет доступ к библиотеке фигур, которые могут использоваться в XAML или в коде.
namespace Petzold.PlayJeuDeTacquin { public class Tile : Canvas { const int SIZE = 64;    // 2/3 дюйма      
        const int BORD = 6;     // 1/16 дюйма       
        TextBlock txtblk;
        public Tile()
        {
            Width = SIZE;
            Height = SIZE;                 
            Polygon poly = new Polygon(); //Рисует многоугольник, представляющий собой последовательность соединенных линий, образующих замкнутый контур, верхняя левая граница
            poly.Points = new PointCollection(new  Point[]              
            {  new Point(0, 0), new Point (SIZE, 0), new Point(SIZE-BORD, BORD),  new Point(BORD, BORD), new Point(BORD, SIZE-BORD),  new Point(0, SIZE)                 });
            poly.Fill = SystemColors .ControlLightLightBrush;
            Children.Add(poly);                     
            poly = new Polygon(); //нижняя правая граница
            poly.Points = new PointCollection(new  Point[]              
            {    new Point(SIZE, SIZE), new  Point(SIZE, 0),   new Point(SIZE-BORD, BORD),  new Point(SIZE-BORD, SIZE-BORD),  new Point(BORD, SIZE-BORD),  new Point(0, SIZE)    });
            poly.Fill = SystemColors .ControlDarkBrush;
            Children.Add(poly);                      
            Border bord = new Border(); //Отображает границу, фон либо и то, и другое вокруг другого элемента.
            bord.Width = SIZE - 2 * BORD;
            bord.Height = SIZE - 2 * BORD;
            bord.Background = SystemColors .ControlBrush;
            Children.Add(bord);
            SetLeft(bord, BORD);
            SetTop(bord, BORD);                      
            txtblk = new TextBlock(); //отображение текста 
            txtblk.FontSize = 32;
            txtblk.Foreground = SystemColors .ControlTextBrush;
            txtblk.HorizontalAlignment =  HorizontalAlignment.Center; //Указывает, где на горизонтальной оси должен отображаться элемент в отношении выделенного раздела макета родительского элемента(в центре)
            txtblk.VerticalAlignment =  VerticalAlignment.Center;
            bord.Child = txtblk;          }              
        public string Text          { //установка текста 
            set { txtblk.Text = value; }
            get { return txtblk.Text; }          }     } } 