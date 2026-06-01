using System.Windows;
using System.Text.Json;
using System.IO;
namespace LearningWords
{
    /// <summary>
    /// Логика взаимодействия для CardsWindow.xaml
    /// </summary>
    public partial class CardsWindow : Window
    {
        //жалкая пародия на сохранение
        private AppSetings _settings;
        private string _settingsFile = "settings.json";
        public CardsWindow() //здесь каша-мала: и сейв, и прочая хуета
        {
            InitializeComponent();
            LoadSettings();
            ApplySettingsToUI();
        }
        public class AppSetings //то, что будет по дефолту
        {
            public int _true_answer { get; set; } = 0;
            public int _learned_words { get; set; } = 0;

            public List<string> _words { get; set; }  = new List<string>
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
        private void LoadSettings() //непонятная хуета, но нужная
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
            true_answer = _settings._true_answer;
            learned_words = _settings._learned_words;
            words = _settings._words;
            translation = _settings._translation;
            save_words = _settings._save_words;
            save_translation = _settings._save_translation;

        }
        private void SaveSettings() //сохранение данных !!!ОБЯЗАТЕЛЬНО ВЫЗВАТЬ!!!
        {
            _settings._true_answer = true_answer;
            _settings._learned_words = learned_words;
            _settings._words = words;
            _settings._translation = translation;
            _settings._save_words = save_words;
            _settings._save_translation = save_translation;
            string json = JsonSerializer.Serialize(_settings);
            File.WriteAllText("settings.json", json);
        }


        public void Her(object sender, RoutedEventArgs e) //Loaded, запускается автоматически вместе с окном
        {
            LearnedWords.Text = "⭐ " + learned_words.ToString() + " слов выучено";  
        }

        //---------SHOW CARDS------------

        int click = 0;
        public static List<string> words = new List<string>
        {
                "Кошка", "Собака", "Яблоко", "Холодильник", "Гриб", "Арбуз", "Стул", "Клубника",
                "Здание", "Клавиатура", "Знание", "Рыба", "Волосы", "Ключ", "Преступление", "Адвокат",
                "Сосед", "Друг", "Будущее", "Голова", "Цвет"
        };
        public static List<string> translation = new List<string>
        {
                "Cat", "Dog", "Apple", "Fridge", "Mushroom", "Watermelon", "Chair", "Strawberry",
                "Building", "Keyboard", "Knowledge", "Fish", "Hair", "Key", "Crime", "Lawyer",
                "Neighbour", "Friend", "Future", "Head", "Color"
        };
        public static List<string> save_words = new List<string>
        {
             
        };
        public static List<string> save_translation = new List<string>
        {
            
        };
        Random random = new Random();
        int count;
        public static int learned_words = 0;
        public void ShowCards(object sender, RoutedEventArgs e)
        {
            if (words.Count == 0)
            {
                Card.Style = (Style)FindResource("BlockButtonStyle");
                Card.Content = "ПОЗДРАВЛЯЕМ!\nВы прошли все карточки!";
                click = -1;
            }
            if (click == 0)
            {
                count = random.Next(words.Count);
            }
            if (Card.Content == "Начать")
            {
                Card.Content = words[count];
                ++click;
            }
            else if (click == 0)
            {
                Card.Content = words[count];
                Card.Style = (Style)FindResource("BlockButtonStyle");
                ++click;
            }
            else if (click == 1)
            {
                Card.Content = translation[count];
                Card.Style = (Style)FindResource("BlockButtonStyle2");
                save_words.Add(words[count]);
                save_translation.Add(translation[count]);
                words.RemoveAt(count);
                translation.RemoveAt(count);
                click = 0;
                ++learned_words;
                LearnedWords.Text = "⭐ " + learned_words.ToString() + " слов выучено"; 
                SaveSettings();
            }
        }
        //------MENUE BUTTONS------

        public TestWindow OwnerTestWindow { get; set; }
        public int true_answer = TestWindow.true_answer;
        public void MenueButton_Click(object sender, RoutedEventArgs e)
        {
            MenueWindow _menue = new MenueWindow();
            _menue.OwnerCardsWindow = this;
            _menue.Show();
            this.Close();
        }

        public void CardsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public void DictionaryButton_Click(object sender, RoutedEventArgs e)
        {
            DictionaryWindow _dictionary = new DictionaryWindow();
            _dictionary.OwnerCardsWindow = this;
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


    }
}

