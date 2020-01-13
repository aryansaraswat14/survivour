using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator ani;
    private bool canmove = false;
    private Vector3 movePosition = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController charactercontroller;
    public float speed;
    // Start is called before the first frame update
    void Awake()
    {
        ani = GetComponent<Animator>();
        charactercontroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        charactercontroller.Move(moveDirection);

        /*if(Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;
            Debug.Log(mousePosition);

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray , out hit))
            {
                movePosition = hit.point;
                canmove = true;
                movePosition.y = transform.position.y;
                moveDirection = (movePosition - transform.position).normalized;
                ani.SetBool("canwalk", true);
                

            }
        }
        if(canmove)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), 0.1f);
            charactercontroller.Move(moveDirection*speed*Time.deltaTime);

        }
        if (Vector3.Distance(movePosition, transform.position) <= 0.5f)
            canmove = false;*/
        
    }
}
