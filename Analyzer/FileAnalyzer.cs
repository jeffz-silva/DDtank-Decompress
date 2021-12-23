using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SplushTools.Analyzer
{
    public class FileAnalyzer
    {
        protected byte[] m_buffer;
        protected int m_length;
        protected int m_offset;

        public FileAnalyzer(byte[] buffer)
        {
            this.m_buffer = buffer; //Dados
            this.m_length = 8192; //Alocação máxima de memoria
            this.m_offset = 0; //Deslocamento na memoria
        }

        //Ler um byte
        public virtual byte ReadByte()
        {
            return this.m_buffer[this.m_offset++];
        }

        //Ler um short
        public virtual short ReadShort()
        {
            return ConvertToInt16(this.ReadByte(), this.ReadByte());
        }

        //Ler uma string
        public virtual string ReadString()
        {
            short shortValue = this.ReadShort();
            string str = Encoding.UTF8.GetString(this.m_buffer, this.m_offset, (int)shortValue);
            this.m_offset += (int)shortValue;
            return str.Replace("\0", "");
        }


        //Ler um inteiro
        public virtual int ReadInt()
        {
            int location1 = (int)this.ReadByte();
            byte location2 = this.ReadByte();
            byte location3 = this.ReadByte();
            byte location4 = this.ReadByte();
            int location5 = (int)location2;
            int location6 = (int)location3;
            int location7 = (int)location4;
            return ConvertToInt32((byte)location1, (byte)location5, (byte)location6, (byte)location7);
        }

        //Conversão inteiro 32 (2.147.483.648)
        public static int ConvertToInt32(byte[] val, int startIndex)
        {
            return ConvertToInt32(val[startIndex], val[startIndex + 1], val[startIndex + 2], val[startIndex + 3]);
        }

        //Conversão inteiro 32 (2.147.483.648)
        public static int ConvertToInt32(byte v1, byte v2, byte v3, byte v4)
        {
            return (int)v1 << 24 | (int)v2 << 16 | (int)v3 << 8 | (int)v4;
        }

        //Conversão inteiro 16 (32767)
        public static short ConvertToInt16(byte v1, byte v2)
        {
            return (short)((int)v1 << 8 | (int)v2);
        }

        //Conversão inteiro 16 (32767)
        public static short ConvertToInt16(byte[] val, int startIndex)
        {
            return ConvertToInt16(val[startIndex], val[startIndex + 1]);
        }

        //Ler um boolean
        public virtual bool ReadBoolean()
        {
            return this.m_buffer[this.m_offset++] > (byte)0;
        }

        //Ler uma data
        public DateTime ReadDateTime()
        {
            return new DateTime((int)this.ReadShort(), (int)this.ReadByte(), (int)this.ReadByte(), (int)this.ReadByte(), (int)this.ReadByte(), (int)this.ReadByte());
        }

        //Ler uma data string
        public string ReadDateTimeString()
        {
            short location1 = this.ReadShort();
            byte location3 = this.ReadByte();
            byte location4 = this.ReadByte();
            byte location5 = this.ReadByte();
            byte location2 = (byte)(this.ReadByte() - 1);
            byte location6 = this.ReadByte();

            string dateString = ((((((((((location1 + "-") + location2) + "-") + location3) + "T") + location4) + ":") + location5) + ":") + location6);


            return dateString;
        }
    }
}
