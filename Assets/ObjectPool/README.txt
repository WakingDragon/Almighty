OBJECT POOLER

Place the pooler in any scene where it will be used. The pooler should only operate within a scene.

Each item that will be pooled should have an IPoolableAsset asset.

THE ASSET NEEDS
1. Prefab
2. Number to pool initially
3. Whether it is expandable
4. You can add different functions into the instantiation of each pool GO to allow for things like setting initial states etc.

TO POOL
> The "spawner" should connect to the singleton pooler
> pooler.TryCreateNewPool(asset)

GET FROM POOL
> pooler.GetObjectFromPool(GO)

RETURN TO POOL
> you can use AutoPool on an object OR
> use pooler.Repool(GO)
