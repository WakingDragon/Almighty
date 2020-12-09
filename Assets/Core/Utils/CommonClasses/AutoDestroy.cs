using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP.Core
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] float lifespan = 2f;
        private float timeToDie;

        private void Start()
        {
            timeToDie = Time.time + lifespan;
        }

        private void Update()
        {
            if (timeToDie <= Time.time)
            {
                Destroy(gameObject);
            }
        }

    }
}

