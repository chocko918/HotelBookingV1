import { Component, OnInit } from '@angular/core';
import { ServicesService } from '../services.service';

// Interface for a single cart item
export interface CartItem {
  itemID: string;
  roomID: number;
  name: string;
  price: number;
  checkInDate: string;
  checkOutDate: string;
}

// Interface for the response from the API
export interface CartResponse {
  rooms: CartItem[];
  totalPrice: number;
}
