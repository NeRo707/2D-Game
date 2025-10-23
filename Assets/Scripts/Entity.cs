using System;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

  protected Animator anim;
  protected Rigidbody2D rb;

  [Header("Attack Details")]
  [SerializeField] protected float attackRadius = 0.5f;
  [SerializeField] protected Transform attackPoint;
  [SerializeField] protected LayerMask whatIsTarget;

  [Header("Movement Details")]
  [SerializeField] protected float moveSpeed = 3.5f;
  [SerializeField] private float jumpForce = 7f;
  protected int facingDir = 1;
  private float xInput;
  private bool facingRight = true;
  protected bool canMove = true;
  private bool canJump = true;

  [Header("Collision Details")]
  [SerializeField] private float groundCheckDistance = 0.1f;
  [SerializeField] private LayerMask whatIsGround;
  private bool isGrounded;


  private void Awake() {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponentInChildren<Animator>();
  }

  protected virtual void Update() {
    HandleCollision();
    HandleInput();
    HandleMovement();
    HandleAnimations();
    HandleFlip();
  }

  public void DamageTargets() {
    Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsTarget);

    foreach (Collider2D enemy in enemyColliders) {
      Entity entityTarget = enemy.GetComponent<Entity>();
      entityTarget.TakeDamage(1);

    }
  }

  private void TakeDamage(int v) => throw new NotImplementedException();

  public void EnableMovementAndJump(bool enable) {
    canMove = enable;
    canJump = enable;
  }

  protected void HandleAnimations() {
    anim.SetFloat("xVelocity", rb.linearVelocity.x);
    anim.SetBool("isGrounded", isGrounded);
    anim.SetFloat("yVelocity", rb.linearVelocity.y);
  }

  private void HandleInput() {
    xInput = Input.GetAxisRaw("Horizontal");

    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)) {
      TryToJump();
    }

    if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.X)) {
      HandleAttack();
    }
  }

  protected virtual void HandleAttack() {
    if (isGrounded) {
      anim.SetTrigger("attack");
    }
  }

  protected virtual void HandleMovement() {
    if (canMove)
      rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    else
      rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
  }

  private void TryToJump() {
    if (isGrounded && canJump)
      rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
  }

  protected virtual void HandleCollision() {
    isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
  }

  protected virtual void HandleFlip() {
    if (rb.linearVelocity.x > 0 && facingRight == false) {
      Flip();
    } else if (rb.linearVelocity.x < 0 && facingRight == true) {
      Flip();
    }
  }

  private void Flip() {
    transform.Rotate(0, 180, 0);
    facingRight = !facingRight;
    facingDir = facingDir * -1;
  }

  private void OnDrawGizmos() {
    Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
  }
}
