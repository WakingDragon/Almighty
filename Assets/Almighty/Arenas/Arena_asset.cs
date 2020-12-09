using UnityEngine;

namespace BP.Almighty
{
    [CreateAssetMenu(fileName ="Arena_asset",menuName ="Almighty!/New Arena")]
    public class Arena_asset : ScriptableObject
    {
        [SerializeField] private GameObject arenaPrefab = null;

        public Arena SetupArena(Vector3 pos)
        {
            var arenaGO = Instantiate(arenaPrefab);
            arenaGO.transform.position = pos;
            var arena = arenaGO.GetComponent<Arena>();
            return arena;
        }
    }
}

