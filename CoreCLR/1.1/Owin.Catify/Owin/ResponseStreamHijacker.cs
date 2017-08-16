using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Owin.Catify
{
    /// <summary>
    /// This implementation is inspired by the awesome Glimpse project.
    /// </summary>
    internal class ImageSourceHighjackerStream : Stream
    {
        public static readonly Regex Regex = new Regex("(<img.+?src=[\"'])(.+?)([\"'].*?>)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public string ImgSrc { get; set; }
        private Stream OutputStream { get; set; }
        private Encoding ContentEncoding { get; set; }

        public ImageSourceHighjackerStream(string imgSrc, Stream outputStream, Encoding contentEncoding)
        {
            ImgSrc = imgSrc;
            OutputStream = outputStream;
            ContentEncoding = contentEncoding;
        }

        public override bool CanRead
        {
            get { return OutputStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return OutputStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return OutputStream.CanWrite; }
        }

        public override long Length
        {
            get { return OutputStream.Length; }
        }

        public override long Position
        {
            get { return OutputStream.Position; }
            set { OutputStream.Position = value; }
        }

        // public override void Close()
        // {
        //     OutputStream.Close();
        // }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return OutputStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            OutputStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return OutputStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            string contentInBuffer = ContentEncoding.GetString(buffer, offset, count);
            contentInBuffer = Regex.Replace(contentInBuffer, m => m.Groups[1].Value + ImgSrc + m.Groups[3].Value);
            WriteToOutputStream(contentInBuffer);
        }

        public override void Flush()
        {
            OutputStream.Flush();
        }

        private void WriteToOutputStream(string content)
        {
            byte[] outputBuffer = ContentEncoding.GetBytes(content);
            OutputStream.Write(outputBuffer, 0, outputBuffer.Length);
        }
    }
}