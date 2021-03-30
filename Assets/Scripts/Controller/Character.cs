using UnityEngine;

namespace Controller
{
    public abstract class Character : MonoBehaviour
    {
    //     //Animator parameter variables
    //     private static readonly int Vx = Animator.StringToHash("vx");
    //     private static readonly int Vy = Animator.StringToHash("vy");
    //     
    //
    //     protected MovementController MovementController;
    //
    //     protected AttackController AttackController;
    //
    //     protected Animator Animator { get; set; }
    //
    //
    //     // Start is called before the first frame update
    //     protected virtual void Start()
    //     {
    //         InitializeGame();
    //     }
    //
    //     void InitializeGame()
    //     {
    //         MovementController = GetComponent<MovementController>();
    //         AttackController = GetComponent<AttackController>();
    //         Animator = GetComponent<Animator>();
    //         
    //     }
    //
    //     // Update is called once per frame
    //     protected virtual void Update()
    //     {
    //         HandleLayers();
    //     }
    //     
    //     //Handle Animations
    //     void HandleLayers()
    //     {
    //         //Sync animator speed and character speed
    //         Animator.speed = MovementController.MoveSpeed;
    //         
    //         if (MovementController.IsMoving)
    //         {
    //             ActivateLayer("WalkLayer");
    //             AttackController.StopAttack();
    //             AnimateMovement();
    //
    //         }
    //         else if (AttackController.IsAttacking)
    //         {
    //             ActivateLayer("AttackLayer");
    //         }
    //         else
    //         {
    //             ActivateLayer("IdleLayer");
    //         }
    //     }
    //     
    //     
    //     private void AnimateMovement()
    //     {
    //         //Sets the animation parameter so character animation walks in the correct direction
    //         Animator.SetFloat(Vx, MovementController.MoveDirection.x);
    //         Animator.SetFloat(Vy, MovementController.MoveDirection.y);
    //     }
    //
    //     private void ActivateLayer(string layerName)
    //     {
    //         for (int i = 0; i < Animator.layerCount; i++)
    //         {
    //             Animator.SetLayerWeight(i, 0);
    //         }
    //
    //         Animator.SetLayerWeight(Animator.GetLayerIndex(layerName), 1);
    //     }
    //
    //     
    }
}