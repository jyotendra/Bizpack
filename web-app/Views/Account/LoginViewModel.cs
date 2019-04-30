/**
 * LoginViewModel.cs - SignalRApp
 * Copyright 2019 -  Bitvivid Solutions Pvt. Ltd. 
 * *********************************************************
 * Author - Jyotendra Sharma 
 * *********************************************************
 * No part of this software may be copied or distributed without written consent from Bitvivid Solutions Pvt. Ltd (company).
 * The company holds right to prosecute the individual/organisation/company found guilty of misusing company's intellectual properties.
 */

using System.ComponentModel.DataAnnotations;

namespace Bizpack.Views.Account 
{
    public class LoginRequestModel {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }

    public class LoginResponseViewModel {
        public string Email { get; set; }

        public string Token { get; set; }
    }
}