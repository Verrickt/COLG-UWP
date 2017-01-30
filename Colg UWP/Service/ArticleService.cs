using Colg_UWP.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colg_UWP.Service
{
    public class ArticleService:ApiBaseService
    {
        public static async Task<ArticleContainer> GetArticleContainerAsync(string articleKindID)
        {
            var articleJObject = await GetJson(ApiUrl.ArticleList(articleKindID, 1)).ConfigureAwait(false);
            int maxCount = Int32.Parse(articleJObject["INFO"]["count"].ToString());
            return new ArticleContainer { Id = articleKindID, MaxCount = maxCount };
        }
        public static async Task<(int,List<Article>)> GetArticlesAsync(string aid, int page)
        {
            var json = await GetJson(ApiUrl.ArticleList(aid, page)).ConfigureAwait(false);
            var newsArray = json["DATA"].ToArray();
            var items =  GetArticlesFromArrayAsync(newsArray);
            int newMaxCount = Int32.Parse(json["INFO"]["count"].ToString());
            return (newMaxCount, items);
        }
        public static async Task LoadArticelContentAsync(Article article)
        {
            var json = await GetJson(ApiUrl.ArticleContent(article.Id));
            article.Content = json["DATA"]["content"].ToString();
            article.Author = json["DATA"]["forum_user_info"]["username"].ToString();
        }

        private static List<Article> GetArticlesFromArrayAsync(JToken[] array)
        {
            List<Article> news = new List<Article>();
            foreach (var articleObj in array)
            {
                string article = articleObj["article_id"].ToString();
                string title = articleObj["title"].ToString();
                int comments = Int32.Parse(articleObj["comment_nums"].ToString());
                string remark = articleObj["remark"].ToString();
                var date =
                    Helper.StringToDateTime(articleObj["create_time"].ToString());

                string imageUri = articleObj.ValueForEitherName("image", "image_url");

                imageUri = new String(imageUri.TakeWhile(x => x != '&').ToArray());

                news.Add(new Article
                {
                    Id = article,
                    Date = date,
                    Title = title,
                    Image = imageUri,
                    Comments = comments,
                    Remark = remark
                }
                    );
            }
            return news;
        }

    }
}
