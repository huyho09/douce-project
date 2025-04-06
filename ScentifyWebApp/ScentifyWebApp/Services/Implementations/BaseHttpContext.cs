using ScentifyWebApp.Helper;
using ScentifyWebApp.Services.Contracts;

namespace ScentifyWebApp.Services.Implementations
{
    public class BaseHttpContext : IBaseHttpContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseHttpContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext HttpCurrent
        {
            get
            {
                return _httpContextAccessor.HttpContext;
            }
        }

        public ISession HttpSession
        {
            get
            {
                return HttpCurrent.Session;
            }
        }

        public TData GetSession<TData>(string key)
            where TData : class
        {
            object sessionObj = HttpSession?.GetObjectFromJson<TData>(key);
            if (sessionObj != null)
            {
                TData value = (TData)sessionObj;
                return value;
            }

            return default;
        }

        public void SetSession(string key, object data)
        {
            HttpSession.SetObjectAsJson(key, data);
        }

        public void ClearAllSession()
        {
            HttpSession.Clear();
        }

        public void RemoveSession(params string[] keys)
        {
            if (keys?.Count() > 0)
            {
                foreach (var key in keys)
                {
                    HttpSession.Remove(key);
                }
            }
        }

        public HttpContext HttpContext()
        {
            return HttpCurrent;
        }
    }
}