using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float _waitTime = 3f;
    [SerializeField] private float _rotateSpeed = 0.5f;
    [SerializeField][Range(-360, 360)] private float _firstAngle;
    [SerializeField][Range(-360, 360)] private float _secondAngle;

    private float _targetAngle;
    private Vector3 lastRotation;
    private bool canTurn;
    private FieldOfView _fieldOfView;
    private Transform _targetTransform;

    private void Awake()
    {
        _fieldOfView = GetComponent<FieldOfView>();
    }

    private void Start()
    {
        _targetTransform = _fieldOfView.playerRef.transform.GetChild(3);

        transform.eulerAngles = new Vector3(0f, _firstAngle, 0f);
        _targetAngle = _secondAngle;
        canTurn = true;
    }

    private void Update()
    {
        if (canTurn && !_fieldOfView.canSeePlayer)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, new Vector3(0f, _targetAngle, 0f), _rotateSpeed * Time.deltaTime);

            if (Mathf.Round(transform.eulerAngles.y) == _targetAngle)
            {
                transform.eulerAngles = new Vector3(0f, _targetAngle, 0f);
                _targetAngle = _targetAngle == _firstAngle ? _secondAngle : _firstAngle;
                canTurn = false;
                StartCoroutine(RotateAround());
            }
        }
    }
    private void LateUpdate()
    {
        if (_fieldOfView.canSeePlayer)
        {
            SmoothLookAt(_targetTransform.position);
        }
    }

    private IEnumerator RotateAround()
    {
        yield return new WaitForSeconds(_waitTime);
        canTurn = true;
    }

    public void SetLookTarget(Transform newTarget)
    {
        _targetTransform = newTarget;
    }

    private void SmoothLookAt(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        //direction.x = 0;
        //direction.z = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3 * Time.deltaTime);
    }
}