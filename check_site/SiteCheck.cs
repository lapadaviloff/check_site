using System;

namespace check_site
{
    /// <summary>
        /// создает обьект проверки
        /// </summary>
    class SiteCheck
    {
       // private TestSite testsite = new TestSite();

        private readonly String StringUrl;
        /// <summary>
        /// создает обьект проверки
        /// </summary>
        public SiteCheck( String url )
        {
            StringUrl = url;

        }
       
        /// <summary>
        /// возвращает сообщение о статусе сайта
        /// </summary>
        /// <returns></returns>
        public MessageAndFlag Refresh()
        {
            MessageAndFlag message = new MessageAndFlag();
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