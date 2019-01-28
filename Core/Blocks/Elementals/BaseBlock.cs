using System.Linq.Expressions;
using System.Xml.Serialization;

namespace Core.Blocks.Elementals
{
    public abstract class BaseBlock : IDataContext
    {
        [XmlIgnore]
        public IDataContext Parent { get; set; }

        public object GetDataContext()
        {
            if (Parent != null)
                return Parent.GetDataContext();
            return null; // lost hierarchy DataContext
        }

        public abstract Expression BuildExpression(ParameterExpression paramExpression);
    }
}
