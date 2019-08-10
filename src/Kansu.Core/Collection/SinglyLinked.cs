// ReSharper disable ArgumentsStyleOther

using static Kansu.Data.Guards;

namespace Kansu.Collection {

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SinglyLinked<T> {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        public SinglyLinked(T head, SinglyLinked<T> tail) {
            ThrowWhen(head, IsNullOrDefault, nameof(head));
            Head = head;
            Tail = tail;
        }

        /// <summary>
        /// 
        /// </summary>
        public T Head { get; }

        /// <summary>
        /// 
        /// </summary>
        public SinglyLinked<T> Tail { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLast => Tail == null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="position"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static SinglyLinked<T> FromArray(T[] items, int position, int count) =>
            items.Length switch {
                0 => null,
                _ => new SinglyLinked<T>(
                    head: items[position],
                    tail: ++position < count ? FromArray(items, position, count) : null
                )};
        
    }

}