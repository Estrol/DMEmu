﻿using DMEmu.Data;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace DMEmu
{
    /// <summary>
    /// Credit to: SirusDoma https://github.com/SirusDoma
    /// Source code: https://github.com/SirusDoma/O2MusicList/blob/master/Source/Decoders/OJNDecoder.cs
    /// </summary>
    public static class OJNDecoder
    {
        public static bool Validate(string FileName)
        {
            return Validate(File.Open(FileName, FileMode.Open));
        }

        public static bool Validate(Stream stream, bool keepOpen = false)
        {
            var encoding = Encoding.UTF8;
            using (var reader = new BinaryReader(stream, encoding, keepOpen)) {
                stream.Seek(0, SeekOrigin.Begin);
                string encryptSign = encoding.GetString(reader.ReadBytes(3));

                stream.Seek(4, SeekOrigin.Begin);
                string fileSign = encoding.GetString(reader.ReadBytes(3));

                return encryptSign == "new" || fileSign == "ojn";
            }
        }

        public static bool IsEncrypted(string FileName)
        {
            return IsEncrypted(File.Open(FileName, FileMode.Open));
        }

        public static bool IsEncrypted(Stream stream, bool keepOpen = false)
        {
            var encoding = Encoding.UTF8;
            using (var reader = new BinaryReader(stream, encoding, keepOpen)) {
                stream.Seek(0, SeekOrigin.Begin);
                string encryptSign = encoding.GetString(reader.ReadBytes(3));

                return encryptSign == "new";
            }
        }

        public static OJN Decode(string filename, Encoding encoding = default(Encoding))
        {
            var header = Decode(File.Open(filename, FileMode.Open), encoding, false);
            header.Source = filename;

            return header;
        }

        public static OJN Decode(Stream stream, Encoding encoding = default(Encoding), bool keepOpen = false)
        {
            byte[] inputData = new byte[0];
            bool encrypted = false;
            using (var reader = new BinaryReader(stream, Encoding.Unicode, keepOpen)) {
                stream.Seek(0, SeekOrigin.Begin);
                Console.WriteLine("Decode");
                string encryptSign = Encoding.UTF8.GetString(reader.ReadBytes(3));
                if (encryptSign == "new") {
                    inputData = Decrypt(stream);
                    encrypted = true;
                }
                else {
                    stream.Seek(0, SeekOrigin.Begin);
                    inputData = reader.ReadBytes((int)stream.Length);
                }
            }

            var header = Decode(inputData, encoding);
            header.Encrypted = encrypted;

            return header;
        }

        public static OJN Decode(byte[] inputData, Encoding encoding = default(Encoding))
        {
            if (encoding == default(Encoding)) {
                encoding = Encoding.UTF8;
            }

            using (var mstream = new MemoryStream(inputData))
            using (var reader = new BinaryReader(mstream)) {
                var header = new OJN();
                header.Id = reader.ReadInt32();
                header.Signature = reader.ReadBytes(4);
                header.EncodingVersion = reader.ReadSingle();
                header.Genre = (Genre)reader.ReadInt32();
                header.BPM = reader.ReadSingle();
                header.LevelEx = reader.ReadInt16();
                header.LevelNx = reader.ReadInt16();
                header.LevelHx = reader.ReadInt16();
                header.Padding = reader.ReadInt16(); 
                header.EventCountEx = reader.ReadInt32();
                header.EventCountNx = reader.ReadInt32();
                header.EventCountHx = reader.ReadInt32();
                header.NoteCountEx = reader.ReadInt32();
                header.NoteCountNx = reader.ReadInt32();
                header.NoteCountHx = reader.ReadInt32();
                header.MeasureCountEx = reader.ReadInt32();
                header.MeasureCountNx = reader.ReadInt32();
                header.MeasureCountHx = reader.ReadInt32();
                header.BlockCountEx = reader.ReadInt32();
                header.BlockCountNx = reader.ReadInt32();
                header.BlockCountHx = reader.ReadInt32();
                header.OldEncodingVersion = reader.ReadInt16();
                header.OldSongId = reader.ReadInt16();
                header.OldGenre = reader.ReadBytes(20);
                header.ThumbnailSize = reader.ReadInt32();
                header.FileVersion = reader.ReadInt32();
                header.Title = reader.ReadBytes(64);
                header.Artist = reader.ReadBytes(32);
                header.Pattern = reader.ReadBytes(32);
                header.OJM = reader.ReadBytes(32);
                header.CoverSize = reader.ReadInt32();
                header.DurationEx = reader.ReadInt32();
                header.DurationNx = reader.ReadInt32();
                header.DurationHx = reader.ReadInt32();
                header.BlockOffsetEx = reader.ReadInt32();
                header.BlockOffsetNx = reader.ReadInt32();
                header.BlockOffsetHx = reader.ReadInt32();
                header.CoverOffset = reader.ReadInt32();
                header.CharacterEncoding = encoding;

                if (mstream.Length > 300 && header.ThumbnailSize > 0) {
                    mstream.Seek(header.CoverOffset + header.CoverSize, SeekOrigin.Begin);
                    using (var bitmapStream = new MemoryStream(reader.ReadBytes(header.ThumbnailSize))) {
                        header.Thumbnail = new System.Drawing.Bitmap(bitmapStream);
                    }
                }

                return header;
            }
        }

        public static byte[] Decrypt(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.Unicode, true)) {
                stream.Seek(0, SeekOrigin.Begin);
                byte[] input = reader.ReadBytes((int)stream.Length);

                stream.Seek(3, SeekOrigin.Begin);
                byte blockSize = reader.ReadByte();
                byte mainKey = reader.ReadByte();
                byte midKey = reader.ReadByte();
                byte initialKey = reader.ReadByte();

                var encryptKeys = Enumerable.Repeat(mainKey, blockSize).ToArray();
                encryptKeys[0] = initialKey;
                encryptKeys[(int)Math.Floor(blockSize / 2f)] = midKey;

                byte[] output = new byte[stream.Length - stream.Position];
                for (int i = 0; i < output.Length; i += blockSize) {
                    for (int j = 0; j < blockSize; j++) {
                        int offset = i + j;
                        if (offset >= output.Length) {
                            return output;
                        }

                        output[offset] = (byte)(input[input.Length - (offset + 1)] ^ encryptKeys[j]);
                    }
                }

                return output;
            }
        }
    }
}
