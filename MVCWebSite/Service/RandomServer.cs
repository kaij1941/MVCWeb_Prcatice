using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebSite.Service
{
    public class RandomServer
    {
        /// <summary>
        ///亂碼產生 
        /// </summary>
        /// <param name="WordLength">產生亂碼的字數</param>
        /// <returns></returns>
        public static string RandomWord(int WordLength=1) 
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            string[] words = allChar.Split(',');
            string Word = "";
            Random rnd = new Random();
            List<int> list1 = new List<int>();
            do
            {
                int number = rnd.Next(0, words.Length);
                Word += words[number];
            } while (Word.Length<WordLength);
            return Word;
        }

    }
}