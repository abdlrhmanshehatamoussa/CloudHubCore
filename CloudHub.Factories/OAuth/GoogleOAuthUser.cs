﻿namespace CloudHub.Factories
{
    public partial class GoogleOAuthExtractor
    {
        private struct GoogleOAuthUser
        {
            public string issued_to { get; set; }
            public string audience { get; set; }
            public string user_id { get; set; }
            public string scope { get; set; }
            public int expires_in { get; set; }
            public string email { get; set; }
            public bool verified_email { get; set; }
            public string access_type { get; set; }

        }
    }
}
