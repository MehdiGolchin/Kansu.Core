using System.Collections.Generic;
using System.Linq;

namespace Kansu.Collection {

    public static class SinglyLinkedExtensions {

        public static SinglyLinked<T> ToSinglyLinked<T>(this IEnumerable<T> source) =>
            ToSinglyLinked(source.ToArray());

        public static SinglyLinked<T> ToSinglyLinked<T>(this T[] source) =>
            SinglyLinked<T>.FromArray(source, 0, source.Length);

    }

}