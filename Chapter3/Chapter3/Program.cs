using System;//System - пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows; //пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Input; //Предоставляет типы для поддержки системы ввода Windows Presentation Foundation (WPF) Сюда входят классы абстрагирования устройств для устройств мыши, клавиатуры и пера, часто используемые классы диспетчера ввода, поддержка для команд и пользовательских команд, а также различные служебные классы.
using System.Windows.Media; //Пространство имен Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео) 
namespace Petzold.RecordKeystrokes
{
    public class RecordKeystrokes : Window
    {
        [STAThread] //Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main()
        {
            Application app = new Application(); //создание нового приложения 
            app.Run(new RecordKeystrokes());
        } //Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        public RecordKeystrokes() { Title = "Record Keystrokes"; Content = ""; } //заголовок и контент, кот будет в строках
        protected override void OnTextInput(TextCompositionEventArgs args)
        {
            base.OnTextInput(args);
            string str = Content as string;
            if (args.Text == "\b") { if (str.Length > 0) str = str.Substring(0, str.Length - 1); }
            else { str += args.Text; }
            Content = str;
        }
    }
}