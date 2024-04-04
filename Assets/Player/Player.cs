using UnityEngine;

using Input = UnityEngine.Input; // explicitly defined due to UnityEngine.Windows.Input causing potential issues

public enum InputRoute
{
    Game = 0, // default
    UI
}

public class Player : MonoBehaviour
{
    // References
    private GameObject PlayerObject = null;
    private Rigidbody2D RB = null;

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
        private InputRoute InputMode = InputRoute.Game;
        public InputRoute GetInputMode() { return InputMode; }
        private void SetInputMode(InputRoute NewMode) { InputMode = NewMode; } 
        #endregion

        #region Input Management
        private void ProcessInput()
        {
            switch (GetInputMode())
            {
                case InputRoute.Game:
                    GameInput();
                    break;
                case InputRoute.UI:
                    UIInput();
                    break;
                default:
                    break;
            }
        }
        private void GameInput()
        {
            if (Input.GetButton("Horizontal"))
            {
                bool IsMovingRight = Input.GetAxis("Horizontal") > 0;
                Debug.Log(IsMovingRight);
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
                CanJump = Mathf.Abs(RB.velocity.y) < 0.0001f;
                if (!CanJump)
                    return;
                if (RB)
                    RB.AddForce(new Vector2(0.0f, JumpForce));
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
        PlayerObject = GetComponent<GameObject>();
        RB = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        ProcessInput();
    }
    private void FixedUpdate()
    {
    }
    #endregion
}
