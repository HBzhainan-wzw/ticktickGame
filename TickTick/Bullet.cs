using Engine;
using Microsoft.Xna.Framework;

/// <summary>
/// Represents a bullet that can be fired by the player.
/// </summary>
class Bullet : AnimatedGameObject
{
    Level level;
    Vector2 startPosition;
    const float Speed = 700; // Constants should be PascalCase
    bool isActive = false; // Assuming you want the bullet to be active when created
    bool facingLeft;
    float lifeSpan = 1f; // The total lifespan of the bullet
    float timeAlive = 0.0f; // The time the bullet has been alive

    public Bullet(Level level, Vector2 startPosition, bool facingLeft)
        : base(TickTick.Depth_LevelObjects)
    {
        this.level = level;
        this.startPosition = startPosition;
        this.facingLeft = facingLeft;

        // Assuming LoadAnimation is supposed to load the texture and set it to the sprite
        LoadAnimation("Sprites/LevelObjects/Bullet/bullet", "Bullet", true, 0.1f);
        
        PlayAnimation("Bullet");
        SetOriginToCenter(); 

        // Set the initial velocity based on the facing direction
        velocity.X = facingLeft ? -Speed : Speed;

        // Set the initial position of the bullet
        LocalPosition = startPosition;
    }

    public override void Reset()
    {
        System.Diagnostics.Debug.WriteLine("Bullet Reset");
        LocalPosition = this.level.Player.GlobalPosition; // Reset to the initial position
        this.localPosition.Y -= 50;
        isActive = false; // Reactivate the bullet
        timeAlive = 0; // Reset the lifespan timer
    }

    public override void Update(GameTime gameTime)
    {
        
        base.Update(gameTime);
        if (isActive)
        {
            // Update the timeAlive
            timeAlive += (float)gameTime.ElapsedGameTime.TotalSeconds;
            // System.Diagnostics.Debug.WriteLine("Bullet time remaining" + (lifeSpan - timeAlive));
            // Check if the bullet's lifespan has expired
            if (timeAlive >= lifeSpan)
            {
                // System.Diagnostics.Debug.WriteLine("Bullet Deactivate");
                isActive = false; // Deactivate the bullet
                Visible = false; // Make the bullet invisible
                
            }
            else
            {
                
                // TODO: Add collision detection or other update logic here
            }
        }
        else {
            // System.Diagnostics.Debug.WriteLine("Bullet inActive");
            Visible = true;
            this.localPosition = this.level.Player.GlobalPosition;
            this.localPosition.Y -= 50;
        }
    }
    public void Activate(bool facingLeft) {
       //  System.Diagnostics.Debug.WriteLine("Bullet Activate");
        velocity.X = facingLeft ? -Speed : Speed;
        LocalPosition = this.level.Player.GlobalPosition; // Reset to the initial position
        this.localPosition.Y -= 50;
        timeAlive = 0;
        this.isActive = true;
    }
}
