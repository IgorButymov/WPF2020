using System; //пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows;//пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls;//Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Input;//Предоставляет типы для поддержки системы ввода Windows Presentation Foundation (WPF) Сюда входят классы абстрагирования устройств для устройств мыши, клавиатуры и пера, часто используемые классы диспетчера ввода, поддержка для команд и пользовательских команд, а также различные служебные классы.
using System.Windows.Media;//Пространство имен Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео) 
namespace Petzold.CheckTheWindowStyle
{
    public class CheckTheWindowStyle : Window {
        MenuItem itemChecked;
        [STAThread] //Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main()
        {
            Application app = new Application();//создание нового приложения 
            app.Run(new CheckTheWindowStyle()); //Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        }
        public CheckTheWindowStyle()
        {
            Title = "Check the Window Style";           //заголовок окна    
            DockPanel dock = new DockPanel();// создание док панели. 
            Content = dock;         //содержимое - док панель           
            Menu menu = new Menu();// создание меню, расположенного сверху.
            dock.Children.Add(menu);//добавление меню 
            DockPanel.SetDock(menu, Dock.Top);              
            TextBlock text = new TextBlock();// создание текстового блока, заполняя остальное. 
            text.Text = Title;
            text.FontSize = 32;
            text.TextAlignment = TextAlignment.Center;
            dock.Children.Add(text);             
            MenuItem itemStyle = new MenuItem();// создание пункта меню, чтообы менять стиль окна. 
            itemStyle.Header = "_Style"; //название этого пункта меню
            menu.Items.Add(itemStyle);//доавление в меню объекта Стиль
            itemStyle.Items.Add(CreateMenuItem("_No border or  caption", WindowStyle.None)); //добавление меню-объектов для смены стиля окна
            itemStyle.Items.Add( CreateMenuItem("_Single-border  window", WindowStyle .SingleBorderWindow));
            itemStyle.Items.Add(CreateMenuItem("3_D-border window", WindowStyle .ThreeDBorderWindow));
            itemStyle.Items.Add(  CreateMenuItem("_Tool window", WindowStyle .ToolWindow));
        }
        MenuItem CreateMenuItem(string str,  WindowStyle style) //создание меню объекта 
        {
            MenuItem item = new MenuItem();
            item.Header = str;
            item.Tag = style;
            item.IsChecked = (style == WindowStyle);
            item.Click += StyleOnClick;
            if (item.IsChecked)
                itemChecked = item;
            return item;
        }
        void StyleOnClick(object sender,  RoutedEventArgs args) //установка стиля 
        {
            itemChecked.IsChecked = false;
            itemChecked = args.Source as MenuItem;
            itemChecked.IsChecked = true;
            WindowStyle = (WindowStyle)itemChecked .Tag;
        }
    }
} 