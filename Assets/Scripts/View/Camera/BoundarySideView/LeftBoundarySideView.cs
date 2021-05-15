using UnityEngine;

namespace View
{
    public class LeftBoundarySideView : MonoBehaviour, IBoundarySideView
    {
        public string BoundarySide => "Left";
        public Vector3 BoundarySideVector => Vector3.left;
    }
}