using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;

namespace GTAVTrainer.Net.Data
{
    public class Data
    {
        private readonly OrderedDictionary _dictionary = new OrderedDictionary();

        public Data()
        {
        }

        public Data(string data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                var c1 = data[i];
                if (c1 != ' ' && c1 != '(' && c1 != ')' && c1 != ',' && c1 != '=')
                {
                    for (int j = i + 1, p = 0; j < data.Length; j++)
                    {
                        var c2 = data[j];
                        if (c2 == ',' || j == data.Length - 1)
                        {
                            if (p == 0)
                            {
                                var match = Regex.Match(data.Substring(i, j - i), "([a-z_0-9]+) = (.+)");
                                _dictionary.Add(match.Groups[1].Value, match.Groups[2].Value);

                                i = j;
                                break;
                            }
                        }
                        else if (c2 == '(')
                        {
                            p++;
                        }
                        else if (c2 == ')')
                        {
                            p--;
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("(");

            foreach (DictionaryEntry entry in _dictionary)
            {
                if (stringBuilder.Length > 1)
                {
                    stringBuilder.Append(", ");
                }

                stringBuilder.Append(entry.Key)
                    .Append(" = ")
                    .Append(entry.Value);
            }

            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }

        public Data AddBool(string key, bool value)
        {
            _dictionary.Add(key, value.ToString().ToLower());

            return this;
        }

        public bool GetBool(string key)
        {
            return bool.Parse((string) _dictionary[key]);
        }

        public Data AddByte(string key, byte value)
        {
            _dictionary.Add(key, value.ToString());

            return this;
        }

        public byte GetByte(string key)
        {
            return byte.Parse((string) _dictionary[key]);
        }

        public Data AddChar(string key, char value)
        {
            _dictionary.Add(key, value.ToString());

            return this;
        }

        public char GetChar(string key)
        {
            return char.Parse((string) _dictionary[key]);
        }

        public Data AddShort(string key, short value)
        {
            _dictionary.Add(key, value.ToString());

            return this;
        }

        public short GetShort(string key)
        {
            return short.Parse((string) _dictionary[key]);
        }

        public Data AddInt(string key, int value)
        {
            _dictionary.Add(key, value.ToString());

            return this;
        }

        public int GetInt(string key)
        {
            return int.Parse((string) _dictionary[key]);
        }

        public Data AddLong(string key, long value)
        {
            _dictionary.Add(key, value.ToString());

            return this;
        }

        public long GetLong(string key)
        {
            return long.Parse((string) _dictionary[key]);
        }

        public Data AddFloat(string key, float value)
        {
            _dictionary.Add(key, value.ToString());

            return this;
        }

        public float GetFloat(string key)
        {
            return float.Parse((string) _dictionary[key]);
        }

        public Data AddDouble(string key, double value)
        {
            _dictionary.Add(key, value.ToString());

            return this;
        }

        public double GetDouble(string key)
        {
            return double.Parse((string) _dictionary[key]);
        }

        public Data AddString(string key, string value)
        {
            _dictionary.Add(key, value);

            return this;
        }

        public string GetString(string key)
        {
            return (string) _dictionary[key];
        }

        public Data AddData(string key, Data value)
        {
            _dictionary.Add(key, value.ToString());

            return this;
        }

        public Data GetData(string key)
        {
            return new Data((string) _dictionary[key]);
        }

        public bool Contains(string key)
        {
            return _dictionary.Contains(key);
        }
    }
}