using System; //пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows;//пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls;//Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Input; //Предоставляет типы для поддержки системы ввода  WPF.
using System.Windows.Media; //Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео) в приложениях  WPF.
using System.Windows.Media.Animation; //Предоставляет типы данных, которые поддерживают функциональность анимации свойств, включая шкалы времени, раскадровки и ключевые кадры.
namespace Petzold.EnlargeButtonWithAnimation
{
    public class EnlargeButtonWithAnimation : Window
    {
        const double initFontSize = 12;
        const double maxFontSize = 48;
        Button btn;
        [STAThread] //Это означает, что все потоки в этой программе выполняются в рамках одного процесса, а управление программой осуществляется одним главным потоком. Этo нужно, чтоб не было проблем, если приложение подключает компоненты
        public static void Main()
        {
            Application app = new Application(); //создание нового приложения 
            app.Run(new EnlargeButtonWithAnimation());  //Запускает стандартный цикл обработки сообщений приложения в текущем потоке
        }
        public EnlargeButtonWithAnimation()
        {
            Title = "Enlarge Button with Animation"; //заголовок
            btn = new Button(); //кнопка
            btn.Content = "Expanding Button"; //содержимое кнопки
            btn.FontSize = initFontSize; //размер шрифта
            btn.HorizontalAlignment = HorizontalAlignment.Center; //выравнивание по горизонтали
            btn.VerticalAlignment = VerticalAlignment.Center; //выравнивание по вертикали
            btn.Click += ButtonOnClick; //назначение клика на кнопку
            Content = btn; //содержимое окна - кнопка
        }
        void ButtonOnClick(object sender, RoutedEventArgs args) //функция, отвечающая за нажатие на кнопку
        {
            DoubleAnimation anima = new DoubleAnimation();//объект, выполняющий анимацию значения свойств double между двумя целевыми значениями 
            anima.Duration = new Duration(TimeSpan.FromSeconds(2)); //продолжительость воспроизведения анимации
            anima.From = initFontSize;//начальное знаечние анимации
            anima.To = maxFontSize; //конечное значение анимации
            anima.FillBehavior = FillBehavior.Stop;//опр. поведение объекта, по достижени. конца активного периода
            btn.BeginAnimation(Button.FontSizeProperty, anima);//запуск анимации для заданного объекта (кнопки)
        }
    }
}