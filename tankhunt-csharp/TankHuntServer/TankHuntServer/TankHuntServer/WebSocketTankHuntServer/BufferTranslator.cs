using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TankHuntServer.WebSocketTankHuntServer
{
    public static class BufferTranslator
    {
        public static Regex handshakeCheckRegex = new Regex("^GET");



		public static Byte[] GetHandshakeResponse(string data)
        {
            return Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + Environment.NewLine
                        + "Connection: Upgrade" + Environment.NewLine
                        + "Upgrade: websocket" + Environment.NewLine
                        + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
                            SHA1.Create().ComputeHash(
                                Encoding.UTF8.GetBytes(
                                    new Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                                )
                            )
                        ) + Environment.NewLine
                        + Environment.NewLine);
        }

		public static string DecodeIncomingRawData(byte[] allData)
        {
			byte rawDataStartIndex = 6;
            int secondByte = allData[1] & 127; // Remove first bit that is always 1 and means message is encrypted
			if (secondByte == 126)
                rawDataStartIndex = 8;
			else if (secondByte == 127)
                rawDataStartIndex = 14;

			// Start decoding
            byte[] mask = new byte[] { allData[rawDataStartIndex - 4], allData[rawDataStartIndex - 3], allData[rawDataStartIndex - 2], allData[rawDataStartIndex - 1] };
            byte[] decoded = new byte[allData.Length - rawDataStartIndex];

            for (int i = rawDataStartIndex, b = 0; b < decoded.Length; i++, b++)
            {
                decoded[b] = (byte)(allData[i] ^ (mask[b % 4]));
            }

            return Encoding.UTF8.GetString(decoded);
        }

		public static byte[] GetSendableDataFromRawData(string rawData)
        {
            byte[] byteRawData = Encoding.UTF8.GetBytes(rawData);
            int outgoingDataSize = byteRawData.Length + 2;
			byte[] outgoingData;
            int rawDataStartIndex = 2;

			if (65535 > byteRawData.Length && byteRawData.Length > 125) // Need two additional bytes
            {
                outgoingDataSize += 2;
				outgoingData = new byte[outgoingDataSize];
                outgoingData[1] = 126;
                byte[] lengthBytes = BitConverter.GetBytes(byteRawData.Length);
                outgoingData[2] = lengthBytes[1];
                outgoingData[3] = lengthBytes[0];
                rawDataStartIndex += 2;
            }
			else if (byteRawData.Length >= 65535) // Need 8 additional bytes
            {
                outgoingDataSize += 8;
                outgoingData = new byte[outgoingDataSize];
                outgoingData[1] = 127;
                byte[] lengthBytes = BitConverter.GetBytes(byteRawData.Length);

                for (int i = 0; i < 8; i++)
                {
                    if (lengthBytes.Length < 8 - i)
                    {
                        outgoingData[i + 2] = 0;
                        continue;
                    }
                    outgoingData[i + 2] = lengthBytes[lengthBytes.Length - (8 - i)];
                }
                rawDataStartIndex += 8;
            }
            else // Need 0 additional bytes
            {
                outgoingData = new byte[outgoingDataSize];
                outgoingData[1] = (byte)byteRawData.Length;
            }

            outgoingData[0] = 129;
            for (int i = 0; i < byteRawData.Length; i++)
            {
               outgoingData[rawDataStartIndex + i] = byteRawData[i];
            }

            return outgoingData;
			  

        }
	

    }
}
