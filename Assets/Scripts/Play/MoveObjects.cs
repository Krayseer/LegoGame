using System.Collections.Generic;
using UnityEngine;

namespace Play
{
    public class MoveObjects : MonoBehaviour
    {
        private readonly List<string> draggingTags = new();
        public Camera cam;

        private Vector3 dis;
        private float posX;
        private float posY;

        private bool touched = false;
        private bool dragging = false;

        private Transform toDrag;
        private Rigidbody toDragRigidbody;
        private Vector3 previousPosition;

        private void Start()
        {
            for (int i = 1; i < 300; i++)
                draggingTags.Add(i.ToString());
        }

        void Update()
        {

            if (Input.touchCount != 1)
            {
                dragging = false;
                touched = false;
                if (toDragRigidbody)
                {
                    SetFreeProperties(toDragRigidbody);
                }
                return;
            }

            Touch touch = Input.touches[0];
            Vector3 pos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = cam.ScreenPointToRay(pos);

                if (Physics.Raycast(ray, out RaycastHit hit) && draggingTags.Contains(hit.collider.tag) 
                                                             && hit.transform.GetComponent<Rigidbody>() != null)
                {
                    toDrag = hit.transform;
                    previousPosition = toDrag.position;
                    toDragRigidbody = toDrag.GetComponent<Rigidbody>();

                    dis = cam.WorldToScreenPoint(previousPosition);
                    posX = Input.GetTouch(0).position.x - dis.x;
                    posY = Input.GetTouch(0).position.y - dis.y;

                    SetDraggingProperties(toDragRigidbody);

                    touched = true;
                }
            }

            if (touched && touch.phase == TouchPhase.Moved)
            {
                dragging = true;

                float posXNow = Input.GetTouch(0).position.x - posX;
                float posYNow = Input.GetTouch(0).position.y - posY;
                var curPos = new Vector3(posXNow, posYNow, dis.z);

                Vector3 worldPos = cam.ScreenToWorldPoint(curPos) - previousPosition;

                toDragRigidbody.velocity = worldPos / (Time.deltaTime * 4);

                previousPosition = toDrag.position;
            }

            if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                dragging = false;
                touched = false;
                previousPosition = new Vector3(0.0f, 0.0f, 0.0f);
                SetFreeProperties(toDragRigidbody);
            }

        }

        private void SetDraggingProperties(Rigidbody rb)
        {
            rb.isKinematic = false;
            rb.useGravity = false;
            rb.drag = 20;
        }

        private void SetFreeProperties(Rigidbody rb)
        {
            rb.isKinematic = true;
            rb.useGravity = true;
            rb.drag = 5;
        }
    }
}