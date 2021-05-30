namespace PartitionIEnumerableBenchmarks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PartitionIEnumerable
    {
        /// <summary>
        /// Splits an <see cref="IEnumerable{T}"/> collection into a list of collections with a fixed size.
        /// </summary>
        /// <typeparam name="T">Type of the IEnumerable.</typeparam>
        /// <param name="enumerable">The collection to split into smaller collections.</param>
        /// <param name="size">Size of the smaller collections.</param>
        public IEnumerable<IEnumerable<T>> PartitionClassic<T>(IEnumerable<T> enumerable, int size)
        {
            var partition = new List<T>(size);
            var counter = 0;

            using (var enumerator = enumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    partition.Add(enumerator.Current);
                    counter++;

                    if (counter % size == 0)
                    {
                        yield return partition.ToList();

                        partition.Clear();
                        counter = 0;
                    }
                }

                if (counter != 0)
                {
                    yield return partition;
                }
            }
        }


        // https://stackoverflow.com/a/18205112/633098
        public IEnumerable<IList<T>> PartitionJodrell<T>(IEnumerable<T> source, int size)
        {
            if (size < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(size), size, "Must be greater or equal to 2.");
            }

            T[] partition;
            int count;

            using (var e = source.GetEnumerator())
            {
                if (e.MoveNext())
                {
                    partition = new T[size];
                    partition[0] = e.Current;
                    count = 1;
                }
                else
                {
                    yield break;
                }

                while (e.MoveNext())
                {
                    partition[count] = e.Current;
                    count++;

                    if (count == size)
                    {
                        yield return partition;
                        count = 0;
                        partition = new T[size];
                    }
                }
            }

            if (count > 0)
            {
                Array.Resize(ref partition, count);
                yield return partition;
            }
        }

        //https://stackoverflow.com/q/41221842/633098
        public IEnumerable<List<T>> PartitionNeo2<T>(IEnumerable<T> enumerable, int count)
        {
            List<T> allItems = enumerable.ToList();

            List<T> items = new List<T>(count);
            foreach (T item in allItems)
            {
                items.Add(item);

                if (items.Count != count) continue;
                yield return items;
                items = new List<T>(count);
            }

            if (items.Any())
                yield return items;
        }

        /// <summary>
        /// Splits an <see cref="IEnumerable{T}"/> collection into a list of collections with a fixed size.
        /// </summary>
        /// <typeparam name="T">Type of the IEnumerable.</typeparam>
        /// <param name="enumerable">The collection to split into smaller collections.</param>
        /// <param name="size">Size of the smaller collections.</param>
        public IEnumerable<IEnumerable<T>> PartitionLinq<T>(IEnumerable<T> enumerable, int size)
        {
            while (enumerable.Any())
            {
                yield return enumerable.Take(size);
                enumerable = enumerable.Skip(size);
            }
        }
    }
}
