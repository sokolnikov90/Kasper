using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CollectionPairs
{

    public static class CollectionHelper
    {

        /// <summary>
        /// Выводит все уникальные пары чисел из коллекции, которые в сумме равны заданному Х
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="X"></param>
        /// <returns></returns>
        public static IEnumerable<NumbersPair> GetAllParis(IEnumerable<int> collection, int X)
        {
            if ((collection == null))
            {
                throw new ArgumentNullException("collection");
            }

            // Получаем минимальное и максимальное значения эллементов коллекции
            var collectionInfo = collection.GroupBy(i => 1).Select(g => new { Max = g.Max(), Min = g.Min() }).First();

            // Создание и заполнение массива индексов для исходной коллекции
            // Все индексы массива сдвинуты на minListCollectionItem (indexArray[0] = minListCollectionItem)
            // Сдвиг необходим, чтобы уйти от массива с ненулевой нижней границей
            var indexArray = new uint[Math.Abs(collectionInfo.Min - collectionInfo.Max) + 1];

            // Инкрементим indexArray[i] для всех индексов, для которых есть числа в коллекции
            foreach (var item in collection)
            {
                indexArray[item - collectionInfo.Min]++;
            }

            // Вычислаем значение X (суммы двух чисел) с учётом сдвига 
            int shiftX = X - 2 * collectionInfo.Min;

            // Создаём набор результирующих пар
            var allPairs = new HashSet<NumbersPair>();

            for (int currentItemIndex = 0; currentItemIndex < indexArray.Length; currentItemIndex++)
            {
                int pairItemIndex = shiftX - currentItemIndex;

                // Проверка на наличие в коллекции текущего числа indexArray[currentItemIndex]
                // и необходимой для него пары indexArray[currentItemIndex]
                if ((indexArray[currentItemIndex] != 0) && 
                    (pairItemIndex >= 0) && 
                    (pairItemIndex < indexArray.Length) && 
                    (indexArray[pairItemIndex] != 0))
                {
                    // Если текущее число равно парному числу, проверяем
                    // что число таких чисел в исходной коллекции >= 2
                    if ((currentItemIndex == pairItemIndex) && (indexArray[currentItemIndex] < 2))
                    {
                        continue;
                    }
                    
                    // Добавляем пару чисел к результату, восстановив сдвинутое значение чисел к оригинальному
                    allPairs.Add(new NumbersPair(currentItemIndex + collectionInfo.Min, pairItemIndex + collectionInfo.Min));
                }
            }

            return allPairs;
        }
    }
}
