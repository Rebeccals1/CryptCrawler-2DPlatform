using UnityEngine;

public class PlayerJumpState : PlayerState {

    public bool _applyForce;

    public PlayerJumpState(Player player) : base(player) {}

    // overload so callers can choose
    public void Enter(bool applyForce) {
        _applyForce = applyForce;
        Enter();
    }

    public override void Enter(){
        base.Enter();
        Anim.SetBool("isJumping", true); // No 'player.' needed!

        if (_applyForce) {
            Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, player.JumpForce);
            AudioManager.Instance?.PlayJump();
        }
        
        JumpPressed = false; 
        JumpReleased = false;
    }

    public override void Exit(){
        base.Exit();
        Anim.SetBool("isJumping", false);
    }

    public override void Update() {
        base.Update();
        
        if(AttackPressed && combat.CanAttack){
            player.attackPressed = false;
            player.ChangeState(player.attackState);
        } else if (player.isGrounded) {
            if (Mathf.Abs(MoveInput.x) > 0.1f)
                player.ChangeState(player.moveState);
            else
                player.ChangeState(player.idleState);
        }
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
        player.ApplyVariableGravity();

        if (JumpReleased && Rb.linearVelocity.y > 0) {
            Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, Rb.linearVelocity.y * player.JumpCutMultiplier);
            JumpReleased = false;
        }

        float targetSpeed = player.RunSpeed * MoveInput.x;
        Rb.linearVelocity = new Vector2(targetSpeed, Rb.linearVelocity.y);
    }
}