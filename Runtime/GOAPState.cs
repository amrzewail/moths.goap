using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moths.GOAP
{
    public abstract class GOAPState : ScriptableObject
    {
        public abstract bool Validate(ref Context context);
        
    }
}