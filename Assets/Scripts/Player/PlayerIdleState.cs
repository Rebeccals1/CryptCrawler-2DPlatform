using UnityEngine;

public class PlayerIdleState : PlayerState {

    public PlayerIdleState(Player player) : base(player) {}
    
    public override void Enter(){
        base.Enter();
        Anim.SetBool("isIdle", true);
                
        // Zero out velocity when entering Idle so the player doesn't slide
        Rb.linearVelocity = new Vector2(0, Rb.linearVelocity.y);
    }

    public override void Exit(){
        base.Exit();
        Anim.SetBool("isIdle", false);
    }

    public override void Update(){
        base.Update();

        if(AttackPressed && combat.CanAttack){
            player.attackPressed = false;
            player.ChangeState(player.attackState);
        }
        else if (player.jumpPressed) {
            JumpPressed = false;
            player.jumpState._applyForce = true;
            player.ChangeState(player.jumpState);
        }
        else if(Mathf.Abs(MoveInput.x) > 0.1f) {
            player.ChangeState(player.moveState); 
        }
    }
}