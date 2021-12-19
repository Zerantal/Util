using System.Collections.Generic;
// ReSharper disable UnusedMember.Global

namespace Util
{
    // ReSharper disable once UnusedMember.Global
    public class FixedQueue<T> : Queue<T>
    {
        public int FixedSize { get; set; }

        public FixedQueue(int fixedSize)
            : base(fixedSize)
        {
            FixedSize = fixedSize;
        }

        public new void Enqueue(T item)
        {
            while (Count >= FixedSize)
            {
                Dequeue();
            }
            base.Enqueue(item);
        }
    }
}
