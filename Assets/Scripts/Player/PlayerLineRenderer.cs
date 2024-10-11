using UnityEngine;

namespace Player
{
    public class PlayerLineRenderer : MonoBehaviour
    {
        public LineRenderer lr;

        public void StartLine(Vector3 startPos)
        {
            lr.positionCount = 1;
            lr.SetPosition(0, startPos);
        }

        public void UpdateLine(Vector3 currentPos)
        {
            lr.positionCount = 2;
            lr.SetPosition(1, currentPos);
        }

        public void ClearLine()
        {
            lr.positionCount = 0;
        }
    }
}