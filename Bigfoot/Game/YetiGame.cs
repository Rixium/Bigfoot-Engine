using System.Runtime.Remoting.Messaging;
using Bigfoot.Errors;
using Bigfoot.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bigfoot.Game
{
    public class YetiGame : Microsoft.Xna.Framework.Game
    {
        protected GraphicsDeviceManager Graphics;
        protected SpriteBatch SpriteBatch;

        private static YetiContent _yetiContent;
        public static YetiContent YetiContent
        {
            get
            {
                if (_yetiContent != null)
                    return _yetiContent;

                _yetiContent = new YetiContent();
                return _yetiContent;
            }
        }

        private static YetiProperties _yetiProperties;
        public YetiProperties YetiProperties {
            get {
                if (_yetiProperties != null)
                    return _yetiProperties;

                _yetiProperties = new YetiProperties();
                return _yetiProperties;
            }
        }

        public static ScreenKeeper ScreenKeeper { get; private set; }

        private bool _drawBegan;
        private YetiCam _yetiCam;

        protected void Setup()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected void Initialize(string title, int width, int height)
        {
            YetiContent.Initialize(Content);
            CreateYetiProperties(title, width, height);
            SetupWindowDefaults();
            ScreenKeeper = new ScreenKeeper(this);
            _yetiCam = new YetiCam(YetiProperties);
            Load();
        }

        protected void SetYetiContent(YetiContent yetiContent)
        {
            _yetiContent = yetiContent;
        }

        private void CreateYetiProperties(string title, int width, int height)
        {
            YetiProperties.WindowHeight = height;
            YetiProperties.WindowWidth = width;
            YetiProperties.WindowTitle = title;
        }

        private void SetupWindowDefaults()
        {
            Graphics.PreferredBackBufferWidth = YetiProperties.WindowWidth;
            Graphics.PreferredBackBufferHeight = YetiProperties.WindowHeight;
            Window.Title = YetiProperties.WindowTitle;
            Graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        protected virtual void Load()
        {

        }

        protected virtual void Unload()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            ScreenKeeper.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            BeginDraw();
            ScreenKeeper.Draw(SpriteBatch);
            EndDraw();
        }

        protected void UpdateCurrentScreen(GameTime gameTime)
        {
            ScreenKeeper.CurrentScreen?.Update(gameTime);
        }

        protected void DrawCurrentScreen()
        {
            if (!_drawBegan)
                throw new DrawErrorException();
            ScreenKeeper.CurrentScreen?.Draw(SpriteBatch);
        }

        protected void SetCamera(YetiCam yetiCam)
        {
            _yetiCam = yetiCam;
        }

        protected YetiCam GetCamera()
        {
            return _yetiCam;
        }

        protected void BeginDraw()
        {
            if (_drawBegan) return;
            SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, _yetiCam.GetMatrix());
            _drawBegan = true;
        }

        protected void EndDraw()
        {
            if (!_drawBegan) return;
            SpriteBatch.End();
            _drawBegan = false;
        }

        protected void Fill(Color color)
        {
            GraphicsDevice.Clear(color);
        }

        protected void Fill(Texture2D texture)
        {
            if (!_drawBegan)
                throw new DrawErrorException();
            if (texture == null)
                throw new NoTextureException();

            SpriteBatch.Draw(texture, new Rectangle(0, 0, YetiProperties.WindowWidth, YetiProperties.WindowHeight),
                Color.White);
        }

    }
}
