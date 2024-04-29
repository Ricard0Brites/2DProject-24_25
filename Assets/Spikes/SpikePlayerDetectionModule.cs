using System.Collections;
using UnityEngine;

public class SpikePlayerDetectionModule : MonoBehaviour
{
    [SerializeField] private int _detectionRange = 100;
    [SerializeField] private float _timeUntilTheSpikeDestoysAfterActivation = 2.0f;
    private Collider2D _myCollider = null;
    private Rigidbody2D _myRigidBody = null;

    private void Start()
    {
        _myCollider = GetComponent<Collider2D>();
        Spike SpikeScript = GetComponent<Spike>();
        _myRigidBody = GetComponent<Rigidbody2D>();

		if (!_myRigidBody)
        {
            _myRigidBody = gameObject.AddComponent<Rigidbody2D>();
            _myRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if(SpikeScript)
        {
            SpikeScript.IsFallingSpike = true;
        }
    }

	private void Update()
	{
        if (!_myCollider)
            return;
		RaycastHit2D[] HitResults = Physics2D.RaycastAll(transform.position, transform.up, _detectionRange);
		if (HitResults.Length <= 0)
			return;

        foreach(RaycastHit2D HitResult in HitResults)
        {
			Player PlayerReference = HitResult.collider.GetComponent<Player>();
			if (PlayerReference)
			{
				_myRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
				StartCoroutine(TriggerDestructionTimer());
			}
		}
	}
    private IEnumerator TriggerDestructionTimer()
    {
        yield return new WaitForSeconds(_timeUntilTheSpikeDestoysAfterActivation);

        if (gameObject)
            Destroy(gameObject);
    }
}
