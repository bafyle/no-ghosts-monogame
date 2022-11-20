using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace No_Ghosts
{
    public interface IDrawable
    {
        public Texture2D texture{get; set;}
        public Vector2 textureScale{get; set;}
        public Single angle{get; set;}
        public Color textureColor {get; set;}
        public void draw(ref SpriteBatch spriteBatch);
        public void loadContent(ContentManager manager);
    }
    public abstract class GameObject
    {
        public Vector2 position;
    }
}