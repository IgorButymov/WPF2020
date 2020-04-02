using System; //System - пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Text; //Пространство содержит классы, которые представляют кодировки ASCII и Юникода; абстрактные базовые классы для преобразования блоков знаков в блоки байтов и обратно; вспомогательный класс, который обрабатывает и форматирует объекты String, не создавая промежуточные экземпляры String.
using System.Windows; //пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls; //Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Input; //Предоставляет типы для поддержки системы ввода Windows Presentation Foundation (WPF) Сюда входят классы абстрагирования устройств для устройств мыши, клавиатуры и пера, часто используемые классы диспетчера ввода, поддержка для команд и пользовательских команд, а также различные служебные классы.
using System.Windows.Media; //Пространство имен Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео)
namespace Petzold.SetSpaceProperty { public class SpaceButton : Button {          
        string txt;
        public string Text //установка текста 
        {  set
            {
                txt = value;
                Content = SpaceOutText(txt);             }
            get
            {
                return txt;             }         }           
        public static readonly DependencyProperty  SpaceProperty; // DependencyProperty - Представляет свойство, которое можно задать с помощью методов, например стили, привязки данных, анимации и наследование. 
        public int Space
        {
            set             {                 SetValue(SpaceProperty, value);
            }
            get
            {                 return (int)GetValue(SpaceProperty);             }         }          
        static SpaceButton()// конструктор статичный.
        {                      
            FrameworkPropertyMetadata metadata =  new FrameworkPropertyMetadata(); // Сообщает или применяет метаданные для свойства зависимостей, добавляя характеристики системы свойств, специфичные для платформы.. 
            metadata.DefaultValue = 1;
            metadata.AffectsMeasure = true;
            metadata.Inherits = true;
            metadata.PropertyChangedCallback +=  OnSpacePropertyChanged;             
            SpaceProperty =  DependencyProperty.Register ("Space", typeof(int), typeof (SpaceButton), metadata, ValidateSpaceValue);
        }     // регистрация DependencyProperty.            
        static bool ValidateSpaceValue(object obj)         {             int i = (int)obj;// метод обратного вызова для проверки значений .  
            return i >= 0;         }         // метод обратного вызова для  изменения свойства.     
        static void OnSpacePropertyChanged (DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {             SpaceButton btn = obj as SpaceButton;
            btn.Content = btn.SpaceOutText(btn.txt);         }           
        string SpaceOutText(string str)  // метод для вставки пробелов в текст. 
        {             if (str == null)                 return null;
            StringBuilder build = new StringBuilder(); //изменяемая строка символов
            foreach (char ch in str)                 build.Append(ch + new string(' ',  Space));
            return build.ToString();         }     } } 