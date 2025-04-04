using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moths.GOAP
{
    public abstract class AISensor : MonoBehaviour
    {

        [SerializeField] GOAPGoal _goal;

        protected AIAgent Agent { get; private set; }

        private void Awake()
        {
            Agent = GetComponentInParent<AIAgent>();
        }

        protected void ApplyGoal()
        {
            Agent.SetGoal(_goal);
        }

        protected void UnapplyGoal()
        {
            Agent.RemoveGoal(_goal);
        }

    }
}