using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player player) : base(player) {}

    public override void AttackAnimationFinished() {
        if(Mathf.Abs(MoveInput.x) > 0.1f){
            player.ChangeState(player.moveState);
        } else {
            player.ChangeState(player.idleState);
        }
    }

    public override void Enter() {
        base.Enter();
        player.jumpPressed = false;
        Anim.SetBool("isAttacking", true);
        Rb.linearVelocity = new Vector2(0, Rb.linearVelocity.y);
        AudioManager.Instance?.PlayHit(); 
    }

    public override void Exit() {
        base.Exit();
        Anim.SetBool("isAttacking", false);
    }
}
