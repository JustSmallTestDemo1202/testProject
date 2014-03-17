using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace IceFire
{
    public class CharacterInput : MonoBehaviour
    {
        public float cameraDist = 8;

        public Camera theMainCam = null;
        public bool inputBlock;

        Character character;

        void Start()
        {
            character = GetComponent<Character>();
            if (theMainCam == null)
            {
                string camName = "Main Camera";
                theMainCam = GameObject.Find(camName).GetComponent<Camera>() as Camera;
            }
        }

        void Update()
        {
            if (!inputBlock)
            {
                UpdateInput();
            }
        }

        void OnGUI()
        {
//             GUILayout.BeginArea(new Rect(Screen.width/2 - 20, 20, 40, 20));
//             if (GUILayout.Button("Skill1"))
//             {
//                 character.state.ChangeState("Skill1");
//             }
//             GUILayout.EndArea();
        }

        void UpdateInput()
        {
            Camera camera = theMainCam;//Camera.main;
            
            if (Input.GetButton("Jump"))
            {
                character.OnJump();
            }
 

            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            Transform cameraTransform = theMainCam.transform;
            cameraTransform = theMainCam.transform;

            // Forward vector relative to the camera along the x-z plane	
            Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
            forward.y = 0;
            forward = forward.normalized;
            // Right vector relative to the camera
            // Always orthogonal to the forward vector
            Vector3 right = new Vector3(forward.z, 0, -forward.x);
            /*
            if (inputH != 0.0f || inputV != 0.0f)
            {
                Vector3 dir = inputH * right + inputV * forward;
                character.OnMove(dir);
            }
            else
            {
             //   character.OnIdle();
            }*/

            if (Input.GetMouseButtonUp(0))
            {
                Vector3 mousePos = Input.mousePosition;
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(mousePos);

                if (Physics.Raycast(ray, out hit))
                {
                    character.GoAndAttack(hit.point);    
                    //   character.OnWalk(hit.point);
                }

                // to do set target

            }
            
            if (Input.GetMouseButtonUp(1))
            {
                Vector3 mousePos = Input.mousePosition;
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(mousePos);

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 currentPos = this.transform.position;
                    Vector3 dir = hit.point - currentPos;
                    dir.y = 0.0f;
                    float dist = dir.magnitude;
                    dir = dir / dist;
                    character.Attack(dir);

                }
            }

        }

        public void LateUpdate()
        {
            //   if (Machinima.Instance.cameraController_.m_camState == CameraController.CAMERA_STATE.CS_WITH_ROLE)
            {
                Camera camera = theMainCam;
                Vector3 cameraDir = new Vector3(0, 1, -1);
                Vector3 cameraPos = rigidbody.transform.position + cameraDir * cameraDist;
                camera.transform.position = cameraPos;
                camera.transform.LookAt(transform);
            }
        }
    }

}