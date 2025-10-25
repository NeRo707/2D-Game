using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour {

  protected Animator anim;
  protected Rigidbody2D rb;
  protected Collider2D col;
  protected SpriteRenderer sr;

  [Header("Health Details")]
  [SerializeField] protected int maxHealth = 1;
  [SerializeField] protected int currentHealth;
  [SerializeField] private Material damageMaterial;
  [SerializeField] private float damageFeedbackDuration = .1f;
  private Coroutine damageFeedbackCoroutine;

  [Header("Attack Details")]
  [SerializeField] protected float attackRadius = 0.5f;
  [SerializeField] protected Transform attackPoint;
  [SerializeField] protected LayerMask whatIsTarget;
  [SerializeField] protected int attackDamage = 1;

  [Header("Collision Details")]
  [SerializeField] private float groundCheckDistance = 0.1f;
  [SerializeField] private LayerMask whatIsGround;
  protected bool isGrounded;

  protected int facingDir = 1;
  protected bool facingRight = true;
  protected bool canMove = true;

  protected virtual void Awake() {
    rb = GetComponent<Rigidbody2D>();
    col = GetComponent<Collider2D>();
    anim = GetComponentInChildren<Animator>();
    sr = GetComponentInChildren<SpriteRenderer>();

    currentHealth = maxHealth;
  }

  protected virtual void Update() {
    HandleCollision();
    HandleMovement();
    HandleAnimations();
    HandleFlip();
  }

  public void DamageTargets() {
    Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsTarget);

    foreach (Collider2D enemy in enemyColliders) {
      Entity entityTarget = enemy.GetComponent<Entity>();
      entityTarget.TakeDamage(attackDamage);

    }
  }

  private void TakeDamage(int v) {
    currentHealth -= v;
    PlayDamageFeedback();
    if (currentHealth <= 0) {
      Die();
    }
  }

  private void PlayDamageFeedback() {
    if (damageFeedbackCoroutine != null)
      StopCoroutine(damageFeedbackCoroutine);
    damageFeedbackCoroutine = StartCoroutine(DamageFeedbackCoroutine());
  }

  private IEnumerator DamageFeedbackCoroutine() {
    Material originalMaterial = sr.material;
    sr.material = damageMaterial;
    yield return new WaitForSeconds(damageFeedbackDuration);
    sr.material = originalMaterial;
  }

  protected virtual void Die() {
    anim.enabled = false;
    col.enabled = false;

    rb.gravityScale = 12;
    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);

    Destroy(gameObject, 3);
  }

  public virtual void EnableMovement(bool enable) {
    canMove = enable;
  }

  protected void HandleAnimations() {
    anim.SetFloat("xVelocity", rb.linearVelocity.x);
    anim.SetBool("isGrounded", isGrounded);
    anim.SetFloat("yVelocity", rb.linearVelocity.y);
  }

  protected virtual void HandleAttack() {
    if (isGrounded) {
      anim.SetTrigger("attack");
    }
  }

  protected virtual void HandleMovement() {

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

  public void Flip() {
    transform.Rotate(0, 180, 0);
    facingRight = !facingRight;
    facingDir = facingDir * -1;
  }

  private void OnDrawGizmos() {
    Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    
    if(attackPoint != null)
    Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
  }
}
