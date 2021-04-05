using Controller;
using UnityEngine;

namespace Model
{
    public class DebuffModel
    {
        public void Root(MovementController movementController, bool rootCondition)
        {
            movementController.StopWalk(rootCondition);
        }
    }
}