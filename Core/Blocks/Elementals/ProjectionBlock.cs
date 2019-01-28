using Core.TypeDefinitions;

namespace Core.Blocks.Elementals
{
    public abstract class ProjectionBlock<TPrimitiveType> : Block<TPrimitiveType>
        where TPrimitiveType : PrimitiveType
    {

        public ProjectionBlock()
        {

        }

        public ProjectionBlock(string selectedProperty)
        {
            SelectedProperty = selectedProperty;
        }

        public string SelectedProperty { get; set; }
    }
}
