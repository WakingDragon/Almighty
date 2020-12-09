using UnityEngine;

namespace BP.Almighty
{
    public class CursorStatus : MonoBehaviour
    {
        [SerializeField] private GameObject standardCursor = null;

        [SerializeField] private Material cursorMat = null;
        [SerializeField] private Color normalColor = Color.yellow;
        [SerializeField] private Color clickedColor = Color.red;
        private string cursorColorVar = "CursorPrimaryColor";

        private Player player;


        private enum StatusType { normal, clicked }

        private void Start()
        {
            HideCursor();
            player = GameManager.instance.GetPlayer1PlayerComponent();
        }

        public void NotifyCursorPosOnArena(Vector3 newPos)
        {
            if(CursorHasMoved(newPos))
            {
                MoveCursor(newPos);
            }
        }

        public void NotifyCursorPosOnArenaWithClick(Vector3 newPos, bool hasClicked)
        {
            ChangeCursorColor(StatusType.clicked);
            player.TryClickAction(newPos);
        }

        public void NotifyCursorOffArena()
        {
            HideCursor();
        }

        private bool CursorHasMoved(Vector3 newPos)
        {
            if(newPos != transform.position)
            {
                return true;
            }
            return false;
        }

        private void MoveCursor(Vector3 newPos)
        {
            standardCursor.SetActive(true);
            ChangeCursorColor(StatusType.normal);
            transform.position = newPos;
        }

        private void ChangeCursorColor(StatusType newType)
        {
            if(newType == StatusType.normal)
            {
                cursorMat.SetColor(cursorColorVar, normalColor);
            }

            if (newType == StatusType.clicked)
            {
                cursorMat.SetColor(cursorColorVar, clickedColor);
            }
        }

        private void HideCursor()
        {
            standardCursor.SetActive(false);
        }
    }
}

