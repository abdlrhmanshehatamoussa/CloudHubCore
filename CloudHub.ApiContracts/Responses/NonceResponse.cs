namespace CloudHub.ApiContracts
{
    public struct NonceResponse
    {
        public string token { get; set; }
        public string created_on { get; set; }
    }

    public struct EmptyResponse { }
}
