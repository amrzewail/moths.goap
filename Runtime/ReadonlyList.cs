using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moths.GOAP.Collections
{
    public struct ReadonlyList<T>
    {
        private List<T> _list;

        public T this[int index] => _list[index];

        public int Length => _list.Count;

        public ReadonlyList(List<T> source)
        {
            _list = source;
        }

        public static implicit operator ReadonlyList<T>(List<T> source)
        {
            return new ReadonlyList<T> (source);
        }

    }
}