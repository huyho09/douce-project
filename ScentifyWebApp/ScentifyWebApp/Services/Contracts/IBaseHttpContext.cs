namespace ScentifyWebApp.Services.Contracts
{
    public interface IBaseHttpContext
    {
        ///// <summary>
        ///// Gets the session.
        ///// </summary>
        ///// <typeparam name="TData">The type of the data.</typeparam>
        ///// <param name="key">The key.</param>
        ///// <returns></returns>
        //TData GetSession<TData>(string key)
        //    where TData : class;

        ///// <summary>
        ///// Sets the session.
        ///// </summary>
        ///// <param name="key">The key.</param>
        ///// <param name="data">The data.</param>
        //void SetSession(string key, object data);

        /// <summary>
        /// Clears all session.
        /// </summary>
        void ClearAllSession();

        /// <summary>
        /// Removes the session.
        /// </summary>
        /// <param name="keys">The keys.</param>
        void RemoveSession(params string[] keys);

        /// <summary>
        /// HTTPs the context.
        /// </summary>
        /// <returns></returns>
        HttpContext HttpContext();
    }
}