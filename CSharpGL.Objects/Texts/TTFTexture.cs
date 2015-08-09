﻿using CSharpGL.Objects.Texts.FreeTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGL.Objects.Texts
{
    /// <summary>
    /// 用一个纹理绘制指定范围内的所有可见字符
    /// </summary>
    public class TTFTexture : IDisposable
    {

        /// <summary>
        /// TTF文件名
        /// </summary>
        public string TtfFullname { get; set; }

        /// <summary>
        /// 字形高度
        /// </summary>
        public int FontHeight { get; set; }

        /// <summary>
        /// 第一个字符
        /// </summary>
        public char FirstChar { get; set; }

        /// <summary>
        /// 最后一个字符
        /// </summary>
        public char LastChar { get; set; }

        /// <summary>
        /// 存储了从<see cref="FirstChar"/>到<see cref="LastChar"/>的所有可见字符的位图。
        /// </summary>
        public System.Drawing.Bitmap BigBitmap { get; set; }

        /// <summary>
        /// 记录每个字符在<see cref="BigBitmap"/>里的偏移量及其字形的宽高。
        /// </summary>
        public Dictionary<char, CharacterInfo> CharInfoDict { get; set; }

        internal TTFTexture() { }

        /// <summary>
        /// 获取一个<see cref="TTFTexture"/>实例。
        /// </summary>
        /// <param name="ttfFullname"></param>
        /// <param name="fontHeight"></param>
        /// <param name="firstChar"></param>
        /// <param name="lastChar"></param>
        /// <param name="maxTextureWidth"></param>
        /// <returns></returns>
        public static TTFTexture GetTTFTexture(string ttfFullname, int fontHeight, char firstChar, char lastChar, int maxTextureWidth)
        {
            var result = TTFTextureHelper.GetTTFTexture(ttfFullname, fontHeight, firstChar, lastChar, maxTextureWidth);

            return result;
        }

        ~TTFTexture()
        {
            this.Dispose();
        }

        #region IDisposable Members

        /// <summary>
        /// Internal variable which checks if Dispose has already been called
        /// </summary>
        protected Boolean disposed;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected void Dispose(Boolean disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                //Managed cleanup code here, while managed refs still valid
            }
            //Unmanaged cleanup code here
            System.Drawing.Bitmap bmp = this.BigBitmap;
            if (bmp != null)
            {
                this.BigBitmap = null;
                this.CharInfoDict.Clear();
                bmp.Dispose();
            }

            disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Call the private Dispose(bool) helper and indicate
            // that we are explicitly disposing
            this.Dispose(true);

            // Tell the garbage collector that the object doesn't require any
            // cleanup when collected since Dispose was called explicitly.
            GC.SuppressFinalize(this);
        }

        #endregion

        public void GetTextureWidthHeight(int maxTextureWidth, out int width, out int height)
        {
            int textureWidth = maxTextureWidth;
            int textureHeight = maxTextureWidth;
            System.Drawing.Bitmap bitmap = this.BigBitmap;

            for (int size = 1; size <= maxTextureWidth; size *= 2)
            {
                if (bitmap.Width < size)
                {
                    textureWidth = size / 2;
                    break;
                }
                if (bitmap.Width == size)
                    textureWidth = size;

            }

            for (int size = 1; size <= maxTextureWidth; size *= 2)
            {
                if (bitmap.Height < size)
                {
                    textureHeight = size / 2;
                    break;
                }
                if (bitmap.Height == size)
                    textureHeight = size;
            }

            width = textureWidth;
            height = textureHeight;
        }
    }

}

