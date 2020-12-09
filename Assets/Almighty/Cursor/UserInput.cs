using UnityEngine;

namespace BP.Almighty
{
    public class UserInput : MonoBehaviour
    {
        private CursorStatus cursorStatus;

        private void Awake()
        {
            cursorStatus = GetComponent<CursorStatus>();
        }

        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, GameStatics.arenaLayerMask))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    cursorStatus.NotifyCursorPosOnArenaWithClick(hit.point, true);
                }
                else
                {
                    cursorStatus.NotifyCursorPosOnArena(hit.point);
                }
                    
            }
            else
            {
                cursorStatus.NotifyCursorOffArena();
            }
        }
    }
}

