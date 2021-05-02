using UnityEngine;

namespace View
{
    public class UpBoundarySideView : MonoBehaviour, IBoundarySideView
    {
        public string BoundarySide => "Up";
        public Vector3 BoundarySideVector => Vector3.up;
    }
}