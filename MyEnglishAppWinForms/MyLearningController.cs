using System;

namespace RepeatingControllers
{
    /// <summary>
    /// Перечисление времени, через сколько необходимо произвести следующее повторение
    /// </summary>
    public enum Dates
    {
        Now,
        Minutes,
        Day,
        Weeks,
        Month
    }
    /// <summary>
    /// Класс состояния параметров повторений
    /// </summary>
    [Serializable]
    public class MyLearningController
    {
        /// <summary>
        /// Проверяет, первый раз создан объект или нет
        /// </summary>
        public bool IsNewObject { get; private set; }
        /// <summary>
        /// Дата настоящего повторения
        /// </summary>
        public DateTime NowTime { get; set; }
        /// <summary>
        /// Количество повторений
        /// </summary>
       
        public int CountOfRepeating { get; set; }
        /// <summary>
        /// Время следующего повторения (из перечисления)
        /// </summary>
        public Dates Date { get; set; }
        /// <summary>
        /// Дата следующего повторения
        /// </summary>
        public DateTime NextTime { get; set; }
        /// <summary>
        /// Конструктор для первого сохранения
        /// </summary>
        public MyLearningController()
        {
            NowTime = DateTime.Now;
            CountOfRepeating = 0;
            Date = Dates.Now;
            IsNewObject = true;
        }
        /// <summary>
        /// Конструктор для загрузки данных
        /// </summary>
        /// <param name="nexttime">Дата следующего повторения</param>
        /// <param name="dates">Формат следующего повтора</param>
        /// <param name="count">Количество совершенных повторений</param>
        public MyLearningController(DateTime nexttime,Dates dates,int count)
        {
            NextTime = nexttime;
            Date = dates;
            CountOfRepeating = count;
            IsNewObject = false;
        }
        /// <summary>
        /// Вывод состояния объекта повторения
        /// </summary>
        /// <returns>строка с информацией</returns>
        public string PrintMyOptions()
        {
            string line = "";
            line += "На данный момент ваш прогресс в изучении слов данного словаря:\n";
            line += $"Это ваше {CountOfRepeating + 1} повторение\n";
            if(CountOfRepeating==4)
            {
                line += "Поздравляю!! Вы закончили курс изучения слов данного словаря. Успехов в дальнейшем изучении";
            }
            else
            {
                line += $"Следующее повторение необходимо {NextTime.ToString()}";
            }
            
            return line;
            
        }
    }
}
