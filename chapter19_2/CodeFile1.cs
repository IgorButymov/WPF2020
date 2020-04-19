using Microsoft.Win32; //Пространство имен, которое предоставляет два типа классов: те, которые обрабатывают события, вызванные операционной системой, и те, которые управляют системным реестром.
using System; //пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.IO; // Пространство имен Пространство имен , которое содержит типы, позволяющие осуществлять чтение и запись в файлы и потоки данных, а также типы для базовой поддержки файлов и папок.
using System.Windows;////пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls;//Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Markup; //Предоставляет типы, поддерживающие XAML
using System.Xml;//предоставляет основанную на стандартах поддержку обработки XML
namespace Petzold.LoadXamlFile
{
    public class LoadXamlFile : Window
    {
        Frame frame;
        [STAThread]//Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main()
        {
            Application app = new Application();//создание нового приложения 
            app.Run(new LoadXamlFile());//Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        }
        public LoadXamlFile()
        {
            Title = "Load XAML File";//заголовок
            DockPanel dock = new DockPanel();//создание док-панели
            Content = dock; //содержимое окна - док-панель
            Button btn = new Button();// создание кнопки для открытия файла
            btn.Content = "Open File...";//содержимое кнопки
            btn.Margin = new Thickness(12);//поля кнопки
            btn.HorizontalAlignment = HorizontalAlignment.Left;//расположение кнопки
            btn.Click += ButtonOnClick;//обработка события нажатия на кнопку
            dock.Children.Add(btn);//добавление кнопки в док-панель
            DockPanel.SetDock(btn, Dock.Top); // создание Frame для размещения загруженного XAML
            frame = new Frame();//элемент управления содержимым с поддержкой навигации.
            dock.Children.Add(frame);//добавление frame в док-панель
        }
        void ButtonOnClick(object sender, RoutedEventArgs args)//функция, отвечающая за нажатие на кнопку
        {
            OpenFileDialog dlg = new OpenFileDialog();//создание диалогового окна, позволяющего пользователю открыть файл 
            dlg.Filter = "XAML Files (*.xaml)|* .xaml|All files (*.*)|*.*"; //задает текущую строку фильтра имен файлов, которая определяет варианты, доступные в поле диалогового окна
            if ((bool)dlg.ShowDialog())
            {
                try
                { // чтение файла XmlTextReader.
                    XmlTextReader xmlreader = new XmlTextReader(dlg.FileName); // конвертирвание XAML в объект.
                    object obj = XamlReader.Load(xmlreader); // если это Window, вызвать Show.
                    if (obj is Window)
                    {
                        Window win = obj as Window;
                        win.Owner = this;
                        win.Show();
                    } // в противном случае установить в качестве содержимого frame
                    else frame.Content = obj;
                }
                catch (Exception exc)
                {

                    MessageBox.Show(exc.Message, Title);//показ месседж-бокса
                }
            }
        }
    }
}
