using Core.Blocks.Elementals;
using System;

namespace Core
{
    public class ParameterDefinition
    {
        public string Name { get; set; }

        public BaseBlock Block { get; set; }

        public Type BlockType { get; set; }
    }
}
