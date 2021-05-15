using UnityEngine;

namespace View
{
    public interface IBoundarySideView
    {
        string BoundarySide { get; }
        Vector3 BoundarySideVector { get; }
    }
}