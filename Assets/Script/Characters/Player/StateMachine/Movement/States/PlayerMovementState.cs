using UnityEngine;
using UnityEngine.InputSystem;

namespace GameDesign
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine stateMachine;
        protected PlayerGroundedData movementData;

        protected bool ShouldInstantRotate;

        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            
            stateMachine = playerMovementStateMachine;
            movementData = stateMachine.Player.Data.GroundedData;
            InitializeData();

            

            
        }

        private void InitializeData()
        {
            stateMachine.ReusableData.TimeToReachTargetRotation = movementData.BaseRotationData.TargetRotationReachTime;
        }

        #region IState Methods
        public virtual void Enter()
        {
            AddInputActionsCallbacks();
        }

        public virtual void Exit()
        {
            RemoveInputActionsCallbacks();
        }

        public virtual void HandleInput()
        {
            ReadMovementInput();
        }

       

        public virtual void PhysicsUpdate()
        {
            Move();
        }

        public virtual void Update()
        {
            //stateMachine.Player.MxM.InputVector = GetMovementInputDirection();
            //Debug.Log("shouldWalk:" + shouldWalk);
            
        }


        public virtual void OnAnimationEnterEvent()
        {

        }

        public virtual void OnAnimationExitEvent()
        {
        }

        public virtual void OnAnimationTransitionEvent()
        {
        }

        public virtual void OnTriggerEnter(Collider collider)
        {

        }

        public virtual void OnTriggerExit(Collider collider)
        {

        }
        #endregion

        #region Main Methods

        private void ReadMovementInput()
        {
            stateMachine.ReusableData.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero || stateMachine.ReusableData.maxSpeed == 0f)
            {
                return;
            }

            

            // 获取基于摄像机方向的移动向量
            Vector3 movementDirection = GetCameraRelativeMovementDirection();



        }

        

        private float Rotate(Vector3 dirction)
        {
            float directionAndlge = UpdateTargetRotation(dirction);

            RotateTowardsTargetRotation();
            return directionAndlge;
        }




        private float AddCameraRotationToAngle(float directionAndlge)
        {
            directionAndlge += stateMachine.Player.MainCameraTransform.eulerAngles.y;

            if (directionAndlge > 360f)
            {
                directionAndlge -= 360f;
            }

            return directionAndlge;
        }

        private static float GetDirectionAngle(Vector3 dirction)
        {
            float directionAndlge = Mathf.Atan2(dirction.x, dirction.y) * Mathf.Rad2Deg;
            if (directionAndlge <= 0f)
            {
                directionAndlge += 360f;
            }

            return directionAndlge;
        }


        private void UpdateTargetRotationData(float targetAngle)
        {
            stateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;
            stateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
        }


        #endregion



        #region Reusable Methods
        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(stateMachine.ReusableData.MovementInput.x, 0f, stateMachine.ReusableData.MovementInput.y);
        }

        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;
            if (currentYAngle == stateMachine.ReusableData.CurrentTargetRotation.y)
            {
                return;
            }

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.ReusableData.CurrentTargetRotation.y, ref stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, stateMachine.ReusableData.TimeToReachTargetRotation.y - stateMachine.ReusableData.DampedTargetRotationPassedTime.y);
            stateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            //stateMachine.Player.Rigidbody.MoveRotation(targetRotation
            //stateMachine.Player.transform.rotation = targetRotation;

        }

        protected float UpdateTargetRotation(Vector3 dirction, bool shouldConsiderCameraRotation = true)
        {
            float directionAndlge = GetDirectionAngle(dirction);

            if (shouldConsiderCameraRotation)
            {
                directionAndlge = AddCameraRotationToAngle(directionAndlge);
            }
            

            if (directionAndlge != stateMachine.ReusableData.CurrentTargetRotation.y)
            { UpdateTargetRotationData(directionAndlge); }

            return directionAndlge;
        }

        protected Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        // 将输入转换为摄像机相对方向
        private Vector3 GetCameraRelativeMovementDirection()
        {
            // 获取摄像机前向和右向（忽略Y轴）
            Vector3 cameraForward = stateMachine.Player.MainCameraTransform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            Vector3 cameraRight = stateMachine.Player.MainCameraTransform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();

            // 组合输入方向（注意：input.y对应前向/后向）
            return cameraForward * stateMachine.ReusableData.MovementInput.y + cameraRight * stateMachine.ReusableData.MovementInput.x;
        }


        protected void RestVelocity()
        { stateMachine.Player.Rigidbody.linearVelocity = Vector3.zero; }


        protected virtual void AddInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;
        }


        protected virtual void RemoveInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;
        }


        #endregion



        #region Input Methods

        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            stateMachine.ReusableData.ShouldWalk = !stateMachine.ReusableData.ShouldWalk;
            
        }


        #endregion








    }
}