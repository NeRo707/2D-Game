using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour {

  private Player player;

  private void Awake() {
    player = GetComponentInParent<Player>();
  }

  public void EnableMovementAndJump() => player.EnableMovementAndJump(true);

  public void DisableMovementAndJump() => player.EnableMovementAndJump(false);
  
}
