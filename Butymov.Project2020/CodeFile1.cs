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
using System.Windows.Media.Imaging;

namespace Butymov.Project2020
{
    public class MainWindow : Window
    {
        //объекты первого экрана
        TextBlock Window1TextBlockPlay;
        TextBlock Window1TextBlockSettings;
        TextBlock Window1TextBlockExit;
        TextBlock WindowSettingsTextBlockBack;
        StackPanel Window1Stack;
        Canvas Window1Canvas;
        //объекты второго экрана
        StackPanel Window2Stack;
        Canvas Window2Canvas;
        TextBlock Window2TextBlockStart;
        TextBlock Window2TextBlockMainMenu;
        TextBox Window2InitialVelocity;
        TextBox Window2Angle;
        //объекты третьего экрана
        StackPanel Window3Stack;
        Canvas Window3Canvas;
        int AnimationCounter = 0;
        int T = 0;
        Point[] Coordinates;
        DispatcherTimer BirdTimer;
        TextBlock Window3TextBlockRestart;
        TextBlock Window3TextBlockBack;
        TextBlock Window3TextBlockNext;
        //Ellipse Dot;
        Image ImageTheBird;
        Image ImageBackground;

        Image ImageRedBird;
        Image ImageBlueBird;

        BitmapImage BitmapImageRedBird;
        BitmapImage BitmapImageBlueBird;

        
        BitmapImage BitmapImageBackground;

        Image ImageTheFlag;
        Image ImageRussianFlag;
        BitmapImage BitmapImageRussianFlag;
        Image ImageBritishFlag;
        BitmapImage BitmapImageBritishFlag;
        Polyline Trajectory;
        //объекты четветрого экрана
        StackPanel Window4Stack;
        Canvas Window4Canvas;
        double LastPoint = 0;
        DateTime CurrentDate;
        Grid Window4GridForButtons;
        Rectangle Window4Score;
        TextBlock Window4TextBlockMainMenu;
        TextBlock Window4TextBlockSave;
        //объекты окна настроек
        Canvas WindowSettingsCanvas;
        StackPanel WindowSettingsStack;
        int BirdValue = 1; //1 - красная, 2 - синяя
        int LanguageValue = 1; //1 - английский, 2 - русский
        Image ImageSettingsTheBird;
        Image ImageSettingsRedBird;
        Image ImageSettingsBlueBird;
        BitmapImage BitmapImageSettingsRedBird;
        BitmapImage BitmapImageSettingsBlueBird;
        TextBlock WindowSettingsTextBlockBird;
        TextBlock WindowSettingsTextBlockLanguage;
        TextBlock WindowSettingsTextBlockTheme;

