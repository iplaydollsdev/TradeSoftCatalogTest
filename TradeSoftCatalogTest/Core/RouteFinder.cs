using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TradeSoftCatalogTest.MVVM.Model;

namespace TradeSoftCatalogTest.Core
{
    /// <summary>
    /// Класс, с помощью которого выполняется поиск аналога
    /// </summary>
    public static class RouteFinder
    {
        /// <summary>
        /// Поиск пусти до искомого товара от исходного
        /// </summary>
        /// <param name="inputFrom">Исходный товар</param>
        /// <param name="inputTo">Искомый товар</param>
        /// <param name="recursionSteps">Глубина рекурсии</param>
        /// <returns>Список маршрутов до исходного товара</returns>
        public static List<AnalogRoute> Find(string inputFrom, string inputTo, int recursionSteps)
        {
            try
            {
                if (string.IsNullOrEmpty(inputFrom) || string.IsNullOrEmpty(inputTo))
                {
                    MessageBox.Show("Поля не должны быть пустыми");
                    throw new Exception();
                }

                // Разделяем введенную строну по разделителю "/"
                string[] partsFrom = inputFrom.Split('/');
                // Определяем исходный артикул
                string articleFrom = partsFrom[0];
                // Определяем исходного производителя
                string manufacturerFrom = partsFrom.ElementAtOrDefault(1);


                //То же для искомого товара
                string[] partsTo = inputTo.Split('/');
                string articleTo = partsTo[0];
                string manufacturerTo = partsTo.ElementAtOrDefault(1);

                if (partsFrom.Length != 2 || partsTo.Length != 2)
                {
                    MessageBox.Show("Формат поиска \"Артикул/Производитель\"");
                    throw new Exception();
                }

                // Инициализируем список маршрутов
                List<AnalogRoute> result = new();
                // Инициализируем HashSet для того, чтобы отслеживать и избегать цепочки, в которых уже были
                HashSet<string> keys = new HashSet<string>();
                // Запускаем рекурсивный метод поиска
                FindRoutes(articleFrom, manufacturerFrom, articleTo, manufacturerTo, recursionSteps, new List<AnalogChain>(), result, keys);
                return result;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Рекурсивный метод поиска, который вызывывается до глубины рекурсии для каждого артикула
        /// </summary>
        /// <param name="articleFrom">Исходный артикул</param>
        /// <param name="manufacturerFrom">Исходный производитель</param>
        /// <param name="articleTo">Искомый артикул</param>
        /// <param name="manufacturerTo">Искомый производитель</param>
        /// <param name="stepsLeft">Оставшаяся глубина рекурсии</param>
        /// <param name="currentRoute">Текущая цепочка</param>
        /// <param name="result">Коллеция маршрутов</param>
        /// <param name="visitedNodes">HashSet для исключения прохождения по повторным цепочкам</param>
        private static void FindRoutes(string articleFrom, string manufacturerFrom, string articleTo, string manufacturerTo, int stepsLeft, List<AnalogChain> currentRoute, List<AnalogRoute> result, HashSet<string> visitedNodes)
        {
            //Убираем разделители из артикулов и приводим к нижнему регистру производителей
            articleFrom = RemoveSeparators(articleFrom);
            manufacturerFrom = manufacturerFrom.ToLower();
            articleTo = RemoveSeparators(articleTo);
            manufacturerTo = manufacturerTo.ToLower();

            // Если исхомый и исходный товар совпадает выходит из текущей рекурсии
            if (articleFrom == articleTo && (manufacturerFrom == null || manufacturerFrom == manufacturerTo))
            {
                MessageBox.Show("Исходный и искомый товары не должны совпадать");
                throw new Exception();
            }

            // Формируется ключ текущей цепочки для HashSet
            string currentNodeKey = $"{articleFrom}/{manufacturerFrom}";

            // Выходит из текущей рекурсии если здесь уже были или если достигли максимальной глубины
            if (visitedNodes.Contains(currentNodeKey) || stepsLeft <= 0)
            {
                return;
            }
            // Добавляется ключ текущей цепочки в HashSet
            visitedNodes.Add(currentNodeKey);

            // Используется using для корректного уничтожения контекста базы после использования
            using (var context = new AnalogContext())
            {
                // Из базы берутся все товары, артикул и производитель совпадают с исходным
                var nextAnalogModels = context.AnalogModels.ToList()
                    .Where(a => RemoveSeparators(a.Article1) == articleFrom &&
                                (manufacturerFrom == null || a.Manufacturer1.ToLower() == manufacturerFrom));

                foreach (var nextModel in nextAnalogModels)
                {
                    // Если уровень доверия 0, то не вызываем следующую рекурсию
                    if (nextModel.TrustLevel == 0)
                    {
                        // Если аналог товара является искомым, то добавляется его маршрут без продолжения рекурсии
                        if (RemoveSeparators(nextModel.Article2) == articleTo &&
                        (manufacturerTo == null || nextModel.Manufacturer2.ToLower() == manufacturerTo))
                        {
                            // Формируется и добавляется новая цепочка
                            currentRoute.Add(new AnalogChain
                            {
                                AtricleFrom = $"{nextModel.Article1}/{nextModel.Manufacturer1}",
                                AtricleTo = $"{nextModel.Article2}/{nextModel.Manufacturer2}"
                            });

                            // Маршрут добавляется к коллекции результата
                            result.Add(new AnalogRoute { Chains = new List<AnalogChain>(currentRoute) });
                            // Удаляется последний элемент маршрута, чтобы избежать накопления в разных маршрутах цепочек из предыдущих маршрутов
                            currentRoute.RemoveAt(currentRoute.Count - 1);
                        }
                    }
                    // Если уровень доверия не 0, то продолжается вызываться рекурсия уже с новымми исходными товарами
                    else
                    {
                        // Формируется и добавляется новая цепочка
                        currentRoute.Add(new AnalogChain
                        {
                            AtricleFrom = $"{nextModel.Article1}/{nextModel.Manufacturer1}",
                            AtricleTo = $"{nextModel.Article2}/{nextModel.Manufacturer2}"
                        });

                        // Вызывается рекурсия
                        FindRoutes(nextModel.Article2, nextModel.Manufacturer2, articleTo, manufacturerTo, stepsLeft - 1, currentRoute, result, visitedNodes);
                        currentRoute.RemoveAt(currentRoute.Count - 1);
                    }
                }
            }
        }

        /// <summary>
        /// Убирает символы-разделители
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Строка, приведенная к формату без разделителей (" ", "-", "_") и LowerCase</returns>
        private static string RemoveSeparators(string input)
        {
            return input.Replace(" ", "").Replace("-", "").Replace("_", "").ToLower();
        }
    }
}
