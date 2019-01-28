using Core.Introspection;
using System;
using System.Linq;

namespace Core
{
    public abstract class BaseFormula
    {

        private static Type[] blockTypes;

        public static Type[] BlockTypes
        {
            get
            {
                if (blockTypes == null)
                    blockTypes = BlockLocator.GetBlockImplementations().ToArray();
                return blockTypes;
            }
        }

    }
}
