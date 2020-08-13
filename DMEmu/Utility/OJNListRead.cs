using DMEmu.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMEmu
{
    public static class OJNListRead
    {
        public static OJN[] GetListOJN(string file)
        {
            List<OJN> list = new List<OJN>();

            using (var fs = File.Open(file, FileMode.Open))
            using (var reader = new BinaryReader(fs)) {
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

                list.Add(header);
            }

            return list.ToArray();
        }
    }
}
