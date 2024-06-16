import { Component, OnInit } from '@angular/core';
import { ServicesService } from '../services.service';
import { CartResponse, CartItem } from './cart.model';  // Adjust the path as necessary
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})

export class CartComponent implements OnInit {

  constructor(
    private service: ServicesService,
    private cookieService: CookieService
  ) { }

  CartList: CartItem[] = [];
  TotalPrice: number = 0;

  ngOnInit(): void {
    this.refreshCartList();

  }

  refreshCartList() {

    console.log("REFRESHED Cart")
    this.service.getAllCartItems().subscribe(
      (data: CartResponse) => {
        this.CartList = data.rooms;
        this.TotalPrice = data.totalPrice;
        console.log(data)
        
      },
    error => {
        console.error('Error fetching cart items:', error);
      }
    );
  }

  deleteCartItem(itemID: string): void {
    if (confirm("Are you sure you want to delete this item?")) {
      this.service.deleteCartItem(itemID).subscribe(response => {
        console.log("Item deleted:", response);
        this.refreshCartList();
      }, error => {
        console.error("Error deleting item:", error);
      });
    }

  }

  confirmCartItem(): void {
    const customerID = this.cookieService.get('customerID');

    if (!customerID) {
      console.error('CustomerID not found in cookies.');
      return;
    }

    this.service.confirmCartItem(customerID).subscribe(
      response => {
        // Handle successful confirmation
        console.log('Cart item confirmed:', response);
        // Perform any additional actions if needed
      },
      error => {
        // Handle error
        console.error('Error confirming cart item:', error);
      }
    );
  }

}