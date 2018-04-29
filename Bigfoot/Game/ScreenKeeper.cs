using System;
using System.Collections.Generic;
using Bigfoot.Errors;
using Bigfoot.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bigfoot.Game
{
    public class ScreenKeeper
    {
        private readonly YetiGame _yetiGame;

        private Dictionary<string, YetiScreen> Screens = new Dictionary<string, YetiScreen>();
        private Texture2D blackPixel;

        public YetiScreen CurrentScreen { get; private set; }
        private YetiScreen _switchScreen;

        private bool _fadeOut;
        private bool _fadeIn;
        private float _opacity = 0;
        private float _fadeSpeed = 0.01f;

        public enum ScreenTransition
        {
            Fade,
            Instant
        }

        public enum TransitionSpeed
        {
            Slowest,
            Slow,
            Fast,
            Fastest
        }

    private ScreenTransition _transition = ScreenTransition.Instant;

        public ScreenKeeper(YetiGame yetiGame)
        {
            _yetiGame = yetiGame;
            blackPixel = new Texture2D(yetiGame.GraphicsDevice, 1, 1);
            var tcolor = new Color[1];
            tcolor[0] = Color.Black;
            blackPixel.SetData(tcolor);
        }

        public void AddScreen(string screenName, YetiScreen yetiScreen)
        {
            Screens.Add(screenName, yetiScreen);
        }

        public void SetTransition(ScreenTransition transition)
        {
            _transition = transition;
        }

        public void SetTransitionSpeed(TransitionSpeed speed)
        {
            switch (speed)
            {
                case TransitionSpeed.Slowest:
                    _fadeSpeed = 0.001f;
                    break;
                case TransitionSpeed.Slow:
                    _fadeSpeed = 0.01f;
                    break;
                case TransitionSpeed.Fast:
                    _fadeSpeed = 0.05f;
                    break;
                case TransitionSpeed.Fastest:
                    _fadeSpeed = 0.1f;
                    break;
            }
        }

        public void ChangeScreen(string screenName)
        {
            Screens.TryGetValue(screenName, out var selectedScreen);

            if (selectedScreen == null)
                throw new ScreenNotFoundException();

            switch (_transition)
            {
                case ScreenTransition.Fade:
                    _switchScreen = selectedScreen;
                    _fadeOut = true;
                    break;
                case ScreenTransition.Instant:
                    CurrentScreen = selectedScreen;
                    break;
            }
        }

        public void Update()
        {
            if (_fadeOut)
            {
                if (_opacity + _fadeSpeed >= 1)
                {
                    _fadeOut = false;
                    _opacity = 1;
                    _fadeIn = true;
                    CurrentScreen = _switchScreen;
                    return;
                }

                _opacity += _fadeSpeed;
            }
            else if (_fadeIn)
            {
                if (_opacity - _fadeSpeed <= 0)
                {
                    _fadeIn = false;
                    _opacity = 0;
                    return;
                }
                _opacity -= _fadeSpeed;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_fadeOut || _fadeIn)
            {
                spriteBatch.Draw(blackPixel, new Rectangle(0, 0, _yetiGame.YetiProperties.WindowWidth, _yetiGame.YetiProperties.WindowHeight), Color.White * _opacity);
            }
        }

        public YetiScreen GetScreen(string screen)
        {
            Screens.TryGetValue(screen, out var selectedScreen);

            if (selectedScreen == null)
                throw new ScreenNotFoundException();

            return selectedScreen;
        }
        
    }
}
