using UnityEngine;

namespace Moths.GOAP.Samples
{
    public class Agent : GOAPAgent
    {
        [Range(0, 100f)]
        public float satiation = 100;
        [Range(0, 100f)]
        public float awakeness = 100; public int food = 0;
        public int money = 0;

        protected override void PrepareContext(ref Context context)
        {
            base.PrepareContext(ref context);

            context.SetValue(this);
        }

        private void Start()
        {
            Replan();
        }
    }
}