using Dreamteck.Splines;
using UnityEngine;

namespace Balloon_Tower.Scripts
{
    public class PathLead : MonoBehaviour
    {
        private SplineComputer splineComputer;
        public Transform player;
        private Vector3[] poss;
        void Awake()
        {
            splineComputer = GetComponent<SplineComputer>();
            poss = new Vector3[splineComputer.pointCount];
            for (int i = 0; i < splineComputer.pointCount; i++)
            {
                poss[i] = splineComputer.GetPointPosition(i);
            }
        }
    
        void Update()
        {
            UpdatePosition();
        }
    
        public float toplama = 0.003f;
        void UpdatePosition()
        {
            for (int i = 0; i < splineComputer.pointCount; i++)
            {
                Vector3 targetPosition = splineComputer.GetPointPosition(i);
                targetPosition.x = Mathf.Lerp(targetPosition.x, player.transform.position.x, (Time.deltaTime * i) + toplama);
                targetPosition.y = Mathf.Lerp(targetPosition.y, player.transform.position.y + poss[i].y - 10f, (Time.deltaTime * i) + toplama);
                //targetPosition.z = Mathf.Lerp(targetPosition.z, player.transform.position.z, (Time.deltaTime * i * carpan) + toplama);
                targetPosition.z = player.position.z;
                splineComputer.SetPointPosition(i, targetPosition);
            }
        }
    }
}
