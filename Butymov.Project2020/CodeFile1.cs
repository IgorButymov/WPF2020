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
using System.Media;

namespace Butymov.Project2020
{
    public class MainWindow : Window
    {
        //User'sData.txt
        //первое число - язык
        //1 - английский, 2 - русский 
        //второе число - птица
        //1 - красная, 2 - синяя
        //третье число - тема
        //1 - светлая, 2 - тёмная
        //четвертое число - звуки
        //0 - без звука, 1 - со звуком

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
        Image ImageWindow1Background;
        Image ImageWindow2Background;
        Image ImageWindow4Background;
        Image ImageWindowSettingsBackground;
        Image ImageBackground;
        Image ImageLightBackground;
        Image ImageDarkBackground;
        Image ImageTheBird;
        Image ImageRedBird;
        Image ImageBlueBird;
        Image ImageThemeIcon;
        Image ImageThemeIconSun;
        Image ImageThemeIconMoon;
        Image ImageBackgroundForAll;
        Image ImageLightBackgroundForAll;
        Image ImageDarkBackgroundForAll;
        BitmapImage BitmapImageRedBird;
        BitmapImage BitmapImageBlueBird;
        BitmapImage BitmapImageIconSun;
        BitmapImage BitmapImageIconMoon;
        BitmapImage BitmapImageLightBackgroundForAll;
        BitmapImage BitmapImageDarkBackgroundForAll;
        BitmapImage BitmapImageLightBackground;
        BitmapImage BitmapImageDarkBackground;
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
        TextBlock WindowSettingsTextBlockSounds;
        Canvas WindowSettingsCanvas;
        StackPanel WindowSettingsStack;
        int BirdValue; //1 - красная, 2 - синяя
        int LanguageValue; //1 - английский, 2 - русский
        int ThemeValue; //1 - светлая, 2 - темная
        int SoundsValue; //0 - без звука, 1 - со звуком
        Image ImageSettingsTheBird;
        Image ImageSettingsRedBird;
        Image ImageSettingsBlueBird;
        Image ImageWindow2TheBird;
        Image ImageWindow2RedBird;
        Image ImageWindow2BlueBird;
        Image ImageWinScreen;
        Image ImageBasketVisible;
        Image ImageBasketInvisible;
        Image ImageWindow2LightBackground;
        Image ImageWindow2DarkBackground;
        Image ImageEnglishWinScreen;
        Image ImageRussianWinScreen;
        BitmapImage BitmapImageEnglishWinScreen;
        BitmapImage BitmapImageRussianWinScreen;
        BitmapImage BitmapImageWindow2RedBird;
        BitmapImage BitmapImageWindow2BlueBird;
        BitmapImage BitmapImageWindow2LightBackground;
        BitmapImage BitmapImageWindow2DarkBackground;
        BitmapImage BitmapImageBasketVisible;
        BitmapImage BitmapImageBasketInvisible;
        BitmapImage BitmapImageSettingsRedBird;
        BitmapImage BitmapImageSettingsBlueBird;
        TextBlock WindowSettingsTextBlockBird;
        TextBlock WindowSettingsTextBlockLanguage;
        TextBlock WindowSettingsTextBlockTheme;
        Image SpecialForImageWindow4LightBackground;
        Image SpecialForImageWindow4DarkBackground;
        BitmapImage SpecialForBitmapImageWindow4LightBackground;
        BitmapImage SpecialForBitmapImageWindow4DarkBackground;
        Image ImageSounds;
        Image ImageSoundsOn;
        BitmapImage BitmapImageSoundsOn;
        Image ImageSoundsOff;
        BitmapImage BitmapImageSoundsOff;
        double MaxHeight;
        double MaxLenght;
        int RandomXMax;
        int RandomXMin;
        int RandomX;
        int TextBlocksFontSize;
        int ButtonsHeight;
        int ButtonsWidth;
        int ButtonsMargin;
        int WindowGridMargin;
        int WidthForElements;
        double[] CosTable;
        double[] SinTable;
        string UD;
        
