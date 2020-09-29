using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace MyEnglishAppWinForms
{   
    /// <summary>
    /// Класс формы с добавлением списка слов
    /// </summary>
    public partial class InputForm : Form
    {
        
        public InputForm()
        {
            InitializeComponent();
            labelEnglishWords.Text = "В левой колонке вы вводите\nанглийские слова";
            labelRussianWords.Text = "В правой колонке вы вводите\nсписок русских переводов\n" +
            "напротив английских оригиналов";
        }

     
        
        /// <summary>
        /// Добавить список слов с переводом
        /// </summary>
        /// <param name="englishWords">Строка с английскими словами</param>
        /// <param name="russianWords">Строка с русскими словами</param>
        public static void SetListOfWords(string englishWords, string russianWords)
        {
            string[] engWords = englishWords.Split(new char[] { '\n','\r' }, StringSplitOptions.RemoveEmptyEntries);
            string[] rusWords = russianWords.Split(new char[] { '\n','\r' }, StringSplitOptions.RemoveEmptyEntries);
            string str;

            using (StreamWriter sw = new StreamWriter(Form1.FullPath,true))
            {
                sw.NewLine = "\n";

                for (int i = 0; i < engWords.Length; i++)
                {
                    str = engWords[i] + "\t" + rusWords[i];
                    sw.WriteLine(str);
                }

                sw.Close();
            }

            Form1 f = new Form1();
            f.AreButtonsEnabled(true);

        }

        /// <summary>
        /// Обработчик кнопки добавления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetListOfWords_Click(object sender, EventArgs e)
        {
            SetListOfWords(textBoxEnglishWords.Text, textBoxRussianWords.Text);
            textBoxRussianWords.Text = "";
            textBoxEnglishWords.Text = "";
            MessageBox.Show("Список слов словаря обновлен!", "Успешно");
        }
        

        /// <summary>
        /// Зависимость темы оформления от основного окна
        /// </summary>
        /// <param name="isDarkTheme">Параметр, указывающий на наличие темного оформления</param>
        public  void DoDarkOrLightTheme(bool isDarkTheme)
        {
            if (isDarkTheme)
            {
                buttonSetListOfWords.ForeColor = Color.White;
                buttonSetListOfWords.BackColor = Color.Black;
                textBoxEnglishWords.ForeColor = Color.White;
                textBoxEnglishWords.BackColor = Color.Black;
                textBoxRussianWords.ForeColor = Color.White;
                textBoxRussianWords.BackColor = Color.Black;
                labelEnglishWords.ForeColor = Color.White;
                labelEnglishWords.BackColor = Color.Black;
                labelRussianWords.ForeColor = Color.White;
                labelRussianWords.BackColor = Color.Black;
                BackColor = Color.Black;
            }
            else
            {
                buttonSetListOfWords.ForeColor = Color.Black;
                buttonSetListOfWords.BackColor = Color.White;
                textBoxEnglishWords.ForeColor = Color.Black;
                textBoxEnglishWords.BackColor = Color.White;
                textBoxRussianWords.ForeColor = Color.Black;
                textBoxRussianWords.BackColor = Color.White;
                labelEnglishWords.ForeColor = Color.Black;
                labelEnglishWords.BackColor = Color.White;
                labelRussianWords.ForeColor = Color.Black;
                labelRussianWords.BackColor = Color.White;
                BackColor = Color.White;
            }
        }
    }
}
