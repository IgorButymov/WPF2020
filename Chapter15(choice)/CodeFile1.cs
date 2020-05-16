using System; //пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows;//пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls; //Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Input;//Предоставляет типы для поддержки системы ввода Windows Presentation Foundation (WPF) Сюда входят классы абстрагирования устройств для устройств мыши, клавиатуры и пера, часто используемые классы диспетчера ввода, поддержка для команд и пользовательских команд, а также различные служебные классы.
using System.Windows.Media; //Пространство имен Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео) 
namespace Petzold.MoveTheToolbar
{
    public class MoveTheToolbar : Window
    {
        [STAThread]  //Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main()
        {
            Application app = new Application(); //создание нового приложения
            app.Run(new MoveTheToolbar()); //Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        }
        public MoveTheToolbar()
        {
            Title = "Move the Toolbar";  //заголовок           
            DockPanel dock = new DockPanel(); //создание DockPanel
            Content = dock;         //содержимое окна - DockPanel
            // создание ToolBarTray сверху и слева окна  
            ToolBarTray trayTop = new ToolBarTray();
            dock.Children.Add(trayTop);
            DockPanel.SetDock(trayTop, Dock.Top);
            ToolBarTray trayLeft = new ToolBarTray();
            trayLeft.Orientation = Orientation .Vertical;
            dock.Children.Add(trayLeft);
            DockPanel.SetDock(trayLeft, Dock.Left);            
            TextBox txtbox = new TextBox(); //создание TextBox
            dock.Children.Add(txtbox);            
            // создание шести Toolbars...     
            for (int i = 0; i < 6; i++)
            {
                ToolBar toolbar = new ToolBar();
                toolbar.Header = "Toolbar " + (i + 1);
                if (i < 3)
                    trayTop.ToolBars.Add(toolbar);
                else
                    trayLeft.ToolBars.Add(toolbar);                
                // ... с шестью кнопками
                for (int j = 0; j < 6; j++)
                {
                    Button btn = new Button();
                    btn.FontSize = 16;
                    btn.Content = (char)('A' + j);
                    toolbar.Items.Add(btn);
                }
            }
        }
    }
} 