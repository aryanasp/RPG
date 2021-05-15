using UnityEngine;

namespace View
{
    public class DownBoundarySideView : MonoBehaviour, IBoundarySideView
    {
        public string BoundarySide => "Down";
        public Vector3 BoundarySideVector => Vector3.down;
    }
}