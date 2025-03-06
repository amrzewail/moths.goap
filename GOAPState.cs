using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public abstract class GOAPState : ScriptableObject
    {
        public abstract bool Validate(ref Context context);
        
    }
}