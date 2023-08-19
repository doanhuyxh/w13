using Framework.Caching;
using Framework.EF;
using Framework.Helper.Cache;
using System.Collections.Generic;

namespace marketplace
{
	public class BaseIpl<T>
	{
		public T unitOfWork;
		protected ICacheProvider cache;
		protected CacheHelper cacheHelper;
		protected string _schema;
        protected string _connection;
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseIpl{T}" /> class.
        /// </summary>
        /// <param name="schema">The schema.</param>
        public BaseIpl(string schema = "dbo")
		{
			_schema = schema;
			cache = new MemcachedProvider(schema);
			cacheHelper = new CacheHelper(schema);
            if (!string.IsNullOrEmpty(_connection))
                unitOfWork = (T)SingletonIpl.GetInstance<T>(schema, _connection);
            else
                unitOfWork = (T)SingletonIpl.GetInstance<T>(schema);
        }
        
    }
}
