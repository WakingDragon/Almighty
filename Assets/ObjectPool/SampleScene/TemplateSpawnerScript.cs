using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BP.ObjectPooling;

public class TemplateSpawnerScript : MonoBehaviour
{
    [SerializeField] private ExampleObjectAsset assetToPool = null;
    [SerializeField] private float spawnInterval = 0.1f;

    private void Start()
    {
        ObjectPool.instance.TryCreateNewPool(assetToPool);
        StartCoroutine(CreateObjects());
    }

    IEnumerator CreateObjects()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnInterval);
            ObjectPool.instance.GetObjectFromPool(assetToPool, RandomisePos(), Quaternion.identity);
        }
    }

    private Vector3 RandomisePos()
    {
        Vector3 pos = transform.position;
        return new Vector3(RandomFloat(pos.x), RandomFloat(pos.y), RandomFloat(pos.z));
    }

    private float RandomFloat(float input)
    {
        return Random.Range(input - 0.1f, input + 0.1f);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
