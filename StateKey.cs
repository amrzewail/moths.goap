using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    [CreateAssetMenu(menuName = "GOAP/State Key")]
    public class StateKey : ScriptableObject
    {
        public static implicit operator string(StateKey instance)
        {
            return instance.name;
        }

        public static bool operator==(StateKey key1, string keyString)
        {
            return (string)key1 == keyString;
        }

        public static bool operator !=(StateKey key1, string keyString)
        {
            return (string)key1 != keyString;
        }

        public override bool Equals(object obj)
        {
            return obj is StateKey key &&
                   base.Equals(obj) &&
                   name == key.name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name);
        }
    }
}