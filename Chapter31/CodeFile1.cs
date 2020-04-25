using System;//пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows;//пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls;//Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Controls.Primitives;//Базовые классы и элементы управления, предназначенные для использования в качестве части других, более сложных элементов управления.
using System.Windows.Input;//Предоставляет типы для поддержки системы ввода WPF
using System.Windows.Media;//Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео)
using System.Windows.Media.Imaging;//Предоставляет типы, используемые для кодирования и декодирования растровых изображений.
namespace Petzold.DrawButtonsOnBitmap
{
    public class DrawButtonsOnBitmap : Window
    {
        [STAThread]//Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main()
        {
            Application app = new Application(); //создание нового приложения
            app.Run(new DrawButtonsOnBitmap());//Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        }
        public DrawButtonsOnBitmap()
        {
            Title = "Draw Buttons on Bitmap";            //заголовок
            UniformGrid unigrid = new UniformGrid(); // создание униформ-грида для хостинга кнопок   
            unigrid.Columns = 4;             //колонки униформ-грида
            for (int i = 0; i < 32; i++)// создание 32 объектов, каждый из кот. может содержать один объект любого типа (ToggleButton)
            {
                ToggleButton btn = new ToggleButton();//создание ToggleButton
                btn.Width = 96;//параметры
                btn.Height = 24;
                btn.IsChecked = (i < 4 | i > 27) ^  (i % 4 == 0 | i % 4 == 3);//устанавивает находится ли ToggleButton во включ. состоянии
                unigrid.Children.Add(btn);//добавление кнопок в грид
            }                      
            unigrid.Measure(new Size(Double .PositiveInfinity,Double .PositiveInfinity));//рекурсивное обновление структуры грида
            Size szGrid = unigrid.DesiredSize;     // размеры униформ-грида.          
            unigrid.Arrange(new Rect(new Point(0,  0), szGrid));             // организация грида.     
            RenderTargetBitmap renderbitmap =new RenderTargetBitmap((int)Math .Ceiling(szGrid.Width),(int)Math .Ceiling(szGrid.Height),96, 96,  PixelFormats.Default);// создание RenderTargetBitmap (преобразует объект Visual в растровое изображение)                     
            renderbitmap.Render(unigrid);            // преобразование грида в растровое изображение.  
            Image img = new Image();// создание нового объекта Image.
            img.Source = renderbitmap;         //установка Source объекта Image в растрвое изображение    
            Content = img;// содержимое окна - объект Image.  
        }
    }
} 