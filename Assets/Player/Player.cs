using System.Runtime.CompilerServices;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

using Input = UnityEngine.Input; // explicitly defined due to UnityEngine.Windows.Input causing potential issues

public enum EInputRoute
{
    Game = 0, // default
    UI
}
public enum EMovementDirection
{
    Undefined = 0,
    Right,
    Left
}

public class Player : MonoBehaviour
{
    // References
    private GameObject _PlayerObject = null;
    private Rigidbody2D _RB = null;

    #region Input
    
        #region Parameters
        [SerializeField] private float MovementSpeed = 1.0f;
        [SerializeField] private float JumpForce = 1.0f;
        #endregion

        #region Input Routing
        /*
            ---------------------------------------------------------------------
            -- Input modes are used to have ui in game that takes over the     --
            -- input. This approach allows for easy management.                --
            --                                                                 --
            --                         INPUT ROUTING                           --
            ---------------------------------------------------------------------
        */
        private EInputRoute InputMode = EInputRoute.Game;
        public EInputRoute GetInputMode() { return InputMode; }
        private void SetInputMode(EInputRoute NewMode) { InputMode = NewMode; } 
        #endregion

        #region Input Management
        private void ProcessInput()
        {
            switch (GetInputMode())
            {
                case EInputRoute.Game:
                    GameInput();
                    break;
                case EInputRoute.UI:
                    UIInput();
                    break;
                default:
                    break;
            }
        }
        private void GameInput()
        {
            _IsMoving = false;
            if (Input.GetButton("Horizontal"))
            {
                _IsMoving = true;
                _MovementDirection = Input.GetAxis("Horizontal") > 0 ? EMovementDirection.Right : EMovementDirection.Left;
                Debug.Log(_MovementDirection.ToString());
            }
            if (Input.GetButtonDown("Vertical"))
            {
                if(Input.GetAxis("Vertical") > 0)
                {
                    Jump();
			    } 
            }
        }
        private void UIInput()
        {

        }
        #endregion

        #region Input Actions

            #region Jump
            private bool CanJump = true;
            private void Jump()
            {
                CanJump = Mathf.Abs(_RB.velocity.y) < 0.0001f;
                if (!CanJump)
                    return;
                if (_RB)
                    _RB.AddForce(new Vector2(0.0f, JumpForce));
            }
    #endregion

            #region Horizontal Movement 
            private bool _IsMoving = false;
            private EMovementDirection _MovementDirection; //Garanteed initialization to EMovementDirection.Undefined
            private void UpdatePlayerLocation()
            {
                if (!_PlayerObject)
                    return;
                if (_IsMoving)
                {
                    switch (_MovementDirection)
                    {
                        case EMovementDirection.Right:
                            _PlayerObject.transform.localPosition += new Vector3(MovementSpeed * Time.deltaTime,0,0);
                            break;
                        case EMovementDirection.Left:
                            _PlayerObject.transform.localPosition += new Vector3((MovementSpeed * Time.deltaTime) * -1.0f, 0, 0);
                            break;
                    }
                }
            }   
            private void UpdatePlayerDirection()
            {
                if(!_PlayerObject)
                    return;
                Quaternion CurrentRot = _PlayerObject.transform.rotation;
                switch (_MovementDirection)
                {
                    case EMovementDirection.Right:
                        _PlayerObject.transform.rotation = new Quaternion(CurrentRot.x, 0, CurrentRot.z, CurrentRot.w);
                        break;
                    case EMovementDirection.Left:
                        _PlayerObject.transform.rotation = new Quaternion(CurrentRot.x, 180, CurrentRot.z, CurrentRot.w);
                        break;
                }
            }
            #endregion

    #endregion

    #endregion

    #region Health
    [SerializeField]
    private int _Health = 5;
    public int GetHealth() { return _Health; }
    #endregion

    #region Items

    /*
     ----------------------------------------------------------------
     --                 Current Items Is a BitMask                 --
     ----------------------------------------------------------------
    */

    private int _CurrentItems = 0; 
    public int GetCurrentItems() { return _CurrentItems; }
    #endregion

    #region Unity Interface
    private void Start()
    {
		//SaveSystem.SaveProgress(this);
        _PlayerObject = gameObject;
        _RB = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        ProcessInput();
    }
    private void FixedUpdate()
    {
        UpdatePlayerLocation();
		UpdatePlayerDirection();
    }
    #endregion
}
