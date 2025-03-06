using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GOAP
{
    public abstract class Modifier : ScriptableObject
    {
        public abstract float GetValue(Context context);
    }
}