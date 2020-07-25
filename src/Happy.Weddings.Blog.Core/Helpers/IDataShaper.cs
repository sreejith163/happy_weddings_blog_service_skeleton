using System.Collections.Generic;

namespace Happy.Weddings.Blog.Core.Helpers
{
	/// <summary>
	/// Interface for shaping the data based on provided keywords
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IDataShaper<T>
	{
        /// <summary>
        /// Shapes the data.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="fieldsString">The fields string.</param>
        /// <returns></returns>
        IEnumerable<Domain.Entity> ShapeData(IEnumerable<T> entities, string fieldsString);

        /// <summary>
        /// Shapes the data.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="fieldsString">The fields string.</param>
        /// <returns></returns>
        Domain.Entity ShapeData(T entity, string fieldsString);
	}
}
