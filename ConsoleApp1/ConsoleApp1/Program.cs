using System; //System - пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows; //пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Input; //Предоставляет типы для поддержки системы ввода Windows Presentation Foundation (WPF) Сюда входят классы абстрагирования устройств для устройств мыши, клавиатуры и пера, часто используемые классы диспетчера ввода, поддержка для команд и пользовательских команд, а также различные служебные классы.
namespace Petzold.HandleAnEvent 
{
    class HandleAnEvent
    {
        [STAThread] //Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main()
        {
            Application app = new Application(); //создание нового приложения 
            Window win = new Window(); //создание нового окна
            win.Title = "Handle An Event"; //надпись, которая распологается сверху в рамке окна
            win.MouseDown += WindowOnMouseDown; //перехват сообщения (нажатия кнопки мыши)
            app.Run(win); //Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        }
        static void WindowOnMouseDown(object sender, MouseButtonEventArgs args) //функция, которая описывает поведение окна при нажатии пользователем кнопки
        {
            Window win = sender as Window; //открывает еще одно окно
            string strMessage = string.Format("Window clicked with  {0} button at point ({1})", args.ChangedButton, args.GetPosition(win)); //выводит на экран окно, которое показывает какой кнопкой мыши пользователь кликнул на главное окно и координаты, куда кликнул пользователь
            MessageBox.Show(strMessage, win.Title); //показ сообщения и заголовка
        }
    }
}
