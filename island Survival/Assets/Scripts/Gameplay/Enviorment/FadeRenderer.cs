using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tmn
{
    public static class FadeRenderer
    {
        private static Color _defaultColor = new (1,1,1,1);
        private static Color _fadedColor = new(1, 1, 1, 0.7f);

        
        public static void FadeIn(this SpriteRenderer sprite)
        {
            sprite.color = _fadedColor;
        }

        public static void FadeOut(this SpriteRenderer sprite)
        {
            sprite.color = _defaultColor;
        }
    }
}