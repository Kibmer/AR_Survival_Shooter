using System;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 6f;            // The speed that the player will move at.
        // public Transform target;
        UnityEngine.AI.NavMeshAgent nav;

        Vector3 movement;                   // The vector to store the direction of the player's movement.
        Animator anim;                      // Reference to the animator component.
        Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
        Vector3 targetPoint;
#if !MOBILE_INPUT
        int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        float camRayLength = 100f;          // The length of the ray from the camera into the scene.
#endif
        PhotonView pv;
        void Awake()
        {
            pv = GetComponent<PhotonView>();

#if !MOBILE_INPUT
            // Create a layer mask for the floor layer.
            floorMask = LayerMask.GetMask("Floor");
#endif
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
            // Set up references.
            anim = GetComponent<Animator>();
            playerRigidbody = GetComponent<Rigidbody>();
        }


        void FixedUpdate()
        {
            if (!pv.isMine) return;
            
            // Store the input axes.
            float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            float v = CrossPlatformInputManager.GetAxisRaw("Vertical");
            bool isMoving;

            // Move the player around the scene.
            Move(h, v);

            // Turn the player to face the mouse cursor.
            // Turning ();

            ClickToMove(out isMoving);

            // Animate the player.
            Animating(isMoving);
        }

        private void ClickToMove(out bool isMoving){
            isMoving = false;
            if (Input.GetMouseButtonDown(0)){
                isMoving = true;
                MoveToPoint(Input.mousePosition);
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
                isMoving = true;
                MoveToPoint(Input.GetTouch(0).position);
            }
        }
        void MoveToPoint(Vector3 _target){
            Ray camRay = Camera.main.ScreenPointToRay(_target);
            RaycastHit floorHit;
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)){
                // nav.SetDestination(floorHit.point);
                targetPoint = floorHit.point;
            }
        }
        void Move(float h, float v)
        {
            nav.SetDestination(targetPoint);
        }


        void Turning()
        {
#if !MOBILE_INPUT
            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            // Perform the raycast and if it hits something on the floor layer...
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                // // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                // Vector3 playerToMouse = floorHit.point - transform.position;

                // // Ensure the vector is entirely along the floor plane.
                // playerToMouse.y = 0f;

                // // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                // Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);

                // // Set the player's rotation to this new rotation.
                // playerRigidbody.MoveRotation (newRotatation);
                // target.transform.position = floorHit.point;
                nav.SetDestination(floorHit.point);
            }
#else

            Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse X") , 0f , CrossPlatformInputManager.GetAxisRaw("Mouse Y"));

            if (turnDir != Vector3.zero)
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = (transform.position + turnDir) - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotatation);
            }
#endif
        }


        void Animating(bool isMoving)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            // bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool("IsWalking", isMoving);
            isMoving = false;
        }
    }
}