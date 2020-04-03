using System; //System - пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows; //пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls; //Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Input; //Предоставляет типы для поддержки системы ввода Windows Presentation Foundation (WPF) Сюда входят классы абстрагирования устройств для устройств мыши, клавиатуры и пера, часто используемые классы диспетчера ввода, поддержка для команд и пользовательских команд, а также различные служебные классы.
using System.Windows.Media;//Пространство имен Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео)
using System.Windows.Shapes;//Предоставляет доступ к библиотеке фигур, которые могут использоваться в XAML или в коде
namespace Petzold.DrawCircles
{
    public class DrawCircles : Window { Canvas canv;                 
        bool isDrawing;// связанные с рисованием поля. 
        Ellipse elips;
        Point ptCenter;            
        bool isDragging;// связанные с перетаскиванием поля . 
        FrameworkElement elDragging;
        Point ptMouseStart, ptElementStart;
        [STAThread] //Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main()
        {
            Application app = new Application(); //создание нового приложения 
            app.Run(new DrawCircles()); //Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        }
        public DrawCircles()
        {
            Title = "Draw Circles"; //заголовок 
            Content = canv = new Canvas(); //контент - новый экземпляр класса Canvas (холст)
        }
        protected override void  OnMouseLeftButtonDown(MouseButtonEventArgs args)
        {
            base.OnMouseLeftButtonDown(args);
            if (isDragging)                 return;             
            ptCenter = args.GetPosition(canv);
            elips = new Ellipse();// создание нового объекта эллипса  
            elips.Stroke = SystemColors .WindowTextBrush;
            elips.StrokeThickness = 1; //параметры эллипса 
            elips.Width = 0;
            elips.Height = 0;
            canv.Children.Add(elips); //добавление эллипс на холст.
            Canvas.SetLeft(elips, ptCenter.X);
            Canvas.SetTop(elips, ptCenter.Y);             // Capture the mouse and prepare for  future events. 
            CaptureMouse();
            isDrawing = true;
        }
        protected override void  OnMouseRightButtonDown(MouseButtonEventArgs args) //функция, позволяющая перетаскивать эллипс по окну
        {
            base.OnMouseRightButtonDown(args);
            if (isDrawing)                 return;             
            ptMouseStart = args.GetPosition(canv);
            elDragging = canv.InputHitTest (ptMouseStart) as FrameworkElement;
            if (elDragging != null)
            {
                ptElementStart = new Point(Canvas .GetLeft(elDragging),     Canvas .GetTop(elDragging));
                isDragging = true;
            }
        }
        protected override void OnMouseDown (MouseButtonEventArgs args)//функция, которая определяет поведение при нажатии на кнопку мыши
        {
            base.OnMouseDown(args);
            if (args.ChangedButton == MouseButton .Middle)
            {
                Shape shape = canv.InputHitTest (args.GetPosition(canv)) as Shape;
                if (shape != null)
                    shape.Fill = (shape.Fill ==  Brushes.Red ?    Brushes .Transparent : Brushes.Red);
            }
        }
        protected override void OnMouseMove (MouseEventArgs args) //функция, позволяющая изменять размеры эллипса при рисаовании его
        {
            base.OnMouseMove(args);
            Point ptMouse = args.GetPosition(canv);                
            if (isDrawing)
            {
                double dRadius = Math.Sqrt(Math .Pow(ptCenter.X - ptMouse.X, 2) + Math .Pow(ptCenter.Y - ptMouse.Y, 2));
                Canvas.SetLeft(elips, ptCenter.X -  dRadius);
                Canvas.SetTop(elips, ptCenter.Y -  dRadius);
                elips.Width = 2 * dRadius;
                elips.Height = 2 * dRadius;
            }                    
            else if (isDragging) //перемещение эллипса
            {
                Canvas.SetLeft(elDragging,  ptElementStart.X + ptMouse.X -  ptMouseStart.X);
                Canvas.SetTop(elDragging,   ptElementStart.Y + ptMouse.Y -  ptMouseStart.Y);
            }
        }
        protected override void OnMouseUp (MouseButtonEventArgs args)//функция, поределяющая конец операции рисования
        {
            base.OnMouseUp(args);                 
            if (isDrawing && args.ChangedButton ==  MouseButton.Left)
            {
                elips.Stroke = Brushes.Blue;
                elips.StrokeThickness = Math.Min (24, elips.Width / 2);
                elips.Fill = Brushes.Red;
                isDrawing = false;
                ReleaseMouseCapture();
            }                 
            else if (isDragging && args .ChangedButton == MouseButton.Right)//конец операции захватывания  
            {
                isDragging = false;
            }
        }
        protected override void OnTextInput (TextCompositionEventArgs args) //конец рисования или перемещения нажатием клавиши Escape
        {
            base.OnTextInput(args);                   
            if (args.Text.IndexOf('\x1B') != -1)
            {
                if (isDrawing)
                    ReleaseMouseCapture();
                else if (isDragging)
                {
                    Canvas.SetLeft(elDragging,  ptElementStart.X);
                    Canvas.SetTop(elDragging,  ptElementStart.Y);
                    isDragging = false;
                }
            }
        }
        protected override void OnLostMouseCapture (MouseEventArgs args)//функция, отвечающая за ненормальный конец рисования: удаление дочернего эллипса
        {
            base.OnLostMouseCapture(args);               
            if (isDrawing)
            {
                canv.Children.Remove(elips);
                isDrawing = false;
            }
        }
    }
} 