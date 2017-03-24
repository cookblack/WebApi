using Cook.WebApi.Common.Tool;

namespace Cook.WebApi.Common
{
    /// <summary>
    /// 获取配置属性值
    /// </summary>
    public class PlatformConfig
    { 
        private static int pageSize;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public static int PageSize
        {
            get
            {
                if (pageSize < 1)
                {
                    pageSize = ConfigurationHelper.GetAppInt("PageSize", 20);
                }
                return pageSize;
            }
        }

        private static int _studioWorksPageSize;
        /// <summary>
        /// 工作室作品列表每页记录数
        /// </summary>
        public static int StudioWorksPageSize
        {
            get
            {
                if (_studioWorksPageSize < 1)
                {
                    _studioWorksPageSize = ConfigurationHelper.GetAppInt("StudioWorksPageSize", 8);
                }
                return _studioWorksPageSize;
            }
        }

        private static int _demandListPageSize;
        /// <summary>
        /// 需求动态列表每页记录数
        /// </summary>
        public static int DemandListPageSize
        {
            get
            {
                if (_demandListPageSize < 1)
                {
                    _demandListPageSize = ConfigurationHelper.GetAppInt("DemandListPageSize", 8);
                }
                return _demandListPageSize;
            }
        }

        private static int _hotDemandSize;
        /// <summary>
        /// 需求列表页右侧-热门需求记录数
        /// </summary>
        public static int HotDemandSize
        {
            get
            {
                if (_hotDemandSize < 1)
                {
                    _hotDemandSize = ConfigurationHelper.GetAppInt("HotDemandSize", 20);
                }

                return _hotDemandSize;
            }
        }

        private static int _similarDemandSize;
        /// <summary>
        /// 需求动态详细页右侧-相似需求记录数
        /// </summary>
        public static int SimilarDemandSize
        {
            get
            {
                if (_similarDemandSize < 1)
                {
                    _similarDemandSize = ConfigurationHelper.GetAppInt("SimilarDemandSize", 10);
                }

                return _similarDemandSize;
            }
        }

        private static int _similarLibrarySize;
        /// <summary>
        /// 素材详细页右侧-相似素材记录数
        /// </summary>
        public static int SimilarLibrarySize
        {
            get
            {
                if (_similarLibrarySize < 1)
                {
                    _similarLibrarySize = ConfigurationHelper.GetAppInt("SimilarLibrarySize",6);
                }

                return _similarLibrarySize;
            }
        }

        private static int _indexDesignerPageSize;
        /// <summary>
        /// 设计师，形象设计首页-推荐设计师推荐数 6
        /// </summary>
        public static int IndexDesignerPageSize
        {
            get
            {
                if (_indexDesignerPageSize < 1)
                {
                    _indexDesignerPageSize = ConfigurationHelper.GetAppInt("IndexDesignerPageSize", 6);
                }

                return _indexDesignerPageSize;
            }
        }

        private static int _indexProductPageSize;
        /// <summary>
        /// 设计师，形象设计首页-热门作品作品数 12
        /// </summary>
        public static int IndexProductPageSize
        {
            get
            {
                if (_indexProductPageSize < 1)
                {
                    _indexProductPageSize = ConfigurationHelper.GetAppInt("IndexProductPageSize", 12);
                }

                return _indexProductPageSize;
            }
        }

        private static int recentDays;
        /// <summary>
        /// 近期天数
        /// </summary>
        public static int RecentDays
        {
            get
            {
                if (recentDays < 1)
                {
                    recentDays = ConfigurationHelper.GetAppInt("RecentDays", 90);
                }
                return recentDays;
            }
        }

        private static string projectName;
        /// <summary>
        /// 项目名
        /// </summary>
        public static string ProjectName
        {
            get
            {
                if (string.IsNullOrEmpty(projectName))
                {
                    projectName = ConfigurationHelper.GetApp("ProjectName");
                }
                return projectName;
            }
        }

        #region 阿里支付配置
        private static string _partner;
        public static string partner
        {
            get
            {
                if (string.IsNullOrEmpty(_partner))
                {
                    _partner = ConfigurationHelper.GetApp("partner");
                }
                return _partner;
            }
        }

        private static string _key;
        public static string key
        {
            get
            {
                if (string.IsNullOrEmpty(_key))
                {
                    _key = ConfigurationHelper.GetApp("key");
                }
                return _key;
            }
        }

        private static string _email;
        public static string email
        {
            get
            {
                if (string.IsNullOrEmpty(_email))
                {
                    _email = ConfigurationHelper.GetApp("email");
                }
                return _email;
            }
        }

        private static string _type;
        public static string type
        {
            get
            {
                if (string.IsNullOrEmpty(_type))
                {
                    _type = ConfigurationHelper.GetApp("type");
                }
                return _type;
            }
        }

        private static string _return_url;
        public static string return_url
        {
            get
            {
                if (string.IsNullOrEmpty(_return_url))
                {
                    _return_url = ConfigurationHelper.GetApp("return_url");
                }
                return _return_url;
            }
        }

