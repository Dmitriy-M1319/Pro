using System;
using System.IO;
using System.Windows.Forms;

namespace MyEnglishAppWinForms
{/// <summary>
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
        /// добавить список слов с переводом
        /// </summary>
        /// <param name="englishWords"></param>
        /// <param name="russianWords"></param>
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
        /// обработчик кнопки добавления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            private void buttonSetListOfWords_Click(object sender, EventArgs e)
            {
              SetListOfWords(textBoxEnglishWords.Text, textBoxRussianWords.Text);
            textBoxRussianWords.Text = "";
            textBoxEnglishWords.Text = "";
            }
        
    }
}
