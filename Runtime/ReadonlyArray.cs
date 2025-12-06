using Moths.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moths.GOAP.Collections
{
    public struct ReadonlyArray<T>
    {
        private T[] _array;
        private InterfaceReference<T>[] _refsArray;
        private int _length;

        public T this[int index] => _array != null ? _array[index] : _refsArray[index];

        public int Length => _length;

        public ReadonlyArray(T[] source)
        {
            this = default;
            _array = source;
            _length = _array.Length;
        }

        public ReadonlyArray(InterfaceReference<T>[] source)
        {
            this = default;
            _refsArray = source;
            _length = _refsArray.Length;
        }

        public static implicit operator ReadonlyArray<T>(T[] source)
        {
            return new ReadonlyArray<T> (source);
        }

        public static implicit operator ReadonlyArray<T>(InterfaceReference<T>[] source)
        {
            return new ReadonlyArray<T> (source);
        }
    }
}