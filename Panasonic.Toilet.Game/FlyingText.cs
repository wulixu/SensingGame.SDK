//------------------------------------------------------------------------------
// <copyright file="FlyingText.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace TronCell.Game
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    // FlyingText creates text that flys out from a given point, and fades as it gets bigger.
    // NewFlyingText() can be called as often as necessary, and there can be many texts flying out at once.
    public class FlyingText
    {
        private static readonly List<FlyingText> FlyingTexts = new List<FlyingText>();
        private readonly double fontGrow;
        private readonly string text;
        private System.Windows.Point center;
        private System.Windows.Media.Brush brush;
        private double fontSize;
        private double alpha;
        private Label label;
        private int refValue;

        public FlyingText(string s, double size, System.Windows.Point center, int refValue = 0)
        {
            this.text = s;
            this.fontSize = Math.Max(1, size);
            this.center = center;
            this.alpha = 1.0;
            this.refValue = refValue;
            if (this.brush == null)
            {
                //Need to reflect.
                if (this.refValue <= 10)
                {
                    this.brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(250, 26, 42, 67));
                    this.fontSize = Math.Max(1, size * 0.5);
                    this.fontGrow = Math.Sqrt(size) * 0.1;
                }
                else if (this.refValue <= 20)
                {
                    this.brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(250, 216, 42, 67));
                    this.fontSize = Math.Max(1, size * 0.6);
                    this.fontGrow = Math.Sqrt(size) * 0.2;
                }
                else if (this.refValue <= 30)
                {
                    this.brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(250, 26, 142, 67));
                    this.fontSize = Math.Max(1, size * 0.8);
                    this.fontGrow = Math.Sqrt(size) * 0.3;
                }
                else if (this.refValue <= 40)
                {
                    this.brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(250, 26, 42, 167));
                    this.fontSize = Math.Max(1, size);
                    this.fontGrow = Math.Sqrt(size) * 0.4;
                }
                else
                {
                    this.brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(250, 26, 182, 167));
                    this.fontSize = Math.Max(1, size);
                    this.fontGrow = Math.Sqrt(size) * 0.5;
                }
                //this.brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(250, 26, 42, 67));
            }

            if (this.label == null)
            {
                this.label = FallingThings.MakeSimpleLabel(this.text, new Rect(0, 0, 0, 0), this.brush);
            }

            this.label.Foreground = this.brush;
        }

        public static void NewFlyingText(double size, System.Windows.Point center, string s,int points)
        {
            FlyingTexts.Add(new FlyingText(s, size, center,points));
        }

        public static void Draw(UIElementCollection children)
        {
            for (int i = 0; i < FlyingTexts.Count; i++)
            {
                FlyingText flyout = FlyingTexts[i];
                if (flyout.alpha <= 0)
                {
                    FlyingTexts.Remove(flyout);
                    i--;
                }
            }

            foreach (var flyout in FlyingTexts)
            {
                flyout.Advance();
                children.Add(flyout.label);
            }
        }

        private void Advance()
        {
            this.alpha -= 0.01;
            if (this.alpha < 0)
            {
                this.alpha = 0;
            }

            if (this.brush == null)
            {
                this.brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 255, 255));
            }

            if (this.label == null)
            {
                this.label = FallingThings.MakeSimpleLabel(this.text, new Rect(0, 0, 0, 0), this.brush);
            }

            this.brush.Opacity = Math.Pow(this.alpha, 1.5);
            this.label.Foreground = this.brush;
            this.fontSize += this.fontGrow;
            this.label.FontSize = Math.Max(1, this.fontSize);
            Rect renderRect = new Rect(this.label.RenderSize);
            this.label.SetValue(Canvas.LeftProperty, this.center.X - (renderRect.Width / 2));
            this.label.SetValue(Canvas.TopProperty, this.center.Y - (renderRect.Height / 2));
        }
    }
}
