using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum EMovementDirection
{
    Undefined = 0,
    Right,
    Left
}

public class Player : MonoBehaviour
{
	[SerializeField] private float _movementSpeed = 1.0f;
	[SerializeField] private float _jumpForce = 1.0f;
	[SerializeField] private int _health = 5;
	[SerializeField] private float _damageForce = 200.0f;
	[SerializeField] private bool _isSecondaryPlayer = false;
	[SerializeField] private float _attackRange = 1.0f;
	[SerializeField] private GameObject _pauseMenuContainer;
	[SerializeField] private float _killY = -50;

	[DoNotSerialize] public bool IsInsideSpikes = false;

	private GameObject _playerObject = null;
    private Rigidbody2D _rB = null;
	private CapsuleCollider2D _myCollider = null;
    private PlayerInputAction _playerControls;
    private PlayerInputAction1 _secondaryPlayerControls;
	private InputAction _move, _attack, _jump, _togglePauseMenu;
	private Vector2 _moveDirection;
	private bool _canJump = true;
	private ECollectibles _currentItems = 0;

	public delegate void OnPlayerTakeDamage(bool IsSecondaryPlayer);
	public event OnPlayerTakeDamage OnPlayerTakeDamageDelegate;

	#region Input

		#region Input Management
		private void ProcessInput()
        {
		    _moveDirection = _move.ReadValue<Vector2>();
	    }
		#endregion

		#region Input Actions       
		private void BindActions()
		{
			if(!_isSecondaryPlayer)
			{
				_move = _playerControls.Player.Move;
				_move.performed += OnMove;
				_move.Enable();

				_attack = _playerControls.Player.Attack;
				_attack.performed += OnAttack;
				_attack.Enable();
			
				_jump = _playerControls.Player.Jump;
				_jump.performed += OnJump;
				_jump.Enable();

				_togglePauseMenu = _playerControls.Player.TogglePauseMenu;
				_togglePauseMenu.performed += OnTogglePauseMenu;
				_togglePauseMenu.Enable();
			}
			else
			{
				_move = _secondaryPlayerControls.Player.Move;
				_move.performed += OnMove;
				_move.Enable();

				_attack = _secondaryPlayerControls.Player.Attack;
				_attack.performed += OnAttack;
				_attack.Enable();
			
				_jump = _secondaryPlayerControls.Player.Jump;
				_jump.performed += OnJump;
				_jump.Enable();
			}
		}
		private void DisableActions()
		{
			if(!_move.IsUnityNull())
				_move.Disable();
			if (!_attack.IsUnityNull())
				_attack.Disable();
			if(!_jump.IsUnityNull())
				_jump.Disable();
			if(!_togglePauseMenu.IsUnityNull())
				_togglePauseMenu.Disable();
		}
        private void MovePlayer()
        {
            if (!_rB)
                return;
            _rB.velocity = new Vector2(_moveDirection.x * _movementSpeed, _rB.velocity.y);
		}   
		private void OnMove(InputAction.CallbackContext Context)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, Context.ReadValue<Vector2>().x > 0 ? 0 : 180, transform.eulerAngles.z);
		}
	    private void OnJump(InputAction.CallbackContext Context)
	    {
            if (!_rB)
                return;

		    _canJump = Mathf.Abs(_rB.velocity.y) < 0.0001f;
		    if (!_canJump)
			    return;
		    if (_rB)
			    _rB.AddForce(new Vector2(0.0f, _jumpForce));
	    }
		private void OnAttack(InputAction.CallbackContext Context)
		{
			RaycastHit2D HitResult = Physics2D.Raycast(transform.position + (transform.right * (_myCollider.bounds.extents.x + 0.01f)), transform.right, _attackRange, 0x7FFFFFFF);
			if (!HitResult)
				return;
			Player OtherPlayer = HitResult.collider.GetComponent<Player>();
			if (OtherPlayer)
				OtherPlayer.DamagePlayer();
		}
		private void OnTogglePauseMenu(InputAction.CallbackContext Context)
		{
			if (_pauseMenuContainer)
				_pauseMenuContainer.SetActive(!_pauseMenuContainer.activeInHierarchy);
		}
		#endregion

	#endregion

	#region Health
	public int GetHealth() { return _health; }
	public void DamagePlayer() 
	{
		OnPlayerTakeDamageDelegate.Invoke(_isSecondaryPlayer);
		if(--_health <= 0)
		{
			Destroy(gameObject);
			//TODO: Trigger some level finish here or something
		}
	}
	public void TriggerPlayerDamageReaction(Vector2 Direction)
	{
		bool Right = Vector2.Angle(Vector2.up, Direction) < 90;
		if (_rB)
			_rB.AddForce(((Vector2.right * (Right ? 1 : -1)) * _damageForce) + Vector2.up * (_jumpForce / 2));
	}
	#endregion

	#region Items

	/*
     ----------------------------------------------------------------
     --                 Current Items Is a BitMask                 --
     ----------------------------------------------------------------
    */
	public ECollectibles GetCurrentItems() { return _currentItems; }
    public void TryAddItem(ECollectibles Item) { _currentItems |= Item; }
	#endregion

	#region Unity Interface
	private void Awake()
	{
		_playerControls = new PlayerInputAction();
		_secondaryPlayerControls = new PlayerInputAction1();
	}
	private void Start()
    {
		//SaveSystem.SaveProgress(this);
        _playerObject = gameObject;
        _rB = GetComponent<Rigidbody2D>();
		_myCollider = GetComponent<CapsuleCollider2D>();

    }
    private void Update()
    {
        ProcessInput();

		if (transform.position.y < _killY)
			SceneManager.LoadScene(0);
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
