using System; //пр-во имен, кот. содержит фундаментальные и базовае классы, пишем строчку, чтобы не писать System перед функциями
using System.Windows; //пр-во имен, содержит классы для создания приложений Windows
using System.Windows.Controls;//Предоставляет классы для создания элементов, называемых элементами управления, которые позволяют пользователю взаимодействовать с приложением.
using System.Windows.Input;//Предоставляет типы для поддержки системы ввода Windows Presentation Foundation (WPF) Сюда входят классы абстрагирования устройств для устройств мыши, клавиатуры и пера, часто используемые классы диспетчера ввода, поддержка для команд и пользовательских команд, а также различные служебные классы.
using System.Windows.Media;//Пространство имен Предоставляет типы, обеспечивающие интеграцию самых разнообразных мультимедийных данных (включая изображения, текст, аудио и видео) 
namespace Petzold.RenderTheBetterEllipse
{
    public class BetterEllipse : FrameworkElement
    {         // св-ва зависимостей.  
        public static readonly DependencyProperty  FillProperty;
        public static readonly DependencyProperty  StrokeProperty;         // публичные интерфейсы к св-вам зависимостей. 
        public Brush Fill //класс, определяющий объекты, исп. для рисования графических объектов
        {
            set
            {
                SetValue(FillProperty, value);
            }
            get
            {
                return (Brush)GetValue (FillProperty);
            }
        }
        public Pen Stroke //класс, описывающий способ рисования контура фигуры
        {
            set
            {
                SetValue(StrokeProperty, value);
            }
            get
            {
                return (Pen)GetValue (StrokeProperty);
            }
        }                  
        static BetterEllipse() // статичный конструктор.
        {
            FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(BetterEllipse), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
            StrokeProperty =DependencyProperty.Register ("Stroke", typeof(Pen), typeof(BetterEllipse), new  FrameworkPropertyMetadata(null,  FrameworkPropertyMetadataOptions.AffectsMeasure));
        }             
        protected override Size MeasureOverride (Size sizeAvailable)
        {
            Size sizeDesired = base .MeasureOverride(sizeAvailable);
            if (Stroke != null)
                sizeDesired = new Size(Stroke .Thickness, Stroke.Thickness);
            return sizeDesired;
        }               
        protected override void OnRender (DrawingContext dc)
        {
            Size size = RenderSize;             // регулирвока размера рендеринга по ширине пера .    
            if (Stroke != null)
            {
                size.Width = Math.Max(0, size .Width - Stroke.Thickness);
                size.Height = Math.Max(0, size .Height - Stroke.Thickness);
            }                      
            dc.DrawEllipse(Fill, Stroke,   new Point(RenderSize.Width / 2,  RenderSize.Height / 2),size.Width / 2, size.Height / 2);// рисование эллипса .
        }
    }
} 