        SoundPlayer SoundPlayerStartGame;
        SoundPlayer SoundPlayerFalling;
        SoundPlayer SoundPlayerWinGame;
        SoundPlayer SoundPlayerButtonClick;
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new MainWindow());
        }

        public MainWindow()
        {
            //работа со звуком
            //начало игры
            SoundPlayerStartGame = new SoundPlayer();
            SoundPlayerStartGame.SoundLocation = @"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Sounds\StartGame.wav";
            SoundPlayerStartGame.Load();
            //падение
            SoundPlayerFalling = new SoundPlayer();
            SoundPlayerFalling.SoundLocation = @"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Sounds\Falling.wav";
            SoundPlayerFalling.Load();
            //победа
            SoundPlayerWinGame = new SoundPlayer();
            SoundPlayerWinGame.SoundLocation = @"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Sounds\WinGame.wav";
            SoundPlayerStartGame.Load();
            //клик по кнопке
            SoundPlayerButtonClick = new SoundPlayer();
            SoundPlayerButtonClick.SoundLocation = @"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Sounds\ButtonClick.wav";
            SoundPlayerButtonClick.Load();
            //работа с файлом User'sData.txt
            const string UDPath = @"C:\Users\pc\source\repos\Butymov.Project2020\User'sData.txt";
            IsUDFileExists();
            //чтение из файла
            string[] ReadUD = File.ReadAllLines(UDPath);
            string CheckReadUD = ReadUD[0];
            //проверка корректных данных в файле
            if (CheckReadUD.Length != 4)
            {
                var UDFile = File.Create(UDPath);
                UDFile.Close();
                const string ValueForDataIfUDFileDoNotExist = "1111";
                File.AppendAllText(UDPath, ValueForDataIfUDFileDoNotExist);
                ReadUD = File.ReadAllLines(UDPath);
                MessageBox.Show("Data file was not correct, settings have been reset.", "Warning.");
            }
            UD = System.Convert.ToString(ReadUD[0]);
            //функция, отвечающая за установку значений языка, птицы, темы, звуков при запуске
            void SetDataValue()
            {
                if (UD[0] == '1')
                {
                    LanguageValue = 1;
                }
                if (UD[0] == '2')
                {
                    LanguageValue = 2;
                }

                if (UD[1] == '1')
                {
                    BirdValue = 1;
                }
                if (UD[1] == '2')
                {
                    BirdValue = 2;
                }

                if (UD[2] == '1')
                {
                    ThemeValue = 1;
                }
                if (UD[2] == '2')
                {
                    ThemeValue = 2;
                }

                if (UD[3] == '0')
                {
                    SoundsValue = 0;
                }
                if (UD[3] == '1')
                {
                    SoundsValue = 1;
                }
                if ((UD[0] != '1') && (UD[0] != '2'))
                {
                    var UDFile = File.Create(UDPath);
                    UDFile.Close();
                    const string ValueForDataIfUDFileDoNotExist = "111";
                    File.AppendAllText(UDPath, ValueForDataIfUDFileDoNotExist);
                    MessageBox.Show("Data was not correct, settings have been reset.", "Warning.");
                    LanguageValue = 1;
                    BirdValue = 1;
                    ThemeValue = 1;
                    SoundsValue = 1;
                }
                if ((UD[1] != '1') && (UD[1] != '2'))
                {
                    var UDFile = File.Create(UDPath);
                    UDFile.Close();
                    const string ValueForDataIfUDFileDoNotExist = "111";
                    File.AppendAllText(UDPath, ValueForDataIfUDFileDoNotExist);
                    MessageBox.Show("Data was not correct, settings have been reset.", "Warning.");
                    LanguageValue = 1;
                    BirdValue = 1;
                    ThemeValue = 1;
                    SoundsValue = 1;
                }
                if ((UD[2] != '1') && (UD[2] != '2'))
                {
                    var UDFile = File.Create(UDPath);
                    UDFile.Close();
                    const string ValueForDataIfUDFileDoNotExist = "111";
                    File.AppendAllText(UDPath, ValueForDataIfUDFileDoNotExist);
                    MessageBox.Show("Data was not correct, settings have been reset.", "Warning.");
                    LanguageValue = 1;
                    BirdValue = 1;
                    ThemeValue = 1;
                    SoundsValue = 1;
                }
                if ((UD[3] != '0') && (UD[3] != '1'))
                {
                    var UDFile = File.Create(UDPath);
                    UDFile.Close();
                    const string ValueForDataIfUDFileDoNotExist = "1111";
                    File.AppendAllText(UDPath, ValueForDataIfUDFileDoNotExist);
                    MessageBox.Show("Data was not correct, settings have been reset.", "Warning.");
                    LanguageValue = 1;
                    BirdValue = 1;
                    ThemeValue = 1;
                    SoundsValue = 1;
                }

            }
            //функция, отвечающая за установку языка, птицы, темы, звуков при запуске
            void SetData()
            {
                switch (LanguageValue)
                {
                    case 1:
                        ImageWinScreen = ImageEnglishWinScreen;
                        ImageTheFlag = ImageBritishFlag;
                        break;
                    case 2:
                        ImageWinScreen = ImageRussianWinScreen;
                        ImageTheFlag = ImageRussianFlag;
                        break;
                }
                switch (BirdValue)
                {
                    case 1:
                        ImageTheBird = ImageRedBird;
                        ImageSettingsTheBird = ImageSettingsRedBird;
                        ImageWindow2TheBird = ImageWindow2RedBird;
                        break;
                    case 2:
                        ImageTheBird = ImageBlueBird;
                        ImageSettingsTheBird = ImageSettingsBlueBird;
                        ImageWindow2TheBird = ImageWindow2BlueBird;
                        break;
                }
                switch (ThemeValue)
                {
                    case 1:
                        ImageBackgroundForAll = ImageLightBackgroundForAll;
                        ImageWindow2Background = ImageWindow2LightBackground;
                        ImageBackground = ImageLightBackground;
                        ImageThemeIcon = ImageThemeIconSun;
                        break;
                    case 2:
                        ImageBackgroundForAll = ImageDarkBackgroundForAll;
                        ImageWindow2Background = ImageWindow2DarkBackground;
                        ImageBackground = ImageDarkBackground;
                        ImageThemeIcon = ImageThemeIconMoon;
                        break;
                }
                switch(SoundsValue)
                {
                    case 1:
                        ImageSounds = ImageSoundsOn;
                        break;
                    case 0:
                        ImageSounds = ImageSoundsOff;
                        break;
                }
            }
            //функция, отвечающая за переписывание файла с данными (вызывается при нажатии на кнопку Language, Bird, Theme, Sounds)
            void WriteNewDataToFile()
            {
                var UDFile = File.Create(UDPath);
                UDFile.Close();
                char LanguageValueForData = System.Convert.ToChar(LanguageValue);
                char BirdValueForData = System.Convert.ToChar(BirdValue);
                char ThemeValueForData = System.Convert.ToChar(ThemeValue);
                string ValueFordata = "";
                ValueFordata += LanguageValue;
                ValueFordata += BirdValue;
                ValueFordata += ThemeValue;
                ValueFordata += SoundsValue;
                File.AppendAllText(UDPath, ValueFordata);
            }
            //функция, отвечающая за проверку существования файла
            bool IsUDFileExists()
            {
                if (!File.Exists(UDPath))
                {
                    var UDFile = File.Create(UDPath);
                    UDFile.Close();
                    const string ValueForDataIfUDFileDoNotExist = "1111";
                    File.AppendAllText(UDPath, ValueForDataIfUDFileDoNotExist);
                    MessageBox.Show("Data file did not exist, settings have been reset.", "Warning.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            //разрешение экрана, установка размеров элементов 
            double Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            double Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            TextBlocksFontSize = System.Convert.ToInt16(Width / 57);
            ButtonsHeight = System.Convert.ToInt16(Height / 15.5);
            ButtonsWidth = System.Convert.ToInt16(Width / 7);
            ButtonsMargin = System.Convert.ToInt16(Width / 27.5);
            WindowGridMargin = System.Convert.ToInt16(Width / 6.8);
            WidthForElements = System.Convert.ToInt16(Width / 2);

            //значения cos и sin
            //dpva.ru/Guide/GuideMathematics/GuideMathematicsFiguresTables/CosinusTable0to360by1/
            //dpva.ru/Guide/GuideMathematics/GuideMathematicsFiguresTables/SinusTable0to360by1/
            CosTable = new double[181] { 1, 0.9998, 0.9994, 0.9986, 0.9976, 0.9962, 0.9945, 0.9925, 0.9903, 0.9877, 0.9848, 0.9816, 0.9781, 0.9744, 0.9703, 0.9659, 0.9613, 0.9563, 0.9511, 0.9455, 0.9397, 0.9336, 0.9272, 0.9205, 0.9135, 0.9063, 0.8988, 0.891, 0.8829, 0.8746, 0.866, 0.8572, 0.848, 0.8387, 0.829, 0.8192, 0.809, 0.7986, 0.788, 0.7771, 0.766, 0.7547, 0.7431, 0.7314, 0.7193, 0.7071, 0.6947, 0.682, 0.6691, 0.6561, 0.6428, 0.6293, 0.6157, 0.6018, 0.5878, 0.5736, 0.5592, 0.5446, 0.5299, 0.515, 0.5, 0.4848, 0.4695, 0.454, 0.4384, 0.4226, 0.4067, 0.3907, 0.3746, 0.3584, 0.342, 0.3256, 0.309, 0.2924, 0.2756, 0.2588, 0.2419, 0.225, 0.2079, 0.1908, 0.1736, 0.1564, 0.1392, 0.1219, 0.1045, 0.0872, 0.0698, 0.0523, 0.0349, 0.0175, 0, -0.0175, -0.0349, -0.0523, -0.0698, -0.0872, -0.1045, -0.1219, -0.1392, -0.1564, -0.1736, -0.1908, -0.2079, -0.225, -0.2419, -0.2588, -0.2756, -0.2924, -0.309, -0.3256, -0.342, -0.3584, -0.3746, -0.3907, -0.4067, -0.4226, -0.4384, -0.454, -0.4695, -0.4848, -0.5, -0.515, -0.5299, -0.5446, -0.5592, -0.5736, -0.5878, -0.6018, -0.6157, -0.6293, -0.6428, -0.6561, -0.6691, -0.682, -0.6947, -0.7071, -0.7193, -0.7314, -0.7431, -0.7547, -0.766, -0.7771, -0.788, -0.7986, -0.809, -0.8192, -0.829, -0.8387, -0.848, -0.8572, -0.866, -0.8746, -0.8829, -0.891, -0.8988, -0.9063, -0.9135, -0.9205, -0.9272, -0.9336, -0.9397, -0.9455, -0.9511, -0.9563, -0.9613, -0.9659, -0.9703, -0.9744, -0.9781, -0.9816, -0.9848, -0.9877, -0.9903, -0.9925, -0.9945, -0.9962, -0.9976, -0.9986, -0.9994, -0.9998, -1 };
            SinTable = new double[181] { 0, 0.0175, 0.0349, 0.0523, 0.0698, 0.0872, 0.1045, 0.1219, 0.1392, 0.1564, 0.1736, 0.1908, 0.2079, 0.225, 0.2419, 0.2588, 0.2756, 0.2924, 0.309, 0.3256, 0.342, 0.3584, 0.3746, 0.3907, 0.4067, 0.4226, 0.4384, 0.454, 0.4695, 0.4848, 0.5, 0.515, 0.5299, 0.5446, 0.5592, 0.5736, 0.5878, 0.6018, 0.6157, 0.6293, 0.6428, 0.6561, 0.6691, 0.682, 0.6947, 0.7071, 0.7193, 0.7314, 0.7431, 0.7547, 0.766, 0.7771, 0.788, 0.7986, 0.809, 0.8192, 0.829, 0.8387, 0.848, 0.8572, 0.866, 0.8746, 0.8829, 0.891, 0.8988, 0.9063, 0.9135, 0.9205, 0.9272, 0.9336, 0.9397, 0.9455, 0.9511, 0.9563, 0.9613, 0.9659, 0.9703, 0.9744, 0.9781, 0.9816, 0.9848, 0.9877, 0.9903, 0.9925, 0.9945, 0.9962, 0.9976, 0.9986, 0.9994, 0.9998, 1, 0.9998, 0.9994, 0.9986, 0.9976, 0.9962, 0.9945, 0.9925, 0.9903, 0.9877, 0.9848, 0.9816, 0.9781, 0.9744, 0.9703, 0.9659, 0.9613, 0.9563, 0.9511, 0.9455, 0.9397, 0.9336, 0.9272, 0.9205, 0.9135, 0.9063, 0.8988, 0.891, 0.8829, 0.8746, 0.866, 0.8572, 0.848, 0.8387, 0.829, 0.8192, 0.809, 0.7986, 0.788, 0.7771, 0.766, 0.7547, 0.7431, 0.7314, 0.7193, 0.7071, 0.6947, 0.682, 0.6691, 0.6561, 0.6428, 0.6293, 0.6157, 0.6018, 0.5878, 0.5736, 0.5592, 0.5446, 0.5299, 0.515, 0.5, 0.4848, 0.4695, 0.454, 0.4384, 0.4226, 0.4067, 0.3907, 0.3746, 0.3584, 0.342, 0.3256, 0.309, 0.2924, 0.2756, 0.2588, 0.2419, 0.225, 0.2079, 0.1908, 0.1736, 0.1564, 0.1392, 0.1219, 0.1045, 0.0872, 0.0698, 0.0523, 0.0349, 0.0175, 0 };

            //счётчик попыток
            int TriesCounter = 0;
            int IsWin = 0; //0 - не выиграл, 1 - выиграл

            //размеры окна
            this.WindowState = WindowState.Maximized;
            /*//фон на четвертом окне
            SpecialForImageWindow4LightBackground = new Image();
            SpecialForImageWindow4LightBackground.Width = System.Convert.ToInt16(Width * 1.3);
            SpecialForBitmapImageWindow4LightBackground = new BitmapImage();
            SpecialForBitmapImageWindow4LightBackground.BeginInit();
            SpecialForBitmapImageWindow4LightBackground.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\LightBackgroundForAll.png");
            SpecialForBitmapImageWindow4LightBackground.DecodePixelWidth = System.Convert.ToInt16(Width * 1.3);
            SpecialForBitmapImageWindow4LightBackground.EndInit();
            SpecialForImageWindow4LightBackground.Source = SpecialForBitmapImageWindow4LightBackground;
            SpecialForImageWindow4LightBackground.Margin = new Thickness(0);

            SpecialForImageWindow4DarkBackground = new Image();
            SpecialForImageWindow4DarkBackground.Width = System.Convert.ToInt16(Width * 1.3);
            SpecialForBitmapImageWindow4DarkBackground = new BitmapImage();
            SpecialForBitmapImageWindow4DarkBackground.BeginInit();
            SpecialForBitmapImageWindow4DarkBackground.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\DarkBackgroundForAll.png");
            SpecialForBitmapImageWindow4DarkBackground.DecodePixelWidth = System.Convert.ToInt16(Width * 1.3);
            SpecialForBitmapImageWindow4DarkBackground.EndInit();
            SpecialForImageWindow4DarkBackground.Source = SpecialForBitmapImageWindow4DarkBackground;
            SpecialForImageWindow4DarkBackground.Margin = new Thickness(0);*/
            SpecialForBitmapImageWindow4LightBackground = new BitmapImage();
            SpecialForBitmapImageWindow4LightBackground.BeginInit();
            SpecialForBitmapImageWindow4LightBackground.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\LightBackgroundForAll.png");
            SpecialForBitmapImageWindow4LightBackground.DecodePixelWidth = System.Convert.ToInt16(Width * 1.3);
            SpecialForBitmapImageWindow4LightBackground.EndInit();
            SpecialForBitmapImageWindow4DarkBackground = new BitmapImage();
            SpecialForBitmapImageWindow4DarkBackground.BeginInit();
            SpecialForBitmapImageWindow4DarkBackground.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\DarkBackgroundForAll.png");
            SpecialForBitmapImageWindow4DarkBackground.DecodePixelWidth = System.Convert.ToInt16(Width * 1.3);
            SpecialForBitmapImageWindow4DarkBackground.EndInit();
            //иконки звуков
            ImageSounds = new Image();
            ImageSounds.Width = WidthForElements;
            //звук включен
            ImageSoundsOn = new Image();
            ImageSoundsOn.Width = WidthForElements;
            BitmapImageSoundsOn = new BitmapImage();
            BitmapImageSoundsOn.BeginInit();
            BitmapImageSoundsOn.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\SoundsOn.png");
            BitmapImageSoundsOn.DecodePixelWidth = WidthForElements;
            BitmapImageSoundsOn.EndInit();
            ImageSoundsOn.Source = BitmapImageSoundsOn;
            ImageSoundsOn.Margin = new Thickness(0);
            //звук выключен
            ImageSoundsOff = new Image();
            ImageSoundsOff.Width = WidthForElements;
            BitmapImageSoundsOff = new BitmapImage();
            BitmapImageSoundsOff.BeginInit();
            BitmapImageSoundsOff.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\SoundsOff.png");
            BitmapImageSoundsOff.DecodePixelWidth = WidthForElements;
            BitmapImageSoundsOff.EndInit();
            ImageSoundsOff.Source = BitmapImageSoundsOff;
            ImageSoundsOff.Margin = new Thickness(0);
            //птицы для второго окна
            ImageWindow2TheBird = new Image();
            ImageWindow2TheBird.Width = WidthForElements;
            //красная птица для второго окна
            ImageWindow2RedBird = new Image();
            ImageWindow2RedBird.Width = WidthForElements;
            BitmapImageWindow2RedBird = new BitmapImage();
            BitmapImageWindow2RedBird.BeginInit();
            BitmapImageWindow2RedBird.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\RedBird.png");
            BitmapImageWindow2RedBird.DecodePixelWidth = WidthForElements;
            BitmapImageWindow2RedBird.EndInit();
            ImageWindow2RedBird.Source = BitmapImageWindow2RedBird;
            ImageWindow2RedBird.Margin = new Thickness(0);
            //синяя птица для второго окна
            ImageWindow2BlueBird = new Image();
            ImageWindow2BlueBird.Width = WidthForElements;
            BitmapImageWindow2BlueBird = new BitmapImage();
            BitmapImageWindow2BlueBird.BeginInit();
            BitmapImageWindow2BlueBird.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\BlueBird.png");
            BitmapImageWindow2BlueBird.DecodePixelWidth = WidthForElements;
            BitmapImageWindow2BlueBird.EndInit();
            ImageWindow2BlueBird.Source = BitmapImageWindow2BlueBird;
            ImageWindow2BlueBird.Margin = new Thickness(0);
            //птица для второго окна по умолчанию
            ImageWindow2TheBird = ImageWindow2RedBird;
            //видимая часть корзины
            ImageBasketVisible = new Image();
            ImageBasketVisible.Width = WidthForElements;
            BitmapImageBasketVisible = new BitmapImage();
            BitmapImageBasketVisible.BeginInit();
            BitmapImageBasketVisible.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\BasketVisible.png");
            BitmapImageBasketVisible.DecodePixelWidth = WidthForElements;
            BitmapImageBasketVisible.EndInit();
            ImageBasketVisible.Source = BitmapImageBasketVisible;
            ImageBasketVisible.Margin = new Thickness(0);
            //невидимая часть корзины
            ImageBasketInvisible = new Image();
            ImageBasketInvisible.Width = WidthForElements;
            BitmapImageBasketInvisible = new BitmapImage();
            BitmapImageBasketInvisible.BeginInit();
            BitmapImageBasketInvisible.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\BasketInvisible.png");
            BitmapImageBasketInvisible.DecodePixelWidth = WidthForElements;
            BitmapImageBasketInvisible.EndInit();
            ImageBasketInvisible.Source = BitmapImageBasketInvisible;
            ImageBasketInvisible.Margin = new Thickness(0);
            //победный экран
            ImageWinScreen = new Image();
            ImageWinScreen.Width = System.Convert.ToInt16(Width * 1.3);
            //победный экран на англиском
            ImageEnglishWinScreen = new Image();
            ImageEnglishWinScreen.Width = System.Convert.ToInt16(Width * 1.3);
            BitmapImageEnglishWinScreen = new BitmapImage();
            BitmapImageEnglishWinScreen.BeginInit();
            BitmapImageEnglishWinScreen.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\EnglishWinScreen.png");
            BitmapImageEnglishWinScreen.DecodePixelWidth = System.Convert.ToInt16(Width * 1.3);
            BitmapImageEnglishWinScreen.EndInit();
            ImageEnglishWinScreen.Source = BitmapImageEnglishWinScreen;
            ImageEnglishWinScreen.Margin = new Thickness(0);
            //победный экран на русском
            ImageRussianWinScreen = new Image();
            ImageRussianWinScreen.Width = System.Convert.ToInt16(Width * 1.3);
            BitmapImageRussianWinScreen = new BitmapImage();
            BitmapImageRussianWinScreen.BeginInit();
            BitmapImageRussianWinScreen.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\RussianWinScreen.png");
            BitmapImageRussianWinScreen.DecodePixelWidth = System.Convert.ToInt16(Width * 1.3);
            BitmapImageRussianWinScreen.EndInit();
            ImageRussianWinScreen.Source = BitmapImageRussianWinScreen;
            ImageRussianWinScreen.Margin = new Thickness(0);
            //фон для окон
            ImageBackgroundForAll = new Image();
            ImageBackgroundForAll.Width = System.Convert.ToInt16(Width * 1.3);
            //светлый фон для окон
            ImageLightBackgroundForAll = new Image();
            ImageLightBackgroundForAll.Width = System.Convert.ToInt16(Width * 1.3);
            BitmapImageLightBackgroundForAll = new BitmapImage();
            BitmapImageLightBackgroundForAll.BeginInit();
            BitmapImageLightBackgroundForAll.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\LightBackgroundForAll.png");
            BitmapImageLightBackgroundForAll.DecodePixelWidth = System.Convert.ToInt16(Width * 1.3);
            BitmapImageLightBackgroundForAll.EndInit();
            ImageLightBackgroundForAll.Source = BitmapImageLightBackgroundForAll;
            ImageLightBackgroundForAll.Margin = new Thickness(0);
            //темный фон для окон
            ImageDarkBackgroundForAll = new Image();
            ImageDarkBackgroundForAll.Width = System.Convert.ToInt16(Width * 1.3);
            BitmapImageDarkBackgroundForAll = new BitmapImage();
            BitmapImageDarkBackgroundForAll.BeginInit();
            BitmapImageDarkBackgroundForAll.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\DarkBackgroundForAll.png");
            BitmapImageDarkBackgroundForAll.DecodePixelWidth = System.Convert.ToInt16(Width * 1.3);
            BitmapImageDarkBackgroundForAll.EndInit();
            ImageDarkBackgroundForAll.Source = BitmapImageDarkBackgroundForAll;
            ImageDarkBackgroundForAll.Margin = new Thickness(0);
            //фон для второго окна
            ImageWindow2LightBackground = new Image();
            ImageWindow2LightBackground.Width = System.Convert.ToInt16(Width * 1.3);
            BitmapImageWindow2LightBackground = new BitmapImage();
            BitmapImageWindow2LightBackground.BeginInit();
            BitmapImageWindow2LightBackground.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\LightBackground.png");
            BitmapImageWindow2LightBackground.DecodePixelWidth = System.Convert.ToInt16(Width * 1.3);
            BitmapImageWindow2LightBackground.EndInit();
            ImageWindow2LightBackground.Source = BitmapImageWindow2LightBackground;
            ImageWindow2LightBackground.Margin = new Thickness(0);

            ImageWindow2DarkBackground = new Image();
            ImageWindow2DarkBackground.Width = System.Convert.ToInt16(Width * 1.3);
            BitmapImageWindow2DarkBackground = new BitmapImage();
            BitmapImageWindow2DarkBackground.BeginInit();
            BitmapImageWindow2DarkBackground.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\DarkBackground.png");
            BitmapImageWindow2DarkBackground.DecodePixelWidth = System.Convert.ToInt16(Width * 1.3);
            BitmapImageWindow2DarkBackground.EndInit();
            ImageWindow2DarkBackground.Source = BitmapImageWindow2DarkBackground;
            ImageWindow2DarkBackground.Margin = new Thickness(0);
            
            //иконка темы
            ImageThemeIcon = new Image();
            ImageThemeIcon.Width = WidthForElements;
            //иконка солнца для темы
            ImageThemeIconSun = new Image();
            ImageThemeIconSun.Width = WidthForElements;
            BitmapImageIconSun = new BitmapImage();
            BitmapImageIconSun.BeginInit();
            BitmapImageIconSun.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\SunSettings.png");
            BitmapImageIconSun.DecodePixelWidth = WidthForElements;
            BitmapImageIconSun.EndInit();
            ImageThemeIconSun.Source = BitmapImageIconSun;
            ImageThemeIconSun.Margin = new Thickness(0);
            //иконка луны для темы
            ImageThemeIconMoon = new Image();
            ImageThemeIconMoon.Width = WidthForElements;
            BitmapImageIconMoon = new BitmapImage();
            BitmapImageIconMoon.BeginInit();
            BitmapImageIconMoon.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\MoonSettings.png");
            BitmapImageIconMoon.DecodePixelWidth = WidthForElements;
            BitmapImageIconMoon.EndInit();
            ImageThemeIconMoon.Source = BitmapImageIconMoon;
            ImageThemeIconMoon.Margin = new Thickness(0);
            //фон для игры
            ImageBackground = new Image();
            ImageBackground.Width = System.Convert.ToInt16(Width * 1.3);
            //светлый фон для игры
            ImageLightBackground = new Image();
            ImageLightBackground.Width = System.Convert.ToInt16(Width * 1.3);
            BitmapImageLightBackground = new BitmapImage();
            BitmapImageLightBackground.BeginInit();
            BitmapImageLightBackground.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\LightBackground.png");
            BitmapImageLightBackground.DecodePixelWidth = System.Convert.ToInt16(Width * 1.3);
            BitmapImageLightBackground.EndInit();
            ImageLightBackground.Source = BitmapImageLightBackground;
            ImageLightBackground.Margin = new Thickness(0);
            //темный фон для игры
            ImageDarkBackground = new Image();
            ImageDarkBackground.Width = System.Convert.ToInt16(Width * 1.3);
            BitmapImageDarkBackground = new BitmapImage();
            BitmapImageDarkBackground.BeginInit();
            BitmapImageDarkBackground.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\DarkBackground.png");
            BitmapImageDarkBackground.DecodePixelWidth = System.Convert.ToInt16(Width * 1.3);
            BitmapImageDarkBackground.EndInit();
            ImageDarkBackground.Source = BitmapImageDarkBackground;
            ImageDarkBackground.Margin = new Thickness(0);
            //объекты флага
            ImageTheFlag = new Image();
            ImageTheFlag.Width = WidthForElements;
            //иконки флага
            //российский флаг
            ImageRussianFlag = new Image();
            ImageRussianFlag.Width = WidthForElements;
            BitmapImageRussianFlag = new BitmapImage();
            BitmapImageRussianFlag.BeginInit();
            BitmapImageRussianFlag.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\RussianFlag.png");
            BitmapImageRussianFlag.DecodePixelWidth = WidthForElements;
            BitmapImageRussianFlag.EndInit();
            ImageRussianFlag.Source = BitmapImageRussianFlag;
            ImageRussianFlag.Margin = new Thickness(0);
            //британский флаг
            ImageBritishFlag = new Image();
            ImageBritishFlag.Width = WidthForElements;
            BitmapImageBritishFlag = new BitmapImage();
            BitmapImageBritishFlag.BeginInit();
            BitmapImageBritishFlag.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\BritishFlag.png");
            BitmapImageBritishFlag.DecodePixelWidth = WidthForElements;
            BitmapImageBritishFlag.EndInit();
            ImageBritishFlag.Source = BitmapImageBritishFlag;
            ImageBritishFlag.Margin = new Thickness(0);
            //объекты птиц
            //птица для игры
            ImageTheBird = new Image();
            ImageTheBird.Width = WidthForElements;
            //птица для настроек
            ImageSettingsTheBird = new Image();
            ImageSettingsTheBird.Width = WidthForElements;
            //иконки птицы
            //красная птица
            ImageRedBird = new Image();
            ImageRedBird.Width = WidthForElements;
            BitmapImageRedBird = new BitmapImage();
            BitmapImageRedBird.BeginInit();
            BitmapImageRedBird.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\RedBird.png");
            BitmapImageRedBird.DecodePixelWidth = WidthForElements;
            BitmapImageRedBird.EndInit();
            ImageRedBird.Source = BitmapImageRedBird;
            ImageRedBird.Margin = new Thickness(0);
            //синяя птица
            ImageBlueBird = new Image();
            ImageBlueBird.Width = WidthForElements;
            BitmapImageBlueBird = new BitmapImage();
            BitmapImageBlueBird.BeginInit();
            BitmapImageBlueBird.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\BlueBird.png");
            BitmapImageBlueBird.DecodePixelWidth = WidthForElements;
            BitmapImageBlueBird.EndInit();
            ImageBlueBird.Source = BitmapImageBlueBird;
            ImageBlueBird.Margin = new Thickness(0);
            //красная птица для настроек
            ImageSettingsRedBird = new Image();
            ImageSettingsRedBird.Width = WidthForElements;
            BitmapImageSettingsRedBird = new BitmapImage();
            BitmapImageSettingsRedBird.BeginInit();
            BitmapImageSettingsRedBird.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\RedBird.png");
            BitmapImageSettingsRedBird.DecodePixelWidth = WidthForElements;
            BitmapImageSettingsRedBird.EndInit();
            ImageSettingsRedBird.Source = BitmapImageSettingsRedBird;
            ImageSettingsRedBird.Margin = new Thickness(0);
            //синяя птица для настроек
            ImageSettingsBlueBird = new Image();
            ImageSettingsBlueBird.Width = WidthForElements;
            BitmapImageSettingsBlueBird = new BitmapImage();
            BitmapImageSettingsBlueBird.BeginInit();
            BitmapImageSettingsBlueBird.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\BlueBird.png");
            BitmapImageSettingsBlueBird.DecodePixelWidth = WidthForElements;
            BitmapImageSettingsBlueBird.EndInit();
            ImageSettingsBlueBird.Source = BitmapImageSettingsBlueBird;
            ImageSettingsBlueBird.Margin = new Thickness(0);
            //второе окно
            ImageWindow2Background = new Image();
            ImageWindow2Background.Width = System.Convert.ToInt16(Width * 1.3);
            //работа с файлом
            SetDataValue();
            SetData();
            //установка фона на окна
            ImageWindow1Background = new Image();
            ImageWindow1Background.Width = System.Convert.ToInt16(Width * 1.3);
            ImageWindow1Background = ImageBackgroundForAll;
            //фон экрана настроек по умолчанию
            ImageWindowSettingsBackground = new Image();
            ImageWindowSettingsBackground.Width = System.Convert.ToInt16(Width * 1.3);
            ImageWindowSettingsBackground = ImageBackgroundForAll;
            //клик по кнопке Next на третьем экране 
            //*была неожиданная ошибка, приводившая к обработке событий кол-во раз, равное времени таймера птицы в миллисекундах
            void Window3NextClicked(object sender, RoutedEventArgs e)
            {
                //*устанавливаем пометку в событие, что оно уже обработано, запрещаем последующую обработку в дереве
                e.Handled = true;
                Window3Canvas.Children.Clear();
                PlaySound(SoundPlayerButtonClick);
                
                CurrentDate = new DateTime();
                CurrentDate = DateTime.Now;
                //объявление и параметры кнопки Main menu на четвертом экране
                Button Window4MainMenu = new Button();
                Window4TextBlockMainMenu = new TextBlock();
                Window4TextBlockMainMenu.FontSize = TextBlocksFontSize;
                Window4TextBlockMainMenu.TextAlignment = TextAlignment.Center;
                switch (LanguageValue)
                {
                    case 1:
                        Window4TextBlockMainMenu.Inlines.Add(new Run("MAIN MENU"));
                        break;
                    case 2:
                        Window4TextBlockMainMenu.Inlines.Add(new Run("ГЛАВНОЕ МЕНЮ"));
                        break;
                }
                Window4MainMenu.Content = Window4TextBlockMainMenu;
                Window4MainMenu.Margin = new Thickness(ButtonsMargin);
                Window4MainMenu.Height = ButtonsHeight;
                Window4MainMenu.Width = ButtonsWidth;
                //объявление и параметры кнопки Save на четвертом экране
                Button Window4Save = new Button();
                Window4TextBlockSave = new TextBlock();
                Window4TextBlockSave.FontSize = TextBlocksFontSize;
                Window4TextBlockSave.TextAlignment = TextAlignment.Center;
                switch (LanguageValue)
                {
                    case 1:
                        Window4TextBlockSave.Inlines.Add(new Run("SAVE"));
                        break;
                    case 2:
                        Window4TextBlockSave.Inlines.Add(new Run("СОХРАНИТЬ"));
                        break;
                }
                Window4Save.Content = Window4TextBlockSave;
                Window4Save.Margin = new Thickness(ButtonsMargin);
                Window4Save.Height = ButtonsHeight;
                Window4Save.Width = ButtonsWidth;
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
                Window4GridForButtons.Margin = new Thickness(WindowGridMargin);
                //первый столбец таблицы результатов
                TextBlock Window4Range = new TextBlock();
                Window4Range.FontSize = TextBlocksFontSize;
                Window4Range.TextAlignment = TextAlignment.Center;
                TextBlock Window4FlightTime = new TextBlock();
                Window4FlightTime.FontSize = TextBlocksFontSize;
                Window4FlightTime.TextAlignment = TextAlignment.Center;
                TextBlock Window4HighestPoint = new TextBlock();
                Window4HighestPoint.FontSize = TextBlocksFontSize;
                Window4HighestPoint.TextAlignment = TextAlignment.Center;
                TextBlock Window4InitialAngle = new TextBlock();
                Window4InitialAngle.FontSize = TextBlocksFontSize;
                Window4InitialAngle.TextAlignment = TextAlignment.Center;
                TextBlock Window4InitialSpeed = new TextBlock();
                Window4InitialSpeed.FontSize = TextBlocksFontSize;
                Window4InitialSpeed.TextAlignment = TextAlignment.Center;
                TextBlock Window4ScoreName = new TextBlock();
                Window4ScoreName.FontSize = TextBlocksFontSize;
                Window4ScoreName.TextAlignment = TextAlignment.Center;
                TextBlock Window4Tries = new TextBlock();
                Window4Tries.FontSize = TextBlocksFontSize;
                Window4Tries.TextAlignment = TextAlignment.Center;
                switch (LanguageValue)
                {
                    case 1:
                        Window4ScoreName.Inlines.Add("RESULTS");
                        Window4Range.Inlines.Add(new Run("Range: "));
                        Window4FlightTime.Inlines.Add(new Run("Flight time: "));
                        Window4HighestPoint.Inlines.Add(new Run("Highest point: "));
                        Window4InitialSpeed.Inlines.Add(new Run("Initial speed: "));
                        Window4InitialAngle.Inlines.Add(new Run("Initial angle: "));
                        Window4Tries.Inlines.Add(new Run("Moves to win: "));
                        break;
                    case 2:
                        Window4ScoreName.Inlines.Add("РЕЗУЛЬТАТЫ");
                        Window4Range.Inlines.Add(new Run("Дистанция: "));
                        Window4FlightTime.Inlines.Add(new Run("Время полёта: "));
                        Window4HighestPoint.Inlines.Add(new Run("Наивысшая точка: "));
                        Window4InitialSpeed.Inlines.Add(new Run("Начальная скорость: "));
                        Window4InitialAngle.Inlines.Add(new Run("Начальный угол: "));
                        Window4Tries.Inlines.Add(new Run("Попыток до победы: "));
                        break;
                }
                //второй столбец таблицы результатов
                TextBlock Window4RangeValue = new TextBlock();
                Window4RangeValue.FontSize = TextBlocksFontSize;
                Window4RangeValue.TextAlignment = TextAlignment.Center;
                Window4RangeValue.Inlines.Add(new Run(System.Convert.ToString(Math.Round(MaxLenght, 2))));
                TextBlock Window4FlightTimeValue = new TextBlock();
                Window4FlightTimeValue.FontSize = TextBlocksFontSize;
                Window4FlightTimeValue.TextAlignment = TextAlignment.Center;
                Window4FlightTimeValue.Inlines.Add(new Run(System.Convert.ToString(LastPoint * 10 / 1000)));
                TextBlock Window4HighestPointValue = new TextBlock();
                Window4HighestPointValue.FontSize = TextBlocksFontSize;
                Window4HighestPointValue.TextAlignment = TextAlignment.Center;
                Window4HighestPointValue.Inlines.Add(new Run(System.Convert.ToString(Math.Round(MaxHeight, 2))));
                TextBlock Window4InitialAngleValue = new TextBlock();
                Window4InitialAngleValue.FontSize = TextBlocksFontSize;
                Window4InitialAngleValue.TextAlignment = TextAlignment.Center;
                Window4InitialAngleValue.Inlines.Add(new Run(System.Convert.ToString(Window2Angle.Text)));
                TextBlock Window4InitialSpeedValue = new TextBlock();
                Window4InitialSpeedValue.FontSize = TextBlocksFontSize;
                Window4InitialSpeedValue.TextAlignment = TextAlignment.Center;
                Window4InitialSpeedValue.Inlines.Add(new Run(System.Convert.ToString(Window2InitialVelocity.Text)));
                TextBlock Window4TriesValue = new TextBlock();
                Window4TriesValue.FontSize = TextBlocksFontSize;
                Window4TriesValue.TextAlignment = TextAlignment.Center;
                switch (IsWin)
                {
                    case 0:
                        switch (LanguageValue)
                        {
                            case 1:
                                Window4TriesValue.Inlines.Add(new Run("Did not win"));
                                break;
                            case 2:
                                Window4TriesValue.Inlines.Add(new Run("Не выиграл"));
                                break;
                        }
                        break;
                    case 1:
                        Window4TriesValue.Inlines.Add(new Run(System.Convert.ToString(TriesCounter)));
                        break;
                }
                //объявляение и параметры Grid для таблицы результатов на четвертом экране
                Grid Window4Grid = new Grid();
                Window4Grid.RowDefinitions.Add(new RowDefinition());
                Window4Grid.RowDefinitions.Add(new RowDefinition());
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
                Window4Grid.Children.Add(Window4Tries);
                Grid.SetRow(Window4Tries, 5);
                Grid.SetColumn(Window4Tries, 0);
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
                Window4Grid.Children.Add(Window4TriesValue);
                Grid.SetRow(Window4TriesValue, 5);
                Grid.SetColumn(Window4TriesValue, 1);
                Window4Grid.Margin = new Thickness(WindowGridMargin);
                //объявление и параметры прямоугольника, имитирующего таблицу результатов
                Window4Score = new Rectangle();
                Window4Score.Stroke = System.Windows.Media.Brushes.Black;
                Window4Score.Fill = System.Windows.Media.Brushes.WhiteSmoke;
                Window4Score.Height = Height / 3.84;
                Window4Score.Width = Width / 2.732;
                //фон на четвертом окне
                SpecialForImageWindow4LightBackground = new Image();
                SpecialForImageWindow4LightBackground.Width = System.Convert.ToInt16(Width * 1.3);
                /*SpecialForBitmapImageWindow4LightBackground = new BitmapImage();
                SpecialForBitmapImageWindow4LightBackground.BeginInit();
                SpecialForBitmapImageWindow4LightBackground.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\LightBackgroundForAll.png");
                SpecialForBitmapImageWindow4LightBackground.DecodePixelWidth = System.Convert.ToInt16(Width * 1.3);
                SpecialForBitmapImageWindow4LightBackground.EndInit();*/
                SpecialForImageWindow4LightBackground.Source = SpecialForBitmapImageWindow4LightBackground;
                SpecialForImageWindow4LightBackground.Margin = new Thickness(0);

                SpecialForImageWindow4DarkBackground = new Image();
                SpecialForImageWindow4DarkBackground.Width = System.Convert.ToInt16(Width * 1.3);
                /*SpecialForBitmapImageWindow4DarkBackground = new BitmapImage();
                SpecialForBitmapImageWindow4DarkBackground.BeginInit();
                SpecialForBitmapImageWindow4DarkBackground.UriSource = new Uri(@"C:\Users\pc\source\repos\Butymov.Project2020\Hit the basket.Images\DarkBackgroundForAll.png");
                SpecialForBitmapImageWindow4DarkBackground.DecodePixelWidth = System.Convert.ToInt16(Width * 1.3);
                SpecialForBitmapImageWindow4DarkBackground.EndInit();*/
                SpecialForImageWindow4DarkBackground.Source = SpecialForBitmapImageWindow4DarkBackground;
                SpecialForImageWindow4DarkBackground.Margin = new Thickness(0);
                //четертое окно
                ImageWindow4Background = new Image();
                ImageWindow4Background.Width = System.Convert.ToInt16(Width * 1.3);
                switch (ThemeValue)
                {
                    case 1:
                        ImageWindow4Background = SpecialForImageWindow4LightBackground;
                        break;
                    case 2:
                        ImageWindow4Background = SpecialForImageWindow4DarkBackground;
                        break;
                }
                //объявление и параметры StackPanel на четвертом экране
                Window4Stack = new StackPanel();
                Window4Stack.Children.Add(Window4Grid);
                //объявление и параметры Canvas на четвертом экране
                Window4Canvas = new Canvas();
                Window4Canvas.Height = SystemParameters.VirtualScreenHeight;
                Window4Canvas.Width = SystemParameters.VirtualScreenWidth;
                Window4Canvas.Children.Add(ImageWindow4Background);
                Window4Canvas.Children.Add(Window4Score);
                Window4Canvas.Children.Add(Window4Stack);
                Window4Canvas.Children.Add(Window4GridForButtons);
                Window4Canvas.Children.Add(Window4ScoreName);
                Content = Window4Canvas;
                Canvas.SetLeft(Window4ScoreName, (Width / 2.14));
                Canvas.SetTop(Window4ScoreName, (Height / 4.74));
                Canvas.SetLeft(Window4GridForButtons, (Width / 7.2));
                Canvas.SetTop(Window4GridForButtons, (Height / 3));
                Canvas.SetLeft(Window4Score, (Width / 3.15));
                Canvas.SetTop(Window4Score, (Height / 4));
                switch (LanguageValue)
                {
                    case 1:
                        Canvas.SetLeft(Window4Stack, (Width / 4));
                        Canvas.SetTop(Window4Stack, (Height / (-190.09)));
                        break;
                    case 2:
                        Canvas.SetLeft(Window4Stack, (Width / 4.5));
                        Canvas.SetTop(Window4Stack, (Height / (-190.09)));
                        break;
                }
                //клик по кнопке MainMenu на четвертом экране
                Window4MainMenu.Click += delegate
                {
                    PlaySound(SoundPlayerButtonClick);
                    Window4Canvas.Children.Clear();
                    Window1Canvas.Children.Add(ImageWindow1Background);
                    Window1Canvas.Children.Add(Window1Stack);
                    Content = Window1Canvas;
                    TriesCounter = 0;
                    IsWin = 0;
                };
                //клик по кнопке Save на четвертом экране
                Window4Save.Click += delegate
                {
                    PlaySound(SoundPlayerButtonClick);
                    if (LanguageValue == 1)
                    {
                        File.AppendAllText("Hit the basket.Data.txt", System.Convert.ToString(CurrentDate) + Environment.NewLine);
                        File.AppendAllText("Hit the basket.Data.txt", "Range: ");
                        File.AppendAllText("Hit the basket.Data.txt", System.Convert.ToString(MaxLenght) + Environment.NewLine);
                        File.AppendAllText("Hit the basket.Data.txt", "Flight time: ");
                        File.AppendAllText("Hit the basket.Data.txt", System.Convert.ToString(LastPoint * 10 / 1000) + Environment.NewLine);
                        File.AppendAllText("Hit the basket.Data.txt", "Highest point: ");
                        File.AppendAllText("Hit the basket.Data.txt", System.Convert.ToString(MaxHeight) + Environment.NewLine);
                        File.AppendAllText("Hit the basket.Data.txt", "Initial angle: ");
                        File.AppendAllText("Hit the basket.Data.txt", System.Convert.ToString(Window2Angle.Text) + Environment.NewLine);
                        File.AppendAllText("Hit the basket.Data.txt", "Initial speed: ");
                        File.AppendAllText("Hit the basket.Data.txt", System.Convert.ToString(Window2InitialVelocity.Text) + Environment.NewLine);
                        File.AppendAllText("Hit the basket.Data.txt", "Moves to win: ");
                        switch (IsWin)
                        {
                            case 0:
                                File.AppendAllText("Hit the basket.Data.txt", "Did not win" + Environment.NewLine);
                                break;
                            case 1:
                                File.AppendAllText("Hit the basket.Data.txt", System.Convert.ToString(TriesCounter) + Environment.NewLine);
                                break;
                        }
                        File.AppendAllText("Hit the basket.Data.txt", Environment.NewLine);
                        MessageBox.Show("Your data has been saved!", "Complete.");
                    }
                    if (LanguageValue == 2)
                    {
                        File.AppendAllText("Hit the basket.Data.txt", System.Convert.ToString(CurrentDate) + Environment.NewLine);
                        File.AppendAllText("Hit the basket.Data.txt", "Дистанция: ");
                        File.AppendAllText("Hit the basket.Data.txt", System.Convert.ToString(MaxLenght) + Environment.NewLine);
                        File.AppendAllText("Hit the basket.Data.txt", "Время полёта: ");
                        File.AppendAllText("Hit the basket.Data.txt", System.Convert.ToString(LastPoint * 10 / 1000) + Environment.NewLine);
                        File.AppendAllText("Hit the basket.Data.txt", "Наивысшая точка: ");
                        File.AppendAllText("Hit the basket.Data.txt", System.Convert.ToString(MaxHeight) + Environment.NewLine);
                        File.AppendAllText("Hit the basket.Data.txt", "Начальный угол: ");
                        File.AppendAllText("Hit the basket.Data.txt", System.Convert.ToString(Window2Angle.Text) + Environment.NewLine);
                        File.AppendAllText("Hit the basket.Data.txt", "Начальная скорость: ");
                        File.AppendAllText("Hit the basket.Data.txt", System.Convert.ToString(Window2InitialVelocity.Text) + Environment.NewLine);
                        File.AppendAllText("Hit the basket.Data.txt", "Попыток до победы: ");
                        switch (IsWin)
                        {
                            case 0:
                                File.AppendAllText("Hit the basket.Data.txt", "Не выиграл" + Environment.NewLine);
                                break;
                            case 1:
                                File.AppendAllText("Hit the basket.Data.txt", System.Convert.ToString(TriesCounter) + Environment.NewLine);
                                break;
                        }
                        File.AppendAllText("Hit the basket.Data.txt", Environment.NewLine);
                        MessageBox.Show("Ваши данные сохранены!", "Успешно.");
                    }
                };
            }
            
            //функция, отвечающая за случайное появление корзины
            void RandomizeBasket()
            {
                int XMin = System.Convert.ToInt16(Width / 6.8);
                int XMax = System.Convert.ToInt16(Width / 1.5);
                Random Randomizer = new Random();
                RandomX = Randomizer.Next(XMin, XMax);
                RandomXMin = RandomX - System.Convert.ToInt16(Width / 55) + System.Convert.ToInt16(Width / 22.39);
                RandomXMax = RandomX + System.Convert.ToInt16(Width / 55) + System.Convert.ToInt16(Width / 22.39);
            }
            //функция, отвечающая за проверку попадания в корзину
            bool GoalCheck(double X)
            {
                if ((X >= RandomXMin) && (X <= RandomXMax))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //функция, отвечающая за смену значения звуков
            void ChangeValueSounds()
            {
                switch (SoundsValue)
                {
                    case 0:
                        SoundsValue = 1;
                        break;
                    case 1:
                        SoundsValue = 0;
                        break;
                }
            }
            //функция, отвечающая за смену режима звуков
            void ChangeSounds()
            {
                switch(SoundsValue)
                {
                    case 0:
                        ImageSounds = ImageSoundsOff;
                        break;
                    case 1:
                        ImageSounds = ImageSoundsOn;
                        break;
                }
            }
            //функция, отвечающая за проигрывание звука
            void PlaySound(SoundPlayer PS)
            {
                if (SoundsValue == 1)
                {
                    PS.Play();
                }
            }
            //функция, отвечающая за смену знаечния темы
            void ChangeValueTheme()
            {
                switch (ThemeValue)
                {
                    case 1:
                        ThemeValue = 2;
                        break;
                    case 2:
                        ThemeValue = 1;
                        break;
                }
            }
            //функция, отвечающая за смену темы
            void ChangeTheme()
            {
                switch (ThemeValue)
                {
                    case 1:
                        ImageBackground = ImageLightBackground;
                        ImageThemeIcon = ImageThemeIconSun;
                        ImageWindow1Background = ImageLightBackgroundForAll;
                        ImageWindow2Background = ImageWindow2LightBackground;
                        ImageWindowSettingsBackground = ImageLightBackgroundForAll;
                        break;
                    case 2:
                        ImageBackground = ImageDarkBackground;
                        ImageThemeIcon = ImageThemeIconMoon;
                        ImageWindow1Background = ImageDarkBackgroundForAll;
                        ImageWindow2Background = ImageWindow2DarkBackground;
                        ImageWindowSettingsBackground = ImageDarkBackgroundForAll;
                        break;
                }
            }
            //функция, отвечающая за смену значения языка
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
            //функция, отвечающая за смену языка
            void ChangeLanguage()
            {
                switch (LanguageValue)
                {
                    case 1:
                        ImageWinScreen = ImageEnglishWinScreen;
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
                        WindowSettingsTextBlockSounds.Inlines.Clear();
                        WindowSettingsTextBlockSounds.Inlines.Add(new Run("SOUNDS"));
                        break;
                    case 2:
                        ImageWinScreen = ImageRussianWinScreen;
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
                        WindowSettingsTextBlockSounds.Inlines.Clear();
                        WindowSettingsTextBlockSounds.Inlines.Add(new Run("ЗВУКИ"));
                        break;
                }
            }
            //функция, отвеачющая за смену значения птицы
            void ChangeValueTheBird()
            {
                switch (BirdValue)
                {
                    case 1:
                        BirdValue = 2;
                        break;
                    case 2:
                        BirdValue = 1;
                        break;
                }
            }
            //функция, отвечающая за смену птицы
            void ChangeTheBird()
            {
                switch (BirdValue)
                {
                    case 1:
                        ImageSettingsTheBird = ImageSettingsRedBird;
                        ImageTheBird = ImageRedBird;
                        ImageWindow2TheBird = ImageWindow2RedBird;
                        break;
                    case 2:
                        ImageSettingsTheBird = ImageSettingsBlueBird;
                        ImageTheBird = ImageBlueBird;
                        ImageWindow2TheBird = ImageWindow2BlueBird;
                        break;
                }
            }

            Title = "Hit the basket";
            //объявление и параметры кнопки Play на первом экране
            Button Window1Play = new Button();
            Window1TextBlockPlay = new TextBlock();
            Window1TextBlockPlay.FontSize = TextBlocksFontSize;
            Window1TextBlockPlay.TextAlignment = TextAlignment.Center;
            switch (LanguageValue)
            {
                case 1:
                    Window1TextBlockPlay.Inlines.Add(new Run("PLAY"));
                    break;
                case 2:
                    Window1TextBlockPlay.Inlines.Add(new Run("ИГРАТЬ"));
                    break;
            }
            Window1Play.Content = Window1TextBlockPlay;
            Window1Play.Margin = new Thickness(ButtonsMargin);
            Window1Play.Height = ButtonsHeight;
            Window1Play.Width = ButtonsWidth;
            //объявление и параметры кнопки Settings на первом экране
            Button Window1Settings = new Button();
            Window1TextBlockSettings = new TextBlock();
            Window1TextBlockSettings.FontSize = TextBlocksFontSize;
            Window1TextBlockSettings.TextAlignment = TextAlignment.Center;
            switch (LanguageValue)
            {
                case 1:
                    Window1TextBlockSettings.Inlines.Add(new Run("SETTINGS"));
                    break;
                case 2:
                    Window1TextBlockSettings.Inlines.Add(new Run("НАСТРОЙКИ"));
                    break;
            }

            Window1Settings.Content = Window1TextBlockSettings;
            Window1Settings.Margin = new Thickness(ButtonsMargin);
            Window1Settings.Height = ButtonsHeight;
            Window1Settings.Width = ButtonsWidth;
            //объявление и параметры кнопки Exit на первом экране
            Button Window1Exit = new Button();
            Window1TextBlockExit = new TextBlock();
            Window1TextBlockExit.FontSize = TextBlocksFontSize;
            Window1TextBlockExit.TextAlignment = TextAlignment.Center;
            switch (LanguageValue)
            {
                case 1:
                    Window1TextBlockExit.Inlines.Add(new Run("EXIT"));
                    break;
                case 2:
                    Window1TextBlockExit.Inlines.Add(new Run("ВЫХОД"));
                    break;
            }

            Window1Exit.Content = Window1TextBlockExit;
            Window1Exit.Margin = new Thickness(ButtonsMargin);
            Window1Exit.Height = ButtonsHeight;
            Window1Exit.Width = ButtonsWidth;
            //объявление StackPanel на первом экране
            Window1Stack = new StackPanel();
            Window1Stack.Margin = new Thickness(0);
            //объявление и параметры Grid на первом экране
            Grid Window1Grid = new Grid();
            Window1Grid.Margin = new Thickness(WindowGridMargin);
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
            Window1Canvas.Height = SystemParameters.VirtualScreenHeight;
            Window1Canvas.Width = SystemParameters.VirtualScreenWidth;
            Window1Canvas.Children.Add(ImageWindow1Background);
            Window1Canvas.Children.Add(Window1Stack);
            Canvas.SetLeft(Window1Stack, (Width / 34.28));
            Canvas.SetTop(Window1Stack, (Height / 5));
            Content = Window1Canvas;
            //клик по кнопке Exit на первом экране
            Window1Exit.PreviewMouseLeftButtonDown += Window1ExitClicked;
            void Window1ExitClicked(object sender, MouseButtonEventArgs e)
            {
                PlaySound(SoundPlayerButtonClick);
                Close();
            }
            //клик по кнопке Settings на первом экране
            Window1Settings.PreviewMouseLeftButtonDown += Window1SettingsClicked;
            void Window1SettingsClicked(object sender, MouseButtonEventArgs e)
            {
                PlaySound(SoundPlayerButtonClick);
                Window1Canvas.Children.Clear();
                //объявление и параметры Sounds кнопки на экране настроек
                Button WindowSettingsSounds = new Button();
                WindowSettingsTextBlockSounds = new TextBlock();
                WindowSettingsTextBlockSounds.FontSize = TextBlocksFontSize;
                WindowSettingsTextBlockSounds.TextAlignment = TextAlignment.Center;
                switch (LanguageValue)
                {
                    case 1:
                        WindowSettingsTextBlockSounds.Inlines.Add(new Run("SOUNDS"));
                        break;
                    case 2:
                        WindowSettingsTextBlockSounds.Inlines.Add(new Run("ЗВУКИ"));
                        break;
                }
                WindowSettingsSounds.Content = WindowSettingsTextBlockSounds;
                WindowSettingsSounds.Margin = new Thickness(ButtonsMargin * 1.5);
                WindowSettingsSounds.Height = ButtonsHeight;
                WindowSettingsSounds.Width = ButtonsWidth;
                //объявление и параметры кнопки Back на экране настроек
                Button WindowSettingsBack = new Button();
                WindowSettingsTextBlockBack = new TextBlock();
                WindowSettingsTextBlockBack.FontSize = TextBlocksFontSize;
                WindowSettingsTextBlockBack.TextAlignment = TextAlignment.Center;
                switch (LanguageValue)
                {
                    case 1:
                        WindowSettingsTextBlockBack.Inlines.Add(new Run("BACK"));
                        break;
                    case 2:
                        WindowSettingsTextBlockBack.Inlines.Add(new Run("НАЗАД"));
                        break;
                }
                WindowSettingsBack.Content = WindowSettingsTextBlockBack;
                WindowSettingsBack.Margin = new Thickness(ButtonsMargin * 1.5);
                WindowSettingsBack.Height = ButtonsHeight;
                WindowSettingsBack.Width = ButtonsWidth;
                //объявление и параметры кнопки Bird на экране настроек
                Button WindowSettingsBird = new Button();
                WindowSettingsTextBlockBird = new TextBlock();
                WindowSettingsTextBlockBird.FontSize = TextBlocksFontSize;
                WindowSettingsTextBlockBird.TextAlignment = TextAlignment.Center;
                switch (LanguageValue)
                {
                    case 1:
                        WindowSettingsTextBlockBird.Inlines.Add(new Run("BIRD"));
                        break;
                    case 2:
                        WindowSettingsTextBlockBird.Inlines.Add(new Run("ПТИЦА"));
                        break;
                }
                WindowSettingsBird.Content = WindowSettingsTextBlockBird;
                WindowSettingsBird.Margin = new Thickness(ButtonsMargin * 1.5);
                WindowSettingsBird.Height = ButtonsHeight;
                WindowSettingsBird.Width = ButtonsWidth;
                //объявление и параметры кнопки Language на экране настроек
                Button WindowSettingsLanguage = new Button();
                WindowSettingsTextBlockLanguage = new TextBlock();
                WindowSettingsTextBlockLanguage.FontSize = TextBlocksFontSize;
                WindowSettingsTextBlockLanguage.TextAlignment = TextAlignment.Center;
                switch (LanguageValue)
                {
                    case 1:
                        WindowSettingsTextBlockLanguage.Inlines.Add(new Run("LANGUAGE"));
                        break;
                    case 2:
                        WindowSettingsTextBlockLanguage.Inlines.Add(new Run("ЯЗЫК"));
                        break;
                }
                WindowSettingsLanguage.Content = WindowSettingsTextBlockLanguage;
                WindowSettingsLanguage.Margin = new Thickness(ButtonsMargin * 1.5);
                WindowSettingsLanguage.Height = ButtonsHeight;
                WindowSettingsLanguage.Width = ButtonsWidth;
                //объявление и параметры кнопки Theme на экране настроек
                Button WindowSettingsTheme = new Button();
                WindowSettingsTextBlockTheme = new TextBlock();
                WindowSettingsTextBlockTheme.FontSize = TextBlocksFontSize;
                WindowSettingsTextBlockTheme.TextAlignment = TextAlignment.Center;
                switch (LanguageValue)
                {
                    case 1:
                        WindowSettingsTextBlockTheme.Inlines.Add(new Run("THEME"));
                        break;
                    case 2:
                        WindowSettingsTextBlockTheme.Inlines.Add(new Run("ТЕМА"));
                        break;
                }
                WindowSettingsTheme.Content = WindowSettingsTextBlockTheme;
                WindowSettingsTheme.Margin = new Thickness(ButtonsMargin * 1.5);
                WindowSettingsTheme.Height = ButtonsHeight;
                WindowSettingsTheme.Width = ButtonsWidth;
                //объявление и параметры StackPanel на экране настроек
                WindowSettingsStack = new StackPanel();
                //объявление и параметры Grid на экране настроек
                Grid WindowSettingsGrid = new Grid();
                WindowSettingsGrid.RowDefinitions.Add(new RowDefinition());
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
                WindowSettingsGrid.Children.Add(WindowSettingsSounds);
                Grid.SetRow(WindowSettingsSounds, 1);
                Grid.SetColumn(WindowSettingsSounds, 1);
                WindowSettingsGrid.Children.Add(WindowSettingsBack);
                Grid.SetRow(WindowSettingsBack, 2);
                Grid.SetColumn(WindowSettingsBack, 1);
                WindowSettingsStack.Children.Add(WindowSettingsGrid);

                
                
                //объявление и параметры Canvas на экране настроек
                WindowSettingsCanvas = new Canvas();
                WindowSettingsCanvas.Height = SystemParameters.VirtualScreenHeight;
                WindowSettingsCanvas.Width = SystemParameters.VirtualScreenWidth;
                WindowSettingsCanvas.Children.Add(ImageWindowSettingsBackground);
                WindowSettingsCanvas.Children.Add(ImageTheFlag);
                WindowSettingsCanvas.Children.Add(ImageSettingsTheBird);
                WindowSettingsCanvas.Children.Add(ImageThemeIcon);
                WindowSettingsCanvas.Children.Add(ImageSounds);
                WindowSettingsCanvas.Children.Add(WindowSettingsStack);

                Canvas.SetLeft(WindowSettingsStack, (Width / 4.05));
                Canvas.SetTop(WindowSettingsStack, (Height / 10)); 
                Canvas.SetLeft(ImageTheFlag, (Width / 2.28));
                Canvas.SetTop(ImageTheFlag, (Height / 12.3));
                Canvas.SetLeft(ImageSettingsTheBird, (Width / 5.59));
                Canvas.SetTop(ImageSettingsTheBird, (Height / 12));
                Canvas.SetLeft(ImageThemeIcon, (Width / 5.59));
                Canvas.SetTop(ImageThemeIcon, (Height / 2.9));
                Canvas.SetLeft(ImageSounds, (Width / 2.28));
                Canvas.SetTop(ImageSounds, (Height / 2.9));
                Content = WindowSettingsCanvas;
                //клик по кнопке Back на экране настроек
                WindowSettingsBack.Click += delegate
                {
                    PlaySound(SoundPlayerButtonClick);
                    Content = Window1Canvas;
                    WindowSettingsCanvas.Children.Clear();
                    Window1Canvas.Children.Add(ImageWindow1Background);
                    Window1Canvas.Children.Add(Window1Stack);
                };
                //клик по кнопке Sounds на экране настроек
                WindowSettingsSounds.Click += delegate
                {
                    PlaySound(SoundPlayerButtonClick);
                    ChangeValueSounds();
                    WindowSettingsCanvas.Children.Clear();
                    ChangeSounds();
                    WindowSettingsCanvas.Children.Add(ImageWindowSettingsBackground);
                    WindowSettingsCanvas.Children.Add(ImageThemeIcon);
                    WindowSettingsCanvas.Children.Add(ImageTheFlag);
                    WindowSettingsCanvas.Children.Add(ImageSounds);
                    WindowSettingsCanvas.Children.Add(ImageSettingsTheBird);
                    WindowSettingsCanvas.Children.Add(WindowSettingsStack);
                    Canvas.SetLeft(ImageSounds, (Width / 2.28));
                    Canvas.SetTop(ImageSounds, (Height / 2.9));
                    WriteNewDataToFile();
                };
                //клик по кнопке Theme на экране настроек
                WindowSettingsTheme.Click += delegate
                {
                    PlaySound(SoundPlayerButtonClick);
                    ChangeValueTheme();
                    WindowSettingsCanvas.Children.Clear();
                    ChangeTheme();
                    WindowSettingsCanvas.Children.Add(ImageWindowSettingsBackground);
                    WindowSettingsCanvas.Children.Add(ImageThemeIcon);
                    WindowSettingsCanvas.Children.Add(ImageTheFlag);
                    WindowSettingsCanvas.Children.Add(ImageSounds);
                    WindowSettingsCanvas.Children.Add(ImageSettingsTheBird);
                    WindowSettingsCanvas.Children.Add(WindowSettingsStack);
                    Canvas.SetLeft(ImageThemeIcon, (Width / 5.59));
                    Canvas.SetTop(ImageThemeIcon, (Height / 2.9));
                    WriteNewDataToFile();
                };
                //клик по кнопке Bird на экране настроек
                WindowSettingsBird.Click += delegate
                {
                    PlaySound(SoundPlayerButtonClick);
                    ChangeValueTheBird();
                    WindowSettingsCanvas.Children.Clear();
                    ChangeTheBird();
                    WindowSettingsCanvas.Children.Add(ImageWindowSettingsBackground);
                    WindowSettingsCanvas.Children.Add(ImageThemeIcon);
                    WindowSettingsCanvas.Children.Add(ImageTheFlag);
                    WindowSettingsCanvas.Children.Add(ImageSounds);
                    WindowSettingsCanvas.Children.Add(ImageSettingsTheBird);
                    WindowSettingsCanvas.Children.Add(WindowSettingsStack);
                    Canvas.SetLeft(ImageSettingsTheBird, (Width / 5.59));
                    Canvas.SetTop(ImageSettingsTheBird, (Height / 12));
                    WriteNewDataToFile();
                };
                //клик по кнопке Language на экране настроек
                WindowSettingsLanguage.Click += delegate
                {
                    PlaySound(SoundPlayerButtonClick);
                    ChangeValueLanguage();
                    WindowSettingsCanvas.Children.Clear();
                    ChangeLanguage();
                    WindowSettingsCanvas.Children.Add(ImageWindowSettingsBackground);
                    WindowSettingsCanvas.Children.Add(ImageThemeIcon);
                    WindowSettingsCanvas.Children.Add(ImageTheFlag);
                    WindowSettingsCanvas.Children.Add(ImageSounds);
                    WindowSettingsCanvas.Children.Add(ImageSettingsTheBird);
                    WindowSettingsCanvas.Children.Add(WindowSettingsStack);
                    Canvas.SetLeft(ImageTheFlag, (Width / 2.28));
                    Canvas.SetTop(ImageTheFlag, (Height / 12.3));
                    WriteNewDataToFile();

                };
            }
            //клик по кнопке Play на первом экране
            Window1Play.PreviewMouseLeftButtonDown += Window1PlayClicked;
            void Window1PlayClicked(object sender, MouseButtonEventArgs e)
            {
                PlaySound(SoundPlayerButtonClick);
                RandomizeBasket();
                Window1Canvas.Children.Clear();
                //объявление и параметры кнопки Start на втором экране
                Button Window2Start = new Button();
                Window2TextBlockStart = new TextBlock();
                Window2TextBlockStart.FontSize = TextBlocksFontSize;
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
                Window2Start.Margin = new Thickness(ButtonsMargin);
                Window2Start.Height = ButtonsHeight;
                Window2Start.Width = ButtonsWidth;
                //объявление и параметры кнопки Main menu на втором экране
                Button Window2MainMenu = new Button();
                Window2TextBlockMainMenu = new TextBlock();
                Window2TextBlockMainMenu.FontSize = TextBlocksFontSize;
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
                Window2MainMenu.Margin = new Thickness(ButtonsMargin);
                Window2MainMenu.Height = ButtonsHeight;
                Window2MainMenu.Width = ButtonsWidth;
                //объявление StackPanel на втором экране
                Window2Stack = new StackPanel();
                //объявление и параметры Grid на втором экране
                Grid Window2Grid = new Grid();
                Window2Grid.Margin = new Thickness(WindowGridMargin);
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
                Window2InitialVelocity.FontSize = TextBlocksFontSize;
                Window2InitialVelocity.Foreground = Brushes.Gray;
                Window2InitialVelocity.TextAlignment = TextAlignment.Center;
                Window2InitialVelocity.Height = ButtonsHeight;
                Window2InitialVelocity.Width = ButtonsWidth;
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
                Window2Angle.FontSize = TextBlocksFontSize;
                Window2Angle.Foreground = Brushes.Gray;
                Window2Angle.TextAlignment = TextAlignment.Center;
                Window2Angle.Height = ButtonsHeight;
                Window2Angle.Width = ButtonsWidth;
                Window2Grid.Children.Add(Window2Angle);
                Grid.SetRow(Window2Angle, 1);
                Grid.SetColumn(Window2Angle, 1);
                Window2Stack.Children.Add(Window2Grid);
                //объявление и параметры Canvas на втором экране
                Window2Canvas = new Canvas();
                Window2Canvas.Height = SystemParameters.VirtualScreenHeight;
                Window2Canvas.Width = SystemParameters.VirtualScreenWidth;
                Window2Canvas.Children.Add(ImageWindow2Background);
                Window2Canvas.Children.Add(ImageWindow2TheBird);
                Window2Canvas.Children.Add(ImageBasketVisible);
                Window2Canvas.Children.Add(ImageBasketInvisible);
                Window2Canvas.Children.Add(Window2Stack);
                Canvas.SetLeft(ImageWindow2TheBird, (Width / (-6.81)));
                Canvas.SetTop(ImageWindow2TheBird, (Height / 1.92));
                Canvas.SetLeft(ImageBasketInvisible, (Width / (-6.81)) + RandomX);
                Canvas.SetTop(ImageBasketInvisible, (Height / 1.92));
                Canvas.SetLeft(ImageBasketVisible, (Width / (-6.81)) + RandomX);
                Canvas.SetTop(ImageBasketVisible, (Height / 1.92));
                Canvas.SetLeft(Window2Stack, (Width / 7.4));
                Canvas.SetTop(Window2Stack, (Height / 4));
                Content = Window2Canvas;
                //клик по кнопке Main menu на втором экране
                Window2MainMenu.PreviewMouseLeftButtonDown += Window2MainMenuClicked;
                void Window2MainMenuClicked(object sender2, MouseButtonEventArgs e2) //клик по кнопке Main menu
                {
                    PlaySound(SoundPlayerButtonClick);
                    Content = Window1Canvas;
                    Window2Canvas.Children.Clear();
                    Window1Canvas.Children.Add(ImageWindow1Background);
                    Window1Canvas.Children.Add(Window1Stack);
                    TriesCounter = 0;
                };
                //клик по кнопке Start на втором экране
                Window2Start.PreviewMouseLeftButtonDown += Window2StartClicked;
                void Window2StartClicked(object sender2, MouseButtonEventArgs e2)
                {
                    PlaySound(SoundPlayerButtonClick);
                    //обработка входных данных
                    if (IsDataCorrect(Window2InitialVelocity.Text, Window2Angle.Text) == true)
                    {
                        PlaySound(SoundPlayerStartGame);
                        TriesCounter++;
                        Window2Canvas.Children.Clear();
                        MaxHeight = 0;
                        MaxLenght = 0;
                        //объявление и параметры кнопки Restart на третьем экране
                        Button Window3Restart = new Button();
                        Window3TextBlockRestart = new TextBlock();
                        Window3TextBlockRestart.FontSize = TextBlocksFontSize;
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
                        Window3Restart.Margin = new Thickness(ButtonsMargin);
                        Window3Restart.Height = ButtonsHeight;
                        Window3Restart.Width = ButtonsWidth;
                        //объявление и параметры кнопки Back на третьем экране
                        Button Window3Back = new Button();
                        Window3TextBlockBack = new TextBlock();
                        Window3TextBlockBack.FontSize = TextBlocksFontSize;
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
                        Window3Back.Margin = new Thickness(ButtonsMargin);
                        Window3Back.Height = ButtonsHeight;
                        Window3Back.Width = ButtonsWidth;
                        //объявление и параметры кнопки Next на третьем экране
                        Button Window3Next = new Button();
                        Window3TextBlockNext = new TextBlock();
                        Window3TextBlockNext.FontSize = TextBlocksFontSize;
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
                        Window3Next.Margin = new Thickness(ButtonsMargin);
                        Window3Next.Height = ButtonsHeight;
                        Window3Next.Width = ButtonsWidth;
                        //объявление StackPanel на третьем экране
                        Window3Stack = new StackPanel();
                        //объявление и параметры Grid на третьем экране
                        Grid Window3Grid = new Grid();
                        Window3Grid.Margin = new Thickness(WindowGridMargin);
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
                        Canvas.SetLeft(Window3Stack, (Width / 16) - (Width / 30));
                        Canvas.SetTop(Window3Stack, (Height / 10));
                        //клик по кнопке Back на третьем экране
                        Window3Back.Click += delegate
                        {
                            PlaySound(SoundPlayerButtonClick);
                            LastPoint = 0;
                            MaxHeight = 0;
                            MaxLenght = 0;
                            Window3Canvas.Children.Clear();
                            Window2Canvas.Children.Add(ImageWindow2Background);
                            Window2Canvas.Children.Add(ImageBasketVisible);
                            Window2Canvas.Children.Add(ImageBasketInvisible);
                            Window2Canvas.Children.Add(ImageWindow2TheBird);
                            Window2Canvas.Children.Add(Window2Stack);
                            Canvas.SetLeft(ImageTheBird, (Width / (-6.81)));
                            Canvas.SetTop(ImageTheBird, (Height / 1.92));
                            Canvas.SetLeft(ImageBasketInvisible, (Width / (-6.81)) + RandomX);
                            Canvas.SetTop(ImageBasketInvisible, (Height / 1.92));
                            Canvas.SetLeft(ImageBasketVisible, (Width / (-6.81)) + RandomX);
                            Canvas.SetTop(ImageBasketVisible, (Height / 1.92));
                            Content = Window2Canvas;
                        };
                        //клик по кнопке Restart на третьем экране
                        Window3Restart.Click += delegate
                        {
                            PlaySound(SoundPlayerButtonClick);
                            Window3Canvas.Children.Clear();
                            Window3Canvas.Children.Add(ImageBackground);
                            if (GoalCheck(MaxLenght) == true)
                            {
                                Window3Canvas.Children.Add(ImageWinScreen);
                            }
                            Window3Canvas.Children.Add(ImageBasketInvisible);
                            Window3Canvas.Children.Add(ImageTheBird);
                            Window3Canvas.Children.Add(ImageBasketVisible);
                            Canvas.SetLeft(ImageTheBird, (Width / (-6.81)));
                            Canvas.SetTop(ImageTheBird, (Height / 1.92));
                            Canvas.SetLeft(ImageBasketInvisible, (Width / (-6.81)) + RandomX);
                            Canvas.SetTop(ImageBasketInvisible, (Height / 1.92));
                            Canvas.SetLeft(ImageBasketVisible, (Width / (-6.81)) + RandomX);
                            Canvas.SetTop(ImageBasketVisible, (Height / 1.92));
                            BirdTimer.Start();
                            SoundPlayerStartGame.Play();
                        };
                        int InitialSpeed = System.Convert.ToInt16(Window2InitialVelocity.Text);
                        int InitialAngle = System.Convert.ToInt16(Window2Angle.Text);
                        const double Step = 0.1; //или 0.01??
                        const double g = 9.8;
                        //вычисление координаты X
                        double CalculateXCord(int InitialVelocity, int Angle, double CurrentTime)
                        {
                            double XCord = InitialVelocity * CosTable[Angle] * CurrentTime;
                            return XCord;
                        }
                        //вычисление координаты Y
                        double CalculateYCord(int InitialVelocity, int Angle, double CurrentTime)
                        {
                            double YCord = (InitialVelocity * SinTable[Angle] * CurrentTime) - ((g * Math.Pow(CurrentTime, 2)) / (2));
                            return YCord;
                        }
                        //вычисление количества координат 
                        int CalculateNumCoordinates(int InitialVelocity, int Angle)
                        {
                            int NumCoordinates = 0;
                            double CurrentTime = 0;
                            int YCordZeroChecker = 0;
                            for (int i = 0; i < i + 1; i++)
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
                                CurrentTime += Step;
                            }
                            return NumCoordinates;
                        }
                        //объявление и заполнение массива координат
                        Coordinates = new Point[CalculateNumCoordinates(InitialSpeed, InitialAngle) + 1];
                        double CurrentTimeForPoints = 0;
                        for (int i = 0; i < Coordinates.Length; i++)
                        {
                            if (i + 1 == Coordinates.Length)
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
                                if (Math.Abs(Coordinates[i].X) > MaxLenght)
                                {
                                    MaxLenght = Coordinates[i].X;
                                }

                            }
                            CurrentTimeForPoints += Step;
                        }
                        //объявление и параметры Canvas на 3 окне
                        Window3Canvas = new Canvas();
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
                        Window3Canvas.Children.Add(ImageBackground);
                        Window3Canvas.Children.Add(ImageBasketInvisible);
                        Window3Canvas.Children.Add(ImageTheBird);
                        Window3Canvas.Children.Add(ImageBasketVisible);
                        Canvas.SetLeft(ImageTheBird, (Width / (-6.81)));
                        Canvas.SetTop(ImageTheBird, (Height / 1.92));
                        Canvas.SetLeft(ImageBasketInvisible, (Width / (-6.81)) + RandomX);
                        Canvas.SetTop(ImageBasketInvisible, (Height / 1.92));
                        Canvas.SetLeft(ImageBasketVisible, (Width / (-6.81)) + RandomX);
                        Canvas.SetTop(ImageBasketVisible, (Height / 1.92));
                        //объявление и параметры таймера
                        BirdTimer = new DispatcherTimer();
                        BirdTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
                        BirdTimer.Start();
                        //функция, описывающая шаг таймера
                        BirdTimer.Tick += delegate
                        {
                            ImageTheBird.Margin = new Thickness(Coordinates[T].X, Coordinates[T].Y, 0, -Coordinates[T].Y);
                            T++;
                            if (T == Coordinates.Length - 40)
                            {
                                PlaySound(SoundPlayerFalling);
                            }
                            //по окночанию анимации
                            if (T == Coordinates.Length)
                            {
                                Window3Canvas.Children.Remove(ImageTheBird);
                                Window3Canvas.Children.Remove(ImageBasketVisible);
                                Window3Canvas.Children.Add(Trajectory);
                                Window3Canvas.Children.Add(ImageTheBird);
                                Window3Canvas.Children.Add(ImageBasketVisible);
                                Canvas.SetLeft(Trajectory, (Width / 7.19));
                                Canvas.SetTop(Trajectory, (Height / 1.476));
                                LastPoint = T;
                                BirdTimer.Stop();
                                AnimationCounter++;
                                T = 0;
                                if ((GoalCheck(MaxLenght) == true) && (Window3Canvas.Children.Contains(ImageWinScreen) == false))
                                {
                                    Window3Canvas.Children.Add(ImageWinScreen);
                                    switch (LanguageValue)
                                    {
                                        case 1:
                                            Canvas.SetLeft(ImageWinScreen, (Width / 46));
                                            Canvas.SetTop(ImageWinScreen, (Height / 92.97));
                                            break;
                                        case 2:
                                            Canvas.SetLeft(ImageWinScreen, (Width / 56));
                                            Canvas.SetTop(ImageWinScreen, (Height / 92.97));
                                            break;
                                    }
                                    IsWin = 1;
                                    PlaySound(SoundPlayerWinGame);
                                }
                                Window3Canvas.Children.Add(Window3Stack);
                            }
                            //клик по кнопке Next на третьем экране
                            Window3Next.Click += Window3NextClicked;
                        };
                    }
                    else
                    {
                        if (LanguageValue == 1)
                        {
                            MessageBox.Show("Your data is not correct! Use only positive integer numbers. Please try again.", "Error.");
                        }
                        if (LanguageValue == 2)
                        {
                            MessageBox.Show("Ваши данные неверны! Используйте только положительные целые числа. Пожалуйста, попробуйте еще раз.", "Ошибка.");
                        }
                    }
                };
                //обработка входных данных
                bool IsDataCorrect(string IS, string AN)
                {
                    int CheckAngle = System.Convert.ToInt16(AN);
                    if ((CheckAngle > 180) || (CheckAngle < 0))
                    {
                        return false;
                    }
                    for (int i = 0; i < IS.Length; i++)
                    {
                        if (IS[i] == ' ')
                        {
                            return false;
                        }
                        if (!(IS[i] >= '0' && IS[i] <= '9'))
                        {
                            return false;
                        }
                    }
                    for (int i = 0; i < AN.Length; i++)
                    {
                        if (AN[i] == ' ')
                        {
                            return false;
                        }
                        if (!(AN[i] >= '0' && AN[i] <= '9'))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            };
        }
    }
}