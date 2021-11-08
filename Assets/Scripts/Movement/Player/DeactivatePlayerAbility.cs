using UnityEngine;

public class DeactivatePlayerAbility : MonoBehaviour
{
    public enum Ability
    {
        Jump,
        Dash,
        Walk,
        WalkLeft,
        WalkRight,
    }
    public Ability ability;

    private PlayerMovement player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void DeactAbility()
    {
        switch (ability)
        {
            case Ability.Jump:
                player.deactJump = true;
                break;
            case Ability.Dash:
                player.deactDash = true;
                break;
            case Ability.Walk:
                player.deactmove = PlayerMovement.DeactMove.both;
                break;
            case Ability.WalkLeft:
                player.deactmove = PlayerMovement.DeactMove.left;
                break;
            case Ability.WalkRight:
                player.deactmove = PlayerMovement.DeactMove.right;
                break;
        }
    }
}
