using Engine;
using Microsoft.Xna.Framework;

/// <summary>
/// Represents a rocket enemy that flies horizontally through the screen.
/// </summary>
class Rocket : AnimatedGameObject
{
    Level level;
    Vector2 startPosition;
    const float speed = 500;
    private bool isActive = true;
    private bool facingLeft = true;

    public Rocket(Level level, Vector2 startPosition, bool facingLeft) 
        : base(TickTick.Depth_LevelObjects)
    {
        this.level = level;

        LoadAnimation("Sprites/LevelObjects/Rocket/spr_rocket@3", "rocket", true, 0.1f);
        PlayAnimation("rocket");
        SetOriginToCenter();
        this.startPosition = startPosition;
        this.facingLeft = facingLeft;
        this.isActive = true;

        Reset();
    }

    public void setSpeed() {
        // System.Diagnostics.Debug.WriteLine("Reset Speed");
        sprite.Mirror = this.facingLeft;
        if (sprite.Mirror)
        {
            velocity.X = -speed;
            this.startPosition = startPosition + new Vector2(2 * speed, 0);
        }
        else
        {
            velocity.X = speed;
            this.startPosition = startPosition - new Vector2(2 * speed, 0);
        }
        velocity.Y = 0;
    }

    public override void Reset()
    {
        // System.Diagnostics.Debug.WriteLine("Entered Reset");
        if (this.isActive)
        {
            // go back to the starting position
            setSpeed();
            LocalPosition = startPosition;
            // System.Diagnostics.Debug.WriteLine("Reset Active");
        }
        else {
            // System.Diagnostics.Debug.WriteLine("Reset Inactive");
            velocity.Y = 1000;
        }

    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (!isActive) {
            if (BoundingBox.Bottom > level.BoundingBox.Bottom) {
                velocity.Y = 0;
                velocity.X = 0;
                LocalPosition = startPosition;
            }
        }
            

        // if the rocket has left the screen, reset it
        if (sprite.Mirror && BoundingBox.Right < level.BoundingBox.Left)
            Reset();
        else if (!sprite.Mirror && BoundingBox.Left > level.BoundingBox.Right)
            Reset();

        // if the rocket touches the player, the player dies
        if (level.Player.CanCollideWithObjects && HasPixelPreciseCollision(level.Player))
            //Check if player jumped on rocket
            if (level.Player.GlobalPosition.Y + 15 < this.GlobalPosition.Y)
            {
                isActive = false;

                //If player pressed jump then jump normally
                if (level.Player.GettimeSinceLastAirborneJumpPress() < level.Player.GetjumpBufferTime())
                {
                    level.Player.Jump();
                }
                else //Little bounce if jump is not pressed
                {
                    level.Player.Jump(600f);
                }
                Reset();
            }
            else //If hit somewhere else the player dies
            {
                level.Player.Die();
            }
    }
}
