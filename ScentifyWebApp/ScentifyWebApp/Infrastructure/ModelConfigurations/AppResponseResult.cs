namespace ScentifyWebApp.Infrastructure.ModelConfigurations
{
    public class AppResponseResult<TData, TDetail> where TDetail : class
    {
        #region Members

        public bool IsSuccess { get; set; }

        public TData Data { get; set; }

        public TDetail Detail { get; set; }

        #endregion Members

        #region Contructors

        public AppResponseResult()
        {
            IsSuccess = false;
            Data = default;
            Detail = default;
        }

        /// <summary>Initializes a new instance for build success result</summary>
        /// <param name="data">The data.</param>
        public AppResponseResult(TData data)
        {
            BuildSuccess(data);
        }

        #endregion Contructors

        #region Functions

        public AppResponseResult<TData, TDetail> BuildSuccess(TData data, TDetail detail = null)
        {
            SetInfoResult(true, detail);
            Data = data;
            return this;
        }

        public AppResponseResult<TData, TDetail> BuildError(TDetail error)
        {
            SetInfoResult(false, error);
            return this;
        }

        protected AppResponseResult<TData, TDetail> SetInfoResult(bool signal, TDetail detail = null)
        {
            IsSuccess = signal;
            Detail = detail;
            return this;
        }

        #endregion Functions
    }

    public class AppResponseResult<TData> : AppResponseResult<TData, string>
    {
        #region Contructors

        public AppResponseResult() : base()
        {
        }

        public AppResponseResult(TData data) : base(data)
        {
        }

        #endregion Contructors

        #region Functions

        public new AppResponseResult<TData> BuildSuccess(TData data, string detail = "")
        {
            SetInfoResult(true, detail);
            Data = data;
            return this;
        }

        public AppResponseResult<TData> BuildError(TData data, string error)
        {
            SetInfoResult(false, error);
            Data = data;
            return this;
        }

        public new AppResponseResult<TData> BuildError(string error)
        {
            SetInfoResult(false, error);
            return this;
        }

        #endregion Functions
    }
}
