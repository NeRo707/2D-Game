using UnityEngine;

public class Player : Entity {
  [Header("Movement Details")]
  [SerializeField] protected float moveSpeed = 3.5f;
  private float xInput;
  [SerializeField] private float jumpForce = 7f;
  private bool canJump = true;

  override protected void Update() {
    base.Update();
    HandleInput();
  }

  protected override void HandleMovement() {
    if (canMove)
      rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    else
      rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
  }

  private void TryToJump() {
    if (isGrounded && canJump)
      rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
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

  public override void EnableMovement(bool enable) {
    base.EnableMovement(enable);
    canJump = enable;
  }

  protected override void Die() {
    base.Die();
    UI.instance.ShowGameOverUI();
  }

}
