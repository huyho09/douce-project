namespace ScentifyWebApp.Infrastructure.ModelConfigurations
{
    public class AppActionResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public AppActionResult()
        {
            BuildError(null);
        }

        public AppActionResult(bool success, string message = null)
        {
            Set(success, message);
        }

        #region Public Methods

        public AppActionResult BuildSuccess(string message = "Successful")
        {
            return Set(true, message);
        }

        public AppActionResult BuildError(string message)
        {
            return Set(false, message);
        }

        #endregion Public Methods

        #region Private Methods

        private AppActionResult Set(bool success, string msg = null)
        {
            IsSuccess = success;
            Message = msg;
            return this;
        }

        #endregion Private Methods
    }
}
