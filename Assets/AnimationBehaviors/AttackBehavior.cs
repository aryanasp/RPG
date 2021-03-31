using Controller;
using UnityEngine;

namespace AnimationBehaviors
{
    public class AttackBehavior : StateMachineBehaviour
    {
        private MovementController _movementController;
        private AttackController _attackController;
        
        private static readonly int AnimatorAttack = Animator.StringToHash("attack");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var animatedGameObject = animator.gameObject;
            _movementController = animatedGameObject.GetComponent<MovementController>();
            _attackController = animatedGameObject.GetComponent<AttackController>();
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            HandleLayers(animator);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    
        void HandleLayers(Animator animator)
        {
            //Sync animator speed and character speed
            animator.speed = _movementController.MoveSpeed;
            
            if (_attackController.IsAttacking)
            {
                ActivateLayer("AttackLayer", animator);
                AnimateAttack(animator);
            }
        
        }
        private void ActivateLayer(string layerName, Animator animator)
        {
            for (int i = 0; i < animator.layerCount; i++)
            {
                animator.SetLayerWeight(i, 0);
            }

            animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
        }
    
        private void AnimateAttack(Animator animator)
        {
            animator.SetBool(AnimatorAttack, _attackController.IsAttacking);
        }
    
    
    }
}
