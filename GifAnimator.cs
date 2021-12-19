using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
// ReSharper disable UnusedMember.Global

namespace Util
{
    internal struct FrameInfo
    {
        internal FrameInfo(Image frame, int delay)
        {
            // // Contract.Requires(frame != null);
            // // Contract.Requires(delay >= 0);

            Frame = frame;
            Delay = delay;
        }

        internal Image Frame { get; }

        internal int Delay { get; }
    }

    // ReSharper disable once UnusedMember.Global
    public class GifAnimator
    {
        private short _maxWidth = 1, _maxHeight = 1; // width and height of the biggest frame

        private readonly byte[] _header = { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 };  // GIF89a
        private readonly byte[] _lsr = new byte[7];       // logical screen descriptor

        private readonly List<FrameInfo> _frameData;

        public GifAnimator()
        {
            _frameData = new List<FrameInfo>();
            _lsr[4] = 0x77;     // no global colour table     
            _lsr[5] = 0x00;
            _lsr[6] = 0x00;
        }

        public void Clear()
        {
            _frameData.Clear();
        }
        
        public void AddFrame(Image frame, int frameTime)
        {
            // // Contract.Requires(frame != null);
            // // Contract.Requires(frameTime >= 0);

            FrameInfo frameDetails = new FrameInfo((Image)frame.Clone(), frameTime);

            _frameData.Add(frameDetails);

            if (frame.Width > _maxWidth)
                _maxWidth = (short)frame.Width;
            if (frame.Height > _maxHeight)
                _maxHeight = (short)frame.Height;
        }

        public void SaveFile(string fileName)
        {
            // // Contract.Requires(fileName != null);                       
            FileStream outFile = null;
            byte[] word = new byte[2];

            try
            {
                outFile = new FileStream(fileName, FileMode.Create);

                using (BinaryWriter writer = new BinaryWriter(outFile))
                {
                    outFile = null;
                    writer.Write(_header);

                    // create logical screen descriptor
                    Array.Copy(BitConverter.GetBytes(_maxWidth), word, word.Length);
                    _lsr[0] = word[0];       //width
                    _lsr[1] = word[1];
                    Array.Copy(BitConverter.GetBytes(_maxHeight), word, word.Length);
                    _lsr[2] = word[0];       // height
                    _lsr[3] = word[1];
                    writer.Write(_lsr);

                    // Process frames into data blocks
                    byte[] animData = GenerateFrameData();

                    writer.Write(animData);

                    // write trailer
                    writer.Write(0x3B);
                }
            }
            catch (IOException ex)
            {
                throw new IOException("Unable to write gif animation", ex);
            }
            catch (ArgumentException ex)
            {
                    throw new ArgumentException(ex.Message, ex);
            }
            finally
            {
                outFile?.Dispose();
            }           
        }

        private byte[] GenerateFrameData()
        {
            // //Contract.Ensures(// Contract.Result<Byte[]>() != null);

            byte[] graphicControlExtension = new byte[8];
            byte[] imageDescriptor = new byte[10];
            byte[] globalColourTable = new byte[768];

            var data = new MemoryStream();
            foreach (var t in _frameData)
            {
                var gifImage = new MemoryStream();               

                t.Frame.Save(gifImage, ImageFormat.Gif);
                gifImage.Seek(0, SeekOrigin.Begin);
    
                // extract relevant data from gif image
                gifImage.Seek(13, SeekOrigin.Current);
                gifImage.Read(globalColourTable, 0, 768);
                gifImage.Read(graphicControlExtension, 0, 8);
                gifImage.Read(imageDescriptor, 0, 10);
                var imageDataLength = (int)(gifImage.Length - gifImage.Position - 1);
                if (imageDataLength >= 0)
                {
                    var imageData = new byte[imageDataLength];
                    gifImage.Read(imageData, 0, imageDataLength);

                    // write out data               
                    Array.Copy(BitConverter.GetBytes(t.Delay), 0, graphicControlExtension, 4, 2); // set delay time for frame
                    data.Write(graphicControlExtension, 0, 8);

                    imageDescriptor[9] = 0x87;      // make decoder aware of local colour table
                    data.Write(imageDescriptor, 0, 10);

                    data.Write(globalColourTable, 0, 768);
                    data.Write(imageData, 0, imageDataLength);
                }

                gifImage.Dispose();
            }

            data.Close();

            var outputData = data.ToArray();

            return outputData; 
        }
    }
}
