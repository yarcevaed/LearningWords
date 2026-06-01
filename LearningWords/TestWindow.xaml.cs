using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LearningWords
{
    /// <summary>
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public void Her(object sender, RoutedEventArgs e) //Loaded, запускается автоматически вместе с окном
        {
            LearnedWords.Text = "⭐ " + true_answer.ToString() + " правильных ответов";
        }

        private AppSetings _settings;
        private string _settingsFile = "settings.json";
        public TestWindow()
        {
            InitializeComponent();
            LoadSettings();
            ApplySettingsToUI();
        }

        public class AppSetings //то, что будет по дефолту
        {
            public int _true_answer { get; set; } = 0;

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
            save_words = _settings._save_words;
            save_translation = _settings._save_translation;

        }
        private void SaveSettings() //сохранение данных !!!ОБЯЗАТЕЛЬНО ВЫЗВАТЬ!!!
        {
            _settings._true_answer = true_answer;
            string json = JsonSerializer.Serialize(_settings);
            File.WriteAllText("settings.json", json);
        }
        //--------TEST--------

        Random random = new Random();
        int count;
        int click = 0;

        int true_answer = 0;
        string translation;
        bool check = true;
        

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            if (save_words.Count == 0)
            {
                TestCard.Content = "Вы еще не выучили\nни одного слова :(";
            }
            else
            {
                string content = TestCard.Content.ToString();
                if (check == true)
                {
                    count = random.Next(save_words.Count);
                    ++click;
                }
                if (content == "Начать")
                {
                    TestCard.Style = (Style)FindResource("BlockButtonStyle");
                    TestCard.Content = save_words[count];
                    check = false;

                }
                else if (check == true)
                {
                    TestCard.Style = (Style)FindResource("BlockButtonStyle");
                    TestCard.Content = save_words[count];
                    check = false;

                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            translation = TextBox_translation.Text;
        }
        private void a(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if ((translation.ToLower() == save_translation[count].ToLower()) && click != 0)
                {
                    ++true_answer;
                    TestCard.Style = (Style)FindResource("BlockButtonStyleTrue");
                    TestCard.Content = "True";
                    SaveSettings();
                }
                else if ((translation.ToLower() != save_translation[count].ToLower()) && click != 0)
                {
                    TestCard.Style = (Style)FindResource("BlockButtonStyleFalse");
                    TestCard.Content = "False";
                }
                check = true;
            }
        }

        //------MENUE BUTTONS------

        public CardsWindow OwnerCardsWindow { get; set; }
        public List<string> save_words = CardsWindow.save_words;
        public List<string> save_translation = CardsWindow.save_translation;

        public void MenueButton_Click(object sender, RoutedEventArgs e)
        {
            MenueWindow _menue = new MenueWindow();
            _menue.Show();
            this.Close();
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


    }
}
