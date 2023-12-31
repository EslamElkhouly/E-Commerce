﻿using Core.Entities;

namespace E_Commerce.Dtos
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientsSecret { get; set; }
    }
}
