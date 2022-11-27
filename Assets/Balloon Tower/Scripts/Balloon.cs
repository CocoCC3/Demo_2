using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Balloon_Tower.Scripts
{
    public class Balloon : MonoBehaviour
    {
        [SerializeField] private BalloonPieceSO _balloonPieceDataSO;
    
        [Space]
    
        [SerializeField] private Transform[] _balloonWaitPos;
    
        [SerializeField] private GameObject _balloonPrefab;
        [SerializeField] private Transform _balloonSpawnPos;
        private int waitIndex;
        private int _balloonCount;
        private LineRenderer _myLine;
        [SerializeField] private Transform[] _lineStartEndPos;

        private void Awake()
        {
            _myLine = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            StartCoroutine(Production());
        }

        IEnumerator Production()
        {
            while (true)
            {
                if (_balloonCount < 100)
                {
                    yield return new WaitForSeconds(1f);
                    SpawnBalloonPiece();
                }
                else { yield return new WaitUntil(() => _balloonCount < 100);}
            }
        }
        
        void SetTheLine()
        {
            _myLine.positionCount = 2;
            _myLine.useWorldSpace = true;

            _myLine.SetPosition(0, _lineStartEndPos[0].position);
            _myLine.SetPosition(1, _lineStartEndPos[1].position);
        }
        
        private void Update()
        {
            SetTheLine();
            
            if (Input.GetKeyDown(KeyCode.S)) { SpawnBalloonPiece();}//test
        }
        
        private void SpawnBalloonPiece()
        {
            var obj = Instantiate(_balloonPrefab, _balloonSpawnPos.position, Quaternion.identity);
            obj.transform.SetParent(transform);
            var balloonActions = obj.GetComponent<IBalloonChildActions>();
            balloonActions.SetTheData(_balloonPieceDataSO.ReturnRandomBalloonPiece());
            balloonActions.BalloonChildMoveWaitPos(ReturnBalloonWaitPos());
        }
        
        Transform ReturnBalloonWaitPos()
        {
            waitIndex++;
            if (waitIndex >= _balloonWaitPos.Length) waitIndex = 0;
            //var randomPos = new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.7f,0.7f), Random.Range(-0.5f,0.5f)) + _balloonWaitPos[waitIndex].position;
            return _balloonWaitPos[waitIndex];
        }
    }
}
