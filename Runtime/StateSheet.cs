using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moths.GOAP
{
    [System.Serializable]
    public struct StateSheet
    {
        [SerializeReference]
        [SerializeField]
        public Dictionary<StateKey, bool> _properties;
    }
}