using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP.Almighty
{
    public static class GameStatics
    {
        private static int LAYER_ARENA = 8;

        public static int arenaLayerMask
        {
            get
            {
                return 1 << LAYER_ARENA;
            }
        }
    }
}

