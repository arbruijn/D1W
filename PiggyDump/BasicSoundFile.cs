﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibDescent.Data;

namespace Descent2Workshop
{
    public class BasicSoundFile
    {
        public short FormatTag { get; set; }
        public short NumChannels { get; set; }
        public int SamplesPerSec { get; set; }
        public int AvgbytesPerSec { get; set; }
        public short BlockAlign { get; set; }
        public short BitsPerSample { get; set; }
        public byte[] Data { get; set; }

        public static BasicSoundFile ReadFromStream(Stream stream)
        {
            BasicSoundFile sound = new BasicSoundFile();
            BinaryReader br = new BinaryReader(stream);

            uint containerSig = br.ReadUInt32();// = Util.MakeSig(
            if (containerSig != Util.MakeSig('R', 'I', 'F', 'F'))
                throw new InvalidDataException("File is not a RIFF container.");
            uint size = br.ReadUInt32();
            uint formatSig = br.ReadUInt32();
            if (formatSig != Util.MakeSig('W', 'A', 'V', 'E'))
                throw new InvalidDataException("File is not in WAVE format.");

            while (br.BaseStream.Position < size)
            {
                uint sig = br.ReadUInt32();
                uint length = br.ReadUInt32();
                long position = br.BaseStream.Position;

                if (sig == Util.MakeSig('f', 'm', 't', ' '))
                {
                    //TODO: Remove the four billion limitations here, make more flexible, this is why I was considering naudio originally I think.....
                    if (length < 16)
                        throw new InvalidDataException("Insufficient information in fmt header");
                    sound.FormatTag = br.ReadInt16();
                    if (sound.FormatTag != 1)
                        throw new InvalidDataException("Unsupported encoding method, only uncompressed waves are currently supported.");
                    sound.NumChannels = br.ReadInt16();
                    if (sound.NumChannels != 1 && sound.NumChannels != 2)
                        throw new InvalidDataException("Only mono or stereo sounds are supported. Preferrably mono.");
                    sound.SamplesPerSec = br.ReadInt32();
                    sound.AvgbytesPerSec = br.ReadInt32();
                    sound.BlockAlign = br.ReadInt16();
                    sound.BitsPerSample = br.ReadInt16();
                }
                else if (sig == Util.MakeSig('d', 'a', 't', 'a'))
                {
                    //Strip stereo data
                    if (sound.NumChannels != 1)
                    {
                        uint numSamples = length / (uint)sound.NumChannels;
                        sound.Data = new byte[numSamples];
                        for (uint i = 0; i < numSamples; i++)
                        {
                            sound.Data[i] = br.ReadByte();
                            br.ReadBytes((int)numSamples - 1);
                        }
                    }
                    else
                    {
                        sound.Data = br.ReadBytes((int)length);
                    }
                }

                br.BaseStream.Seek(position + length, SeekOrigin.Begin);
            }

            return sound;
        }
    }
}
