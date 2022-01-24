﻿using System.ComponentModel.DataAnnotations;

namespace Coyote.Models.User
{
    public class UserRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CreditCardExpiration { get; set; }

        [Required]
        public string BillingAddress1 { get; set; }

        [Required]
        public string BillingAddress2 { get; set; }

        [Required]
        public string BillingCity { get; set; }

        [Required]
        public string BillingState { get; set; }

        [Required]
        public string BillingZip { get; set; }
    }
}