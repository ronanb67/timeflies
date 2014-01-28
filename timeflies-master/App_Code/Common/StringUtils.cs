using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace TimeFlies.Common
{
  /// <summary>
  /// Helpers method for string and chars manipulation.
  /// </summary>
  public static class StringUtils
  {
    /// <summary>
    /// Indicates whether a integer is legal XML character.
    /// </summary>
    /// <param name="c">The integer to evaluate.</param>
    /// <returns>True if c is a legal XML character; otherwise, false..</returns>
    private static bool IsLegalXmlCharacter(int c)
    {
      // Legal XML characters
      //Char ::= #x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD] | [#x10000-#x10FFFF] 
      if (
          ((c >= 0x0) && (c < 0x9)) ||
          ((c > 0xA) && (c < 0xD)) ||
          ((c > 0xD) && (c < 0x20)) ||
          ((c > 0xD7FF) && (c < 0xE000)) ||
          ((c > 0xFFFD) && (c < 0x10000)) ||
          (c > 0x10FFFF)
         )
      {
        return false;
      }
      else
        return true;
    }

    /// <summary>
    /// Returns the text from input string by specified length. 
    /// </summary>
    /// <param name="text">The text string to evaluate.</param>
    /// <param name="length">Length of the string to evaluate.</param>
    /// <returns>Part of the input string.</returns>
    public static string ExtractString(string text, int length)
    {
      if (length < 1)
        throw new ArgumentOutOfRangeException();

      if (text.Length <= length)
        return text;

      int splitInterval = length / 10;

      int index = text.LastIndexOfAny(new char[] { '.', ' ', ',', '!', '?' },
        length - splitInterval, splitInterval);

      if (index != -1)
        return text.Substring(0, index).Trim();
      else
        return text.Substring(0, length).Trim();
    }

    /// <summary>
    /// Returns a string where all alphabetic characters have been converted to uppercase.
    /// </summary>
    /// <param name="input">The string to uppercase.</param>
    /// <returns>Uppercased string.</returns>
    public static string ToTitleCase(string input)
    {
      CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
      TextInfo textInfo = cultureInfo.TextInfo;

      return textInfo.ToTitleCase(input.ToLower());
    }

    /// <summary>
    /// Returns a string with first char in uppercase
    /// </summary>
    /// <param name="input">The string to uppercase first char.</param>
    /// <returns>Uppercased first char string.</returns>
    public static string ToSentenceCase(string input)
    {
      if (string.IsNullOrEmpty(input))
        return input;

      return Char.ToUpper(input[0]) + input.Substring(1);
    }

    /// <summary>
    /// Removes all leading and trailing white-space characters.
    /// </summary>
    /// <param name="s">The string to evaluate.</param>
    /// <returns>The string that remains after all white-space characters are removed from the start and end of the input string.</returns>
    public static string TrimExcessiveWhitespaces(string s)
    {
      if (s == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder(s.Length);

      bool firstNotWSOccured = false;
      int? lastNotWSIndex = null;
      char? prev = null;
      char c;
      char space = ' ';
      int length;

      for (int i = 0; i < s.Length; i++)
      {
        if (!IsLegalXmlCharacter(s[i]))
          continue;

        if (Char.IsWhiteSpace(s[i]))
        {
          switch (s[i])
          {
            case '\r':
              {
                c = '\n';
                break;
              }
            case '\t':
              {
                c = space;
                break;
              }
            default:
              {
                c = s[i];
                break;
              }
          }
          if (c == prev)
          {
            // skip
          }
          else
          {
            if ((c == space) & (prev == '\n'))
            {
              // skip
            }
            else
            {
              if (c == '\n')
                if (prev == space)//except space before line transfer
                {
                  length = sb.Length;
                  if (length > 0)
                  {
                    sb.Remove(length - 1, 1);
                  }
                }
              if (firstNotWSOccured)
                sb.Append(c);
            }
          }
        }
        else
        {
          c = s[i];
          sb.Append(c);
          lastNotWSIndex = sb.Length - 1;
          firstNotWSOccured = true;
        }
        prev = c;
      }

      if (lastNotWSIndex.HasValue && (lastNotWSIndex.Value != (sb.Length - 1)))
      {
        sb.Remove(lastNotWSIndex.Value + 1, sb.Length - (lastNotWSIndex.Value + 1));
      }

      return sb.ToString();
    }

    /// <summary>
    /// Truncates the string to the new length.
    /// </summary>
    /// <param name="s">Input string.</param>
    /// <param name="length">The new length of the string.</param>
    /// <param name="replaceNullByEmpty">Indicates where null char must be replaced to empty string.</param>
    /// <returns>Truncated string.</returns>
    public static string Truncate(string s, int length, bool replaceNullByEmpty)
    {
      if (s == null)
      {
        if (replaceNullByEmpty)
          return String.Empty;
        else
          return null;
      }

      s = s.Trim();

      if (s.Length <= length)
        return s;
      else
        return s.Substring(0, length);
    }

    /// <summary>
    /// Converts the value string to its equivalent String representation consisting of base 64 digits.
    /// </summary>
    /// <param name="data">The string containing the data to be decoded.</param>
    /// <returns>Base64 encoded string.</returns>
    public static string Base64Encode(string data)
    {
      byte[] encDataByte = Encoding.UTF8.GetBytes(data);
      return Convert.ToBase64String(encDataByte);
    }

    /// <summary>
    /// Decodes a base64 string
    /// </summary>
    /// <param name="data">A string that is base64 encoded according to RFC 4648.</param>
    /// <returns>A String containing the results of decoding the specified input string.</returns>
    public static string Base64Decode(string data)
    {
      byte[] bytes = Convert.FromBase64String(data);
      return new UTF8Encoding().GetString(bytes);
      /*
      UTF8Encoding encoder = new UTF8Encoding();
      Decoder utf8Decode = encoder.GetDecoder();

      byte[] todecodeByte = Convert.FromBase64String(data);
      int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
      char[] decodedChar = new char[charCount];
      utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
      return new String(decodedChar);*/
    }

    /// <summary>
    /// Return a string representation of the input argument as hex string. 
    /// </summary>
    /// <param name="data">Input string.</param>
    /// <returns>The input argument as hex string.</returns>
    public static string ToHexString(string data)
    {
      if (String.IsNullOrEmpty(data))
        return data;
      StringBuilder sb = new StringBuilder(data.Length);

      foreach (char c in data)
      {
        sb.AppendFormat("{0:x2}", (byte)c);
      }
      return sb.ToString();
    }

    /// <summary>
    /// Return a string representation of the input hex argument .
    /// </summary>
    /// <param name="data">Input string with hex data.</param>
    /// <returns>The input argument as string.</returns>
    public static string FromHexString(string data)
    {
      if (String.IsNullOrEmpty(data))
        return data;

      StringBuilder sb = new StringBuilder(data.Length / 2);

      for (int i = 0; i < data.Length; i += 2)
      {
        int n = Convert.ToInt32(data.Substring(i, 2), 16);
        sb.Append((char)n);
      }
      return sb.ToString();
    }
  }
}
