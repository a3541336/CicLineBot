using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CicLineBot.Controllers
{
    public class SimpleWebHookController : isRock.LineBot.LineWebHookControllerBase
    {
        [Route("api/WebHookController")]
        [HttpPost]
        public ActionResult POST()
        {
            try
            {

                //設定ChannelAccessToken(或不設定直接抓取Web.Config中key為ChannelAccessToken的AppSetting)
                this.ChannelAccessToken = "XKE/FntxppTHMzkqRrn+/SicWUG7v8Z13BC1SoczkyziIbaSfWhEQOsImKRy+RGYV/4GwZ69EYWRe8xcSobFDwsOaco9Djx+A8mMNaO9q6iYWusIlx+/SbsKFRKuVzKnJEmUIOShv7JIegHlPuhkgwdB04t89/1O/w1cDnyilFU=";
                //取得Line Event
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                var actions = new List<isRock.LineBot.TemplateActionBase>();
                var aboutUs = @"香港商中檢實業有限公司臺灣分公司是中國國家質量監督檢驗檢疫總局駐香港的窗口公司――中國檢驗有限公司在臺灣設立的分公司。
中國檢驗有限公司在臺灣開展業務已有十餘年。自1997年起，中檢公司就通過合作在臺灣開展臺灣輸往大陸廢物原料的裝運前檢驗，2007年又開展了舊機電的裝運前檢驗。由於受兩岸關係的影響，在台設立機構的努力一直未能成功。
隨著近年來兩岸關係的改善，2009年4月臺灣向陸資開放了技術檢測和分析服務業，中國檢驗有限公司馬上著手籌組台灣分公司，於2010年9月7日正式在台註冊成立了香港商中檢實業有限公司臺灣分公司（由於註冊名稱重複，不能使用中國檢驗有限公司台灣分公司的名號），統一編號：29181607。
中國檢驗有限公司在台灣設立分公司是為了適應兩岸三地經貿發展新形勢，加強進出口商品質量把關，拓展檢驗檢疫和檢測認證領域合作，繁榮兩岸三地經濟的需要。中國國家質量監督檢驗檢疫總局對此十分重視，2011年3月24日，總局孫大偉副局長親自出席了在廈門舉行的中國檢驗有限公司台灣分公司揭牌儀式，與中國檢驗有限公司于樺董事長一起共同為臺灣分公司揭牌。
香港商中檢實業有限公司臺灣分公司（中國檢驗有限公司台灣分公司）將本著設立初衷，服務於兩岸三地經貿發展需要，發揮積極的作用。";
                actions.Add(new isRock.LineBot.UriAction()
                { label = "1129 第二百二十二期", uri = new Uri("http://www.cictw.com.tw/upload/pdf/222.pdf") });
                actions.Add(new isRock.LineBot.UriAction()
                { label = "1130 第二百二十三期", uri = new Uri("http://www.cictw.com.tw/upload/pdf/223.pdf") });
                actions.Add(new isRock.LineBot.UriAction()
                { label = "1201 第二百二十四期", uri = new Uri("http://www.cictw.com.tw/upload/pdf/224.pdf") });
                actions.Add(new isRock.LineBot.UriAction()
                { label = "查看更多", uri = new Uri("http://www.cictw.com.tw/Big5/ReadNews.aspx?id=A3E3851DEDDD8860") });
                var ButtonTemplate = new isRock.LineBot.ButtonsTemplate()
                {
                    altText = "請至手機觀看新型冠状病毒肺炎近期報導",
                    text = "請點選下列想看的期數",
                    title = "新型冠状病毒肺炎近期報導",
                    //設定圖片
                    thumbnailImageUrl = new Uri("https://i.imgur.com/appcyEW.png"),
                    actions = actions //設定回覆動作
                };
                if (LineEvent.replyToken == "00000000000000000000000000000000") return Ok();
                //準備回覆訊息
                if (LineEvent.type.ToLower() == "message" && LineEvent.message.type == "text")
                    if (LineEvent.message.text == "新冠肺炎報導")
                        this.ReplyMessage(LineEvent.replyToken, ButtonTemplate);
                    else if (LineEvent.message.text == "關於我們")
                        this.ReplyMessage(LineEvent.replyToken, aboutUs);
                    else
                        this.ReplyMessage(LineEvent.replyToken, "我不懂你在說啥");
                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                //回覆訊息
                this.PushMessage("!!!改成你的AdminUserId!!!", "發生錯誤:\n" + ex.Message);
                //response OK
                return Ok();
            }
        }
    }
}
