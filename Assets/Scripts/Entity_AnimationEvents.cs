using UnityEngine;

public class Entity_AnimationEvents : MonoBehaviour {

  private Entity entity;

  private void Awake() {
    entity = GetComponentInParent<Entity>();
  }

  public void DamageTargets() => entity.DamageTargets();

  public void EnableMovementAndJump() => entity.EnableMovement(true);

  public void DisableMovementAndJump() => entity.EnableMovement(false);
  
}
