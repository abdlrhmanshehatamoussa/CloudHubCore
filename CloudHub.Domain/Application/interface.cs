namespace CloudHub.Domain
{
    public interface IApplication
    {
        int ID { get; set; }

        string Name { get; set; }

        Guid GUID { get; set; }

        DateTime ModifiedOn { get; set; }


        DateTime CreatedOn { get; set; }

        bool Active { get; set; }

        //List<IAction> Actions { get; set; }


        //List<IApplicationSecret> ApplicationSecret { get; set; }

        //List<IFeature> Features { get; set; }

        //List<INonce> Nonces { get; set; }

        List<IUser> Users { get; set; }
    }
}