        [STAThread]
        public static void Main()
        { 
            Application app = new Application();
            app.Run(new MainWindow());
        }
        void Window1CanvasOnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            Canvas.SetLeft(Window1Stack, (args.NewSize.Width / -150)+40);
            Canvas.SetTop(Window1Stack, (args.NewSize.Height / -150)+200);
        }
        void Window2CanvasOnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            Canvas.SetLeft(Window2Stack, (args.NewSize.Width / -150) + 190);
            Canvas.SetTop(Window2Stack, (args.NewSize.Height / -150) + 200);
        }
        void Window3CanvasOnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            Canvas.SetLeft(ImageTheBird, (args.NewSize.Width / 6) -400); //-230
            Canvas.SetTop(ImageTheBird, (args.NewSize.Height / 1.5) -130); //-70 ///400*400
            Canvas.SetLeft(ImageBackground, (args.NewSize.Width / 6) - 300);
            Canvas.SetTop(ImageBackground, (args.NewSize.Height / 1.5) -552);
            Canvas.SetLeft(Trajectory, (args.NewSize.Width  / 6) - 5);
            Canvas.SetTop(Trajectory, (args.NewSize.Height / 1.5) - 5);
            Canvas.SetLeft(Window3Stack, (args.NewSize.Width / -150)+190);
            Canvas.SetTop(Window3Stack, (args.NewSize.Height / -150)+150);
        }
        void Window4CanvasOnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            Canvas.SetLeft(Window4Score, (args.NewSize.Width / 10)+300);
            Canvas.SetTop(Window4Score, (args.NewSize.Height / 4));
            Canvas.SetLeft(Window4Stack, (args.NewSize.Width / 7.5)+120);
            Canvas.SetTop(Window4Stack, (args.NewSize.Height / 100)+20);
            Canvas.SetLeft(Window4GridForButtons, (args.NewSize.Width / 56)+190);
            Canvas.SetTop(Window4GridForButtons, (args.NewSize.Height / 3));
        }
        void WindowSettingsCanvasOnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            Canvas.SetLeft(WindowSettingsStack, (args.NewSize.Width / -150) + 330);
            Canvas.SetTop(WindowSettingsStack, (args.NewSize.Height / -150) + 200);
            Canvas.SetLeft(ImageSettingsTheBird, (args.NewSize.Width / -150) + 200);
            Canvas.SetTop(ImageSettingsTheBird, (args.NewSize.Height / -150) + 147);
            Canvas.SetLeft(ImageTheFlag, (args.NewSize.Width / -150) + 570);
            Canvas.SetTop(ImageTheFlag, (args.NewSize.Height / -150) + 147);
        }

        

        public MainWindow()
        {
            //размеры окна
            this.WindowState = WindowState.Maximized;

            //объекты флага
            ImageTheFlag = new Image();
            ImageTheFlag.Width = 700;
            //иконки флага
            ImageRussianFlag = new Image();
            ImageRussianFlag.Width = 700;
            BitmapImageRussianFlag = new BitmapImage();
            BitmapImageRussianFlag.BeginInit();
            BitmapImageRussianFlag.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Angry birds)) Images\RussianFlag.png");
            BitmapImageRussianFlag.DecodePixelWidth = 700;
            BitmapImageRussianFlag.EndInit();
            ImageRussianFlag.Source = BitmapImageRussianFlag;
            ImageRussianFlag.Margin = new Thickness(40);

            ImageBritishFlag = new Image();
            ImageBritishFlag.Width = 700;
            BitmapImageBritishFlag = new BitmapImage();
            BitmapImageBritishFlag.BeginInit();
            BitmapImageBritishFlag.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Angry birds)) Images\BritishFlag.png");
            BitmapImageBritishFlag.DecodePixelWidth = 700;
            BitmapImageBritishFlag.EndInit();
            ImageBritishFlag.Source = BitmapImageBritishFlag;
            ImageBritishFlag.Margin = new Thickness(40);

            //объекты птиц
            ImageTheBird = new Image();
            ImageTheBird.Width = 700;

            ImageSettingsTheBird = new Image();
            ImageSettingsTheBird.Width = 700;
            //иконки птицы
            ImageRedBird = new Image();
            ImageRedBird.Width = 700;
            BitmapImageRedBird = new BitmapImage();
            BitmapImageRedBird.BeginInit();
            BitmapImageRedBird.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Angry birds)) Images\RedBird.png");
            BitmapImageRedBird.DecodePixelWidth = 700;
            BitmapImageRedBird.EndInit();
            ImageRedBird.Source = BitmapImageRedBird;
            ImageRedBird.Margin = new Thickness(40);

            ImageBlueBird = new Image();
            ImageBlueBird.Width = 700;
            BitmapImageBlueBird = new BitmapImage();
            BitmapImageBlueBird.BeginInit();
            BitmapImageBlueBird.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Angry birds)) Images\BlueBird.png");
            BitmapImageBlueBird.DecodePixelWidth = 700;
            BitmapImageBlueBird.EndInit();
            ImageBlueBird.Source = BitmapImageBlueBird;
            ImageBlueBird.Margin = new Thickness(40);

            ImageSettingsRedBird = new Image();
            ImageSettingsRedBird.Width = 700;
            BitmapImageSettingsRedBird = new BitmapImage();
            BitmapImageSettingsRedBird.BeginInit();
            BitmapImageSettingsRedBird.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\\Angry birds)) Images\RedBird.png");
            BitmapImageSettingsRedBird.DecodePixelWidth = 700;
            BitmapImageSettingsRedBird.EndInit();
            ImageSettingsRedBird.Source = BitmapImageSettingsRedBird;
            ImageSettingsRedBird.Margin = new Thickness(40);

            ImageSettingsBlueBird = new Image();
            ImageSettingsBlueBird.Width = 700;
            BitmapImageSettingsBlueBird = new BitmapImage();
            BitmapImageSettingsBlueBird.BeginInit();
            BitmapImageSettingsBlueBird.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Angry birds)) Images\BlueBird.png");
            BitmapImageSettingsBlueBird.DecodePixelWidth = 700;
            BitmapImageSettingsBlueBird.EndInit();
            ImageSettingsBlueBird.Source = BitmapImageSettingsBlueBird;
            ImageSettingsBlueBird.Margin = new Thickness(40);
            //птицы по умолчанию
            ImageTheBird = ImageRedBird;
            ImageSettingsTheBird = ImageSettingsRedBird;
            //флаг по умолчанию
            ImageTheFlag = ImageBritishFlag;

           

            void ChangeValueLanguage()
            {
                switch (LanguageValue)
                {
                    case 1:
                        LanguageValue = 2;
                        break;
                    case 2:
                        LanguageValue = 1;
                        break;
                }
            }
            void ChangeLanguage() ///////////////////////////////////////////////////////
            {
                switch (LanguageValue)
                {
                    case 1:
                        ImageTheFlag = ImageBritishFlag;
                        Window1TextBlockPlay.Inlines.Clear();
                        Window1TextBlockPlay.Inlines.Add(new Run("PLAY"));
                        Window1TextBlockSettings.Inlines.Clear();
                        Window1TextBlockSettings.Inlines.Add(new Run("SETTINGS"));
                        Window1TextBlockExit.Inlines.Clear();
                        Window1TextBlockExit.Inlines.Add(new Run("EXIT"));
                        WindowSettingsTextBlockBack.Inlines.Clear();
                        WindowSettingsTextBlockBack.Inlines.Add(new Run("BACK"));
                        WindowSettingsTextBlockBird.Inlines.Clear();
                        WindowSettingsTextBlockBird.Inlines.Add(new Run("BIRD"));
                        WindowSettingsTextBlockLanguage.Inlines.Clear();
                        WindowSettingsTextBlockLanguage.Inlines.Add(new Run("LANGUAGE"));
                        WindowSettingsTextBlockTheme.Inlines.Clear();
                        WindowSettingsTextBlockTheme.Inlines.Add(new Run("THEME"));
                        break;
                    case 2:
                        ImageTheFlag = ImageRussianFlag;
                        Window1TextBlockPlay.Inlines.Clear();
                        Window1TextBlockPlay.Inlines.Add(new Run("ИГРАТЬ"));
                        Window1TextBlockSettings.Inlines.Clear();
                        Window1TextBlockSettings.Inlines.Add(new Run("НАСТРОЙКИ"));
                        Window1TextBlockExit.Inlines.Clear();
                        Window1TextBlockExit.Inlines.Add(new Run("ВЫХОД"));
                        WindowSettingsTextBlockBack.Inlines.Clear();
                        WindowSettingsTextBlockBack.Inlines.Add(new Run("НАЗАД"));
                        WindowSettingsTextBlockBird.Inlines.Clear();
                        WindowSettingsTextBlockBird.Inlines.Add("ПТИЦА");
                        WindowSettingsTextBlockLanguage.Inlines.Clear();
                        WindowSettingsTextBlockLanguage.Inlines.Add(new Run("ЯЗЫК"));
                        WindowSettingsTextBlockTheme.Inlines.Clear();
                        WindowSettingsTextBlockTheme.Inlines.Add(new Run("ТЕМА"));
                        break;
                }
            }
            void ChangeValueTheBird()
            {
                switch(BirdValue)
                {
                    case 1:
                        BirdValue = 2; 
                        break;
                    case 2:
                        BirdValue = 1;
                        break;
                }
            }
            void ChangeTheBird()
            {
                switch(BirdValue)
                {
                    case 1:
                        ImageSettingsTheBird = ImageSettingsRedBird;
                        ImageTheBird = ImageRedBird;
                        break;
                    case 2:
                        ImageSettingsTheBird = ImageSettingsBlueBird;
                        ImageTheBird = ImageBlueBird;
                        break;
                }
            }
            Title = "Angry birds))";
            Background = Brushes.Green;
            //объявление и параметры кнопки Play на первом экране
            Button Window1Play = new Button();
            Window1TextBlockPlay = new TextBlock();
            Window1TextBlockPlay.FontSize = 24;
            Window1TextBlockPlay.TextAlignment = TextAlignment.Center;
            Window1TextBlockPlay.Inlines.Add(new Run("PLAY"));
            Window1Play.Content = Window1TextBlockPlay;
            Window1Play.Margin = new Thickness(50);
            Window1Play.Height = 50;
            Window1Play.Width = 200;
            //объявление и параметры кнопки Settings на первом экране
            Button Window1Settings = new Button();
            Window1TextBlockSettings = new TextBlock();
            Window1TextBlockSettings.FontSize = 24;
            Window1TextBlockSettings.TextAlignment = TextAlignment.Center;
            Window1TextBlockSettings.Inlines.Add(new Run("SETTINGS"));
            Window1Settings.Content = Window1TextBlockSettings;
            Window1Settings.Margin = new Thickness(50);
            Window1Settings.Height = 50;
            Window1Settings.Width = 200;
            //объявление и параметры кнопки Exit на первом экране
            Button Window1Exit = new Button();
            Window1TextBlockExit = new TextBlock();
            Window1TextBlockExit.FontSize = 24;
            Window1TextBlockExit.TextAlignment = TextAlignment.Center;
            Window1TextBlockExit.Inlines.Add(new Run("EXIT"));
            Window1Exit.Content = Window1TextBlockExit;
            Window1Exit.Margin = new Thickness(50);
            Window1Exit.Height = 50;
            Window1Exit.Width = 200;
            //объявление StackPanel на первом экране
            Window1Stack = new StackPanel();
            //объявление и параметры Grid на первом экране
            Grid Window1Grid = new Grid();
            Window1Grid.Margin = new Thickness(200);
            Window1Grid.RowDefinitions.Add(new RowDefinition());
            Window1Grid.ColumnDefinitions.Add(new ColumnDefinition());
            Window1Grid.ColumnDefinitions.Add(new ColumnDefinition());
            Window1Grid.ColumnDefinitions.Add(new ColumnDefinition());
            Window1Grid.Children.Add(Window1Play);
            Grid.SetRow(Window1Play, 0);
            Grid.SetColumn(Window1Play, 0);
            Window1Grid.Children.Add(Window1Settings);
            Grid.SetRow(Window1Settings, 0);
            Grid.SetColumn(Window1Settings, 1);
            Window1Grid.Children.Add(Window1Exit);
            Grid.SetRow(Window1Exit, 0);
            Grid.SetColumn(Window1Exit, 2);
            Window1Stack.Children.Add(Window1Grid);
            //объявление и параметры Canvas на первом экране
            Window1Canvas = new Canvas();
            Window1Canvas.SizeChanged += Window1CanvasOnSizeChanged;
            Window1Canvas.Height = SystemParameters.VirtualScreenHeight; 
            Window1Canvas.Width = SystemParameters.VirtualScreenWidth; 
            Window1Canvas.Children.Add(Window1Stack);
            Content = Window1Canvas;
            //клик по кнопке Exit на первом экране
            Window1Exit.PreviewMouseLeftButtonDown += Window1ExitClicked;
            void Window1ExitClicked(object sender, MouseButtonEventArgs e) 
            {
                Close();
            }
            //клик по кнопке Settings на первом экране
            Window1Settings.PreviewMouseLeftButtonDown += Window1SettingsClicked;
            void Window1SettingsClicked(object sender, MouseButtonEventArgs e)
            {
                //объявление и параметры кнопки Back на экране настроек
                Button WindowSettingsBack = new Button();
                WindowSettingsTextBlockBack = new TextBlock();
                WindowSettingsTextBlockBack.FontSize = 24;
                WindowSettingsTextBlockBack.TextAlignment = TextAlignment.Center;
                WindowSettingsTextBlockBack.Inlines.Add(new Run("BACK"));
                WindowSettingsBack.Content = WindowSettingsTextBlockBack;
                WindowSettingsBack.Margin = new Thickness(80);
                WindowSettingsBack.Height = 50;
                WindowSettingsBack.Width = 200;
                //объявление и параметры кнопки Bird на экране настроек
                Button WindowSettingsBird = new Button();
                WindowSettingsTextBlockBird = new TextBlock();
                WindowSettingsTextBlockBird.FontSize = 24;
                WindowSettingsTextBlockBird.TextAlignment = TextAlignment.Center;
                WindowSettingsTextBlockBird.Inlines.Add(new Run("BIRD"));
                WindowSettingsBird.Content = WindowSettingsTextBlockBird;
                WindowSettingsBird.Margin = new Thickness(80);
                WindowSettingsBird.Height = 50;
                WindowSettingsBird.Width = 200;
                //объявление и параметры кнопки Language на экране настроек
                Button WindowSettingsLanguage = new Button();
                WindowSettingsTextBlockLanguage = new TextBlock();
                WindowSettingsTextBlockLanguage.FontSize = 24;
                WindowSettingsTextBlockLanguage.TextAlignment = TextAlignment.Center;
                WindowSettingsTextBlockLanguage.Inlines.Add(new Run("LANGUAGE"));
                WindowSettingsLanguage.Content = WindowSettingsTextBlockLanguage;
                WindowSettingsLanguage.Margin = new Thickness(80);
                WindowSettingsLanguage.Height = 50;
                WindowSettingsLanguage.Width = 200;
                //объявление и параметры кнопки Theme на экране настроек
                Button WindowSettingsTheme = new Button();
                WindowSettingsTextBlockTheme = new TextBlock();
                WindowSettingsTextBlockTheme.FontSize = 24;
                WindowSettingsTextBlockTheme.TextAlignment = TextAlignment.Center;
                WindowSettingsTextBlockTheme.Inlines.Add(new Run("THEME"));
                WindowSettingsTheme.Content = WindowSettingsTextBlockTheme;
                WindowSettingsTheme.Margin = new Thickness(80);
                WindowSettingsTheme.Height = 50;
                WindowSettingsTheme.Width = 200;
                //объявление и параметры StackPanel на экране настроек
                WindowSettingsStack = new StackPanel();
                //объявление и параметры Grid на экране настроек
                Grid WindowSettingsGrid = new Grid();
                WindowSettingsGrid.RowDefinitions.Add(new RowDefinition());
                WindowSettingsGrid.RowDefinitions.Add(new RowDefinition());
                WindowSettingsGrid.ColumnDefinitions.Add(new ColumnDefinition());
                WindowSettingsGrid.ColumnDefinitions.Add(new ColumnDefinition());
                WindowSettingsGrid.Children.Add(WindowSettingsBird);
                Grid.SetRow(WindowSettingsBird, 0);
                Grid.SetColumn(WindowSettingsBird, 0);
                WindowSettingsGrid.Children.Add(WindowSettingsLanguage);
                Grid.SetRow(WindowSettingsLanguage, 0);
                Grid.SetColumn(WindowSettingsLanguage, 1);
                WindowSettingsGrid.Children.Add(WindowSettingsTheme);
                Grid.SetRow(WindowSettingsTheme, 1);
                Grid.SetColumn(WindowSettingsTheme, 0);
                WindowSettingsGrid.Children.Add(WindowSettingsBack);
                Grid.SetRow(WindowSettingsBack, 1);
                Grid.SetColumn(WindowSettingsBack, 1);
                WindowSettingsStack.Children.Add(WindowSettingsGrid);
                //объявление и параметры Canvas на экране настроек
                WindowSettingsCanvas = new Canvas();
                WindowSettingsCanvas.SizeChanged += WindowSettingsCanvasOnSizeChanged;
                WindowSettingsCanvas.Height = SystemParameters.VirtualScreenHeight;
                WindowSettingsCanvas.Width = SystemParameters.VirtualScreenWidth;
                WindowSettingsCanvas.Children.Add(ImageTheFlag);
                WindowSettingsCanvas.Children.Add(ImageSettingsTheBird);
                
                WindowSettingsCanvas.Children.Add(WindowSettingsStack);
                
                Content = WindowSettingsCanvas;
                //клик по кнопке Back на экране настроек
                WindowSettingsBack.Click += delegate
                {
                    Content = Window1Canvas;
                    WindowSettingsCanvas.Children.Clear();
                };
                //клик по кнопке Bird на экране настроек
                WindowSettingsBird.Click += delegate
                {
                    ChangeValueTheBird();
                    WindowSettingsCanvas.Children.Clear();
                    ChangeTheBird();
                    WindowSettingsCanvas.Children.Add(ImageTheFlag);
                    WindowSettingsCanvas.Children.Add(ImageSettingsTheBird);
                    WindowSettingsCanvas.Children.Add(WindowSettingsStack);
                    Canvas.SetLeft(ImageSettingsTheBird, (WindowSettingsCanvas.Width / -150) + 200);
                    Canvas.SetTop(ImageSettingsTheBird, (WindowSettingsCanvas.Height / -150) + 147);
                    
                };
                //клик по кнопке Language на экране настроек
                WindowSettingsLanguage.Click += delegate
                {
                    ChangeValueLanguage();
                    WindowSettingsCanvas.Children.Clear();
                    ChangeLanguage();
                    WindowSettingsCanvas.Children.Add(ImageTheFlag);
                    WindowSettingsCanvas.Children.Add(ImageSettingsTheBird);
                    WindowSettingsCanvas.Children.Add(WindowSettingsStack);
                    Canvas.SetLeft(ImageTheFlag, (WindowSettingsCanvas.Width / -150) + 570);
                    Canvas.SetTop(ImageTheFlag, (WindowSettingsCanvas.Height / -150) + 147);
                };
            }
            //клик по кнопке Play на первом экране
            Window1Play.PreviewMouseLeftButtonDown += Window1PlayClicked;
            void Window1PlayClicked(object sender, MouseButtonEventArgs e) 
            {
                //объявление и параметры кнопки Start на втором экране
                Button Window2Start = new Button();
                Window2TextBlockStart = new TextBlock();
                Window2TextBlockStart.FontSize = 24;
                Window2TextBlockStart.TextAlignment = TextAlignment.Center;
                switch (LanguageValue)
                {
                    case 1:
                        Window2TextBlockStart.Inlines.Add(new Run("START"));
                        break;
                    case 2:
                        Window2TextBlockStart.Inlines.Add(new Run("ПУСК"));
                        break;
                }
                Window2Start.Content = Window2TextBlockStart;
                Window2Start.Margin = new Thickness(50);
                Window2Start.Height = 50;
                Window2Start.Width = 200;
                //объявление и параметры кнопки Main menu на втором экране
                Button Window2MainMenu = new Button();
                Window2TextBlockMainMenu = new TextBlock();
                Window2TextBlockMainMenu.FontSize = 24;
                Window2TextBlockMainMenu.TextAlignment = TextAlignment.Center;
                switch (LanguageValue)
                {
                    case 1:
                        Window2TextBlockMainMenu.Inlines.Add(new Run("MAIN MENU"));
                        break;
                    case 2:
                        Window2TextBlockMainMenu.Inlines.Add(new Run("ГЛАВНОЕ МЕНЮ"));
                        break;
                }
                Window2MainMenu.Content = Window2TextBlockMainMenu;
                Window2MainMenu.Margin = new Thickness(50);
                Window2MainMenu.Height = 50;
                Window2MainMenu.Width = 200;
                //объявление StackPanel на втором экране
                Window2Stack = new StackPanel();
                //объявление и параметры Grid на втором экране
                Grid Window2Grid = new Grid();
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
                Window2InitialVelocity = new TextBox();
                string Window2InitialVelocityTextBritish = "INITIAL VELOCITY";
                string Window2InitialVelocityTextRussian = "НАЧ. СКОРОСТЬ";
                switch (LanguageValue)
                {
                    case 1:
                        Window2InitialVelocity.Text = Window2InitialVelocityTextBritish;
                        break;
                    case 2:
                        Window2InitialVelocity.Text = Window2InitialVelocityTextRussian;
                        break;
                }
                Window2InitialVelocity.FontSize = 24;
                Window2InitialVelocity.Foreground = Brushes.Gray;
                Window2InitialVelocity.TextAlignment = TextAlignment.Center;
                Window2InitialVelocity.Height = 50;
                Window2InitialVelocity.Width = 200;
                Window2Grid.Children.Add(Window2InitialVelocity);
                Grid.SetRow(Window2InitialVelocity, 1);
                Grid.SetColumn(Window2InitialVelocity, 0);
                //объявление и парметры TextBox для угла на втором экране
                Window2Angle = new TextBox();
                string Window2AngleTextBritish = "ANGLE";
                string Window2AngleTextRussian = "УГОЛ";
                switch (LanguageValue)
                {
                    case 1:
                        Window2Angle.Text = Window2AngleTextBritish;
                        break;
                    case 2:
                        Window2Angle.Text = Window2AngleTextRussian;
                        break;
                }
                Window2Angle.FontSize = 24;
                Window2Angle.Foreground = Brushes.Gray;
                Window2Angle.TextAlignment = TextAlignment.Center;
                Window2Angle.Height = 50;
                Window2Angle.Width = 200;
                Window2Grid.Children.Add(Window2Angle);
                Grid.SetRow(Window2Angle, 1);
                Grid.SetColumn(Window2Angle, 1);
                Window2Stack.Children.Add(Window2Grid);
                //объявление и параметры Canvas на втором экране
                Window2Canvas = new Canvas();
                Window2Canvas.SizeChanged += Window2CanvasOnSizeChanged;
                Window2Canvas.Height = SystemParameters.VirtualScreenHeight;
                Window2Canvas.Width = SystemParameters.VirtualScreenWidth;
                Window2Canvas.Children.Add(Window2Stack);
                Content = Window2Canvas;
                //клик по кнопке Main menu на втором экране
                Window2MainMenu.PreviewMouseLeftButtonDown += Window2MainMenuClicked;
                void Window2MainMenuClicked(object sender2, MouseButtonEventArgs e2) //клик по кнопке Main menu
                {
                    Content = Window1Canvas;
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
                        //объявление и параметры кнопки Restart на третьем экране
                        Button Window3Restart = new Button();
                        Window3TextBlockRestart = new TextBlock();
                        Window3TextBlockRestart.FontSize = 24;
                        Window3TextBlockRestart.TextAlignment = TextAlignment.Center;
                        switch (LanguageValue)
                        {
                            case 1:
                                Window3TextBlockRestart.Inlines.Add(new Run("RESTART"));
                                break;
                            case 2:
                                Window3TextBlockRestart.Inlines.Add(new Run("ЗАНОВО"));
                                break;
                        }
                        Window3Restart.Content = Window3TextBlockRestart;
                        Window3Restart.Margin = new Thickness(50);
                        Window3Restart.Height = 50;
                        Window3Restart.Width = 200;
                        //объявление и параметры кнопки Back на третьем экране
                        Button Window3Back = new Button();
                        Window3TextBlockBack = new TextBlock();
                        Window3TextBlockBack.FontSize = 24;
                        Window3TextBlockBack.TextAlignment = TextAlignment.Center;
                        switch (LanguageValue)
                        {
                            case 1:
                                Window3TextBlockBack.Inlines.Add(new Run("BACK"));
                                break;
                            case 2:
                                Window3TextBlockBack.Inlines.Add(new Run("НАЗАД"));
                                break;
                        }
                        Window3Back.Content = Window3TextBlockBack;
                        Window3Back.Margin = new Thickness(50);
                        Window3Back.Height = 50;
                        Window3Back.Width = 200;
                        //объявление и параметры кнопки Next на третьем экране
                        Button Window3Next = new Button();
                        Window3TextBlockNext = new TextBlock();
                        Window3TextBlockNext.FontSize = 24;
                        Window3TextBlockNext.TextAlignment = TextAlignment.Center;
                        switch (LanguageValue)
                        {
                            case 1:
                                Window3TextBlockNext.Inlines.Add(new Run("NEXT"));
                                break;
                            case 2:
                                Window3TextBlockNext.Inlines.Add(new Run("ДАЛЬШЕ"));
                                break;

                        }
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
                            Content = Window2Canvas;
                            Background = Brushes.Green;
                            Window3Canvas.Children.Clear();
                        };
                        //клик по кнопке Restart на третьем экране
                        Window3Restart.Click += delegate
                        {
                            Window3Canvas.Children.Clear();
                            Window3Canvas.Children.Add(ImageBackground);
                            Window3Canvas.Children.Add(ImageTheBird);
                            BirdTimer.Start();
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
                        Window3Canvas.Height = SystemParameters.VirtualScreenHeight;
                        Window3Canvas.Width = SystemParameters.VirtualScreenWidth;
                        Content = Window3Canvas;
                        //объявление и параметры тракетории
                        Trajectory = new Polyline();
                        for (int i = 0; i < Coordinates.Length; i++)
                        {
                            Trajectory.Points = new PointCollection(Coordinates);
                        }
                        Trajectory.StrokeDashArray = new DoubleCollection() { 5 };
                        Trajectory.Stroke = SystemColors.WindowTextBrush;
                        

                        ImageBackground = new Image();
                        ImageBackground.Width = 1730;
                        BitmapImageBackground = new BitmapImage();
                        BitmapImageBackground.BeginInit();
                        BitmapImageBackground.UriSource = new Uri(@"C:\Users\pc\Desktop\Angry birds)) Images\SummerLightBackground.png");
                        BitmapImageBackground.DecodePixelWidth = 1730;
                        BitmapImageBackground.EndInit();
                        ImageBackground.Source = BitmapImageBackground;
                        ImageBackground.Margin = new Thickness(40);

                        
                        Window3Canvas.Children.Add(ImageBackground);
                        Window3Canvas.Children.Add(ImageTheBird);
   
                        //объявление и параметры таймера
                        BirdTimer = new DispatcherTimer();
                        BirdTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
                        BirdTimer.Start();
                        //функция, описывающая шаг таймера
                        BirdTimer.Tick += delegate
                        {
                            ImageTheBird.Margin = new Thickness(Coordinates[T].X, Coordinates[T].Y, 0, -Coordinates[T].Y);
                            /*Dot = new Ellipse();
                            Dot.Width = 2;
                            Dot.Height = 2;
                            Dot.StrokeThickness = 2;
                            Dot.Stroke = Brushes.Black;
                            Canvas.SetLeft(Dot, (Window3Canvas.Width / 6) -5);
                            Canvas.SetTop(Dot, (Window3Canvas.Height / 1.5) -5);
                            Dot.Margin = new Thickness(Coordinates[T].X, Coordinates[T].Y, 0, 0);
                            Window3Canvas.Children.Add(Dot);*/
                            T++;
                            Console.WriteLine(T.ToString());
                            //после конца анимации
                            if (T == Coordinates.Length)
                            {
                                Window3Canvas.Children.Add(Trajectory);
                                LastPoint = T;
                                BirdTimer.Stop();
                                AnimationCounter++;
                                T = 0;
                                Console.WriteLine("Good");
                                Window3Canvas.Children.Add(Window3Stack);
                            }
                            //клик по кнопке Next на третьем экране
                            Window3Next.Click += delegate
                            {
                                Window3Canvas.Children.Clear();
                                Background = Brushes.Green;
                                CurrentDate = new DateTime();
                                CurrentDate = DateTime.Now;
                                //объявление и параметры кнопки Main menu на четвертом экране
                                Button Window4MainMenu = new Button();
                                Window4TextBlockMainMenu = new TextBlock();
                                Window4TextBlockMainMenu.FontSize = 24;
                                Window4TextBlockMainMenu.TextAlignment = TextAlignment.Center;
                                switch(LanguageValue)
                                {
                                    case 1:
                                        Window4TextBlockMainMenu.Inlines.Add(new Run("MAIN MENU"));
                                        break;
                                    case 2:
                                        Window4TextBlockMainMenu.Inlines.Add(new Run("ГЛАВНОЕ МЕНЮ"));
                                        break;
                                }
                                
                                Window4MainMenu.Content = Window4TextBlockMainMenu;
                                Window4MainMenu.Margin = new Thickness(50);
                                Window4MainMenu.Height = 50;
                                Window4MainMenu.Width = 200;
                                //объявление и параметры кнопки Save на четвертом экране
                                Button Window4Save = new Button();
                                Window4TextBlockSave = new TextBlock();
                                Window4TextBlockSave.FontSize = 24;
                                Window4TextBlockSave.TextAlignment = TextAlignment.Center;
                                switch(LanguageValue)
                                {
                                    case 1:
                                        Window4TextBlockSave.Inlines.Add(new Run("SAVE"));
                                        break;
                                    case 2:
                                        Window4TextBlockSave.Inlines.Add(new Run("СОХРАНИТЬ"));
                                        break;
                                }
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
                                TextBlock Window4FlightTime = new TextBlock();
                                Window4FlightTime.FontSize = 24;
                                Window4FlightTime.TextAlignment = TextAlignment.Center;
                                TextBlock Window4HighestPoint = new TextBlock();
                                Window4HighestPoint.FontSize = 24;
                                Window4HighestPoint.TextAlignment = TextAlignment.Center;
                                TextBlock Window4InitialAngle = new TextBlock();
                                Window4InitialAngle.FontSize = 24;
                                Window4InitialAngle.TextAlignment = TextAlignment.Center;
                                TextBlock Window4InitialSpeed = new TextBlock();
                                Window4InitialSpeed.FontSize = 24;
                                Window4InitialSpeed.TextAlignment = TextAlignment.Center;
                                TextBlock Window4ScoreName = new TextBlock();
                                Window4ScoreName.FontSize = 24;
                                Window4ScoreName.TextAlignment = TextAlignment.Center;
                                switch (LanguageValue)
                                {
                                    case 1:
                                        Window4ScoreName.Inlines.Add("RESULTS");
                                        Window4Range.Inlines.Add(new Run("Range: "));
                                        Window4FlightTime.Inlines.Add(new Run("Flight time: "));
                                        Window4HighestPoint.Inlines.Add(new Run("Highest point: "));
                                        Window4InitialSpeed.Inlines.Add(new Run("Initial speed: "));
                                        Window4InitialAngle.Inlines.Add(new Run("Initial angle: "));
                                        break;
                                    case 2:
                                        Window4ScoreName.Inlines.Add("РЕЗУЛЬТАТЫ");
                                        Window4Range.Inlines.Add(new Run("Дистанция: "));
                                        Window4FlightTime.Inlines.Add(new Run("Время полёта: "));
                                        Window4HighestPoint.Inlines.Add(new Run("Наивысшая точка: "));
                                        Window4InitialSpeed.Inlines.Add(new Run("Начальная скорость: "));
                                        Window4InitialAngle.Inlines.Add(new Run("Начальный угол: "));
                                        break;
                                }
                                //второй столбец таблицы результатов
                                TextBlock Window4RangeValue = new TextBlock();
                                Window4RangeValue.FontSize = 24;
                                Window4RangeValue.TextAlignment = TextAlignment.Center;
                                Window4RangeValue.Inlines.Add(new Run(System.Convert.ToString(MaxLenght)));
                                TextBlock Window4FlightTimeValue = new TextBlock();
                                Window4FlightTimeValue.FontSize = 24;
                                Window4FlightTimeValue.TextAlignment = TextAlignment.Center;
                                Window4FlightTimeValue.Inlines.Add(new Run(System.Convert.ToString(LastPoint * 10 / 1000)));
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
                                Window4Score.Width = 500;
                                
                                //объявление и параметры StackPanel на четвертом экране
                                Window4Stack = new StackPanel();
                                Window4Stack.Children.Add(Window4Grid);
                                //объявление и параметры Canvas на четвертом экране
                                Window4Canvas = new Canvas();
                                Window4Canvas.SizeChanged += Window4CanvasOnSizeChanged;
                                Window4Canvas.Height = SystemParameters.VirtualScreenHeight;
                                Window4Canvas.Width = SystemParameters.VirtualScreenWidth;
                                Window4Canvas.Children.Add(Window4Score);
                                Window4Canvas.Children.Add(Window4Stack);
                                Window4Canvas.Children.Add(Window4GridForButtons);
                                Window4Canvas.Children.Add(Window4ScoreName);
                                Content = Window4Canvas;
                                Canvas.SetLeft(Window4ScoreName, (Window4Canvas.Width / 6) +410);
                                Canvas.SetTop(Window4ScoreName, (Window4Canvas.Height / 1.5) - 350);
                                //клик по кнопке MainMenu на четвертом экране
                                Window4MainMenu.Click += delegate
                                {
                                    Content = Window1Canvas;
                                };
                                //клик по кнопке Save на четвертом экране
                                Window4Save.Click += delegate
                                {
                                    if (LanguageValue == 1)
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
                                    }
                                    if (LanguageValue == 2)
                                    {
                                        File.AppendAllText("Angry birds)) Data.txt", System.Convert.ToString(CurrentDate) + Environment.NewLine);
                                        File.AppendAllText("Angry birds)) Data.txt", "Дистанция: ");
                                        File.AppendAllText("Angry birds)) Data.txt", System.Convert.ToString(MaxLenght) + Environment.NewLine);
                                        File.AppendAllText("Angry birds)) Data.txt", "Время полёта: ");
                                        File.AppendAllText("Angry birds)) Data.txt", System.Convert.ToString(LastPoint * 10 / 1000) + Environment.NewLine);
                                        File.AppendAllText("Angry birds)) Data.txt", "Наивысшая точка: ");
                                        File.AppendAllText("Angry birds)) Data.txt", System.Convert.ToString(MaxHeight) + Environment.NewLine);
                                        File.AppendAllText("Angry birds)) Data.txt", "Начальный угол: ");
                                        File.AppendAllText("Angry birds)) Data.txt", System.Convert.ToString(Window2Angle.Text) + Environment.NewLine);
                                        File.AppendAllText("Angry birds)) Data.txt", "Начальная скорость: ");
                                        File.AppendAllText("Angry birds)) Data.txt", System.Convert.ToString(Window2InitialVelocity.Text) + Environment.NewLine);
                                        File.AppendAllText("Angry birds)) Data.txt", Environment.NewLine);
                                        MessageBox.Show("Ваши данные сохранены!", "Успешно.");
                                    }
                                };
                            };
                        };
                    }
                    else
                    {
                        if (LanguageValue == 1)
                        {
                            MessageBox.Show("Your data is not correct! Please try again.", "Error.");
                        }
                        if (LanguageValue == 2)
                        {
                            MessageBox.Show("Ваши данные неверны! Пожалуйста, попробуйте еще раз.", "Ошибка.");
                        }
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