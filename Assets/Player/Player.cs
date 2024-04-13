using UnityEngine;
using UnityEngine.InputSystem;

public enum EMovementDirection
{
    Undefined = 0,
    Right,
    Left
}

public class Player : MonoBehaviour
{
	[SerializeField] private float MovementSpeed = 1.0f;
	[SerializeField] private float JumpForce = 1.0f;
	[SerializeField] private int _Health = 5;

	private GameObject _PlayerObject = null;
    private Rigidbody2D _RB = null;
    private PlayerInputAction _PlayerControls;
	private InputAction _Move, _Interact, _Jump;
	private Vector2 _MoveDirection;
	private bool _CanJump = true;
	private ECollectibles _CurrentItems = 0;

	#region Input

	    #region Input Management
	    private void ProcessInput()
        {
		    _MoveDirection = _Move.ReadValue<Vector2>();
	    }
		#endregion

		#region Input Actions       
		private void BindActions()
		{
			_Move = _PlayerControls.Player.Move;
			_Move.performed += OnMove;
			_Move.Enable();

			_Interact = _PlayerControls.Player.Interact;
			_Interact.Enable();
			_Interact.performed += OnInteract;

			_Jump = _PlayerControls.Player.Jump;
			_Jump.Enable();
			_Jump.performed += OnJump;
		}
		private void DisableActions()
		{
			_Move.Disable();
			_Interact.Disable();
			_Jump.Disable();
		}
        private void MovePlayer()
        {
            if (!_RB)
                return;
            _RB.velocity = new Vector2(_MoveDirection.x * MovementSpeed, _RB.velocity.y);
		}   
		private void OnMove(InputAction.CallbackContext Context)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, Context.ReadValue<Vector2>().x > 0 ? 0 : 180, transform.eulerAngles.z);
		}
        private void OnInteract(InputAction.CallbackContext Context)
        {
            Debug.Log("INTERACTING");
        }
	    private void OnJump(InputAction.CallbackContext Context)
	    {
            if (!_RB)
                return;

		    _CanJump = Mathf.Abs(_RB.velocity.y) < 0.0001f;
		    if (!_CanJump)
			    return;
		    if (_RB)
			    _RB.AddForce(new Vector2(0.0f, JumpForce));
	    }
	    #endregion

	#endregion

	#region Health
    public int GetHealth() { return _Health; }
	public void DamagePlayer() 
	{
		if(--_Health <= 0)
		{
			Destroy(gameObject);
			//TODO: Trigger some level finish here or something
		}
	}
    #endregion

    #region Items

    /*
     ----------------------------------------------------------------
     --                 Current Items Is a BitMask                 --
     ----------------------------------------------------------------
    */
    public ECollectibles GetCurrentItems() { return _CurrentItems; }
    public void TryAddItem(ECollectibles Item) { _CurrentItems |= Item; }
	#endregion

	#region Unity Interface
	private void Awake()
	{
		_PlayerControls = new PlayerInputAction();
	}
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
        MovePlayer();
    }
	private void OnEnable()
	{
		BindActions();
	}
	private void OnDisable()
	{
		DisableActions();
	}
	#endregion
}
