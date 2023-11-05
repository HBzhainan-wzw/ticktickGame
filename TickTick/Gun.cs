//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using MonoTube.Sprites;

//class Gun : Sprite
//{
//    Level level;
//    public Bullet Bullet;
//    Vector2 startPosition;

//    public Gun(Texture2D texture, Level level, Vector2 startPosition)
//    {
//        this.level = level;
//        this.startPosition = startPosition;
//    }

//    public override void Update(GameTime gameTime, List<Sprite> sprites)
//    {
//        Position = level.GlobalPosition;
//        _previousKey = _currentKey;
//        _currentKey = Keyboard.GetState();
//        if (_currentKey.IsKeyDown(Keys.Space) &&
//            _previousKey.IsKeyUp(Keys.Space))
//        {
//            AddBullet(sprites);
//        }
//        foreach (var sprite in sprites)
//        {
//            if (sprite is Gun)
//                continue;
//        }
//    }

//    private void AddBullet(List<Sprite> sprites)
//    {
//        var bullet = Bullet.Clone() as Bullet;
//        bullet.Direction = this.Direction;
//        bullet.Position = this.Position;
//        bullet.LinearVelocity = this.LinearVelocity * 2;
//        bullet.LifeSpan = 2f;
//        bullet.Parent = this;

//        sprites.Add(bullet);
//    }
//}