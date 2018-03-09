﻿using System;

namespace MonoGame.Extended.Gui.Controls
{
    public class StackPanel : LayoutControl
    {
        public StackPanel()
        {
        }

        public Orientation Orientation { get; set; } = Orientation.Vertical;
        public int Spacing { get; set; } = 0;

        public override Size2 GetContentSize(IGuiContext context)
        {
            var width = 0f;
            var height = 0f;

            foreach (var control in Items)
            {
                var actualSize = control.GetActualSize(context);

                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        width += actualSize.Width;
                        height = actualSize.Height > height ? actualSize.Height : height;
                        break;
                    case Orientation.Vertical:
                        width = actualSize.Width > width ? actualSize.Width : width;
                        height += actualSize.Height;
                        break;
                    default:
                        throw new InvalidOperationException($"Unexpected orientation {Orientation}");
                }
            }

            width += Orientation == Orientation.Horizontal ? (Items.Count - 1) * Spacing : 0;
            height += Orientation == Orientation.Vertical ? (Items.Count - 1) * Spacing : 0;

            return new Size2(width, height);
        }

        public override void Layout(IGuiContext context, RectangleF rectangle)
        {
            var x = 0f;
            var y = 0f;
            var availableSize = rectangle.Size;

            foreach (var control in Items)
            {
                var actualSize = control.GetActualSize(context);

                switch (Orientation)
                {
                    case Orientation.Vertical:
                        //control.VerticalAlignment = VerticalAlignment.Top;

                        PlaceControl(context, control, 0f, y, Width, actualSize.Height);
                        y += actualSize.Height + Spacing;
                        availableSize.Height -= actualSize.Height;
                        break;
                    case Orientation.Horizontal:
                        //control.HorizontalAlignment = HorizontalAlignment.Left;

                        PlaceControl(context, control, x, 0f, actualSize.Width, Height);
                        x += actualSize.Width + Spacing;
                        availableSize.Height -= actualSize.Height;
                        break;
                    default:
                        throw new InvalidOperationException($"Unexpected orientation {Orientation}");
                }
            }
        }
    }
}