using System.IO;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static LearningWords.CardsWindow;

namespace LearningWords
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MenueWindow : Window
    {
        // СОХРАНЕНИЕ (дура не удаляй, оно должно читать файл)

        private AppSetings _settings;
        private string _settingsFile = "settings.json";
        public MenueWindow()
        {
            InitializeComponent();
            LoadSettings();
            ApplySettingsToUI();

        }
        public class AppSetings //то, что будет по дефолту
        {
            public int _learned_words { get; set; } = 0;

            public List<string> _words { get; set; } = new List<string>
            {
                "Кошка", "Собака", "Яблоко", "Холодильник", "Гриб", "Арбуз", "Стул", "Клубника",
                "Здание", "Клавиатура", "Знание", "Рыба", "Волосы", "Ключ", "Преступление", "Адвокат",
                "Сосед", "Друг", "Будущее", "Голова", "Цвет"
            };

            public List<string> _translation { get; set; } = new List<string>
            {
                "Cat", "Dog", "Apple", "Fridge", "Mushroom", "Watermelon", "Chair", "Strawberry",
                "Building", "Keyboard", "Knowledge", "Fish", "Hair", "Key", "Crime", "Lawyer",
                "Neighbour", "Friend", "Future", "Head", "Color"
            };
            public List<string> _save_words { get; set; } = new List<string>
            {

            };
            public List<string> _save_translation { get; set; } = new List<string>
            {

            };

        }
        private void LoadSettings() 
        {
            if (File.Exists(_settingsFile))
            {
                string json = File.ReadAllText(_settingsFile);
                _settings = JsonSerializer.Deserialize<AppSetings>(json);
            }
            if (_settings == null)
            {
                _settings = new AppSetings();

            }
        }
        public void ApplySettingsToUI() //чтение
        {
            learned_words = _settings._learned_words;
        }

        //------MENUE BUTTONS------

        public CardsWindow OwnerCardsWindow {  get; set; }
        public int learned_words = CardsWindow.learned_words;

        public void Her(object sender, RoutedEventArgs e)
        {
            LearnedWords.Text = "⭐ " + learned_words.ToString() + " слов выучено";
        }
        public void MenueButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public void CardsButton_Click(object sender, RoutedEventArgs e)
        {
            CardsWindow _card = new CardsWindow();
            _card.Show();
            this.Close();
        }

        public void DictionaryButton_Click(object sender, RoutedEventArgs e)
        {
            DictionaryWindow _dictionary = new DictionaryWindow();
            _dictionary.Show();
            this.Close();
        }

        public void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public void TestButton_Click(object sender, RoutedEventArgs e)
        {
            TestWindow _test = new TestWindow();
            _test.Show();
            this.Close();
        }



        //------DEFOULT BUTTONS------

        public void Cards_Click(object sender, RoutedEventArgs e)
        {
            CardsButton_Click(sender, e);
        }

        public void Dictionary_Click(object sender, RoutedEventArgs e)
        {
            DictionaryButton_Click(sender, e);
        }

        public void Repetition_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Test_Click(object sender, RoutedEventArgs e)
        {
            TestButton_Click(sender, e);
        }
    }
}