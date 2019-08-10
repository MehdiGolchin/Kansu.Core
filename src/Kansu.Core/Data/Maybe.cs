using System;
using System.Collections.Generic;
using static Kansu.Data.Guards;

namespace Kansu.Data {

    public struct Maybe<T> : IEquatable<Maybe<T>> where T : struct {

        private readonly T _value;

        public static readonly Maybe<T> None = new Maybe<T>();

        public Maybe(T value) {
            ThrowWhen(value, IsNullOrDefault, nameof(value));
            _value = value;
        }

        public bool IsNone => Equals(None);

        public T TryGetValue(T defaultValue) =>
            IsNone ? _value : defaultValue;

        public bool Equals(Maybe<T> other) =>
            EqualityComparer<T>.Default.Equals(_value, other._value);

        public override bool Equals(object obj) =>
            obj is Maybe<T> other && Equals(other);

        public override int GetHashCode() =>
            EqualityComparer<T>.Default.GetHashCode(_value);

        public override string ToString() =>
            IsNone ? "None" : $"Some ({_value.ToString()})";

    }

}