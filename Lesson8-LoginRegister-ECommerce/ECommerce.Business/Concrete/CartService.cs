﻿using ECommerce.Business.Abstract;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class CartService : ICartService
    {
        public void AddToCart(Cart cart, Product product)
        {
            CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == product.ProductId);
            if (cartLine != null)
            {
                cartLine.Quantity++;
            }
            else
            {
                cart.CartLines.Add(new CartLine { Product = product, Quantity = 1 });
            }
        }

        public void Decrease(Cart cart, int productId)
        {
            var cartLine = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == productId);
            if (cartLine is not null)
                cartLine.Quantity = cartLine.Quantity > 1 ? cartLine.Quantity - 1 : cartLine.Quantity;
        }

        public void Increase(Cart cart, int productId)
        {
            var cartLine = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == productId);
            if (cartLine != null)
                cartLine.Quantity = cartLine.Quantity < cartLine.Product.UnitsInStock ? cartLine.Quantity+1 : cartLine.Quantity;
        }

        public List<CartLine>? List(Cart cart)
        {
            return cart.CartLines;
        }

        public void RemoveFromCart(Cart cart, int productId)
        {
            var item = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == productId);
            if (item != null)
            {
                cart.CartLines.Remove(item);
            }
        }
    }
}
