using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moths.GOAP
{
    public abstract class AISensor : MonoBehaviour
    {

        [SerializeField] GOAPGoal _goal;

        protected GOAPAgent Agent { get; private set; }

        private void Awake()
        {
            Agent = GetComponentInParent<GOAPAgent>();
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