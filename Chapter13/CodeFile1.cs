using System; //пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Reflection;//Пространство имен  содержит типы, предназначенные для извлечения сведений о сборках, модулях, членах, параметрах и других объектах в управляемом коде путем обработки их метаданных.
using System.Windows; //пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls;//Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Input; //Предоставляет типы для поддержки системы ввода Windows Presentation Foundation (WPF) Сюда входят классы абстрагирования устройств для устройств мыши, клавиатуры и пера, часто используемые классы диспетчера ввода, поддержка для команд и пользовательских команд, а также различные служебные классы.
using System.Windows.Media;//Пространство имен Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео) 
namespace Petzold.ListColorNames
{
    class ListColorNames : Window {
        [STAThread] //Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main()
        {
            Application app = new Application(); //создание нового приложения 
            app.Run(new ListColorNames()); //Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        }
        public ListColorNames()
        {
            Title = "List Color Names";      //заголовок окна        
            ListBox lstbox = new ListBox();// создание ListBox (списка) как содержимого окна.
            lstbox.Width = 150; //параметры
            lstbox.Height = 150;
            lstbox.SelectionChanged +=  ListBoxOnSelectionChanged;
            Content = lstbox;              
            PropertyInfo[] props = typeof(Colors) .GetProperties(); // заполнение списка названиями цветов.  
            foreach (PropertyInfo prop in props)   lstbox.Items.Add(prop.Name);
        }
        void ListBoxOnSelectionChanged(object sender,    SelectionChangedEventArgs args)//функция, отвечающая за смену цвета фона при выборе его из списка 
        {
            ListBox lstbox = sender as ListBox;
            string str = lstbox.SelectedItem as  string;
            if (str != null)
            {
                Color clr =  (Color)typeof(Colors) .GetProperty(str).GetValue(null, null);
                Background = new SolidColorBrush(clr);//смена цвета фона 
            }
        }
    }
} 