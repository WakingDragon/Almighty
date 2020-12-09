using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP.Almighty
{
    public class Arena : MonoBehaviour
    {
        [SerializeField] private GameObject ground = null;
        private Vector3 size;

        private void OnEnable()
        {
            size = ground.transform.localScale;
        }
    }
}


