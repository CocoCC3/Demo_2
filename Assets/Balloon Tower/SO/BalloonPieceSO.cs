using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Balloon_Tower
{
    [CreateAssetMenu(fileName = "BalloonPieceDataSO", menuName = "ScriptableObjects/BalloonPieceData", order = 1)]
    public class BalloonPieceSO : ScriptableObject
    {
        [SerializeField] private List<BalloonPieceData> balloonPieceDataList;

        public BalloonPieceData ReturnRandomBalloonPiece()
        {
            var randIndex = Random.Range(0, balloonPieceDataList.Count);
            return balloonPieceDataList[randIndex];
        }
    
        [Serializable]
        public class BalloonPieceData
        {
            public Color balloonColor;
            public float moveSpeed;
            public BalloonPieceData(Color balloonColor, float moveSpeed)
            {
                this.balloonColor = balloonColor;
                this.moveSpeed = moveSpeed;
            }
        }
    }
}
