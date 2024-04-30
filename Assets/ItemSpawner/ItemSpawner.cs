using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
	[SerializeField] private ECollectibles _itemsToSpawn = 0;
	[SerializeField] private float _spawnInterval = 30.0f, _spawnRadius = 10;
	[SerializeField] private GameObject _collectibleObject;

	private List<int> _PossibleObjectList = new List<int>{};

	private void Start()
	{
		StartCoroutine(SpawnItems());

		for(int i = 0; i < 31; ++i) // loop through the 31 bits
		{
			int CurrentBitValue = 1 << i & (int)_itemsToSpawn;

			if (CurrentBitValue != 0)
			{
				_PossibleObjectList.Add(CurrentBitValue);
			}
		}
	}
	private IEnumerator SpawnItems()
	{
		while(this)
		{
			yield return new WaitForSeconds(_spawnInterval);

			Collectible NewItem = Instantiate(_collectibleObject, transform).GetComponent<Collectible>();
			NewItem.HasLifeCycle = true;
			NewItem.transform.position += (transform.right * Random.Range(-1.0f, 1.0f)) * _spawnRadius;
			NewItem.Item = (ECollectibles)_PossibleObjectList[Random.Range(0, _PossibleObjectList.Count)];
		}
	}
}
