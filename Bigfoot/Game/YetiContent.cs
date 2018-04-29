using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Bigfoot.Game
{
    public class YetiContent
    {

        private ContentManager _content;

        public void Initialize(ContentManager content)
        {
            _content = content;
        }

        public virtual void Load()
        {

        }

        public Texture2D LoadTexture2D(string relativePath)
        {
            return _content.Load<Texture2D>(relativePath);
        }

        public Song LoadSong(string relativePath)
        {
            return _content.Load<Song>(relativePath);
        }

        public SoundEffect LoadSoundEffect(string relativePath)
        {
            return _content.Load<SoundEffect>(relativePath);
        }

        public SpriteFont LoadFont(string relativePath)
        {
            return _content.Load<SpriteFont>(relativePath);
        }

    }
}
