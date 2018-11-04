using System;
using System.Collections.Generic;
using System.Linq;

namespace LPQ
{
    //TODO add enumerators
    //TODO abstract base class and division into minHeap and maxHeap
    //TODO NUnit tests
    public class BinaryHeap<T>
    {
        private List<T> heapList;

        public IComparer<T> Comparer { get; private set; }
        public int Count { get => heapList != null ? heapList.Count : 0; }

        public BinaryHeap(IComparer<T> comparer)
        {
            this.Comparer = comparer;
            heapList = new List<T>();
        }

        public T this[int index]
        {
            get => heapList[index];
        }


        /// <summary>
        /// Inserts element into heap, IComparer assumes following order:
        /// It returns -1 when first element is smaller, 0 when they are equal, 1 when first element is bigger
        /// </summary>
        /// <param name="element">element to insert</param>
        public void Insert(T element)
        {
            heapList.Add(element);
            int parentIndex = Parent(Count - 1);

            int elementPosition = Count - 1;

            // while first element is smaller than the other, swap with parent
            while (Comparer.Compare(element, heapList[parentIndex]) < 0)
            {
                int newPosition = SwapWithParent(elementPosition);
                elementPosition = newPosition;
                parentIndex = Parent(newPosition);
            }
        }

        public void Insert(params T[] elements)
        {
            elements.ForEach(x => Insert(x));
        }

        public void Insert(IEnumerable<T> elements)
        {
            elements.ToList().ForEach(x => Insert(x));
        }

        /// <summary>
        /// Deletes root element and reorganises heap
        /// </summary>
        /// <returns>Root element before deletion</returns>
        public T DeleteRoot()
        {
            if (Count <= 0) throw new ArgumentException("Heap is empty, cannot delete root.");

            T temporary = heapList[0];

            heapList[0] = heapList[Count - 1];
            heapList[Count - 1] = temporary;

            DeleteLast();

            GoDown(0);

            return temporary;
        }

        public T DeleteLast()
        {
            if (Count <= 0) throw new ArgumentException("Heap is empty, cannot delete root.");

            int lastIndex = Count - 1;
            T lastElement = heapList[lastIndex];
            heapList.RemoveAt(lastIndex);

            return lastElement;
        }

        /// <summary>
        /// Gradually goes down in the tree and swaps root item until
        /// its on the right place in the hierarchy (recursive method)
        /// </summary>
        /// <param name="parentIndex"></param>
        private void GoDown(int parentIndex)
        {
            int leftChild = GetDominating(parentIndex, LeftChild(parentIndex));
            int rightChild = GetDominating(leftChild, RightChild(parentIndex));

            if (rightChild == parentIndex)
                return;

            SwapWithParent(rightChild);
            GoDown(rightChild);
        }

        /// <summary>
        /// Compares values of the given nodes
        /// </summary>
        /// <param name="parentIndex"></param>
        /// <returns>Smaller child(higher priority) index</returns>
        private int GetDominating(int parentIndex, int childIndex)
        {
            if ((Count - 1 < childIndex) || (Comparer.Compare(heapList[parentIndex], heapList[childIndex]) <= 0))
                return parentIndex;
            else
                return childIndex;
        }

        /// <summary>
        /// Swaps child node with its parent
        /// </summary>
        /// <param name="elementIndex"></param>
        /// <returns>parent's index</returns>
        private int SwapWithParent(int elementIndex)
        {
            int parentIndex = Parent(elementIndex);
            T temporary = heapList[elementIndex];

            heapList[elementIndex] = heapList[parentIndex];
            heapList[parentIndex] = temporary;

            return parentIndex;
        }

        private int Parent(int childIndex)
        {
            return 0.Clamp(0, Count-1, (childIndex - 1) / 2);
        }

        private int LeftChild(int parentIndex)
        {
            return (parentIndex * 2) + 1;
        }

        private int RightChild(int parentIndex)
        {
            return LeftChild(parentIndex) + 1;
        }

        public bool Contains(T element)
        {
            return heapList.Contains(element) ? true : false;
        }

        public override string ToString()
        {
            string result = "";
            heapList.ForEach(x => result += $"{x}, ");
            return result;
        }
    }
}
