using UnityEngine;

public class Enemy : Entity {

  [Header("Movement Details")]
  [SerializeField] protected float moveSpeed = 3.5f;

  private bool playerDetected;

  protected override void Update() {
    base.Update();

    HandleAttack();
    Debug.Log(playerDetected);
  }

  protected override void HandleAttack() {
    if (playerDetected) {
      anim.SetTrigger("attack");
      Debug.Log("Enemy Attack");
    }
  }


  protected override void HandleMovement() {
    if (canMove)
      rb.linearVelocity = new Vector2(facingDir * moveSpeed, rb.linearVelocity.y);
    else
      rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
  }

  protected override void HandleCollision() {
    base.HandleCollision();

    playerDetected = Physics2D.OverlapCircle(attackPoint.position, attackRadius, whatIsTarget);
  }
}
