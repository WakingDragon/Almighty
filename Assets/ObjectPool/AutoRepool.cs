using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP.ObjectPooling
{
    public class AutoRepool : MonoBehaviour
    {
        [SerializeField] private float lifetime = 2f;
        private float timeToRepool;

        private void OnEnable()
        {
            SetLifeTime(lifetime);
        }

        private void LateUpdate()
        {
            if (Time.time >= timeToRepool) { Repool(); }
        }

        private void Repool()
        {
            if (ObjectPool.instance != null)
            {
                ObjectPool.instance.Repool(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetLifeTime(float newLifetime)
        {
            timeToRepool = Time.time + newLifetime;
        }
    }
}

