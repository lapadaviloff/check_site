using System;

namespace check_site
{
    
    class SiteCheck
    {
       // private TestSite testsite = new TestSite();

        private readonly String StringUrl;
        public SiteCheck( String url )
        {
            StringUrl = url;

        }
       
        /// <summary>
        /// возвращает статус сайта
        /// </summary>
        /// <returns></returns>
        public messageAndFlag Refresh()
        {
            messageAndFlag message = new messageAndFlag();
            if (TestSite.Test(StringUrl))
            {
                message.message= "all correct";
                message.flag = true;
            }
            else
            {
                message.message= "not work";
                message.flag = false;
            }
            return message;
        }

    }
}