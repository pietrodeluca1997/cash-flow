﻿using CF.CustomMediator.Models;

namespace CF.Core.Messages.IntegrationEvents
{
    public class CreditTransactionRequestedEvent : Event
    {
        public Guid UserId { get; set; }
        public decimal MoneyAmount { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }

        public CreditTransactionRequestedEvent() : base()
        {

        }

        public CreditTransactionRequestedEvent(Guid userId, decimal moneyAmount, string description, string email)
        {
            UserId = userId;
            MoneyAmount = moneyAmount;
            Description = description;
            Email = email;
        }
    }
}
