﻿using MassTransit;
using Shop.Domain.Commands;
using Shop.Domain.Events;
using Shop.Product.DataProvider.Services;

namespace Shop.Product.Api.Consumers
{
    public class ProductCreateConsumer: IConsumer<ProductCreateCommand>, IConsumer<ProductCreatedEvent>
    {
        private readonly IProductService _productService;

        public ProductCreateConsumer(IProductService productService)
        {
            _productService = productService;
        }
    
        public Task Consume(ConsumeContext<ProductCreateCommand> context)
        {
            return _productService.AddProductAsync(context.Message);
        }

        public Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}