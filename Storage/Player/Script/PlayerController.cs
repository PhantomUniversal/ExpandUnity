using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PhantomEngine
{
    // Require data get -> GameData 
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, PlayerInput.IBaseActions
    {
        private Transform player;
        private Transform playerCamera;
        private CharacterController playerController;
        private AsyncOperationHandle<PlayerData> playerHandle;
        private PlayerData playerData;
        private PlayerInput playerInput;
        private Vector3 playerMove;
        private Vector3 playerVelocity;
        private float playerSpeed;


        public bool IsGrounded
        {
            get
            {
                float distance = 0.2f;
                Vector3 origin = transform.position + Vector3.up * 0.1f;
                return Physics.Raycast(origin, Vector3.down, distance);
            }
        }

        public bool IsBehavior => playerData != null;
        
        public bool IsVelocity => playerVelocity.y < 0;
        
        public bool IsRun { get; private set; }
        
        
        private void Awake()
        {
            player = gameObject.transform;
            playerCamera = Camera.main?.transform;
            playerController = GetComponent<CharacterController>();
        }
        
        private void OnEnable()
        {
            playerInput = new PlayerInput();
            playerInput.Base.SetCallbacks(this);
            playerInput.Enable(); 
        }

        private void Start()
        {
            Patch().Forget();
        }

        private void Update()
        {
            Calculate();
        }

        private void OnDrawGizmos()
        {
            // Down
            // Gizmos.color = Color.red;
            // Gizmos.DrawRay(transform.position, transform.down.normalized * 5f);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 1f);
        }

        private void OnDisable()
        {
            playerInput.Disable();   
            playerInput.Base.RemoveCallbacks(this);
            playerInput = null;
        }

        private void OnDestroy()
        {
            Release();
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 playerContext = context.ReadValue<Vector2>();
            Vector3 playerForward = playerCamera.forward;
            playerForward.y = 0f;
            playerForward.Normalize();
            
            Vector3 playerRight = playerCamera.right;
            playerRight.y = 0f;
            playerRight.Normalize();
            
            playerMove = (playerForward * playerContext.y + playerRight * playerContext.x).normalized;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Jump();
            }
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsRun = true;
            }

            if (context.canceled)
            {
                IsRun = false;
            }
        }
        
        
        private async UniTask Patch()
        {
            playerHandle = Addressables.LoadAssetAsync<PlayerData>("PlayerData");
            await playerHandle;

            if (playerHandle.Status != AsyncOperationStatus.Succeeded)
                return;

            if (playerHandle.IsValid())
            {
                playerData = playerHandle.Result;   
            }
        }

        private void Release()
        {
            if (playerHandle.IsValid())
            {
                Addressables.Release(playerHandle);
            }
        }
        
        private void Calculate()
        {
            if (!IsBehavior)
                return;
            
            if (IsGrounded && IsVelocity)
            {
                playerVelocity.y = -0.5f;
            }
            
            Vector3 playerCalculate = playerMove * playerSpeed;
            playerCalculate.y = playerVelocity.y;
            playerController.Move(playerCalculate * Time.deltaTime);
            
            if (playerMove.magnitude > 0.1f)
            {
                playerSpeed = IsRun ? playerData.RunValue : playerData.WalkValue;
                
                Quaternion playerRotation = Quaternion.LookRotation(playerMove);
                player.rotation = Quaternion.Slerp(player.rotation, playerRotation, 10f * Time.deltaTime);
            }
            else
            {
                playerSpeed = 0;
            }
        }
        
        private void Jump()
        {
            if (!IsBehavior || !IsGrounded) 
                return;
            
            playerVelocity.y = Mathf.Sqrt(playerData.JumpValue * -2f * playerData.GravityValue);
            playerController.Move(playerVelocity * Time.deltaTime);
        }
        
        // Test
        // private Animator playerAnimator;
        //
        // private void AnimationTest()
        // {
        //     playerAnimator.SetTrigger("Jump");
        //     playerAnimator.ResetTrigger("Idle");
        // }
    }
}