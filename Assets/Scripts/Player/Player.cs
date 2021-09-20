using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float direction;
    bool facingRight = true;
    float speed = 5f;
    float jumpForce = 8f;
    bool canPickGun = false;
    bool isWallJumping = false;
    
    float WallJumpForce_X = 5f;
    float WallJumpForce_Y = 8f;

    Rigidbody2D rb;
    SpriteRenderer renderer;
    Animator animator;
    GameObject currentGun;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        animator.SetFloat("Move", Mathf.Abs(direction));
        if (direction > 0 && !facingRight || direction < 0 && facingRight){
            transform.Rotate(0, 180, 0);
            facingRight = !facingRight;
        }
        IsHoldingWall();
    }

    void FixedUpdate(){
        if(!isWallJumping)
            rb.velocity = new Vector2(speed*direction, rb.velocity.y);
    }

    bool IsGrounded(){
        int mask = 1 << LayerMask.NameToLayer("Ground");

        // colisão com o chão
        RaycastHit2D hit_floor = Physics2D.Raycast(transform.position, -Vector2.up, 0.55f, mask);
        Debug.DrawRay(transform.position, new Vector2(0, -0.55f), Color.red);

        if( hit_floor.collider ){
            //animator
            return true;
        }
        //animator
        return false;
    }

    bool IsHoldingWall(){
        int mask = 1 << LayerMask.NameToLayer("Ground");

        // colisão com parede a esquerda
        RaycastHit2D hit_left = Physics2D.Raycast(transform.position, Vector2.left, 0.309f, mask);
        Debug.DrawRay(transform.position, new Vector2( -0.309f, 0), Color.red);

        // colisão com parede a direita
        RaycastHit2D hit_right = Physics2D.Raycast(transform.position, Vector2.right, 0.309f, mask);
        Debug.DrawRay(transform.position, new Vector2( 0.309f, 0), Color.red);

        if( hit_left.collider || hit_right.collider){
            //animator
            return true;
        }
        //animator
        return false;
    }

    IEnumerator WallJumping(){
        isWallJumping = true;
        int side = facingRight ? 1 : -1;
        rb.AddForce( new Vector2( -WallJumpForce_X*side, WallJumpForce_Y), ForceMode2D.Impulse );

        yield return new WaitForSeconds(0.2f);

        isWallJumping = false;
    }

    public void Jump(){
        if( IsGrounded() ) rb.AddRelativeForce( transform.up*jumpForce, ForceMode2D.Impulse );
        else if ( IsHoldingWall() ){
            StartCoroutine("WallJumping");
        }
        
        return;
    }

    public void PickUpGun(){
        if(!canPickGun)
            return;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("gun")){
            canPickGun = true;
            currentGun = col.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.CompareTag("gun"))
            canPickGun = true;
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("gun"))
            canPickGun = false;
    }
}