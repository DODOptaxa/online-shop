using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Contractors
{
    public class Field
    {
        public string Label { get; }

        public string Name { get; }

        public string Value { get; }

        public bool Recorder { get; }


        public IReadOnlyDictionary<string, string> Items;

        // Ебать, варьяче, свухай, recorded то хуйня просто для удобства в відображенні інфи, забий хуй на нього
        public Field(string label, string name, string value , bool recorder, IReadOnlyDictionary<string, string> items)
        {
            Label = label;
            Name = name;
            Items = items;
            Recorder = recorder;

            if(!items.ContainsKey(value)) Value = items.Keys.First();

            else Value = value;

        }
        }
    }
