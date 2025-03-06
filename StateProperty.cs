using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    [System.Serializable]
    public struct StateProperty
    {
        [SerializeField] StateKey _key;
        [SerializeField] bool _value;

        public string Key => _key;

        public bool Value
        {
            get => _value;
            set => _value = value;
        }
    }
}