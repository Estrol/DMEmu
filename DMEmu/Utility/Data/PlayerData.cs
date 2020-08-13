using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace DMEmu.Data
{
    /// <summary>
    /// Represent header data for D007.spt
    /// </summary>
    public class PlayerData
    {
        private string _InGameName;
        private short _Level;
        private short _Gender;
        private short _Instrument;
        private short _Hair;
        private short _Accessories;
        private short _Gloves;
        private short _Necklaces;
        private short _Shirt;
        private short _Pants;
        private short _Glasses;
        private short _Earrings;
        private short _ClothesAccessories;
        private short _Shoes;
        private short _Faces;
        private short _Wings;
        private short _InstrumentAccessories;
        private short _Pets;
        private short _HairAccessories;

        private readonly byte[] FileBuffer;
        private readonly string FilePath;
        private readonly Dictionary<short, string> ListOffsets = new Dictionary<short, string>()
        {
            {41, "Gender"}, {79, "Instrument"}, {83, "Hair"}, {87, "Accessories"}, {91, "Gloves"},
            {95, "Necklaces"}, {99, "Shirt"}, {103, "Pants"}, {107, "Glasses"}, {111, "Earrings"},
            {115, "ClothesAccessories"}, {119, "Shoes"}, {123, "Faces"}, {127, "Wings"}, 
            {131, "InstrumentAccessories"}, {135, "Pets"}, {139, "HairAccessories"}, {54, "Level"}
        };

        /// <summary>
        /// File contructor, Load D007.spt and parse the binary.
        /// </summary>
        /// <param name="FilePath"></param>
        public PlayerData(string FilePath) 
        {
            this.FilePath = FilePath;

            FileBuffer = File.ReadAllBytes(FilePath);
            short PacketLength = FileBuffer[20];
            if (PacketLength != 0x67) {
                if (PacketLength == 0x60) {
                    throw new InvalidDataException("Unsupported D007.spt");
                }
                throw new InvalidDataException("The SPT file is not UserData or D007.spt");
            }

            byte[] inGameName = new byte[12];
            Buffer.BlockCopy(FileBuffer, 28, inGameName, 0, 12);
            this._InGameName = Encoding.ASCII.GetString(inGameName);

            foreach (short offset in this.ListOffsets.Keys) {
                string offsetName = this.ListOffsets[offset];
                short value;

                if (offsetName == "Gender") {
                    value = FileBuffer[offset];
                } else {
                    value = BitConverter.ToInt16(FileBuffer, offset);
                }

                this.SetValue(this, this.ListOffsets[offset], value);
            }
        }

        private void SetValue(object src, string propName, short value)
        {
            Type tSrc = src.GetType();
            PropertyInfo pSrc = tSrc.GetProperty(propName);

            try {
                pSrc.SetValue(src, value, null);
            } catch (Exception err) {
                throw err;
            }
        }
        
        /// <summary>
        /// Save current PlayerData to File.
        /// </summary>
        public void Save()
        {
            File.WriteAllBytes(this.FilePath, this.FileBuffer);
        }

        private int CountDigit(int val)
        {
            return val.ToString().Length;
        }

        /// <summary>
        /// 12 byte[] - Represent In game name.
        /// Offset: 28  
        /// </summary>
        public string InGameName {
            set
            {
                if (value.Length > 12) {
                    throw new ArgumentOutOfRangeException("The value must be at range 12 less or equal");
                }

                this._InGameName = value;
            }
            get
            {
                return this._InGameName;
            }
        }

        /// <summary>
        /// 1 byte[] - Represent Character Gender.
        /// Offset: 41
        /// </summary>
        public short Gender {
            set 
            {
                if (value > 1 || value < 0) {
                    throw new ArgumentOutOfRangeException("Invalid gender!, The value must be 0 for Female, and 1 for Male");
                }

                this._Gender = value;
            }
            get 
            {
                return this._Gender;
            } 
        }

        public short Level {
            set
            {
                if (value > 100 || value < 0) {
                    throw new ArgumentOutOfRangeException("Invalid level, it must be between 1-100");
                }

                this._Level = value;
            }
            get
            {
                return this._Level;
            }
        }

        /// <summary>
        /// 2 byte[] - Represent Instrument that character use.
        /// Offset: 63
        /// </summary>
        public short Instrument {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._Instrument = value;
            }
            get
            {
                return this._Instrument;
            }
        }

        /// <summary>
        /// 2 byte[] - Represent Hair that character use.
        /// Offset: 66
        /// </summary>
        public short Hair {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._Hair = value;
            }
            get
            {
                return this._Hair;
            }
        }

        public short Accessories {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._Accessories = value;
            }
            get
            {
                return this._Accessories;
            }
        }

        /// <summary>
        /// 2 byte[] - Represent Gloves that character use.
        /// Offset: 70
        /// </summary>
        public short Gloves {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._Gloves = value;
            }
            get
            {
                return this._Gloves;
            }
        }

        public short Necklaces {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._Necklaces = value;
            }
            get
            {
                return this._Necklaces;
            }
        }

        /// <summary>
        /// 2 byte[] - Represent Shirt that character use.
        /// Offset: 74
        /// </summary>
        public short Shirt {
            set 
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._Shirt = value;
            }
            get 
            {
                return this._Shirt;
            } 
        }

        /// <summary>
        /// 2 byte[] - Reperesent Pants that character use.
        /// Offset: 78
        /// </summary>
        public short Pants {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._Pants = value;
            }
            get
            {
                return this._Pants;
            }
        }

        /// <summary>
        /// 2 byte[] - Reperesent Glasses that character use.
        /// Offset: 82
        /// </summary>
        public short Glasses {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._Glasses = value;
            }
            get
            {
                return this._Glasses;
            }
        }

        /// <summary>
        /// 2 byte[] - Reperesent Earrings that character use.
        /// Offset: 86
        /// </summary>
        public short Earrings {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._Earrings = value;
            }
            get
            {
                return this._Earrings;
            }
        }

        /// <summary>
        /// 2 byte[] - Reperesent Clothes Accessories that character use.
        /// Offset: 90
        /// </summary>
        public short ClothesAccessories {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._ClothesAccessories = value;
            }
            get
            {
                return this._ClothesAccessories;
            }
        }

        /// <summary>
        /// 2 byte[] - Reperesent Shoes that character use.
        /// Offset: 94
        /// </summary>
        public short Shoes {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._Shoes = value;
            }
            get
            {
                return this._Shoes;
            }
        }

        /// <summary>
        /// 2 byte[] - Reperesent Faces that character use.
        /// Offset: 98
        /// </summary>
        public short Faces {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._Faces = value;
            }
            get
            {
                return this._Faces;
            }
        }

        /// <summary>
        /// 2 byte[] - Reperesent Wings that character use.
        /// Offset: 102
        /// </summary>
        public short Wings {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._Wings = value;
            }
            get
            {
                return this._Wings;
            }
        }

        /// <summary>
        /// 2 byte[] - Reperesent Instrument Accessories that character use.
        /// Offset: 106
        /// </summary>
        public short InstrumentAccessories {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._InstrumentAccessories = value;
            }
            get
            {
                return this._InstrumentAccessories;
            }
        }

        /// <summary>
        /// 2 byte[] - Reperesent Pets that character use.
        /// Offset: 110
        /// </summary>
        public short Pets {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._Pets = value;
            }
            get
            {
                return this._Pets;
            }
        }

        /// <summary>
        /// 2 byte[] - Reperesent Hair Accessories that character use.
        /// Offset: 114
        /// </summary>
        public short HairAccessories {
            set
            {
                if (CountDigit(value) > 6) {
                    throw new ArgumentOutOfRangeException("The value must be at range 6 less or equal");
                }

                this._HairAccessories = value;
            }
            get
            {
                return this._HairAccessories;
            }
        }
    }
}
