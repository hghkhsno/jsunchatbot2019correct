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


        //�����r��

        public string stockDayTrade = "��R";
        public string stockDayTradeinformation = "��R����";
        public string Settlement = "���";
        public string Settlementinformation = "��λ���";
        public string nomoney = "�H�����";
        public string nomoneyinformation = "�H����λ���";
        public string marginTrading = "�H�Υ��";
        public string marginTradinginformation = "�H�Υ������";

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {

            //�Ѳ��`��
            if (StringExtensions.Contains(turnContext.Activity.Text, "�Ѳ��`��", 0))
            {

                // Create a HeroCard with options for the user to interact with the bot.
                var card = new HeroCard
                {
                    Text = "�Ѳ��W���ܦh��?�A�Q���q����}�l�O?�i�H������J�ο�ܤU�誺���s��",
                    Buttons = new List<CardAction>
        {
            // Note that some channels require different values to be used in order to get buttons to display text.
            // In this code the emulator is accounted for with the 'title' parameter, but in other channels you may
            // need to provide a value for other parameters like 'text' or 'displayText'.
            new CardAction(ActionTypes.ImBack, title: "����O�H�����", value: "����O�H�����"),
            new CardAction(ActionTypes.ImBack, title: "��R?�O�Y����", value: "��R?�O�Y����"),
            new CardAction(ActionTypes.ImBack, title: "�ڷQ���H�Υ��", value: "�ڷQ���H�Υ��"),
        },
                };

                var reply = MessageFactory.Attachment(card.ToAttachment());
                await turnContext.SendActivityAsync(reply, cancellationToken);
            }






            //��R
            if (StringExtensions.Contains(turnContext.Activity.Text, stockDayTrade, 0))
            {
                await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak(stockDayTradeinformation), cancellationToken);
                
            }

            //�H�����
            if (StringExtensions.Contains(turnContext.Activity.Text, nomoney, 0))
            {
                await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak(nomoneyinformation), cancellationToken);
            }
            else
            {
                //���
                if (StringExtensions.Contains(turnContext.Activity.Text, Settlement, 0))
                {
                    await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak(Settlementinformation), cancellationToken);
                }
            }

            //�H�Υ��
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
                    await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak($"�z�n�A�w��Ө��Ҩ�E�_�ӡA�w�ﴣ�ݪѲ���������¦���D�A�p:�p���R�B����O�H�Υ��\n�Y�s���Ҩ�b���A�ڭ��ٯണ�ѹw����^���A���z�ήɭץ�����ζR����!"), cancellationToken);
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
