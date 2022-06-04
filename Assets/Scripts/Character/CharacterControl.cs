using System;
using System.Collections;
using System.Threading.Tasks;
using Controllers;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterControl : MonoBehaviour, ITarget
    {
        private float _xMoveAxis, _zMoveAxis;
        private float _xRotateAxis, _yRotateAxis;

        private Coroutine _lostCountDown;
        private const float FallTime = 1;

        private Transform _camTransform;
        private Rigidbody _characterRb;

        [SerializeField] private float movementSpeed;
        [SerializeField] private float xMouseIntensity;
        [SerializeField] private float yMouseIntensity;

        [SerializeField] private float onHitForce = 5;

        private void Start()
        {
            if (Camera.main != null) _camTransform = Camera.main.transform;
            
            _characterRb = GetComponent<Rigidbody>();
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void Update()
        {
            MoveCharacter();
            RotateCharacter();
        }
        
        private void MoveCharacter()
        {
            _xMoveAxis = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
            _zMoveAxis = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
            
            transform.Translate(_xMoveAxis, 0, _zMoveAxis);
        }
        private void RotateCharacter()
        {
            _xRotateAxis = Input.GetAxis("Mouse Y") * xMouseIntensity;
            _yRotateAxis = Input.GetAxis("Mouse X") * yMouseIntensity;
            
            transform.Rotate(0, _yRotateAxis, 0);
            _camTransform.Rotate(-_xRotateAxis, 0, 0);
        }

        public void OnHit(Vector3 direction)
        {
            _characterRb.velocity = Vector3.zero;
            _characterRb.AddForce(direction.normalized * onHitForce, ForceMode.Impulse);
        }

        private void OnCollisionExit(Collision other)
        {
            if(other.gameObject.layer != 6) return;
            
            _lostCountDown = StartCoroutine(LostCountDown(Time.time));
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.layer != 6) return;

            if(_lostCountDown != null) StopCoroutine(_lostCountDown);
        }

        private IEnumerator LostCountDown(float startTime)
        {
            while (startTime + FallTime >= Time.time)
            {
                yield return null;
            }
            StateController.Instance.LoseGame(this);
        }
    }
}
