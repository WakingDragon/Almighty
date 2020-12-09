using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP.Almighty
{
    [CreateAssetMenu(fileName ="PlayerLibrary_asset",menuName ="Almighty!/(single) Player Library")]
    public class PlayerList_asset : ScriptableObject
    {
        [SerializeField] private List<Player_asset> playerAssets = new List<Player_asset>();
        private Player_asset player1Asset = null;
        private Player_asset player2Asset = null;

        public void SetPlayer1Asset(Player_asset newAsset)
        {
            player1Asset = newAsset;
            Debug.Log("player 1 set");
        }

        public void SetPlayer2Asset(Player_asset newAsset)
        {
            player2Asset = newAsset;
            Debug.Log("player 1 set");

            
        }
    }
}


