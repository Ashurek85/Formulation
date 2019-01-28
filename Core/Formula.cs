using AgileObjects.ReadableExpressions;
using Core.Blocks.Elementals;
using Core.TypeDefinitions;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Serialization;

namespace Core
{
    public class Formula<TResult, TPrimitiveType, TDataContext> : BaseFormula, IDataContext
        where TDataContext : class
        where TPrimitiveType : BaseType<TResult>
    {

        private Block<TPrimitiveType> operations;
        public Block<TPrimitiveType> Operations
        {
            get => operations;
            set
            {
                operations = value;
                if (operations != null)
                    operations.Parent = this;
            }
        }

        private TDataContext currentDataContext;

        public TResult Calculate(TDataContext dataContext)
        {
            currentDataContext = dataContext;

            // Enter parameter with data context
            ParameterExpression paramExp = Expression.Parameter(typeof(TDataContext), "context");
            // Build internal expression
            Expression internalExpression = Operations.BuildExpression(paramExp);

            try
            {
                // Compile expression
                Func<TDataContext, TResult> compiled =
                    Expression.Lambda<Func<TDataContext, TResult>>(internalExpression, new ParameterExpression[] { paramExp }).Compile();
                return compiled.Invoke(dataContext);
            }
            catch (Exception e)
            {
                throw new Exception($"Error al calcular la fórmula\n {internalExpression.ToReadableString()}\n{e.Message}\n{e.TargetSite.ToString()}", e);
            }            
        }


        public string ReadableFormula
        {
            get
            {                
                ParameterExpression paramExp = Expression.Parameter(typeof(TDataContext), "context");
                Expression internalExpression = Operations.BuildExpression(paramExp);
                return internalExpression.ToReadableString();                
            }
        }

        /// <summary>
        /// Serializa una fórmula en XML
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        public static string SerializeFormula(Formula<TResult, TPrimitiveType, TDataContext> formula)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(Formula<TResult, TPrimitiveType, TDataContext>), BlockTypes);
            using (MemoryStream stream = new MemoryStream())
            {
                serializador.Serialize(stream, formula);
                return Encoding.UTF8.GetString(stream.GetBuffer());
            }            
        }

        public static Formula<TResult, TPrimitiveType, TDataContext> DeserializeFormula(string xml)
        {            
            XmlSerializer serializador = new XmlSerializer(typeof(Formula<TResult, TPrimitiveType, TDataContext>), BlockTypes);
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                return serializador.Deserialize(ms) as Formula<TResult, TPrimitiveType, TDataContext>;
            }            
        }

        /// <summary>
        /// Return current formula DataContext
        /// </summary>
        /// <returns></returns>
        public object GetDataContext()
        {
            return currentDataContext;
        }
    }
}
