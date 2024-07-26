using System.Text;
using Library.DualGlobe.ERP.Interfaces;

namespace DualGlobe.ERP.Utility
{
    public static class Helper
    {
        public static string GetClientName(int id)
        {
            string clientName = string.Empty;
            if (id != 0)
            {
                var conf = ClientInterface.Read(id);

                if (conf != null)
                    clientName = conf.CompanyName;
            }
            return clientName;
        }

        public static string GetProjectName(int id)
        {
            string projectName = string.Empty;
            if (id != 0)
            {
                var conf = ProjectInterface.Read(id);

                if (conf != null)
                    projectName = conf.ProjectName;
            }
            return projectName;
        }

        public static string GetClientAddress(int id)
        {
            string clientAddress = string.Empty;
            if (id != 0)
            {
                var conf = ClientInterface.Read(id);

                if (conf != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(conf.AddressLine1);
                    sb.Append(" ");
                    sb.Append(conf.AddressLine2);
                    sb.Append(" ");
                    sb.Append(conf.City);
                    sb.Append(" ");
                    sb.Append(conf.State);
                    sb.Append(" ");
                    sb.Append(conf.Country);
                    sb.Append(" ");
                    sb.Append(conf.Zip);

                    clientAddress = sb.ToString();
                }
            }
            return clientAddress;
        }
       

    }
}