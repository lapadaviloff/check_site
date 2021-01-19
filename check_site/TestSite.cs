using System;
using System.Net;


namespace check_site
{/// <summary>
 /// класс проверки сайта на доступность
 /// </summary>
    class TestSite
    {
        /// <summary>
        /// функция проверки сайта на доступность. false - не доступен
        /// "url"  адрес сайта
        /// </summary>
        /// <param name="url">адрес сайта </param>
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
