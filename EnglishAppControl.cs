using System;
using System.Collections.Generic;
using System.IO;

namespace MyEnglishAppLibraryFramework
{
    public class EnglishAppControl : IPrintable, IListAnswers, ISetting,ICorrectable
    {
        public static event Action NewFile;
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
        public int Count { get; }
        public string FileName { get; }
        public EnglishAppControl()
        {
            englishWords = new List<string>();
            russianWords = new List<string>();
            Console.WriteLine("Введите название словаря, с которым хотите работать");
            string name = Console.ReadLine();
            FileName = name;
            try
            {
                using (StreamReader sr = File.OpenText($"{name}.txt"))
                {
                    string line;
                    string[] words;
                    while ((line = sr.ReadLine()) != null)
                    {
                        words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        englishWords.Add(words[0]);
                        russianWords.Add(words[1]);
                    }
                }
                Count = englishWords.Count;
            }
            catch (Exception)
            {
                Console.WriteLine("В словаре нет слов для загрузки в приложение!");
                Console.WriteLine("Хотите создать новый словарь?");
                Console.WriteLine("Введите да");
                string answer = Console.ReadLine();
                if (answer == "да")
                {
                    FileInfo fi = new FileInfo(FileName);
                    StreamWriter sw = fi.CreateText();
                    NewFile?.Invoke();
                    Console.WriteLine("Можете добавлять слова");

                }
                else
                {
                    Console.WriteLine("Создайте словарь вручную. Дальнейшая работа невозможна");
                }
            }
        }
        /// <summary>
        /// Вывод словаря на консоль
        /// </summary>
        public void Print()
        {
            Console.WriteLine("Ваш словарь");
            Console.WriteLine("Слово\t\tПеревод");
            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine($"{englishWords[i]}   ->  {russianWords[i]}");

                Console.WriteLine();
            }
        }
        /// <summary>
        /// Тест на русский перевод
        /// </summary>
        public void ListRussianAnswers()
        {
            Console.WriteLine("Вам будут предлагаться слова, а вы должны написать их перевод");
            Console.WriteLine("За каждый правильный ответ вы получаете балл. Наберите максимальное количество баллов для успеха!");
            Console.WriteLine("Если вы готовы, введите Да");
            string a = Console.ReadLine();
            if (a == "Да")
            {
                int trueAnswers = 0;
                bool count;
                List<string> falseanswers = new List<string>();
                int[] arr = new int[Count];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = -1;
                }
                Random rnd = new Random();
                for (int i = 0; i < Count; i++)
                {
                Link:
                    int k = rnd.Next(Count);
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
                        Console.WriteLine($"Как будет перевод слова {englishWords[k]} ?");
                    }
                    else
                    {
                        goto Link;
                    }
                    arr[i] = k;
                    string answer = Console.ReadLine();
                    if (russianWords[k].Contains(answer) || answer == russianWords[k])
                    {
                        Console.WriteLine();
                        Console.WriteLine("Вы ответили правильно!");
                        Console.WriteLine();
                        trueAnswers++;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Вы неправильно перевели слово((  Правильный ответ -> {russianWords[k]}");
                        falseanswers.Add(englishWords[k]);
                        Console.WriteLine();
                    }
                }
                if (trueAnswers == Count)
                {
                    Console.WriteLine("Поздравляем вас!! Вы правильно перевели все слова и закрепили знания. Успехов!");
                }
                else
                {
                    Console.WriteLine("Увы, но вы справились не со всеми словами из темы. Учите лучше и пройдите тест еще раз");
                    Console.WriteLine("Вы допустили ошибку в следующих словах:");
                    foreach (string word in falseanswers)
                    {
                        Console.WriteLine(word);
                    }
                }
                Console.WriteLine($"Общее количество правильных ответов: {trueAnswers} из {Count}");
            }
            else
            {
                Console.WriteLine("Ну тогда в следующий раз. Удачи");
                return;
            }

        }
        /// <summary>
        /// Тест на английский перевод
        /// </summary>
        public void ListEnglishAnswers()
        {
            Console.WriteLine("Вам будут предлагаться слова, а вы должны написать их английский перевод");
            Console.WriteLine("За каждый правильный ответ вы получаете балл. Наберите максимальное количество баллов для успеха!");
            Console.WriteLine("Если вы готовы, введите Да");
            string a = Console.ReadLine();
            if (a == "Да")
            {
                int trueAnswers = 0;
                bool count;
                List<string> falseanswers = new List<string>();
                int[] arr = new int[Count];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = -1;
                }
                Random rnd = new Random();
                for (int i = 0; i < Count; i++)
                {
                Link:
                    int k = rnd.Next(Count);
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
                        Console.WriteLine($"Как будет перевод слова {russianWords[k]} ?");
                    }
                    else
                    {
                        goto Link;
                    }
                    arr[i] = k;
                    string answer = Console.ReadLine();
                    if (englishWords[k].Contains(answer) || answer == englishWords[k])
                    {
                        Console.WriteLine();
                        Console.WriteLine("Вы ответили правильно!");
                        Console.WriteLine();
                        trueAnswers++;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Вы неправильно перевели слово((  Правильный ответ -> {englishWords[k]}");
                        falseanswers.Add(englishWords[k]);
                        Console.WriteLine();
                    }
                }
                if (trueAnswers == Count)
                {
                    Console.WriteLine("Поздравляем вас!! Вы правильно перевели все слова и закрепили знания. Успехов!");
                }
                else
                {
                    Console.WriteLine("Увы, но вы справились не со всеми словами из темы. Учите лучше и пройдите тест еще раз");
                    Console.WriteLine("Вы допустили ошибку в следующих словах:");
                    foreach (string word in falseanswers)
                    {
                        Console.WriteLine(word);
                    }
                }
                Console.WriteLine($"Общее количество правильных ответов: {trueAnswers} из {Count}");
            }
            else
            {
                Console.WriteLine("Ну тогда в следующий раз. Удачи");
                return;
            }
        }
        /// <summary>
        /// Добавление новых слов в словарь через консоль
        /// </summary>
        public void SetNewWords()
        {
            Console.WriteLine("Сначала введите слово на английском, а затем перевод");
            string englishword = Console.ReadLine();
            string russianword = Console.ReadLine();
            using (StreamWriter sw = new StreamWriter($"{FileName}.txt", true))
            {
                string str = englishword + " " + russianword;
                sw.WriteLine(str);

            }
            Console.WriteLine();
            Console.WriteLine("Добавлено");
            Console.WriteLine("Если вы создали новый словарь, то для работы с функциями 2, 3, 4 перезапустите приложение и введите его название снова");
            Console.WriteLine();
        }
        /// <summary>
        /// Создание файла для нового словаря
        /// </summary>
        public static void CreateNewDictionary()
        {
            Console.WriteLine("Введите название нового файла");
            string name = Console.ReadLine();
            FileInfo fi = new FileInfo(name);
            fi.CreateText();
            NewFile?.Invoke();
        }
        /// <summary>
        /// Поиск слова в словаре
        /// </summary>
        public void FindWord()
        {
            Console.WriteLine("Введите слово на английском");
            string name = Console.ReadLine();
            for (int i = 0; i < englishWords.Count; i++)
            {
                if (name == englishWords[i])
                {
                    Console.WriteLine($"{englishWords[i]}   ->  {russianWords[i]}");
                    break;
                }
            }
        }
        /// <summary>
        /// Исправление перевода необходимого слова
        /// </summary>
        public void CorrectWord()
        {


            Console.WriteLine("Введите, перевод какого слова вы будете исправлять (введите eng или rus), а затем само слово");
            string typeWord = Console.ReadLine();
            string word = Console.ReadLine();
            Console.WriteLine("Теперь введите исправленный перевод");
            string newword = Console.ReadLine();
            if (typeWord == "eng")
            {
                for (int i = 0; i < englishWords.Count; i++)
                {
                    if (englishWords[i] == word)
                    {
                        russianWords[i] = newword;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < russianWords.Count; i++)
                {
                    if (russianWords[i] == word)
                    {
                        englishWords[i] = newword;
                        break;
                    }
                }
            }
        }
    }
}
