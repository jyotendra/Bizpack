/**
 * Notifier.cs - SignalRApp
 * Copyright 2019 -  Bitvivid Solutions Pvt. Ltd. 
 * *********************************************************
 * Author - Jyotendra Sharma 
 * *********************************************************
 * No part of this software may be copied or distributed without written consent from Bitvivid Solutions Pvt. Ltd (company).
 * The company holds right to prosecute the individual/organisation/company found guilty of misusing company's intellectual properties.
 */

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Sock
{
    public class NotifierHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}