using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moths.GOAP
{
    public struct Context
    {
        private static uint _globalId = 0;
        private static Dictionary<Type, object>[] _pool = new Dictionary<Type, object>[1024];

        private uint _id;
        private Dictionary<Type, object> _types;

        public static Context Create()
        {
            if (_pool[0] == null)
            {
                for (int i = 0; i < _pool.Length; i++) _pool[i] = new Dictionary<Type, object>(100);
            }

            var context = new Context();
            context._id = _globalId++;
            context._types = _pool[context._id % _pool.Length];
            context._types.Clear();

            return context;
        }

        public bool TryReadValue<TValue>(out TValue value)
        {
            if (_types.TryGetValue(typeof(TValue), out object v))
            {
                value = (TValue)v;
                return true;
            }
            value = default;
            return false;
        }

        public void SetValue<TValue>(TValue value)
        {
            _types[typeof(TValue)] = value;
        }
    }
}