        private static string _notify_url;
        public static string notify_url
        {
            get
            {
                if (string.IsNullOrEmpty(_notify_url))
                {
                    _notify_url = ConfigurationHelper.GetApp("notify_url");
                }
                return _notify_url;
            }
        }

        #endregion
        #region 重定向登陆页面
        private static string _Login_url;
        public static string Login_url
        {
            get
            {
                if (string.IsNullOrEmpty(_Login_url))
                {
                    _Login_url = ConfigurationHelper.GetApp("LoginUrl");
                }
                return _Login_url;
            }
        }
        #endregion

        #region 各系统页面跳转
        private static string _UrlDesign;
        private static string _UrlTool;
        private static string _UrlMaker;
        private static string _UrlFactory;
        private static string _UrlShare;
        private static string _UrlStandard;
        private static string _UrlMall;
        private static string _UrlMaterial;
        private static string _UrlInformation;
        private static string _UrlUser;
        private static string _UrlIndex;
        private static string _UrlDomain;


        public static string UrlIndex
        {
            get
            {
                if (string.IsNullOrEmpty(_UrlIndex))
                {
                    _UrlIndex = ConfigurationHelper.GetApp("UrlIndex");
                }
                return _UrlIndex;
            }
        }
        public static string UrlUser
        {
            get
            {
                if (string.IsNullOrEmpty(_UrlUser))
                {
                    _UrlUser = ConfigurationHelper.GetApp("UrlUser");
                }
                return _UrlUser;
            }
        }
        public static string UrlDesign
        {
            get
            {
                if (string.IsNullOrEmpty(_UrlDesign))
                {
                    _UrlDesign = ConfigurationHelper.GetApp("UrlDesign");
                }
                return _UrlDesign;
            }
        }
        public static string UrlTool
        {
            get
            {
                if (string.IsNullOrEmpty(_UrlTool))
                {
                    _UrlTool = ConfigurationHelper.GetApp("UrlTool");
                }
                return _UrlTool;
            }
        }
        public static string UrlMaker
        {
            get
            {
                if (string.IsNullOrEmpty(_UrlMaker))
                {
                    _UrlMaker = ConfigurationHelper.GetApp("UrlMaker");
                }
                return _UrlMaker;
            }
        }

        public static string UrlFactory
        {
            get
            {
                if (string.IsNullOrEmpty(_UrlFactory))
                {
                    _UrlFactory = ConfigurationHelper.GetApp("UrlFactory");
                }
                return _UrlFactory;
            }
        }

        public static string UrlShare
        {
            get
            {
                if (string.IsNullOrEmpty(_UrlShare))
                {
                    _UrlShare = ConfigurationHelper.GetApp("UrlShare");
                }
                return _UrlShare;
            }
        }

        public static string UrlStandard
        {
            get
            {
                if (string.IsNullOrEmpty(_UrlStandard))
                {
                    _UrlStandard = ConfigurationHelper.GetApp("UrlStandard");
                }
                return _UrlStandard;
            }
        }
        public static string UrlMall
        {
            get
            {
                if (string.IsNullOrEmpty(_UrlMall))
                {
                    _UrlMall = ConfigurationHelper.GetApp("UrlMall");
                }
                return _UrlMall;
            }
        }
        public static string UrlMaterial
        {
            get
            {
                if (string.IsNullOrEmpty(_UrlMaterial))
                {
                    _UrlMaterial = ConfigurationHelper.GetApp("UrlMaterial");
                }
                return _UrlMaterial;
            }
        }

        public static string UrlInformation
        {
            get
            {
                if (string.IsNullOrEmpty(_UrlInformation))
                {
                    _UrlInformation = ConfigurationHelper.GetApp("UrlInformation");
                }
                return _UrlInformation;
            }
        }

        public static string UrlDomain
        {
            get
            {
                if (string.IsNullOrEmpty(_UrlDomain))
                {
                    _UrlDomain = ConfigurationHelper.GetApp("UrlDomain");
                }
                return _UrlDomain;
            }
        }

        #endregion
        #region 用户管理列表每页行数
        private static int _userPageSize;

        /// <summary>
        /// 每页记录数
        /// </summary>
        public static int UserPageSize
        {
            get
            {
                if (_userPageSize < 1)
                {
                    _userPageSize = ConfigurationHelper.GetAppInt("UserPageSize", 15);
                }
                return _userPageSize;
            }
        }


        private static int _worksPageSize;

        /// <summary>
        /// 客户管理-作品列表每页记录数
        /// </summary>
        public static int WorksPageSize
        {
            get
            {
                if (_worksPageSize < 1)
                {
                    _worksPageSize = ConfigurationHelper.GetAppInt("WorksPageSize", 8);
                }
                return _worksPageSize;
            }
        }

        private static int _loadLetterCount;

