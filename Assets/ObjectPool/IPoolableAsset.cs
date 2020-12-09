using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolableAsset
{
    GameObject InstantiatePrefabToPool(Transform parent);

    string GetIPoolableAssetName();
    IPoolableAsset GetIPoolableAsset();
    int GetNumberToPool();
    bool GetIsPoolExpandable();
}
