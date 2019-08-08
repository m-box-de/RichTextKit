﻿using System;
using Topten.RichTextKit;
using SkiaSharp;
using System.Diagnostics;

namespace SandboxDriver
{
    public class SandboxDriver
    {
        public int ContentModeCount = 11;
        public int ContentMode = 0;
        public TextDirection BaseDirection = TextDirection.LTR;
        public TextAlignment TextAlignment = TextAlignment.Auto;
        public float Scale = 1.0f;
        public bool UseMaxWidth = true;
        public bool UseMaxHeight = false;
        public bool ShowMeasuredSize = false;

        TextBlock _textBlock = new TextBlock();

        public void Render(SKCanvas canvas, float canvasWidth, float canvasHeight)
        {
            canvas.Clear(new SKColor(0xFFFFFFFF));

            const float margin = 80;


            float? height = (float)(canvasHeight - margin * 2);
            float? width = (float)(canvasWidth - margin * 2);
            //width = 25;

            if (!UseMaxHeight)
                height = null;
            if (!UseMaxWidth)
                width = null;

            using (var gridlinePaint = new SKPaint() { Color = new SKColor(0xFFFF0000), StrokeWidth = 1 })
            {
                canvas.DrawLine(new SKPoint(margin, 0), new SKPoint(margin, (float)canvasHeight), gridlinePaint);
                if (width.HasValue)
                    canvas.DrawLine(new SKPoint(margin + width.Value, 0), new SKPoint(margin + width.Value, (float)canvasHeight), gridlinePaint);
                canvas.DrawLine(new SKPoint(0, margin), new SKPoint((float)canvasWidth, margin), gridlinePaint);
                if (height.HasValue)
                    canvas.DrawLine(new SKPoint(0, margin + height.Value), new SKPoint((float)canvasWidth, margin + height.Value), gridlinePaint);
            }

            //string typefaceName = "Times New Roman";
            string typefaceName = "Segoe UI";
            //string typefaceName = "Segoe Script";

            var styleSmall = new Style() { FontFamily = typefaceName, FontSize = 12 * Scale };
            var styleScript = new Style() { FontFamily = "Segoe Script", FontSize = 18 * Scale };
            var styleHeading = new Style() { FontFamily = typefaceName, FontSize = 24 * Scale, FontWeight = 700 };
            var styleNormal = new Style() { FontFamily = typefaceName, FontSize = 18 * Scale, LineHeight = 1.0f };
            var styleLTR = new Style() { FontFamily = typefaceName, FontSize = 18 * Scale, TextDirection = TextDirection.LTR };
            var styleBold = new Style() { FontFamily = typefaceName, FontSize = 18 * Scale, FontWeight = 700 };
            var styleUnderline = new Style() { FontFamily = typefaceName, FontSize = 18 * Scale, Underline = UnderlineStyle.Gapped };
            var styleStrike = new Style() { FontFamily = typefaceName, FontSize = 18 * Scale, StrikeThrough = StrikeThroughStyle.Solid };
            var styleSubScript = new Style() { FontFamily = typefaceName, FontSize = 18 * Scale, FontVariant = FontVariant.SubScript };
            var styleSuperScript = new Style() { FontFamily = typefaceName, FontSize = 18 * Scale, FontVariant = FontVariant.SuperScript };
            var styleItalic = new Style() { FontFamily = typefaceName, FontItalic = true, FontSize = 18 * Scale };
            var styleBoldLarge = new Style() { FontFamily = typefaceName, FontSize = 28 * Scale, FontWeight = 700 };
            var styleRed = new Style() { FontFamily = typefaceName, FontSize = 18 * Scale, TextColor = new SKColor(0xFFFF0000) };
            var styleBlue = new Style() { FontFamily = typefaceName, FontSize = 18 * Scale, TextColor = new SKColor(0xFF0000FF) };


            _textBlock.Clear();
            _textBlock.MaxWidth = width;
            _textBlock.MaxHeight = height;

            _textBlock.BaseDirection = BaseDirection;
            _textBlock.Alignment = TextAlignment;

            switch (ContentMode)
            {
                case 0:
                    _textBlock.AddText("Welcome to RichTextKit!\n", styleHeading);
                    _textBlock.AddText("\nRichTextKit is a rich text layout, rendering and measurement library for SkiaSharp.\n\nIt supports normal, ", styleNormal);
                    _textBlock.AddText("bold", styleBold);
                    _textBlock.AddText(", ", styleNormal);
                    _textBlock.AddText("italic", styleItalic);
                    _textBlock.AddText(", ", styleNormal);
                    _textBlock.AddText("underline", styleUnderline);
                    _textBlock.AddText(" (including ", styleNormal);
                    _textBlock.AddText("gaps over descenders", styleUnderline);
                    _textBlock.AddText("), ", styleNormal);
                    _textBlock.AddText("strikethrough", styleStrike);
                    _textBlock.AddText(", superscript (E=mc", styleNormal);
                    _textBlock.AddText("2", styleSuperScript);
                    _textBlock.AddText("), subscript (H", styleNormal);
                    _textBlock.AddText("2", styleSubScript);
                    _textBlock.AddText("O), ", styleNormal);
                    _textBlock.AddText("colored ", styleRed);
                    _textBlock.AddText("text", styleBlue);
                    _textBlock.AddText(" and ", styleNormal);
                    _textBlock.AddText("mixed ", styleNormal);
                    _textBlock.AddText("sizes", styleSmall);
                    _textBlock.AddText(" and ", styleNormal);
                    _textBlock.AddText("fonts", styleScript);
                    _textBlock.AddText(".\n\n", styleNormal);
                    _textBlock.AddText("Font fallback means emojis work: 🌐 🍪 🍕 🚀 and ", styleNormal);
                    _textBlock.AddText("text shaping and bi-directional text support means complex scripts and languages like Arabic: مرحبا بالعالم, Japanese: ハローワールド, Chinese: 世界您好 and Hindi: हैलो वर्ल्ड are rendered correctly!\n\n", styleNormal);
                    _textBlock.AddText("RichTextKit also supports left/center/right text alignment, word wrapping, truncation with ellipsis place-holder, text measurement, hit testing, painting a selection range, caret position & shape helpers.", styleNormal);
                    break;

                case 1:
                    _textBlock.AddText("Hello Wor", styleNormal);
                    _textBlock.AddText("ld", styleRed);
                    _textBlock.AddText(". This is normal 18px. These are emojis: 🌐 🍪 🍕 🚀 ", styleNormal);
                    _textBlock.AddText("This is ", styleNormal);
                    _textBlock.AddText("bold 28px", styleBoldLarge);
                    _textBlock.AddText(". ", styleNormal);
                    _textBlock.AddText("This is italic", styleItalic);
                    _textBlock.AddText(". This is ", styleNormal);
                    _textBlock.AddText("red", styleRed);
                    _textBlock.AddText(". This is Arabic: (", styleNormal);
                    _textBlock.AddText("تسجّل ", styleNormal);
                    _textBlock.AddText("يتكلّم", styleNormal);
                    _textBlock.AddText("), Hindi: ", styleNormal);
                    _textBlock.AddText("हालाँकि प्रचलित रूप पूज", styleNormal);
                    _textBlock.AddText(", Han: ", styleNormal);
                    _textBlock.AddText("緳 踥踕", styleNormal);
                    break;

                case 2:
                    _textBlock.AddText("Hello Wor", styleNormal);
                    _textBlock.AddText("ld", styleRed);
                    _textBlock.AddText(".\nThis is normal 18px.\nThese are emojis: 🌐 🍪 🍕 🚀\n", styleNormal);
                    _textBlock.AddText("This is ", styleNormal);
                    _textBlock.AddText("bold 28px", styleBoldLarge);
                    _textBlock.AddText(".\n", styleNormal);
                    _textBlock.AddText("This is italic", styleItalic);
                    _textBlock.AddText(".\nThis is ", styleNormal);
                    _textBlock.AddText("red", styleRed);
                    /*
                    tle.AddText(".\nThis is Arabic: (", styleNormal);
                    tle.AddText("تسجّل ", styleNormal);
                    tle.AddText("يتكلّم", styleNormal);
                    tle.AddText("), Hindi: ", styleNormal);
                    */
                    _textBlock.AddText(".\nThis is Arabic: (تسجّل يتكلّم), Hindi: ", styleNormal);
                    _textBlock.AddText("हालाँकि प्रचलित रूप पूज", styleNormal);
                    _textBlock.AddText(", Han: ", styleNormal);
                    _textBlock.AddText("緳 踥踕", styleNormal);
                    break;

                case 3:
                    _textBlock.AddText("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus semper, sapien vitae placerat sollicitudin, lorem diam aliquet quam, id finibus nisi quam eget lorem.\nDonec facilisis sem nec rhoncus elementum. Cras laoreet porttitor malesuada.\n\nVestibulum sed lacinia diam. Mauris a mollis enim. Cras in rhoncus mauris, at vulputate nisl. Sed nec lobortis dolor, hendrerit posuere quam. Vivamus malesuada sit amet nunc ac cursus. Praesent volutpat euismod efficitur. Nam eu ante.", styleNormal);
                    break;

                case 4:
                    _textBlock.AddText("مرحبا بالعالم.  هذا هو اختبار التفاف \nالخط للتأكد من أنه يعمل للغات من اليمين إلى اليسار.", styleNormal);
                    break;

                case 5:
                    //_textBlock.AddText("مرحبا بالعالم.  هذا هو اختبار التفاف الخط للتأكد من \u2066ACME Inc.\u2069 أنه يعمل للغات من اليمين إلى اليسار.", styleNormal);
                    _textBlock.AddText("مرحبا بالعالم.  هذا هو اختبار التفاف الخط للتأكد من ", styleNormal);
                    _textBlock.AddText("ACME Inc.", styleLTR);
                    _textBlock.AddText(" أنه يعمل للغات من اليمين إلى اليسار.", styleNormal);
                    break;

                case 6:
                    _textBlock.AddText("Subscript: H", styleNormal);
                    _textBlock.AddText("2", styleSubScript);
                    _textBlock.AddText("O  Superscript: E=mc", styleNormal);
                    _textBlock.AddText("2", styleSuperScript);
                    _textBlock.AddText("  Key: C", styleNormal);
                    _textBlock.AddText("♯", styleSuperScript);
                    _textBlock.AddText(" B", styleNormal);
                    _textBlock.AddText("♭", styleSubScript);
                    break;

                case 7:
                    _textBlock.AddText("The quick brown fox jumps over the lazy dog.", styleUnderline);
                    _textBlock.AddText(" ", styleNormal);
                    _textBlock.AddText("Strike Through", styleStrike);
                    _textBlock.AddText(" something ends in wooooooooq", styleNormal);
                    break;

                case 8:
                    _textBlock.AddText("Apples and Bananas\r\n", styleNormal);
                    _textBlock.AddText("Pears\r\n", styleNormal);
                    _textBlock.AddText("Bananas\r\n", styleNormal);
                    break;

                case 9:
                    _textBlock.AddText("Hello World", styleNormal);
                    break;

                case 10:
                    _textBlock.AddText("", styleNormal);
                    break;

            }

            var sw = new Stopwatch();
            sw.Start();
            _textBlock.Layout();
            var elapsed = sw.ElapsedMilliseconds;

            var options = new TextPaintOptions()
            {
                SelectionColor = new SKColor(0x60FF0000),
            };

            HitTestResult? htr = null;
            CaretInfo? ci = null;
            if (_showHitTest)
            {
                htr = _textBlock.HitTest(_hitTestX - margin, _hitTestY - margin);
                if (htr.Value.OverCodePointIndex >= 0)
                {
                    options.SelectionStart = htr.Value.OverCodePointIndex;
                    options.SelectionEnd = _textBlock.CaretIndicies[_textBlock.LookupCaretIndex(htr.Value.OverCodePointIndex) + 1];
                }

                ci = _textBlock.GetCaretInfo(htr.Value.ClosestCodePointIndex);
            }

            if (ShowMeasuredSize)
            {
                using (var paint = new SKPaint()
                {
                    Color = new SKColor(0x1000FF00),
                    IsStroke = false,
                })
                {
                    var rect = new SKRect(margin + _textBlock.MeasuredPadding.Left, margin, margin + _textBlock.MeasuredWidth + _textBlock.MeasuredPadding.Left, margin + _textBlock.MeasuredHeight);
                    canvas.DrawRect(rect, paint);
                }
            }

            if (_textBlock.MeasuredOverhang.Left > 0)
            {
                using (var paint = new SKPaint() { Color = new SKColor(0xFF00fff0), StrokeWidth = 1 })
                {
                    canvas.DrawLine(new SKPoint(margin - _textBlock.MeasuredOverhang.Left, 0), new SKPoint(margin - _textBlock.MeasuredOverhang.Left, (float)canvasHeight), paint);
                }
            }

            if (_textBlock.MeasuredOverhang.Right > 0)
            {
                using (var paint = new SKPaint() { Color = new SKColor(0xFF00ff00), StrokeWidth = 1 })
                {
                    float x;
                    if (_textBlock.MaxWidth.HasValue)
                    {
                        x = margin + _textBlock.MaxWidth.Value;
                    }
                    else
                    {
                        x = margin + _textBlock.MeasuredWidth;
                    }
                    x += _textBlock.MeasuredOverhang.Right;
                    canvas.DrawLine(new SKPoint(x, 0), new SKPoint(x, (float)canvasHeight), paint);
                }
            }

            _textBlock.Paint(canvas, new SKPoint(margin, margin), options);

            if (ci != null)
            {
                using (var paint = new SKPaint()
                {
                    Color = new SKColor(0xFF000000),
                    IsStroke = true,
                    IsAntialias = true,
                    StrokeWidth = Scale,
                })
                {
                    var rect = ci.Value.CaretRectangle;
                    rect.Offset(margin, margin);
                    canvas.DrawLine(rect.Right, rect.Top, rect.Left, rect.Bottom, paint);
                }
            }

            var state = $"Size: {width} x {height} Base Direction: {BaseDirection} Alignment: {TextAlignment} Content: {ContentMode} scale: {Scale} time: {elapsed}";
            canvas.DrawText(state, margin, 20, new SKPaint()
            {
                Typeface = SKTypeface.FromFamilyName("Arial"),
                TextSize = 12,
            });

            state = $"Selection: {options.SelectionStart}-{options.SelectionEnd} Closest: {(htr.HasValue ? htr.Value.ClosestCodePointIndex.ToString() : "-")}";
            canvas.DrawText(state, margin, 40, new SKPaint()
            {
                Typeface = SKTypeface.FromFamilyName("Arial"),
                TextSize = 12,
            });
        }

        float _hitTestX;
        float _hitTestY;
        bool _showHitTest;

        public void HitTest(float x, float y)
        {
            _hitTestX = x;
            _hitTestY = y;
            _showHitTest = true;
        }

    }   
}
