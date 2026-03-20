using UnityEngine;

public abstract class PlayerState
{
    protected Player player;

    // Bridge Properties to make child states cleaner
    // These reach into 'player' and grab the public/serialized values
    protected bool JumpPressed { get => player.jumpPressed; set => player.jumpPressed = value; }
    protected bool JumpReleased { get => player.jumpReleased; set => player.jumpReleased = value; }
    protected bool RunPressed => player.runPressed;
    protected bool AttackPressed => player.attackPressed;
    
    // Match the casing from your new Player.cs properties
    protected Vector2 MoveInput => player.MoveInput;
    protected Rigidbody2D Rb => player.Rb;
    protected Animator Anim => player.Anim;
    protected Combat combat;

    // Constructor
    public PlayerState(Player player){
        this.player = player;
        combat = player.Combat;
    }

    public virtual void Enter(){}
    public virtual void Exit(){}
    public virtual void Update(){}
    public virtual void FixedUpdate(){}
    public virtual void AttackAnimationFinished(){}
}