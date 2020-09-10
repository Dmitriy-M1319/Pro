using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyEnglishAppWinForms
{
    /// <summary>
    /// Часть класса с обработчиками событий
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form1.NewFile += Form1_NewFile;
            Form1.NewWords += Form1_NewWords;
            Form1.Correcting += Form1_Correcting;
            InstructionFirstTime();
            HelloApp();
        }
        /// <summary>
        /// Инструкция для первого запуска
        /// </summary>
        private void InstructionFirstTime()
        {
            label20.Text = "Инструкция к прочтению:\n";
            label20.Text += "Если вы пользуетесь приложением первый раз, то 1 функция необходима для создания новой папки.\n";
            label20.Text += "А последующие разы вы вводите название этой папки для открытия словарей.\n";
            label20.Text += "Вторая часть 1 функции необходима только после добавления словарей.";
        }
      /// <summary>
      /// Приветствие перед работой
      /// </summary>
        private void HelloApp()
        {
            string hello = "Добро пожаловать в приложение MyEnglishWord!\n";
            hello += "Перед началом работы над словарем не забудьте воспользоваться 1 функцией\n";
            hello += "А именно внести название папки со словарями и сам словарь\n";
            hello += "Если что, воспользуйтесть инструкцией\n";
            hello += "Удачи вам в изучении языка!";
            MessageBox.Show(hello,"Hello,MyEnglishWord!");
        }
        /// <summary>
        /// Обработчик события замены неправильного перевода
        /// </summary>
        private void Form1_Correcting()
        {
            MessageBox.Show("Замена слова выполнена успешно!", "Успешно");
        }
        /// <summary>
        /// Обработчик события добавления новых слов
        /// </summary>
        private void Form1_NewWords()
        {
            MessageBox.Show("Ваши слова были добавлены в словарь", "Результат");
            textBox2.Text = "";
            textBox7.Text = "";
        }
        /// <summary>
        /// Обработчик события создания словаря
        /// </summary>
        private void Form1_NewFile()
        {
            MessageBox.Show("Был создан новый текстовый файл!","Результат");
        }

   
        //Кнопка: загрузка данных в приложение
        private void NewFolderCreate_Click(object sender, EventArgs e)
        {
            FolderName = textBoxNewFolder.Text;
            FileName = textBox1.Text;
            this.CreateUser();
            textBox1.Text = "";
        }
        //Кнопка: создание словаря
        private void buttonCreateDictionary_Click(object sender, EventArgs e)
        {
            this.CreateNewDictionary();
        }
        //Кнопка: вывод списка словарей
        private void button3_Click(object sender, EventArgs e)
        {
            this.CheckDictionaries();
        }
       
        
        //Кнопка: замена неправильного перевода
        private void button6_Click(object sender, EventArgs e)
        {
            this.CorrectWord();
        }
       //Кнопка: внесение ответа на тест на обработку
        private  void listAnswersSend_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => { answer = textBox5.Text; });
        }

        

       
        //Кнопка: поиск слова
        private void button1_Click(object sender, EventArgs e)
        {
            this.FindWord();
        }
        // Кнопка: вывод словаря на экран
        private void button7_Click(object sender, EventArgs e)
        {
            this.Print();
        }

        
        //Кнопка: добавление новых слов
        private void button2_Click(object sender, EventArgs e)
        {
            this.SetNewWords(textBox2.Text, textBox7.Text);
        }

        
        //Элемент прокрутки: выбор языка для замены перевода
        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            switch (domainUpDown1.Text)
            {
                case "ENG":
                    typeword = "eng";
                    break;
                case "RUS":
                    typeword = "rus";
                    break;
               
            }
        }
        //Кнопка: выбор режима тестирования
        private void buttonForTests_Click(object sender, EventArgs e)
        {
            switch (domainUpDownForTests.Text)
            {
                case "ENG":
                    ListRussianAnswers();
                    break;
                case "RUS":
                    ListEnglishAnswers();
                    break;
            }
        }
        //Кнопка: включение темной темы
        private void buttonDarkTheme_Click(object sender, EventArgs e)
        {
            buttonDarkTheme.Enabled = false;
            label1.ForeColor = Color.White;
            label10.ForeColor = Color.White;
            label11.ForeColor = Color.White;
            label12.ForeColor = Color.White;
            label13.ForeColor = Color.White;
            label14.ForeColor = Color.White;
            label15.ForeColor = Color.White;
            label16.ForeColor = Color.White;
            label17.ForeColor = Color.White;
            label18.ForeColor = Color.White;
            label19.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            label20.ForeColor = Color.White;
            label21.ForeColor = Color.White;
            label22.ForeColor = Color.White;
            label3.ForeColor = Color.White;
            label4.ForeColor = Color.White;
            label5.ForeColor = Color.White;
            label6.ForeColor = Color.White;
            label7.ForeColor = Color.White;
            label8.ForeColor = Color.White;
            label9.ForeColor = Color.White;
            labelCount.ForeColor = Color.White;
            listAnswersLabel2.ForeColor = Color.White;
            BackColor = Color.Black;
            buttonLightTheme.Enabled = true;
            
        }
        

        //Кнопка: включение светлой темы
        private void buttonLightTheme_Click(object sender, EventArgs e)
        {
            buttonLightTheme.Enabled = false;
            label1.ForeColor = Color.Black;
            label10.ForeColor = Color.Black;
            label11.ForeColor = Color.Black;
            label12.ForeColor = Color.Black;
            label13.ForeColor = Color.Black;
            label14.ForeColor = Color.Black;
            label15.ForeColor = Color.Black;
            label16.ForeColor = Color.Black;
            label17.ForeColor = Color.Black;
            label18.ForeColor = Color.Black;
            label19.ForeColor = Color.Black;
            label2.ForeColor = Color.Black;
            label20.ForeColor = Color.Black;
            label21.ForeColor = Color.Black;
            label22.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;
            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;
            label7.ForeColor = Color.Black;
            label8.ForeColor = Color.Black;
            label9.ForeColor = Color.Black;
            labelCount.ForeColor = Color.Black;
            listAnswersLabel2.ForeColor = Color.Black;
            BackColor = Color.White;
            buttonDarkTheme.Enabled = true;
        }
    }
}
