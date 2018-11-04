using System.Collections.Generic;

namespace LPQ
{
    public class PriorityQueue<T>
    {
        private BinaryHeap<T> queue;

        public IComparer<T> Comparer { get; private set; }

        public int Count { get => queue.Count; }


        public PriorityQueue(IComparer<T> comparer)
        {
            queue = new BinaryHeap<T>(comparer);
        }

        public T Peek()
        {
            return queue.Count > 0 ? queue[0] : default(T);
        }

        public void Enqueue(T element)
        {
            queue.Insert(element);
        }

        public T Dequeue()
        {
            return queue.DeleteRoot();
        }

        public bool Contains(T element)
        {
            return queue.Contains(element);
        }
    }
}
