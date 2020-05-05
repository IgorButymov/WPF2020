using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text;
using System.Windows.Documents;
using System.Linq;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Timers;
using System.Windows.Threading;
using System.IO;

namespace Butymov.Project2020
{
    public class MainWindow : Window
    {
        DateTime CurrentDate;
        Grid Window4GridForButtons;
        StackPanel Window4Stack;
        StackPanel Window3Stack;
        int AnimationCounter = 0;
        int T = 0;
        double LastPoint = 0;
        DispatcherTimer BirdTimer;
        Point[] Coordinates;
        Rectangle Field;
        Rectangle Window4Score;
        Canvas Window3Canvas;
        Ellipse Dot;
        Ellipse Bird;
        [STAThread]
        public static void Main()
        { 
            Application app = new Application();
            app.Run(new MainWindow());
        }
        
        void Window3CanvasOnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            /*Canvas.SetLeft(Trajectory, args.NewSize.Width  * 6);
            Canvas.SetTop(Trajectory, args.NewSize.Height * 1.5);*/
            Canvas.SetLeft(Field, args.NewSize.Width / 1000);
            Canvas.SetTop(Field, args.NewSize.Height / 1.5);
            Canvas.SetLeft(Bird, (args.NewSize.Width / 6) - 5);
            Canvas.SetTop(Bird, (args.NewSize.Height / 1.5) - 5);
        }
        void Window4CanvasOnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            Canvas.SetLeft(Window4Score, args.NewSize.Width / 3.5);
            Canvas.SetTop(Window4Score, args.NewSize.Height / 3);
            Canvas.SetLeft(Window4Stack, args.NewSize.Width / 7.5);
            Canvas.SetTop(Window4Stack, args.NewSize.Height / 100);
            Canvas.SetLeft(Window4GridForButtons, args.NewSize.Width / 56);
            Canvas.SetTop(Window4GridForButtons, args.NewSize.Height / 3);
        }
        //функция, обрабатывающая шаг таймера
        void BirdTimerTick(object sender, EventArgs e)
        {
            if (T + 1 == Coordinates.Length)
            {
                BirdTimer.Stop();
            }
            Bird.Margin = new Thickness(Coordinates[T].X, Coordinates[T].Y, 0, 0);
            Ellipse Dot = new Ellipse();
            Dot.Width = 2;
            Dot.Height = 2;
            Dot.StrokeThickness = 2;
            Dot.Stroke = Brushes.Black;
            Canvas.SetLeft(Dot, (Window3Canvas.Width / 6) - 5);
            Canvas.SetTop(Dot, (Window3Canvas.Height / 1.5) - 5);
            Dot.Margin = new Thickness(Coordinates[T].X, Coordinates[T].Y, 0, 0);
            Window3Canvas.Children.Add(Dot);
            T++;
        }
        public MainWindow()
        {
            //размеры окна
            this.Height = 500;
            this.Width = 1000;
            Title = "Angry birds))";
            Background = Brushes.Green;
            //объявление и параметры кнопки Play на первом экране
            Button Window1Play = new Button();
            TextBlock Window1TextBlockPlay = new TextBlock();
            Window1TextBlockPlay.FontSize = 24;
            Window1TextBlockPlay.TextAlignment = TextAlignment.Center;
            Window1TextBlockPlay.Inlines.Add(new Run("PLAY"));
            Window1Play.Content = Window1TextBlockPlay;
            Window1Play.Margin = new Thickness(50);
            Window1Play.Height = 50;
            Window1Play.Width = 200;
            //объявление и параметры кнопки Exit на первом экране
            Button Window1Exit = new Button();
            TextBlock Window1TextBlockExit = new TextBlock();
            Window1TextBlockExit.FontSize = 24;
            Window1TextBlockExit.TextAlignment = TextAlignment.Center;
            Window1TextBlockExit.Inlines.Add(new Run("EXIT"));
            Window1Exit.Content = Window1TextBlockExit;
            Window1Exit.Margin = new Thickness(50);
            Window1Exit.Height = 50;
            Window1Exit.Width = 200;
            //объявление StackPanel на первом экране
            StackPanel Window1Stack = new StackPanel();
            Content = Window1Stack;
            //объявление и параметры Grid на первом экране
            Grid Window1Grid = new Grid();
            Window1Grid.Margin = new Thickness(200);
            Window1Grid.RowDefinitions.Add(new RowDefinition());
            Window1Grid.ColumnDefinitions.Add(new ColumnDefinition());
            Window1Grid.ColumnDefinitions.Add(new ColumnDefinition());
            Window1Grid.Children.Add(Window1Play);
            Grid.SetRow(Window1Play, 0);
            Grid.SetColumn(Window1Play, 0);
            Window1Grid.Children.Add(Window1Exit);
            Grid.SetRow(Window1Exit, 0);
            Grid.SetColumn(Window1Exit, 1);
            Window1Stack.Children.Add(Window1Grid);
            //клик по кнопке Exit на первом экране
            Window1Exit.PreviewMouseLeftButtonDown += Window1ExitClicked;
            void Window1ExitClicked(object sender, MouseButtonEventArgs e) 
            {
                Close();
            }
            //клик по кнопке Play на первом экране
            Window1Play.PreviewMouseLeftButtonDown += Window1PlayClicked;
            void Window1PlayClicked(object sender, MouseButtonEventArgs e) 
            {
                Window1Stack.Visibility = Visibility.Collapsed;
                //объявление и параметры кнопки Start на втором экране
                Button Window2Start = new Button();
                TextBlock Window2TextBlockStart = new TextBlock();
                Window2TextBlockStart.FontSize = 24;
                Window2TextBlockStart.TextAlignment = TextAlignment.Center;
                Window2TextBlockStart.Inlines.Add(new Run("START"));
                Window2Start.Content = Window2TextBlockStart;
                Window2Start.Margin = new Thickness(50);
                Window2Start.Height = 50;
                Window2Start.Width = 200;
                //объявление и параметры кнопки Main menu на втором экране
                Button Window2MainMenu = new Button();
                TextBlock Window2TextBlockMainMenu = new TextBlock();
                Window2TextBlockMainMenu.FontSize = 24;
                Window2TextBlockMainMenu.TextAlignment = TextAlignment.Center;
                Window2TextBlockMainMenu.Inlines.Add(new Run("MAIN MENU"));
                Window2MainMenu.Content = Window2TextBlockMainMenu;
                Window2MainMenu.Margin = new Thickness(50);
                Window2MainMenu.Height = 50;
                Window2MainMenu.Width = 200;
                //объявление StackPanel на втором экране
                StackPanel Window2Stack = new StackPanel();
                Content = Window2Stack;
                Grid Window2Grid = new Grid();
                //объявление и параметры Grid на втором экране
                Window2Grid.Margin = new Thickness(200);
                Window2Grid.RowDefinitions.Add(new RowDefinition());
                Window2Grid.RowDefinitions.Add(new RowDefinition());
                Window2Grid.ColumnDefinitions.Add(new ColumnDefinition());
                Window2Grid.ColumnDefinitions.Add(new ColumnDefinition());
                Window2Grid.Children.Add(Window2Start);
                Grid.SetRow(Window2Start, 0);
                Grid.SetColumn(Window2Start, 0);
                Window2Grid.Children.Add(Window2MainMenu);
                Grid.SetRow(Window2MainMenu, 0);
                Grid.SetColumn(Window2MainMenu, 1);
                //объявление и парметры TextBox для начальной скорости на втором экране
                TextBox Window2InitialVelocity = new TextBox();
                string Window2InitialVelocityText = "INITIAL VELOCITY";
                Window2InitialVelocity.Text = Window2InitialVelocityText;
                Window2InitialVelocity.FontSize = 24;
                Window2InitialVelocity.Foreground = Brushes.Gray;
                Window2InitialVelocity.TextAlignment = TextAlignment.Center;
                Window2InitialVelocity.Height = 50;
                Window2InitialVelocity.Width = 200;
                Window2Grid.Children.Add(Window2InitialVelocity);
                Grid.SetRow(Window2InitialVelocity, 1);
                Grid.SetColumn(Window2InitialVelocity, 0);
                //объявление и парметры TextBox для угла на втором экране
                TextBox Window2Angle = new TextBox();
                string Window2AngleText = "ANGLE";
                Window2Angle.Text = Window2AngleText;
                Window2Angle.FontSize = 24;
                Window2Angle.Foreground = Brushes.Gray;
                Window2Angle.TextAlignment = TextAlignment.Center;
                Window2Angle.Height = 50;
                Window2Angle.Width = 200;
                Window2Grid.Children.Add(Window2Angle);
                Grid.SetRow(Window2Angle, 1);
                Grid.SetColumn(Window2Angle, 1);
                Window2Stack.Children.Add(Window2Grid);
                
                //клик по кнопке Main menu на втором экране
                Window2MainMenu.PreviewMouseLeftButtonDown += Window2MainMenuClicked;
                void Window2MainMenuClicked(object sender2, MouseButtonEventArgs e2) //клик по кнопке Main menu
                {
                    Window1Stack.Visibility = Visibility.Visible;
                    Window2Stack.Visibility = Visibility.Collapsed;
                    Content = Window1Stack;
                };
                //клик по кнопке Start на втором экране
                Window2Start.PreviewMouseLeftButtonDown += Window2StartClicked;
                void Window2StartClicked(object sender2, MouseButtonEventArgs e2)
                {
                    //обработка входных данных
                    if (IsDataCorrect(Window2InitialVelocity.Text, Window2Angle.Text) == true) 
                    {
                        double MaxHeight = 0;
                        double MaxLenght = 0;
                        Window2Stack.Visibility = Visibility.Collapsed;
                        Background = Brushes.SkyBlue;
                        //объявление и параметры кнопки Restart на третьем экране
                        Button Window3Restart = new Button();
                        TextBlock Window3TextBlockRestart = new TextBlock();
                        Window3TextBlockRestart.FontSize = 24;
                        Window3TextBlockRestart.TextAlignment = TextAlignment.Center;
                        Window3TextBlockRestart.Inlines.Add(new Run("RESTART"));
                        Window3Restart.Content = Window3TextBlockRestart;
                        Window3Restart.Margin = new Thickness(50);
                        Window3Restart.Height = 50;
                        Window3Restart.Width = 200;
                        //объявление и параметры кнопки Back на третьем экране
                        Button Window3Back = new Button();
                        TextBlock Window3TextBlockBack = new TextBlock();
                        Window3TextBlockBack.FontSize = 24;
                        Window3TextBlockBack.TextAlignment = TextAlignment.Center;
                        Window3TextBlockBack.Inlines.Add(new Run("BACK"));
                        Window3Back.Content = Window3TextBlockBack;
                        Window3Back.Margin = new Thickness(50);
                        Window3Back.Height = 50;
                        Window3Back.Width = 200;
                        //объявление и параметры кнопки Next на третьем экране
                        Button Window3Next = new Button();
                        TextBlock Window3TextBlockNext = new TextBlock();
                        Window3TextBlockNext.FontSize = 24;
                        Window3TextBlockNext.TextAlignment = TextAlignment.Center;
                        Window3TextBlockNext.Inlines.Add(new Run("NEXT"));
                        Window3Next.Content = Window3TextBlockNext;
                        Window3Next.Margin = new Thickness(50);
                        Window3Next.Height = 50;
                        Window3Next.Width = 200;
                        //объявление StackPanel на третьем экране
                        Window3Stack = new StackPanel();
                        //объявление и параметры Grid на третьем экране
                        Grid Window3Grid = new Grid();
                        Window3Grid.Margin = new Thickness(50);
                        Window3Grid.RowDefinitions.Add(new RowDefinition());
                        Window3Grid.ColumnDefinitions.Add(new ColumnDefinition());
                        Window3Grid.ColumnDefinitions.Add(new ColumnDefinition());
                        Window3Grid.ColumnDefinitions.Add(new ColumnDefinition());
                        Window3Grid.Children.Add(Window3Restart);
                        Grid.SetRow(Window3Restart, 0);
                        Grid.SetColumn(Window3Restart, 0);
                        Window3Grid.Children.Add(Window3Back);
                        Grid.SetRow(Window3Back, 0);
                        Grid.SetColumn(Window3Back, 1);
                        Window3Grid.Children.Add(Window3Next);
                        Grid.SetRow(Window3Next, 0);
                        Grid.SetColumn(Window3Next, 2);
                        Window3Stack.Children.Add(Window3Grid);
                        //клик по кнопке Back на третьем экране
                        Window3Back.Click += delegate
                        {
                            LastPoint = 0;
                            MaxHeight = 0;
                            MaxLenght = 0;
                            Content = Window2Stack;
                            Window2Stack.Visibility = Visibility.Visible;
                            Background = Brushes.Green;
                        };
                        Window3Restart.Click += delegate
                        {
                            Window3Canvas.Children.Clear();
                            Window3Canvas.Children.Add(Field);
                            Window3Canvas.Children.Add(Bird);
                            BirdTimer.Start();
                        };
                        Window3Next.Click += delegate
                        {
                            Background = Brushes.Green;
                            CurrentDate = new DateTime();
                            CurrentDate = DateTime.Now;
                            //объявление и параметры кнопки Main menu на четвертом экране
                            Button Window4MainMenu = new Button();
                            TextBlock Window4TextBlockMainMenu = new TextBlock();
                            Window4TextBlockMainMenu.FontSize = 24;
                            Window4TextBlockMainMenu.TextAlignment = TextAlignment.Center;
                            Window4TextBlockMainMenu.Inlines.Add(new Run("MAIN MENU"));
                            Window4MainMenu.Content = Window4TextBlockMainMenu;
                            Window4MainMenu.Margin = new Thickness(50);
                            Window4MainMenu.Height = 50;
                            Window4MainMenu.Width = 200;
                            //объявление и параметры кнопки Save на четвертом экране
                            Button Window4Save = new Button();
                            TextBlock Window4TextBlockSave = new TextBlock();
                            Window4TextBlockSave.FontSize = 24;
                            Window4TextBlockSave.TextAlignment = TextAlignment.Center;
                            Window4TextBlockSave.Inlines.Add(new Run("SAVE"));
                            Window4Save.Content = Window4TextBlockSave;
                            Window4Save.Margin = new Thickness(50);
                            Window4Save.Height = 50;
                            Window4Save.Width = 200;
                            //объявление и парметры Grid для кнопок на четвертом экране
                            Window4GridForButtons = new Grid();
                            Window4GridForButtons.RowDefinitions.Add(new RowDefinition());
                            Window4GridForButtons.ColumnDefinitions.Add(new ColumnDefinition());
                            Window4GridForButtons.ColumnDefinitions.Add(new ColumnDefinition());
                            Window4GridForButtons.Children.Add(Window4Save);
                            Grid.SetRow(Window4Save, 0);
                            Grid.SetColumn(Window4Save, 0);
                            Window4GridForButtons.Children.Add(Window4MainMenu);
                            Grid.SetRow(Window4MainMenu, 0);
                            Grid.SetColumn(Window4MainMenu, 1);
                            Window4GridForButtons.Margin = new Thickness(170);
                            //первый столбец таблицы результатов
                            TextBlock Window4Range = new TextBlock();
                            Window4Range.FontSize = 24;
                            Window4Range.TextAlignment = TextAlignment.Center;
                            Window4Range.Inlines.Add(new Run("Range: "));
                            TextBlock Window4FlightTime = new TextBlock();
                            Window4FlightTime.FontSize = 24;
                            Window4FlightTime.TextAlignment = TextAlignment.Center;
                            Window4FlightTime.Inlines.Add(new Run("Flight time: "));
                            TextBlock Window4HighestPoint = new TextBlock();
                            Window4HighestPoint.FontSize = 24;
                            Window4HighestPoint.TextAlignment = TextAlignment.Center;
                            Window4HighestPoint.Inlines.Add(new Run("Highest point: "));
                            TextBlock Window4InitialAngle = new TextBlock();
                            Window4InitialAngle.FontSize = 24;
                            Window4InitialAngle.TextAlignment = TextAlignment.Center;
                            Window4InitialAngle.Inlines.Add(new Run("Initial angle: "));
                            TextBlock Window4InitialSpeed = new TextBlock();
                            Window4InitialSpeed.FontSize = 24;
                            Window4InitialSpeed.TextAlignment = TextAlignment.Center;
                            Window4InitialSpeed.Inlines.Add(new Run("Initial speed: "));
                            //второй столбец таблицы результатов
                            TextBlock Window4RangeValue = new TextBlock();
                            Window4RangeValue.FontSize = 24;
                            Window4RangeValue.TextAlignment = TextAlignment.Center;
                            Window4RangeValue.Inlines.Add(new Run(System.Convert.ToString(MaxLenght)));
                            TextBlock Window4FlightTimeValue = new TextBlock();
                            Window4FlightTimeValue.FontSize = 24;
                            Window4FlightTimeValue.TextAlignment = TextAlignment.Center;
                            Window4FlightTimeValue.Inlines.Add(new Run(System.Convert.ToString(LastPoint * 10/1000)));
                            TextBlock Window4HighestPointValue = new TextBlock();
                            Window4HighestPointValue.FontSize = 24;
                            Window4HighestPointValue.TextAlignment = TextAlignment.Center;
                            Window4HighestPointValue.Inlines.Add(new Run(System.Convert.ToString(MaxHeight)));
                            TextBlock Window4InitialAngleValue = new TextBlock();
                            Window4InitialAngleValue.FontSize = 24;
                            Window4InitialAngleValue.TextAlignment = TextAlignment.Center;
                            Window4InitialAngleValue.Inlines.Add(new Run(System.Convert.ToString(Window2Angle.Text)));
                            TextBlock Window4InitialSpeedValue = new TextBlock();
                            Window4InitialSpeedValue.FontSize = 24;
                            Window4InitialSpeedValue.TextAlignment = TextAlignment.Center;
                            Window4InitialSpeedValue.Inlines.Add(new Run(System.Convert.ToString(Window2InitialVelocity.Text)));
                            //объявляение и параметры Grid для таблицы результатов на четвертом экране
                            Grid Window4Grid = new Grid();
                            Window4Grid.RowDefinitions.Add(new RowDefinition());
                            Window4Grid.RowDefinitions.Add(new RowDefinition());
                            Window4Grid.RowDefinitions.Add(new RowDefinition());
                            Window4Grid.RowDefinitions.Add(new RowDefinition());
                            Window4Grid.RowDefinitions.Add(new RowDefinition());
                            Window4Grid.ColumnDefinitions.Add(new ColumnDefinition());
                            Window4Grid.ColumnDefinitions.Add(new ColumnDefinition());
                            Window4Grid.Children.Add(Window4Range);
                            Grid.SetRow(Window4Range, 0);
                            Grid.SetColumn(Window4Range, 0);
                            Window4Grid.Children.Add(Window4FlightTime);
                            Grid.SetRow(Window4FlightTime, 1);
                            Grid.SetColumn(Window4FlightTime, 0);
                            Window4Grid.Children.Add(Window4HighestPoint);
                            Grid.SetRow(Window4HighestPoint, 2);
                            Grid.SetColumn(Window4HighestPoint, 0);
                            Window4Grid.Children.Add(Window4InitialAngle);
                            Grid.SetRow(Window4InitialAngle, 3);
                            Grid.SetColumn(Window4InitialAngle, 0);
                            Window4Grid.Children.Add(Window4InitialSpeed);
                            Grid.SetRow(Window4InitialSpeed, 4);
                            Grid.SetColumn(Window4InitialSpeed, 0);
                            Window4Grid.Children.Add(Window4RangeValue);
                            Grid.SetRow(Window4RangeValue, 0);
                            Grid.SetColumn(Window4RangeValue, 1);
                            Window4Grid.Children.Add(Window4FlightTimeValue);
                            Grid.SetRow(Window4FlightTimeValue, 1);
                            Grid.SetColumn(Window4FlightTimeValue, 1);
                            Window4Grid.Children.Add(Window4HighestPointValue);
                            Grid.SetRow(Window4HighestPointValue, 2);
                            Grid.SetColumn(Window4HighestPointValue, 1);
                            Window4Grid.Children.Add(Window4InitialAngleValue);
                            Grid.SetRow(Window4InitialAngleValue, 3);
                            Grid.SetColumn(Window4InitialAngleValue, 1);
                            Window4Grid.Children.Add(Window4InitialSpeedValue);
                            Grid.SetRow(Window4InitialSpeedValue, 4);
                            Grid.SetColumn(Window4InitialSpeedValue, 1);
                            Window4Grid.Margin = new Thickness(170);
                            //объявление и параметры прямоугольника, имитирующего таблицу результатов
                            Window4Score = new Rectangle();
                            Window4Score.Stroke = System.Windows.Media.Brushes.Black;
                            Window4Score.Fill = System.Windows.Media.Brushes.WhiteSmoke;
                            Window4Score.Height = 170;
                            Window4Score.Width = 400;
                            //объявление и параметры StackPanel на четвертом экране
                            Window4Stack = new StackPanel();
                            Window4Stack.Children.Add(Window4Grid);
                            Canvas Window4Canvas = new Canvas();
                            Window4Canvas.SizeChanged += Window4CanvasOnSizeChanged;
                            Window4Canvas.Height = 500;
                            Window4Canvas.Width = 1000;
                            Window4Canvas.Children.Add(Window4Score);
                            Window4Canvas.Children.Add(Window4Stack);
                            Window4Canvas.Children.Add(Window4GridForButtons);
                            Content = Window4Canvas;
                            //клик по кнопке MainMenu на четвертом экране
                            Window4MainMenu.Click += delegate
                            {
                                Content = Window1Stack;
                                Window1Stack.Visibility = Visibility.Visible;
                            };
                            //клик по кнопке Save на четвертом экране
                            Window4Save.Click += delegate
                            {
                                File.AppendAllText("Angry birds)) Data.txt", System.Convert.ToString(CurrentDate) + Environment.NewLine);
                                File.AppendAllText("Angry birds)) Data.txt", "Range: ");
                                File.AppendAllText("Angry birds)) Data.txt", System.Convert.ToString(MaxLenght) + Environment.NewLine);
                                File.AppendAllText("Angry birds)) Data.txt", "Flight time: ");
                                File.AppendAllText("Angry birds)) Data.txt", System.Convert.ToString(LastPoint * 10 / 1000) + Environment.NewLine);
                                File.AppendAllText("Angry birds)) Data.txt", "Highest point: ");
                                File.AppendAllText("Angry birds)) Data.txt", System.Convert.ToString(MaxHeight) + Environment.NewLine);
                                File.AppendAllText("Angry birds)) Data.txt", "Initial angle: ");
                                File.AppendAllText("Angry birds)) Data.txt", System.Convert.ToString(Window2Angle.Text) + Environment.NewLine);
                                File.AppendAllText("Angry birds)) Data.txt", "Initial speed: ");
                                File.AppendAllText("Angry birds)) Data.txt", System.Convert.ToString(Window2InitialVelocity.Text) + Environment.NewLine);
                                File.AppendAllText("Angry birds)) Data.txt", Environment.NewLine);
                                MessageBox.Show("Your data has been saved!", "Complete.");
                            };
                        };

                        double InitialSpeed = System.Convert.ToDouble(Window2InitialVelocity.Text);
                        double InitialAngle = System.Convert.ToDouble(Window2Angle.Text);
                        const double Step = 0.1; //или 0.01??
                        const double g = 9.8;
                        //вычисление координаты X
                        double CalculateXCord(double InitialVelocity, double Angle, double CurrentTime)
                        {
                            double XCord = InitialVelocity * Math.Cos(Angle) * CurrentTime;
                            return XCord;
                        }
                        //вычисление координаты Y
                        double CalculateYCord(double InitialVelocity, double Angle, double CurrentTime)
                        {
                            double YCord = (InitialVelocity * Math.Sin(Angle) * CurrentTime) - ((g * Math.Pow(CurrentTime, 2)) / (2));
                            return YCord;
                        }
                        //вычисление количества координат 
                        int CalculateNumCoordinates(double InitialVelocity, double Angle)
                        {
                            int NumCoordinates = 0;
                            double CurrentTime = 0;
                            int YCordZeroChecker = 0;
                            for (int i = 0; i < i+1; i++)
                            {
                                CalculateXCord(InitialSpeed, InitialAngle, CurrentTime);
                                CalculateYCord(InitialSpeed, InitialAngle, CurrentTime);
                                if (CalculateYCord(InitialSpeed, InitialAngle, CurrentTime) <= 0)
                                {
                                    YCordZeroChecker++;
                                }
                                if (YCordZeroChecker >= 2)
                                {
                                    break;
                                }
                                NumCoordinates++;
                                CurrentTime+=Step;
                            }
                            return NumCoordinates;
                        }
                        //объявление и заполнение массива координат
                        Coordinates = new Point[CalculateNumCoordinates(InitialSpeed, InitialAngle)+1];
                        double CurrentTimeForPoints = 0;
                        for (int i = 0; i < Coordinates.Length; i++)
                        {
                            if(i+1==Coordinates.Length)
                            {
                                Coordinates[i].X = CalculateXCord(InitialSpeed, InitialAngle, CurrentTimeForPoints);
                                Coordinates[i].Y = 0;
                            }
                            else
                            {
                                Coordinates[i].X = CalculateXCord(InitialSpeed, InitialAngle, CurrentTimeForPoints);
                                Coordinates[i].Y = -CalculateYCord(InitialSpeed, InitialAngle, CurrentTimeForPoints);
                                if (-Coordinates[i].Y > MaxHeight)
                                {
                                    MaxHeight = -Coordinates[i].Y;
                                }
                                if (Coordinates[i].X > MaxLenght)
                                {
                                    MaxLenght = Coordinates[i].X;
                                }
                            }
                            CurrentTimeForPoints += Step;
                        }
                        //объявление и параметры Canvas на 3 окне
                        Window3Canvas = new Canvas();
                        Window3Canvas.SizeChanged += Window3CanvasOnSizeChanged;
                        Window3Canvas.Height = 500;
                        Window3Canvas.Width = 1000;
                        Content = Window3Canvas;
                        //неправильная траектория
                        /*Trajectory = new Polyline();
                        for (int i = 0; i < Coordinates.Length; i++)
                        {
                            Trajectory.Points = new PointCollection(Coordinates);
                        }
                        Trajectory.StrokeDashArray = new DoubleCollection() { 5 };
                        Trajectory.Stroke = SystemColors.WindowTextBrush;
                        Window3Canvas.Children.Add(Trajectory);*/
                        //объявление и параметры прямоугольника, имитирующего землю
                        Field = new Rectangle();
                        Field.Stroke = System.Windows.Media.Brushes.ForestGreen;
                        Field.Fill = System.Windows.Media.Brushes.ForestGreen;
                        Field.Height = 500;
                        Field.Width = 1500;
                        Window3Canvas.Children.Add(Field);
                        //объявление и параметры шара, имитирующего птицу
                        Bird = new Ellipse();
                        Bird.Stroke = System.Windows.Media.Brushes.Black;
                        Bird.Fill = System.Windows.Media.Brushes.Red;
                        Bird.Height = 10;
                        Bird.Width = 10;
                        Window3Canvas.Children.Add(Bird);
                        //объявление и параметры таймера
                        BirdTimer = new DispatcherTimer();
                        BirdTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
                        BirdTimer.Start();
                        //BirdTimer.Tick += new EventHandler(BirdTimerTick);
                        BirdTimer.Tick += delegate
                        {
                            Bird.Margin = new Thickness(Coordinates[T].X, Coordinates[T].Y, 0, 0);
                            Ellipse Dot = new Ellipse();
                            Dot.Width = 2;
                            Dot.Height = 2;
                            Dot.StrokeThickness = 2;
                            Dot.Stroke = Brushes.Black;
                            Canvas.SetLeft(Dot, (Window3Canvas.Width / 6) - 5);
                            Canvas.SetTop(Dot, (Window3Canvas.Height / 1.5) - 5);
                            Dot.Margin = new Thickness(Coordinates[T].X, Coordinates[T].Y, 0, 0);
                            Window3Canvas.Children.Add(Dot);
                            T++;
                            Console.WriteLine(T.ToString());
                            //после конца анимации
                            if (T == Coordinates.Length)
                            {
                                LastPoint = T;
                                BirdTimer.Stop();
                                AnimationCounter++;
                                T = 0;
                                Console.WriteLine("Good");
                                Window3Canvas.Children.Add(Window3Stack);
                                
                            }
                        };
                    }
                    else
                    {
                        MessageBox.Show("Your data is not correct! Please try again.", "Error.");
                    }
                };
                //обработка входных данных
                bool IsDataCorrect(string IS, string AN) 
                {
                    int DotCheckerIS = 0;
                    for (int i = 0; i < IS.Length; i++)
                    {
                        if (DotCheckerIS > 1)
                        {
                            return false;
                        }
                        if (!(IS[i] >= '0' && IS[i] <= '9'))
                        {
                            if (IS[i] == '.')
                            {
                                DotCheckerIS++;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    int DotCheckerAN = 0;
                    for (int i = 0; i < AN.Length; i++)
                    {
                        if (DotCheckerAN > 1)
                        {
                            return false;
                        }
                        if (!(AN[i] >= '0' && AN[i] <= '9'))
                        {
                            if (AN[i] == '.')
                            {
                                DotCheckerAN++;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
            };
        }
    }
}