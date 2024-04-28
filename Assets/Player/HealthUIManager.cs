using UnityEngine;

public class HealthUIManager : MonoBehaviour
{
	[SerializeField] private GameObject _playerOneHPContainer = null, _playerTwoHPContainer = null;
	[SerializeField] private Player _playerOneReference, _playerTwoReference;
	[SerializeField] private float _offset = 10.0f;
	private GameObject _baseHPobject = null;
	private void Awake()
	{
		_baseHPobject = _playerOneHPContainer.transform.GetChild(0).gameObject;

		if(_baseHPobject && _playerOneReference && _playerOneHPContainer && _playerTwoHPContainer)
		{
			for(int i = 1; i < _playerOneReference.GetHealth(); ++i) // we run 1 less time than needed as we already have 1 object there
			{
				GameObject NewObject = Instantiate<GameObject>(_baseHPobject, _playerOneHPContainer.transform);
				NewObject.transform.position += Vector3.right * ((NewObject.GetComponent<RectTransform>().rect.width + _offset) * i);

				GameObject NewObject2 = Instantiate<GameObject>(_baseHPobject, _playerTwoHPContainer.transform);
				RectTransform RT = NewObject2.GetComponent<RectTransform>();
				RT.anchorMax = new Vector2(1,0);
				RT.anchorMin = RT.anchorMax;
				RT.pivot = new Vector2(3, 0);
				NewObject2.transform.position += Vector3.right * ((RT.rect.width + _offset) * -i);
			}
		}

		_playerOneReference.OnPlayerTakeDamageDelegate += RemoveHPFromPlayer;
		if (_playerTwoReference)
			_playerTwoReference.OnPlayerTakeDamageDelegate += RemoveHPFromPlayer;
	}
	private void RemoveHPFromPlayer(bool IsSecondaryPlayer)
	{
		GameObject ParentToRemoveFrom = IsSecondaryPlayer ? _playerTwoHPContainer : _playerOneHPContainer;

		int ChildToRemove = (ParentToRemoveFrom.transform.childCount - 1);

		if (ChildToRemove > -1)
			Destroy(ParentToRemoveFrom.transform.GetChild(ChildToRemove).gameObject);

		if(ChildToRemove == 0)
			if(IsSecondaryPlayer)
				_playerTwoReference.OnPlayerTakeDamageDelegate -= RemoveHPFromPlayer;
			else
				_playerOneReference.OnPlayerTakeDamageDelegate -= RemoveHPFromPlayer;
	}
}
