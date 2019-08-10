using Shouldly;
using Xunit;

namespace Kansu.Collection.Tests {

    public class LinkedListTests {

        [Fact]
        public void FromArray_should_create_LinkedList_from_array() {
            // arrange
            var array = new[] {1, 2, 3};
            // act
            var list = SinglyLinked<int>.FromArray(array, 0, array.Length);
            // assert
            AreEqual(list, array).ShouldBeTrue();
        }

        #region [ Helpers ]

        private static bool AreEqual(SinglyLinked<int> list, int[] array, int position = 0) =>
            list.Head == array[position] && (list.IsLast || AreEqual(list.Tail, array, ++position));

        #endregion

    }

}