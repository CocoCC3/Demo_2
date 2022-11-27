using UnityEngine;

namespace Balloon_Tower.Scripts
{
    public class BalloonChild : MonoBehaviour, IBalloonChildActions
    {
        private MeshRenderer _mainMesh;
        private Rigidbody _mainRig;
        private Transform _targetPos;
        private bool _canMove;
        private float _movementSpeed;
        
        private void Awake()
        {
            _mainRig = GetComponent<Rigidbody>();
            _mainMesh = GetComponent<MeshRenderer>();
        }

        void Update()
        {
            CheckDistance();
            MoveWaitPos();
        }
    
        private void CheckDistance()
        {
            var distanceToTarget = Vector3.Distance(transform.position, _targetPos.position);
            if (distanceToTarget <= 0.4f)
            {
                _mainRig.drag = 20f;
                _canMove = false;
            }
            else if (distanceToTarget > 0.4f)
            {
                //_canMove = true;
            }
        }
    
        private void MoveWaitPos()
        {
            if (!_canMove) return;

            var myPosition = transform.position;
            var targetPosition = _targetPos.position;
            myPosition = Vector3.Slerp(myPosition, targetPosition, (Time.deltaTime * _movementSpeed) / Vector3.Distance(targetPosition, myPosition));
            transform.position = myPosition;
        }
        
        public void BalloonChildMoveWaitPos(Transform targetPos)
        {
            _targetPos = targetPos;
            _canMove = true;
        }

        public void SetTheData(BalloonPieceSO.BalloonPieceData myData)
        {
            _mainMesh.materials[0].color = myData.balloonColor;
            _movementSpeed = myData.moveSpeed;
        }
    }
}
