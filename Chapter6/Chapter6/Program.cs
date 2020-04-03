using System;//System - пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows;//пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls;//Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Controls.Primitives; //Базовые классы и элементы управления, предназначенные для использования в качестве части других, более сложных элементов управления.
using System.Windows.Input; //Предоставляет типы для поддержки системы ввода Windows Presentation Foundation (WPF) Сюда входят классы абстрагирования устройств для устройств мыши, клавиатуры и пера, часто используемые классы диспетчера ввода, поддержка для команд и пользовательских команд, а также различные служебные классы.
using System.Windows.Media; //Пространство имен Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео) 
namespace Petzold.MeetTheDockers { public class MeetTheDockers : Window {
        [STAThread] //Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main() {
            Application app = new Application(); //создание нового приложения
            app.Run(new MeetTheDockers());
        } //Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        public MeetTheDockers() { 
                Title = "Meet the Dockers";//заголовок 
            DockPanel dock = new DockPanel(); Content = dock;             // сооздание меню
            Menu menu = new Menu(); 
            MenuItem item = new MenuItem();
            item.Header = "Menu"; //название 
            menu.Items.Add(item);                
            DockPanel.SetDock(menu, Dock.Top);// dock меню  вверху панели 
            dock.Children.Add(menu);                 
            ToolBar tool = new ToolBar();// создание панели инструментов.  
            tool.Header = "Toolbar";            
            DockPanel.SetDock(tool, Dock.Top);// dock панель инструментов вверху панели .
            dock.Children.Add(tool);                   
            StatusBar status = new StatusBar();// создание панели статуса . 
            StatusBarItem statitem = new  StatusBarItem(); //создание компонента панели статуса 
            statitem.Content = "Status";
            status.Items.Add(statitem);              
            DockPanel.SetDock(status, Dock.Bottom);// dock панель статуса внизу панели .  
            dock.Children.Add(status);                  
            ListBox lstbox = new ListBox();// создание списка .  
            lstbox.Items.Add("List Box Item");               
            DockPanel.SetDock(lstbox, Dock.Left);// список слева панели .
            dock.Children.Add(lstbox);                   
            TextBox txtbox = new TextBox();// создание текстовой панели  .
            txtbox.AcceptsReturn = true;                
            dock.Children.Add(txtbox);// добавить текст в панельи дать ему фокус ввода . 
            txtbox.Focus();         }     } } 
