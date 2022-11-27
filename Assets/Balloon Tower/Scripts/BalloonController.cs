using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balloon_Tower.Scripts
{
    public class BalloonController : MonoBehaviour
    {
        private bool MoveByTouch, StartTheGame;
        private Vector3 _mouseStartPos, PlayerStartPos;
        [SerializeField] private float _playerSpeed, _swipeSpeed;
        [SerializeField] private List<Transform> collectableObjects;
        
        void Start()
        {
            collectableObjects.Add(transform);
        }
        
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) { ButtonDown();}

            if (Input.GetMouseButtonUp(0)) { MoveByTouch = false;}
        
            if (MoveByTouch)
            {
                var plane = new Plane(Vector3.up, 0f);

                float distance;

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (plane.Raycast(ray, out distance))
                {
                    Vector3 mousePos = ray.GetPoint(distance);
                    Vector3 desirePso = mousePos - _mouseStartPos;
                    Vector3 move = PlayerStartPos + desirePso;

                    move.x = Mathf.Clamp(move.x, -2.2f, 2.2f);
                    move.z = -7f;

                    var player = transform.position;

                    player = new Vector3(Mathf.Lerp(player.x, move.x, Time.deltaTime * (_swipeSpeed + 10f)), player.y, player.z);

                    transform.position = player;
                }
            }

            if (StartTheGame) { transform.Translate(Vector3.forward * (_playerSpeed * Time.deltaTime));}
            
            /*
            if (collectableObjects.Count > 1)
            {
                for (int i = 1; i < collectableObjects.Count; i++)
                {
                    var FirstElement = collectableObjects.ElementAt(i - 1);
                    var SectElement = collectableObjects.ElementAt(i);
                    SectElement.position = new Vector3(Mathf.Lerp(SectElement.position.x,FirstElement.position.x,_swipeSpeed * Time.deltaTime), Mathf.Lerp(SectElement.position.y,FirstElement.position.y - 2f,_swipeSpeed * Time.deltaTime),FirstElement.position.z);
                }
            }*/
        }
        
        private void ButtonDown()
        {
            StartTheGame = MoveByTouch = true;
            
            Plane newPlan = new Plane(Vector3.up, 0f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (newPlan.Raycast(ray,out var distance))
            {
                _mouseStartPos = ray.GetPoint(distance);
                PlayerStartPos = transform.position;
            }
        }
    }
}
