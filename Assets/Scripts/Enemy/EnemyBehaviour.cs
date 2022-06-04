using System;
using Character;
using Controllers;
using UnityEditor;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace Enemy
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        private static Transform _characterTransform;
        private Transform _weapon;
        private bool _willIMove;

        private MeshRenderer _meshRenderer;
        protected Color MyColor;
        
        private void Start()
        {
            if (_characterTransform == null) _characterTransform = FindObjectOfType<CharacterControl>().transform;
            _weapon = transform.GetChild(0);
            _meshRenderer = GetComponent<MeshRenderer>();
            MyColor = _meshRenderer.material.color;
            
            _willIMove = Random.Range(0, 2) == 0;
            
            StateController.Instance.AddEnemyToList(this);
        }

        private void Update()
        {
            TrackCharacter();
            if(!_willIMove) return;
            
            Move();
        }

        private void Move()
        {
            var pretendPosition = new Vector3(movementSpeed * Time.deltaTime, 0, 0);
            if (Physics.Raycast(transform.position - pretendPosition, Vector3.down))
            {
                transform.Translate(pretendPosition);
            }
            else
            {
                movementSpeed *= -1;
            }
        }

        private void TrackCharacter()
        {
            _weapon.LookAt(_characterTransform);
            transform.LookAt(_characterTransform);
        }

        protected void ChangeColor(Color newColor)
        {
            _meshRenderer.material.color = newColor;
        }
    }
}