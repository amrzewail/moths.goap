using Moths.GOAP.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moths.GOAP
{
    public struct GOAPPlan
    {
        private static uint _globalId = 0;
        private static List<GOAPAction>[] _pool = new List<GOAPAction>[4096];


        private uint _id;
        private List<GOAPAction> _actions;
        private int _count;
        private int _currentIndex;

        public GOAPAction Current => _actions[_count - _currentIndex - 1];

        public void Next() => _currentIndex++;

        public bool IsComplete() => _currentIndex == _count;

        public void Push(GOAPAction action)
        {
            _actions.Add(action);
            _count++;
        }

        public static GOAPPlan Create()
        {
            if (_pool[0] == null)
            {
                for (int i = 0; i < _pool.Length; i++) _pool[i] = new List<GOAPAction>(100);
            }

            var plan = new GOAPPlan();
            plan._id = _globalId++;
            plan._actions = _pool[plan._id % _pool.Length];
            plan._actions.Clear();
            plan._count = 0;

            return plan;
        }
    }
}