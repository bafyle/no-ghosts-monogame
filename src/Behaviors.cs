using System;
using MonoGame.Extended;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace No_Ghosts
{
    public abstract class GameObjectBehavior
    {

    }
    public class CollisionBehavior : GameObjectBehavior
    {
        public RectangleF Bounds;
        public CollisionBehavior()
        {
            Bounds = new RectangleF();
        }
        public void setRectangleAttrs(float x, float y, float width, float height)
        {
            Bounds.Position = new Vector2(x, y);
            Bounds.Size = new Size2(width, height);
        }
        public void setRectangleAttrs(Vector2 position, Size2 size)
        {
            Bounds.Position = position;
            Bounds.Size = size;
        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(Bounds, Color.Red);
        }
    }
}