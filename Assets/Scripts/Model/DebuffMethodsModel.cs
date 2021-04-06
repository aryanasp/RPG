using Controller;
using UnityEngine;

namespace Model
{
    public class DebuffMethodsModel
    {
        public void Root(MovementController movementController, bool rootCondition)
        {
            movementController.StopWalk(rootCondition);
        }
    }
}