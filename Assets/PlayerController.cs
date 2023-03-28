using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 360;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float jumpPower = 3;

    private CharacterController characterController;
    private Transform _transform;
    private float turn;
    private Vector3 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        _transform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        turn = Input.GetAxis("Horizontal") * turnSpeed;
        _transform.Rotate(0, turn * Time.deltaTime, 0);

        if (characterController.isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                moveVelocity.y = jumpPower;
            }
        }
        else
        {
            moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 move = new Vector3(moveVelocity.z * _transform.forward.x, moveVelocity.y, moveVelocity.z * _transform.forward.z);

        characterController.Move(move * Time.deltaTime);
    }
}
