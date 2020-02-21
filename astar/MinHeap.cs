using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astar
{
    public class MinHeap<T>
    {
        private Tuple<T, double>[] elements;
        private HashSet<T> keys;
        private int size = 0;

        public int Count { get => size; }

        public MinHeap(int size)
        {
            elements = new Tuple<T, double>[size];
            keys = new HashSet<T>();
        }

        #region public methods
        public void Add(T elem, double val)
        {
            if (size == elements.Length)
            {
                throw new Exception("MinHeap size exceeded.");
            }
            elements[size++] = new Tuple<T, double>(elem, val);
            keys.Add(elem);
            pushUp(size - 1);
        }

        public T Remove()
        {
            if (size == 0)
            {
                throw new Exception("MinHeap is empty. Can't remove elements");
            }
            var ret = elements[0].Item1;
            elements[0] = elements[size - 1];
            elements[size - 1] = null;
            size--;
            keys.Remove(ret);
            pushDown(0);

            return ret;
        }

        public void Render()
        {
            int i = 0, idx = 0;
            if (size == 0) return;
            var levels = (int)Math.Log(size, 2) + 1;
            for (int l = 0; l < levels; l++)
            {
                for (int k = 0; k < Math.Pow(2, l); k++)
                {
                    Console.Write(elements[idx].Item1 + "\t");
                    idx++;
                    if (idx == size) break;
                }
                Console.WriteLine();
            }
        }

        public bool Contains(T elem)
        {
            return keys.Contains(elem);
        }
        #endregion

        #region private methods
        private void pushUp(int idx)
        {
            var curr = elements[idx];
            var pidx = GetParentIndex(idx);
            var p = elements[pidx];
            if (curr.Item2 < p.Item2)
            {
                swap(idx, pidx);
                pushUp(pidx);
            }
        }

        private void pushDown(int idx)
        {
            var curr = elements[idx];
            var left = GetLeftChildIndex(idx);
            var right = GetRightChildIndex(idx);

            if (left >= size) return;
            var minIdx = left;

            if (right < size && elements[right].Item2 < elements[left].Item2)
            {
                minIdx = right;
            }

            if (curr.Item2 > elements[minIdx].Item2)
            {
                swap(idx, minIdx);
                pushDown(minIdx);
            }

        }

        private void swap(int v1, int v2)
        {
            var tmp = elements[v1];
            elements[v1] = elements[v2];
            elements[v2] = tmp;
        }

        private Tuple<T, double> GetParent(int idx) => elements[GetParentIndex(idx)];

        private int GetParentIndex(int idx) => (idx - 1) / 2;

        private int GetLeftChildIndex(int idx) => (2 * idx) + 1;

        private int GetRightChildIndex(int idx) => (2 * idx) + 2;
        #endregion





    }
}
