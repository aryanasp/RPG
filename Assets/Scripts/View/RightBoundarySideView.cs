using System;
using UnityEngine;

namespace View
{
    public class RightBoundarySideView : MonoBehaviour, IBoundarySideView
    {
        public string BoundarySide => "Right";
        public Vector3 BoundarySideVector => Vector3.right;
    }
}