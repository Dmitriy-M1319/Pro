using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using RepeatingControllers;
using System.Threading.Tasks;

namespace MyEnglishAppWinForms
{
   /// <summary>
   /// Часть класса с внутренней логикой программы
   /// </summary>
    public partial class Form1
    {
        private DirectoryInfo newFolder;
        private MyLearningController controller;
        private BinaryFormatter formatter = new BinaryFormatter();
        private string answer="";
        private string typeword;

        /// <summary>
        /// Событие добавления новых слов
        /// </summary>
        public static event Action NewWords;

        /// <summary>
        /// Событие создания нового словаря
        /// </summary>
        public static event Action NewFile;

        /// <summary>
        /// Событие успешной замены неправильного перевода
        /// </summary>
        public static event Action Correcting;

        /// <summary>
        /// Лист английских слов
        /// </summary>
        private List<string> englishWords;

        /// <summary>
        /// Лист русских слов
        /// </summary>
        private List<string> russianWords;

        /// <summary>
        /// Количество слов в словаре
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Название словаря, с которым идет работа
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Название каталога для хранения словарей
        /// </summary>
        public string FolderName { get; set; } = "-";

        /// <summary>
        /// Полный путь к словарю
        /// </summary>
        public static string FullPath { get; private set; }

        /// <summary>
        /// Создание пользователя приложения
        /// </summary>

        public void CreateUser()
        {
            buttonSetNewWords.Enabled = false;
            buttonInputForm.Enabled = false;
            DirectoryInfo folderDirectory = new DirectoryInfo(".");

            //1 раз: для того, чтобы этот объект не был null
            newFolder = new DirectoryInfo($@"{folderDirectory.FullName}\{FolderName}");

            //Попытка чтения названия папки со словарями из специального файла
            try
            {
                using (StreamReader sr = File.OpenText("File With FolderName"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        FolderName = line;
                    }
                    //В этот раз объект создается, если уже названная папка существует
                    newFolder = new DirectoryInfo($@"{folderDirectory.FullName}\{FolderName}");
                }
            }
            catch (Exception)
            {
                //Если такого файла нет, то переходим по ссылке к созданию папки и файла->
                goto Link;
            }



        //-> Вот сюда
        Link:
            // Создание новой папки
            if (!newFolder.Exists)
            {
                while (true)
                {
                    if (FolderName != "-")
                    {
                        break;
                    }
                }

                //3 раз: создается новая папка, если таковой не было
                newFolder = new DirectoryInfo($@"{folderDirectory.FullName}\{FolderName}");
                newFolder.Create();
                using (StreamWriter sw = File.CreateText("File With FolderName"))
                {
                    sw.WriteLine(FolderName);
                }
            }

            englishWords = new List<string>();
            russianWords = new List<string>();

            //Блок загрузки словаря
            try
            {
                using (StreamReader sr = new StreamReader($@"{newFolder.FullName}\{FileName}.txt"))
                {
                    string line;
                    string[] words;
                    while ((line = sr.ReadLine()) != null)
                    {
                        words = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        englishWords.Add(words[0]);
                        russianWords.Add(words[1]);
                    }
                }

                Count = englishWords.Count;
                FullPath = $@"{newFolder.FullName}\{FileName}.txt";
                AreButtonsEnabled(true);
                buttonSetNewWords.Enabled = true;
                buttonInputForm.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Необходимо создать новый словарь\nВоспользуйтесь второй функцией!","!!!");
            }

            //Блок загрузки состояния объекта параметров повторений
            try
            {
                using (FileStream fs=new FileStream($"{FileName}.dat",FileMode.OpenOrCreate))
                {
                    controller = (MyLearningController)formatter.Deserialize(fs);
                    if(controller.NowTime.Date!=controller.NextTime.Date)
                    {
                        buttonSaveOptions.Enabled = false;
                        if (controller.NowTime.Date > controller.NextTime.Date)
                        {
                            buttonSaveOptions.Enabled = true;
                            MessageBox.Show("Вы пропустили повторение слов в данном словаре! Наверстывайте упущенное");
                        }
                    }
                }
            }
            catch (Exception)
            {
                controller = new  MyLearningController();
            }
            
        }


        /// <summary>
        /// Создание нового текстового файла для словаря
        /// </summary>
        public void CreateNewDictionary()
        {
            string nameDictionaty = textBoxCreateDictionaryText.Text;
            StreamWriter sw = new StreamWriter($@"{newFolder.FullName}\{nameDictionaty}.txt", true);
            FileName = nameDictionaty;
            sw.Close();
            NewFile?.Invoke();
            textBoxCreateDictionaryText.Text = "";
            buttonSetNewWords.Enabled = true;
            buttonInputForm.Enabled = true;
        }


