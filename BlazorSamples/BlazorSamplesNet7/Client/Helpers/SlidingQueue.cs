using System.Collections;
using System.Text;

namespace BlazorTemplate.Client.Helpers
{
    public class SlidingQueue<T> : IEnumerable<T>
    {
        private readonly Queue<T> _queue;
        private readonly int _maxCount;

        public SlidingQueue(int maxCount)
        {
            _maxCount = maxCount;
            _queue = new Queue<T>(maxCount);
        }

        public void Add(T item)
        {
            if (_queue.Count >= _maxCount)
                _queue.Dequeue(); // Remove the oldest item
            _queue.Enqueue(item); // Add the new item
        }

        public IEnumerator<T> GetEnumerator() => _queue.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        { 
            StringBuilder sb = new StringBuilder();
            foreach (var item in _queue) 
            {
                sb.AppendLine(item?.ToString() ?? string.Empty);
            }
            return sb.ToString();
        }
    }
}
