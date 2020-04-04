using System;// пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows;//пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls;//Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Media;//Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео)
namespace Petzold.XamlCruncher
{
    public class XamlCruncherSettings : Petzold.NotepadClone.NotepadCloneSettings
    {               
        public Dock Orientation = Dock.Left; // стандартные настройки предпочтения пользователя.
        public int TabSpaces = 4;
        public string StartupDocument = "<Button xmlns=\"http://schemas .microsoft.com/winfx" + "/2006/xaml/presentation\" \r\n" + " xmlns:x=\"http://schemas .microsoft.com/winfx" + "/2006/xaml\">\r\n" +"Hello, XAML!\r\n" +"</Button>\r\n";   
        

        public XamlCruncherSettings() //конструктор для инициализации настроек в NotepadCloneSettings
        {
            string FontFamily = "Lucida Console";
            double FontSize = 10 / 0.75;
        }
    }
} 
