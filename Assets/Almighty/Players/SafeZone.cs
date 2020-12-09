using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP.Almighty
{
    public class SafeZone : MonoBehaviour
    {
        [SerializeField] private GameObject safeZoneMesh = null;
        [SerializeField] private float zoneDiameter = 15f;
        private float zoneDepth = 0.01f;

        private void Start()
        {
            var goTransform = safeZoneMesh.transform;
            goTransform.localScale = new Vector3(zoneDiameter,zoneDepth,zoneDiameter);
        }
    }
}