        /// <summary>
        /// Вывод списка доступных словарей
        /// </summary>
        public void CheckDictionaries()
        {
            string line = "";
            line += "Сейчас будет доступен список имеющихся словарей:\n";
            FileInfo[] dictionaries = newFolder.GetFiles("*.txt", SearchOption.AllDirectories);
            foreach (FileInfo file in dictionaries)
            {
                line += $"{file.Name}\n";
            }
            MessageBox.Show(line, "Список");
        }


        /// <summary>
        /// Замена неправильного перевода
        /// </summary>
        public void CorrectWord()
        {

            if (typeword == "eng")
            {
                for (int i = 0; i < englishWords.Count; i++)
                {
                    if (englishWords[i] == textBoxOldWord.Text)
                    {
                        englishWords[i] = textBoxNewWord.Text;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < russianWords.Count; i++)
                {
                    if (russianWords[i] == textBoxOldWord.Text)
                    {
                        russianWords[i] = textBoxNewWord.Text;
                        break;
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter($@"{newFolder.FullName}\{FileName}.txt", false))
            {
                for (int i = 0; i < Count; i++)
                {
                    string str = englishWords[i] + "\t" + russianWords[i];
                    sw.WriteLine(str);
                }
                sw.Close();
                Correcting?.Invoke();
            }
            textBoxOldWord.Text = "";
            textBoxNewWord.Text = "";
        }


        /// <summary>
        /// Тестирование для проверки знаний на русский перевод
        /// </summary>
        public  void TestRussianAnswers()
        {
            
                int trueAnswers = 0;
                bool count;
                List<string> falseanswers = new List<string>();
                int[] arr = new int[Count];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = -1;
                }
                Random random = new Random();
                for (int i = 0; i < Count; i++)
                {
                Link:
                    
                    int k = random.Next(Count);
                    count = false;
                    for (int m = 0; m < arr.Length; m++)
                    {
                        if (arr[m] == k)
                        {
                            count = true;
                            break;
                        }
                    }
                    if (!count)
                    {
                        listAnswersLabel2.Text = $"Как будет перевод слова {englishWords[k]} ?";
                        labelCount.Text = $"Осталось слов: {Count - i}";
                    }
                    else
                    {
                        goto Link;
                    }
                    arr[i] = k;

                    while (true)
                    {

                        if (answer != "")
                        {
                            break;
                        }
                    }
                    if (russianWords[k].Contains(answer) || answer == russianWords[k])
                    {
                        
                        trueAnswers++;
                    }
                    else
                    {

                        falseanswers.Add(englishWords[k]+"->"+ russianWords[k] + "\n" + "(ошибка: " + answer + ")");

                    }
                    answer = "";
                    textBoxAnswers.Text = "";
                }
                if (trueAnswers == Count)
                {
                    labelCount.Text = "";
                    string line = "";
                    line += "Поздравляем вас!! Вы правильно перевели все слова и закрепили знания. Успехов!\n";
                    line += $"Общее количество правильных ответов: {trueAnswers} из {Count}";
                    listAnswersLabel2.Text = "";
                    MessageBox.Show(line,"Результат");
                }
                else
                {
                    labelCount.Text = "";
                    string line = "";
                    line += "Увы, но вы справились не со всеми словами из темы. Учите лучше и пройдите тест еще раз\n";
                    line += "Вы допустили ошибку в следующих словах:\n\n";
                    foreach (string word in falseanswers)
                    {
                        line += $"{word}\n";
                    }
                    line+="\n";
                    line += $"Общее количество правильных ответов: {trueAnswers} из {Count}";
                    listAnswersLabel2.Text = "";
                    MessageBox.Show(line,"Результат");
                }
                



        }


        /// <summary>
        /// Асинхронная версия ListRussianAnswers
        /// </summary>
        /// <returns></returns>
        public async Task TestRussianAnswersAsync()
        {
            await Task.Run(() => TestRussianAnswers());
        }


        /// <summary>
        /// Тестирование для проверки знаний на английский перевод
        /// </summary>
        public void TestEnglishAnswers()
        {
            
                int trueAnswers = 0;
                bool count;
                List<string> falseanswers = new List<string>();
                int[] arr = new int[Count];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = -1;
                }
                Random random = new Random();
                for (int i = 0; i < Count; i++)
                {
                Link:

                    int k = random.Next(Count);
                    count = false;
                    for (int m = 0; m < arr.Length; m++)
                    {
                        if (arr[m] == k)
                        {
                            count = true;
                            break;
                        }
                    }
                    if (!count)
                    {
                        listAnswersLabel2.Text = $"Как будет перевод слова {russianWords[k]} ?";
                        labelCount.Text = $"Осталось слов: {Count - i}";
                    }
                    else
                    {
                        goto Link;
                    }
                    arr[i] = k;
                    while (true)
                    {
                        if (answer != "")
                        {
                            break;
                        }
                    }

                    if (englishWords[k].Contains(answer) || answer == englishWords[k])
                    {

                        trueAnswers++;
                    }
                    else
                    {

                        falseanswers.Add(englishWords[k] + "->" + russianWords[k] + "\n" + "(ошибка: " + answer + ")");

                    }
                    answer = "";
                    textBoxAnswers.Text = "";
                }
                if (trueAnswers == Count)
                {
                    labelCount.Text = "";
                    string line = "";
                    line += "Поздравляем вас!! Вы правильно перевели все слова и закрепили знания. Успехов!\n\n";
                    line += $"Общее количество правильных ответов: {trueAnswers} из {Count}";
                    listAnswersLabel2.Text = "";
                    MessageBox.Show(line, "Результат");
                }
                else
                {
                    labelCount.Text = "";
                    string line = "";
                    line += "Увы, но вы справились не со всеми словами из темы. Учите лучше и пройдите тест еще раз\n";
                    line += "Вы допустили ошибку в следующих словах:\n\n";
                    foreach (string word in falseanswers)
                    {
                        line += $"{word}\n";
                    }
                    line += "\n";
                    line += $"Общее количество правильных ответов: {trueAnswers} из {Count}";
                    listAnswersLabel2.Text = "";
                    MessageBox.Show(line,"Результат");
                }


           

        }


        /// <summary>
        /// Асинхронная версия ListEnglishAnswers
        /// </summary>
        /// <returns></returns>
        public async Task TestEnglishAnswersAsync()
        {
            await Task.Run(() => TestEnglishAnswers());
        }


        /// <summary>
        /// Поиск перевода слова по словарю
        /// </summary>
        public void FindWord()
        { 
            string name = textBoxFindWord.Text;
            for (int i = 0; i < englishWords.Count; i++)
            {
                if (name == englishWords[i])
                {
                    MessageBox.Show($"{englishWords[i]}   ->  {russianWords[i]}","Найдено слово");
                    break;
                }
            }
            textBoxFindWord.Text = "";
        }


        /// <summary>
        /// Вывод словаря на экран
        /// </summary>
        public void Print()
        {
            string line = "";
            line += "Ваш словарь\nСлово\t\tПеревод\n";

            for (int i = 0; i < Count; i++)
            {
                line += $"{englishWords[i]}   ->  {russianWords[i]}\n";
            }
            MessageBox.Show(line,"Вывод");
        }


        /// <summary>
        /// Добавление новых слов в словарь
        /// </summary>
        /// <param name="englishword">Слово на английском</param>
        /// <param name="russianword">Слово на русском</param>
        public void SetNewWords(string englishword, string russianword)
        {
            
            using (StreamWriter sw = new StreamWriter($@"{newFolder.FullName}\{FileName}.txt", true))
            {
                string str = englishword + "\t" + russianword;
                sw.WriteLine(str);
                sw.Close();
                
            }
            NewWords?.Invoke();
            AreButtonsEnabled(true);
        }


        /// <summary>
        /// Сохранение состояния объекта повторений
        /// </summary>
        public void SaveNewOptions()
        {
            int k = (int)controller.Date;
            k++;
            controller.CountOfRepeating++;
            controller.Date = (Dates)k;
            controller.NowTime = DateTime.Now;
            switch (controller.Date)
            {
                case Dates.Now:
                    controller.NextTime = controller.NowTime;
                    break;
                case Dates.Minutes:
                    controller.NextTime = controller.NowTime.AddMinutes(30);
                    break;
                case Dates.Day:
                    controller.NextTime = controller.NowTime.AddDays(2);
                    break;
                case Dates.Weeks:
                    controller.NextTime = controller.NowTime.AddDays(15);
                    break;
                case Dates.Month:
                    controller.NextTime = controller.NowTime.AddMonths(2);
                    break;
                default:
                    throw new Exception("Недопустимая дата!");
                    
            }
            using (FileStream fs = new FileStream($"{FileName}.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, controller);
            }
        }


        /// <summary>
        /// Функция включения\выключения кнопок
        /// </summary>
        /// <param name="option">Параметр активации кнопок</param>
        public void AreButtonsEnabled(bool option)
        {
            buttonCorrecrTranslate.Enabled = option;
            buttonFindWord.Enabled = option;
            buttonForTests.Enabled = option;
            buttonInputForm.Enabled = option;
            buttonListAnswersSend.Enabled = option;
            buttonPrintOptions.Enabled = option;
            buttonPrintWords.Enabled = option;
            buttonSaveOptions.Enabled = option;
        }


    }
}