        /// <summary>
        /// 客户管理-私信记录一次最多加载条数
        /// </summary>
        public static int LoadLetterCount
        {
            get
            {
                if (_loadLetterCount < 1)
                {
                    _loadLetterCount = ConfigurationHelper.GetAppInt("LoadLetterCount", 20);
                }
                return  _loadLetterCount;
            }
        }

        #endregion
        private static string _contractSite;
        /// <summary>
        /// 第三方合同查看地址
        /// </summary>
        public static string ContractSite
        {
            get
            {
                if (string.IsNullOrEmpty(_contractSite))
                {
                    _contractSite = ConfigurationHelper.GetApp("ContractSite");
                }
                return _contractSite;
            }
        }
        private static string _WebRTCServers;
        /// <summary>
        /// 信令服务器地址
        /// </summary>
        public static string WebRTCServers
        {
            get
            {
                if (string.IsNullOrEmpty(_WebRTCServers))
                {
                    _WebRTCServers = ConfigurationHelper.GetApp("WebRTCServers", "ws://169.254.77.127:9210");
                }
                return _WebRTCServers;
            }
        }
        private static string _IsGenerateThumbnail;
        /// <summary>
        /// 上传时是否开启缩略图
        /// </summary>
        public static string IsGenerateThumbnail
        {
            get
            {
                if (string.IsNullOrEmpty(_IsGenerateThumbnail))
                {
                    _IsGenerateThumbnail = ConfigurationHelper.GetApp("IsGenerateThumbnail", "true");
                }
                return _IsGenerateThumbnail;
            }
        }
        private static string _Thumbnail;
        /// <summary>
        /// 设置缩略图的尺寸大小
        /// </summary>
        public static string Thumbnail
        {
            get
            {
                if (string.IsNullOrEmpty(_Thumbnail))
                {
                    _Thumbnail = ConfigurationHelper.GetApp("Thumbnail", "300,300");
                }
                return _Thumbnail;
            }
        }

        #region 推送配置
        private static string _production;
        /// <summary>
        /// 友蒙app钥匙
        /// </summary>
        public static string ProductionMode
        {
            get
            {
                if (string.IsNullOrEmpty(_production))
                {
                    _production = ConfigurationHelper.GetApp("ProductionMode");
                }
                return _production;
            }
        }

        private static string _appkeyAndroid;
        /// <summary>
        /// 友盟Android钥匙
        /// </summary>
        public static string AppkeyAndroid
        {
            get
            {
                if (string.IsNullOrEmpty(_appkeyAndroid))
                {
                    _appkeyAndroid = ConfigurationHelper.GetApp("AppkeyAndroid");
                }
                return _appkeyAndroid;
            }
        }

        private static string _masterSecretAndroid;
        /// <summary>
        /// 友盟AndroidMasterSecret钥匙
        /// </summary>
        public static string MasterSecretAndroid
        {
            get
            {
                if (string.IsNullOrEmpty(_masterSecretAndroid))
                {
                    _masterSecretAndroid = ConfigurationHelper.GetApp("MasterSecretAndroid");
                }
                return _masterSecretAndroid;
            }
        }
        private static string _appkeyIos;
        /// <summary>
        /// 友盟Android钥匙
        /// </summary>
        public static string AppkeyIos
        {
            get
            {
                if (string.IsNullOrEmpty(_appkeyIos))
                {
                    _appkeyIos = ConfigurationHelper.GetApp("AppkeyIos");
                }
                return _appkeyIos;
            }
        }

        private static string _masterSecretIos;
        /// <summary>
        /// 友盟AndroidMasterSecret钥匙
        /// </summary>
        public static string MasterSecretIos
        {
            get
            {
                if (string.IsNullOrEmpty(_masterSecretIos))
                {
                    _masterSecretIos = ConfigurationHelper.GetApp("MasterSecretIos");
                }
                return _masterSecretIos;
            }
        }

        private static string _pushWhiteList;
        /// <summary>
        /// 推送配置系统互推地址白名单
        /// </summary>
        public static string PushWhiteList
        {
            get
            {
                if (string.IsNullOrEmpty(_pushWhiteList))
                {
                    _pushWhiteList = ConfigurationHelper.GetApp("PushWhiteList");
                }
                return _pushWhiteList;
            }
        }
        #endregion

        #region SMS短信API配置
        /// <summary>
        /// sdk app id 码
        /// </summary>
        public static int SMSsdkappid
        {
            get
            {
                try
                {
                    return int.Parse(ConfigurationHelper.GetApp("SMSsdkappid"));
                }
                catch
                {
                    return 0;
                }

            }
        }
        /// <summary>
        /// appkey
        /// </summary>
        public static string SMSappkey
        {
            get
            {
                try
                {
                    return ConfigurationHelper.GetApp("SMSappkey");
                }
                catch
                {
                    return string.Empty;
                }

            }
        }
        /// <summary>
        /// 短信格式
        /// </summary>
        public static string SMSmsg
        {
            get
            {
                try
                {
                    return ConfigurationHelper.GetApp("SMSmsg");
                }
                catch
                {
                    return string.Empty;
                }

            }
        }





        #endregion







    }
}
