// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.6.2

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;




namespace jsunchatbot2019correct.Bots
{



    public static class StringExtensions
    {





        public static bool Contains(this String str, String substring,
                                    StringComparison comp)
        {
            if (substring == null)
                throw new ArgumentNullException("substring",
                                             "substring cannot be null.");
            else if (!Enum.IsDefined(typeof(StringComparison), comp))
                throw new ArgumentException("comp is not a member of StringComparison",
                                         "comp");

            return str.IndexOf(substring, comp) >= 0;
        }


        internal static bool Contains(string na)
        {
            throw new NotImplementedException();
        }
    }



   




    public class EchoBot : ActivityHandler
    {


        //說明字串

        public string stockDayTrade = "當沖";
        public string stockDayTradeinformation = "當沖說明";
        public string Settlement = "交割";
        public string Settlementinformation = "交割說明";
        public string nomoney = "違約交割";
        public string nomoneyinformation = "違約交割說明";
        public string marginTrading = "信用交易";
        public string marginTradinginformation = "信用交易說明";

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {

            //當沖
            if (StringExtensions.Contains(turnContext.Activity.Text, stockDayTrade, 0))
            {
                await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak(stockDayTradeinformation), cancellationToken);
            }

            //違約交割
            if (StringExtensions.Contains(turnContext.Activity.Text, nomoney, 0))
            {
                await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak(nomoneyinformation), cancellationToken);
            }
            else
            {
                //交割
                if (StringExtensions.Contains(turnContext.Activity.Text, Settlement, 0))
                {
                    await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak(Settlementinformation), cancellationToken);
                }
            }

            //信用交易
            if (StringExtensions.Contains(turnContext.Activity.Text, marginTrading, 0))
            {
                await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak(marginTradinginformation), cancellationToken);


            }


            try
            {
                if (System.Convert.ToInt32(turnContext.Activity.Text)!=0) {
                    await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak("https://goodinfo.tw/StockInfo/StockDetail.asp?STOCK_ID="+ turnContext.Activity.Text), cancellationToken);



                }
            }
            catch (FormatException)
            {
                // the FormatException is thrown when the string text does 
                // not represent a valid integer.
            }
            catch (OverflowException)
            {
                // the OverflowException is thrown when the string is a valid integer, 
                // but is too large for a 32 bit integer.  Use Convert.ToInt64 in
                // this case.
            }













            /*  switch (turnContext.Activity.Text) {
                  case StringExtensions.Contains():
                      break;




              }
              */

            await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak($"Echo: {turnContext.Activity.Text}"), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak($"您好，歡迎來到證券E起來，歡迎提問股票相關的基礎問題，如:如何當沖、什麼是信用交易\n若連接證券帳號，我們還能提供預成交回報，讓您及時修正賣錯或買錯唷!"), cancellationToken);
                }
            }
        }

        private IActivity CreateActivityWithTextAndSpeak(string message)
        {
            var activity = MessageFactory.Text(message);
            string speak = @"<speak version='1.0' xmlns='https://www.w3.org/2001/10/synthesis' xml:lang='en-US'>
              <voice name='Microsoft Server Speech Text to Speech Voice (en-US, JessaRUS)'>" +
              $"{message}" + "</voice></speak>";
            activity.Speak = speak;
            return activity;
        }
    }
}
