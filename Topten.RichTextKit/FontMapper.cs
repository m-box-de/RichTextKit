﻿// RichTextKit
// Copyright © 2019-2020 Topten Software. All Rights Reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may 
// not use this product except in compliance with the License. You may obtain 
// a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT 
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
// License for the specific language governing permissions and limitations 
// under the License.

using SkiaSharp;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Topten.RichTextKit
{
    /// <summary>
    /// The FontMapper class is responsible for mapping style typeface information
    /// to an SKTypeface.
    /// </summary>
    public class FontMapper
    {
        static Dictionary<string, List<SKTypeface>> _customFonts = new Dictionary<string, List<SKTypeface>>();

        /// <summary>
        /// Constructs a new FontMapper instnace
        /// </summary>
        public FontMapper()
        {
        }

        /// <summary>
        /// Loads a private font from a stream
        /// </summary>
        /// <param name="stream">The stream to load from</param>
        /// <param name="familyName">An optional family name to override the font's built in name</param>
        /// <returns>True if the font was successully loaded</returns>
        public static bool LoadPrivateFont(System.IO.Stream stream, string familyName = null)
        {
            var tf = SKTypeface.FromStream(stream);
            if (tf == null)
                return false;

            var qualifiedName = familyName ?? tf.FamilyName;
            if (tf.FontSlant != SKFontStyleSlant.Upright)
            {
                qualifiedName += "-Italic";
            }

            // Get a list of typefaces with this family
            if (!_customFonts.TryGetValue(qualifiedName, out var listFonts))
            {
                listFonts = new List<SKTypeface>();
                _customFonts[qualifiedName] = listFonts;
            }

            // Add to the list
            listFonts.Add(tf);

            return true;
        }


        /// <summary>
        /// Map a RichTextKit style to an SKTypeface
        /// </summary>
        /// <param name="style">The style</param>
        /// <param name="ignoreFontVariants">True to ignore variants (super/subscript)</param>
        /// <returns>The mapped typeface</returns>
        public virtual SKTypeface TypefaceFromStyle(IStyle style, bool ignoreFontVariants)
        {
            // Work out the qualified name
            var qualifiedName = style.FontFamily;
            if (style.FontItalic)
                qualifiedName += "-Italic";

            // Look up custom fonts
            List<SKTypeface> listFonts;
            if (_customFonts.TryGetValue(qualifiedName, out listFonts))
            {
                // Find closest weight
                listFonts.OrderBy(x => Math.Abs(x.FontWeight - style.FontWeight));
                return listFonts[0];
            }
            
            else
            {
            // Extra weight for superscript / subscript
            int extraWeight = 0;
            if (!ignoreFontVariants && (style.FontVariant == FontVariant.SuperScript || style.FontVariant == FontVariant.SubScript))
                    {
                        extraWeight += 100;
                    }

                // Get the typeface
                return SKTypeface.FromFamilyName(
                    style.FontFamily,
                    (SKFontStyleWeight)(style.FontWeight + extraWeight),
                    0,
                    style.FontItalic ? SKFontStyleSlant.Italic : SKFontStyleSlant.Upright
                    ) ?? SKTypeface.CreateDefault();
            }

            // Do default mapping
            //return SKTypeface.Default;

            

        }

        /*
        /// <summary>
        /// Maps a given style to a specific typeface
        /// </summary>
        /// <param name="style">The style to be mapped</param>
        /// <param name="ignoreFontVariants">Indicates the mapping should ignore font variants (use to get font for ellipsis)</param>
        /// <returns>A mapped typeface</returns>
        public virtual SKTypeface TypefaceFromStyle(IStyle style, bool ignoreFontVariants)
        {
            // Extra weight for superscript/subscript
            int extraWeight = 0;
            if (!ignoreFontVariants && (style.FontVariant == FontVariant.SuperScript || style.FontVariant == FontVariant.SubScript))
            {
                extraWeight += 100;
            }

            // Get the typeface
            return SKTypeface.FromFamilyName(
                style.FontFamily, 
                (SKFontStyleWeight)(style.FontWeight + extraWeight), 
                0, 
                style.FontItalic ? SKFontStyleSlant.Italic : SKFontStyleSlant.Upright
                ) ?? SKTypeface.CreateDefault();
        }
        */


        /// <summary>
        /// The default font mapper instance.  
        /// </summary>
        /// <remarks>
        /// The default font mapper is used by any TextBlocks that don't 
        /// have an explicit font mapper set (see the <see cref="TextBlock.FontMapper"/> property).
        /// 
        /// Replacing the default font mapper allows changing the font mapping
        /// for all text blocks that don't have an explicit mapper assigned.
        /// </remarks>
        public static FontMapper Default = new FontMapper();
    }
}
