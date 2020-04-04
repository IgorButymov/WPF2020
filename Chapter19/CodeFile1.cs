using System;// пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.IO; //Пространство имен содержит типы, позволяющие осуществлять чтение и запись в файлы и потоки данных, а также типы для базовой поддержки файлов и папок.
using System.Windows;//пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls;//Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Markup;//Предоставляет типы, поддерживающие XAML.
using System.Xml;//Пространство имен предоставляет основанную на стандартах поддержку обработки XML.
namespace Petzold.LoadEmbeddedXaml
{
    public class LoadEmbeddedXaml : Window {
        [STAThread] ////Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main()
        {
            Application app = new Application(); //создание нового приложения 
            app.Run(new LoadEmbeddedXaml()); //Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        }
        public LoadEmbeddedXaml()
        {
            Title = "Load Embedded Xaml"; //заголовок
            string strXaml = "<Button xmlns='http://schemas .microsoft.com/" + "winfx/2006 /xaml/presentation'" + "Foreground='LightSeaGreen' FontSize='24pt'>" + "Click me!" + "</Button>";
            StringReader strreader = new StringReader(strXaml); //инициализация нового экземпляра класса StringReader, осуществляющий чтение из строки strXaml 
            XmlTextReader xmlreader = new XmlTextReader(strreader);//инифиализация нового экземпляра класса XmlTextReader указанным чтением TextReader
            object obj = XamlReader.Load(xmlreader);//считывание указанных данных XAML в классе XamlReader и возвращение корневого объекта соответсвующего дерева объектов 
            //создание граф объъекта с исп. средства чтения XAML
            Content = obj;//установка содержимого окна 
        }
    }
}
