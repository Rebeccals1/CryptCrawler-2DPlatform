using UnityEngine;

public class PlayerMoveState : PlayerState {

    public PlayerMoveState(Player player) : base(player) {}

    public override void Enter() {
        base.Enter();
        // The boolean name here should match your Animator parameter
        Anim.SetBool("isRunning", true); 
    }

    public override void Exit() {
        base.Exit();
        Anim.SetBool("isRunning", false);
    }

    public override void Update() {
        base.Update();

        if(AttackPressed && combat.CanAttack){
            player.attackPressed = false;
            player.ChangeState(player.attackState);
        }
        else if (JumpPressed) {
            player.jumpState._applyForce = true;
            player.ChangeState(player.jumpState);
        }
        else if (Mathf.Abs(MoveInput.x) < 0.1f) {
            player.ChangeState(player.idleState);
        } 
        else if (!player.isGrounded) {
            player.jumpState._applyForce = false;  // no launch force
            player.ChangeState(player.jumpState);
        }
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
        
        // Always use RunSpeed now
        float horizontalVelocity = MoveInput.x * player.RunSpeed;
        Rb.linearVelocity = new Vector2(horizontalVelocity, Rb.linearVelocity.y);
    }
}