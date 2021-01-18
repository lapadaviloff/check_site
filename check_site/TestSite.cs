using System;
using System.Net;


namespace check_site
{
    class TestSite
    {
        public static bool Test(string url)
        {

            Uri uri = new Uri(url);

            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                httpWebResponse.Close();
            }
            catch
            {
                return false;
            }
            return true;


        }

    }
}
