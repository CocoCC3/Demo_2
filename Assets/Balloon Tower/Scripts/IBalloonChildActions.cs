using UnityEngine;

namespace Balloon_Tower.Scripts
{
    public interface IBalloonChildActions
    {
        public void BalloonChildMoveWaitPos(Transform targetPos);
        public void SetTheData(BalloonPieceSO.BalloonPieceData targetPos);
    }
}
