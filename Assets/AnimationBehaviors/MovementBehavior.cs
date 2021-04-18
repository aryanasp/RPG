using Controller;
using UnityEngine;
using View;

namespace AnimationBehaviors
{
    public class MovementBehavior : StateMachineBehaviour
    {
        private ICharacterMovementAnimation _characterMovementView;
        private static readonly int Vx = Animator.StringToHash("vx");
        private static readonly int Vy = Animator.StringToHash("vy");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _characterMovementView = animator.gameObject.GetComponent<ICharacterMovementAnimation>();
            _characterMovementView.OnVelocityChangedInView += AnimateMovement;
        }
        
        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            HandleLayers(animator);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _characterMovementView.OnVelocityChangedInView -= AnimateMovement;
        }


        void HandleLayers(Animator animator)
        {
            ActivateLayer("MovementLayer", animator);
        }
        private void AnimateMovement(object sender, VelocityChangedInViewEventArgs e)
        {
            //Sets the animation parameter so character animation walks in the correct direction
            e.Animator.SetFloat(Vx, e.Velocity.x);
            e.Animator.SetFloat(Vy, e.Velocity.y);
        }

        private void ActivateLayer(string layerName, Animator animator)
        {
            for (int i = 0; i < animator.layerCount; i++)
            {
                animator.SetLayerWeight(i, 0);
            }
            animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
        }
    }
}