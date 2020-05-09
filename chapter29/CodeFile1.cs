using System; //пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows;//пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls;//Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Input; //Предоставляет типы для поддержки системы ввода  WPF.
using System.Windows.Media; //Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео) в приложениях  WPF.
namespace Petzold.TransformedButtons
{
    public class TransformedButtons : Window
    {
        [STAThread]//Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main()
        {
            Application app = new Application(); //создание нового приложения 
            app.Run(new TransformedButtons()); //Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        }
        public TransformedButtons()
        {
            Title = "Transformed Buttons";  //заголовок                
            Canvas canv = new Canvas(); //канвас
            Content = canv;            //содержимое окна - канвас   
            //объявление и пармаетры кнопок, различных форм; установка их в канвас
            //обычная кнопка
            Button btn = new Button();
            btn.Content = "Untransformed"; 
            canv.Children.Add(btn);
            Canvas.SetLeft(btn, 50);
            Canvas.SetTop(btn, 100);  
            //переведенная кнопка
            btn = new Button();
            btn.Content = "Translated";
            btn.RenderTransform = new  TranslateTransform(-100, 150);
            canv.Children.Add(btn);
            Canvas.SetLeft(btn, 200);
            Canvas.SetTop(btn, 100); 
            //масштабируемая кнопка
            btn = new Button();
            btn.Content = "Scaled";
            btn.RenderTransform = new  ScaleTransform(1.5, 4);
            canv.Children.Add(btn);
            Canvas.SetLeft(btn, 350);
            Canvas.SetTop(btn, 100);
            // косая кнопка.
            btn = new Button();
            btn.Content = "Skewed";
            btn.RenderTransform = new  SkewTransform(0, 20);
            canv.Children.Add(btn);
            Canvas.SetLeft(btn, 500);
            Canvas.SetTop(btn, 100);
            // повернутая кнопка.
            btn = new Button();
            btn.Content = "Rotated";
            btn.RenderTransform = new  RotateTransform(-30);
            canv.Children.Add(btn);
            Canvas.SetLeft(btn, 650);
            Canvas.SetTop(btn, 100);
        }
    }
} 