using System; //System - пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Reflection; //пр-во имен, кот. содержит типы , предназначенные для извлечения сведений о сборках, модулях, членах, параметрах и других объектах в управляемом коде путем обработки их метаданных
using System.Windows;//пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Input;//Предоставляет типы для поддержки системы ввода Windows Presentation Foundation (WPF) Сюда входят классы абстрагирования устройств для устройств мыши, клавиатуры и пера, часто используемые классы диспетчера ввода, поддержка для команд и пользовательских команд, а также различные служебные классы.
using System.Windows.Media; //Пространство имен Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео) 
namespace Petzold.FlipThroughTheBrushes {
    public class FlipThroughTheBrushes : Window { int index = 0; PropertyInfo[] props;
        [STAThread] //Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main()
        { Application app = new Application(); //создание нового приложения 
            app.Run(new FlipThroughTheBrushes()); //Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        }
        public FlipThroughTheBrushes() { props = typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static);
            SetTitleAndBackground(); } //заливка области
        protected override void OnKeyDown(KeyEventArgs args) { if (args.Key == Key.Down || args.Key == Key.Up) { index += args.Key == Key.Up ? 1 : props.Length - 1;
                index %= props.Length; SetTitleAndBackground();
            } //Предоставляет данные для события KeyDown или KeyUp.
            base.OnKeyDown(args); }
        void SetTitleAndBackground() { Title = "Flip Through the Brushes - " + props[index].Name; //установка заголовка и фона
            Background = (Brush)props[index].GetValue(null, null); } } }