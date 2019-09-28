using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Managers;
using Template.Models;

namespace Template.Sprites
{
    public class Sprite
    {
        private Dictionary<string, Animation> _animations;
        #region Fields

        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animation;

        protected Vector2 _position;

        protected Texture2D _texture;

        #endregion

        #region Propeties

        public Input Input;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        public float Speed = 1f;

        public Vector2 Velocity;

        #endregion

        #region Method

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new Exception("funkar inte");


        }

        public virtual void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Jump))
                Velocity.Y = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Left))
                Velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
                Velocity.X = Speed;
        }

        protected virtual void SetAnimations()
        {
            if (Velocity.X < 0)
                _animationManager.Play(_animation["WalkLeft"]);
            else if (Velocity.X > 0)
                _animationManager.Play(_animation["WalkRight"]);
            else if (Velocity.Y > 0)
                _animationManager.Play(_animation["Jump"]);
            else
                _animationManager.Play(_animation["Idle"]);
        }

        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();

            SetAnimations();

            _animationManager.Update(gameTime);

            Position += Velocity;
            Velocity = Vector2.Zero;
        }

        #endregion
    }
}
