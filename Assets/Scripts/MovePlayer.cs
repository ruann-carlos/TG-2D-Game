using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovePlayer : MonoBehaviour
{
    public float playerSpeed;
    [SerializeField] private float playerDashSpeed;
    protected Rigidbody2D playerRgbd;
    protected Animator animator;
    public bool isMoving;
    private Vector3 lastMotionVector;
    private Vector2 mousePos;
    [SerializeField] private Camera cam;


    void OnEnable() //subscribing to the method that send the notification to move
    {
        InputManager.OnPlayerMoved += Move;
        InputManager.OnPlayerDashed = PlayerDashMovement;
    }

    void OnDisable()//unsubscribing to the method that send the notification to move
    {
        InputManager.OnPlayerMoved -= Move;
    }
    void Start()
    {
        playerRgbd = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        playerRgbd.rotation = GetLookAngle();
    }
    public void Move(Vector3 direction, float horizontal, float vertical)
    {
        playerRgbd.velocity = 5 * playerSpeed * direction;
    }

    private IEnumerator PlayerDashMovement()
    {
        playerSpeed += playerDashSpeed;
        yield return new WaitForSeconds(0.2f);
        playerSpeed -= playerDashSpeed;
    }
    
    private float GetLookAngle()
    {
        Vector2 lookDir = mousePos - playerRgbd.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        return angle;
    }

    public void SetAnimation(float horizontal, float vertical)
    {
        if (horizontal != 0 || vertical != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetBool("IsMoving", isMoving);
    }

    public void SetIdleDirection(float horizontal, float vertical)
    {
        if (isMoving)
        {
            lastMotionVector = new Vector3(horizontal, vertical, 0).normalized;

            animator.SetFloat("LastVertical", vertical);
            animator.SetFloat("LastHorizontal", horizontal);
        }
    }

}
