using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float turnSpeed;
    [SerializeField] private float groundTurnSpeed = 90;
    [SerializeField] private float jumpTurnSpeed = 180;
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private float stepSpeed = 5.5f;
    [SerializeField] private float jumpPower = 3.5f;

    [SerializeField] private Animator animator;

    private CharacterController characterController;
    private Transform _transform;
    private float turn;
    private Vector3 moveVelocity;

    private float startMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        _transform = this.transform;

        FindAnyObjectByType<EnemyMove>().attackPlayer.AddListener(OnAttacked);
    }

    // Update is called once per frame
    void Update()
    {

        if (characterController.isGrounded)
        {
            turnSpeed = groundTurnSpeed;
            if (Input.GetMouseButtonDown(0))
            {
                moveVelocity.y = jumpPower;
                startMousePosition = Input.mousePosition.x;
            }
        }
        else
        {
            turnSpeed = jumpTurnSpeed;
            moveVelocity.y += Physics.gravity.y * Time.deltaTime;

            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x < startMousePosition)
                {
                    moveVelocity.x = -stepSpeed;
                }
                else if (startMousePosition < Input.mousePosition.x)
                {
                    moveVelocity.x = stepSpeed;
                }

                Vector3 step = new Vector3(moveVelocity.x * _transform.right.x, moveVelocity.y, moveVelocity.x * _transform.right.z);
                characterController.Move(step * Time.deltaTime);
            }
        }

        turn = Input.GetAxis("Horizontal") * turnSpeed;
        _transform.Rotate(0, turn * Time.deltaTime, 0);

        moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 move = new Vector3(moveVelocity.z * _transform.forward.x, moveVelocity.y, moveVelocity.z * _transform.forward.z);

        characterController.Move(move * Time.deltaTime);

        animator.SetFloat("MoveSpeed", new Vector3(0, 0, moveVelocity.z).magnitude);
    }

    private void OnAttacked()
    {
        Debug.Log("GameOver");
    }
}
