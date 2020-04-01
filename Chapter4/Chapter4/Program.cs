using System; //System - пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows; //пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls; //Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Documents; //Содержит типы, поддерживающие FixedDocument, FlowDocument и создание документов XPS.
using System.Windows.Input; //Предоставляет типы для поддержки системы ввода Windows Presentation Foundation (WPF) Сюда входят классы абстрагирования устройств для устройств мыши, клавиатуры и пера, часто используемые классы диспетчера ввода, поддержка для команд и пользовательских команд, а также различные служебные классы.
using System.Windows.Media; //Пространство имен Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео) 

namespace Petzold.FormatTheButton {
    public class FormatTheButton : Window { Run runButton;
        [STAThread] //Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main() {
            Application app = new Application(); //создание нового приложения 
            app.Run(new FormatTheButton());
        } //Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        public FormatTheButton() { Title = "Format the Button"; // создание новой кнопки и установка ее как содержимого окна         
            Button btn = new Button(); //создание новой кнопки
            btn.HorizontalAlignment =  HorizontalAlignment.Center;
            btn.VerticalAlignment =  VerticalAlignment.Center;
            btn.MouseEnter += ButtonOnMouseEnter;
            btn.MouseLeave += ButtonOnMouseLeave;
            Content = btn;             //создание блока текста как содержимое кнопки       
            TextBlock txtblk = new TextBlock(); //создание нового текстового блока 
            txtblk.FontSize = 24;
            txtblk.TextAlignment = TextAlignment .Center;
            btn.Content = txtblk;             // добавление форматированного текста к текстовому блоку     
            txtblk.Inlines.Add(new Italic(new Run ("Click")));
            txtblk.Inlines.Add(" the ");
            txtblk.Inlines.Add(runButton = new Run ("button"));
            txtblk.Inlines.Add(new LineBreak());
            txtblk.Inlines.Add("to launch the ");
            txtblk.Inlines.Add(new Bold(new Run ("rocket")));         }
        void ButtonOnMouseEnter(object sender,  MouseEventArgs args)         {
            runButton.Foreground = Brushes.Red;         }
        void ButtonOnMouseLeave(object sender,  MouseEventArgs args)         {
            runButton.Foreground = SystemColors .ControlTextBrush;         }     } } 