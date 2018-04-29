using Bigfoot.Game;
using Microsoft.Xna.Framework;

namespace Bigfoot.Utils
{
    public class YetiCam
    {

        private YetiProperties _yetiProperties;
        public Matrix transform;
        public Vector2 position;
        public int minX, maxX, minY, maxY;

        public YetiCam(YetiProperties yetiProperties)
        {
            position = Vector2.Zero;
            _yetiProperties = yetiProperties;
        }

        public void Move(Vector2 amount)
        {
            position += amount;
            ApplyClamp();
        }

        public void Goto(Vector2 pos)
        {
            position = pos;
            ApplyClamp();
        }

        private void ApplyClamp()
        {
            position.X = MathHelper.Clamp(position.X, minX, maxX);
            position.Y = MathHelper.Clamp(position.Y, minY, maxY);
        }

        public Vector2 Position {
            get {
                return position;
            }
            set {
                position = value;
            }
        }

        public Matrix GetMatrix()
        {
            if (_yetiProperties == null) return transform;

            transform =
                Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0));

            return transform;
        }

    }
}
