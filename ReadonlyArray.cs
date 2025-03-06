using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP.Collections
{
    public struct ReadonlyArray<T>
    {
        private T[] _array;

        public T this[int index] => _array[index];

        public int Length => _array.Length;

        public ReadonlyArray(T[] source)
        {
            _array = source;
        }

        public static implicit operator ReadonlyArray<T>(T[] source)
        {
            return new ReadonlyArray<T> (source);
        }

    }
}