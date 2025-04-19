using UnityEngine;
using UnityEngine.Events;
using MxM;
using System;

namespace GameDesign
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {

        [field: Header("Reference")]
        [field: SerializeField] public PlayerSO Data { get; private set; }

        private PlayerMovementStateMachine movementStateMachine;
        private PlayerCombatStateMachine combatStateMachine;

        private UnityAction<bool> mouseControlListener;

        public PlayerInput Input { get; private set; }
        public PlayerStats stats { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Transform MainCameraTransform { get; private set; }
        public GameObject Weapon {  get; private set; }

        public PlayerReusableData ReusableData { get; private set; }

        


        private void Awake()
        {

            //Rigidbody.freezeRotation = true;


            movementStateMachine = new PlayerMovementStateMachine(this);
            combatStateMachine = new PlayerCombatStateMachine(this);

            ReusableData = new PlayerReusableData();

            Rigidbody = GetComponentInChildren<Rigidbody>();
            Input = GetComponent<PlayerInput>();
            stats = GetComponent<PlayerStats>();
            MainCameraTransform = Camera.main.transform;
            initWeapon();

            mouseControlListener = new UnityAction<bool>(ToggleMouseControl);

        }

        private void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdlingState);
            combatStateMachine.ChangeState(combatStateMachine.CombatNullState);
        }



        private void Update()
        {
            movementStateMachine.HandleInput();
            movementStateMachine.Update();
        }


        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
            
        }

        public void ToggleWeapon()
        {
            ReusableData.WeaponShow = !ReusableData.WeaponShow;
            Weapon.SetActive(ReusableData.WeaponShow);
        }

        public float PlayerAtkDamage()
        {
            return ReusableData.PlayerAtk;
        }


        private void initWeapon()
        {
            foreach (Transform child in GetComponentsInChildren<Transform>())
            {
                if (child.CompareTag("weapon"))
                {
                    Weapon = child.gameObject;
                    Weapon.SetActive(false);
                    break;
                }
            }
        }

        private void ToggleMouseControl(bool useMouse)
        {
            ReusableData.UseMouse = useMouse;
            if (useMouse)
            {
                Input.PlayerActions.Disable();
                EventManager.TriggerEvent<CameraLockEvent, bool>(true);
            }
            else
            {
                
                EventManager.TriggerEvent<CameraLockEvent, bool>(false);
                Input.PlayerActions.Enable();
            }
        }

        private void OnEnable()
        {
            EventManager.StartListening<MouseControlEvent, bool>(mouseControlListener);
        }

        private void OnDisable()
        {
            EventManager.StopListening<MouseControlEvent, bool>(mouseControlListener);
        }
    
    }

}