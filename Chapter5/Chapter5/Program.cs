using System; //System - пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows; //пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls; //Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Input; //Предоставляет типы для поддержки системы ввода Windows Presentation Foundation (WPF) Сюда входят классы абстрагирования устройств для устройств мыши, клавиатуры и пера, часто используемые классы диспетчера ввода, поддержка для команд и пользовательских команд, а также различные служебные классы.
using System.Windows.Media; //Пространство имен Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео) 

namespace Petzold.StackThirtyButtons { class StackThirtyButtons : Window {
        [STAThread] //Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main() {
            Application app = new Application(); //создание нового приложения
            app.Run(new StackThirtyButtons());//Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        } 
        public StackThirtyButtons() { 
                Title = "Stack Thirty Buttons"; //заголовок
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize; AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));
            StackPanel stackMain = new StackPanel(); //создание объекта класса выравнивающего дочерние объекты в одну линию, в данном случае горизонатльно (строка ниже) 
            stackMain.Orientation = Orientation.Horizontal;
            stackMain.Margin = new Thickness(5); Content = stackMain;
            for (int i = 0; i < 3; i++) { StackPanel stackChild = new StackPanel();//создание трех панелей 
                stackMain.Children.Add(stackChild);//добавление их в стэк
                for (int j = 0; j < 10; j++) { Button btn = new Button();
                    btn.Content = "Button No. " + (10 * i + j + 1); //именует кнопки
                    btn.Margin = new Thickness(5); stackChild.Children.Add(btn); } } }
        void ButtonOnClick(object sender, RoutedEventArgs args) { MessageBox.Show("You clicked the  button labeled " + (args.Source as Button).Content); } } } //что будет если будет нажата кнопка
