import { Component, OnInit } from '@angular/core';
import { ServicesService } from '../services.service';
import { CartResponse, CartItem } from './cart.model';  // Adjust the path as necessary
import { CookieService } from 'ngx-cookie-service';
import { v4 as uuidv4 } from 'uuid';
import { Router } from '@angular/router';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})

export class CartComponent implements OnInit {

  constructor(
    private service: ServicesService,
    private cookieService: CookieService,
    private router: Router,
    private sharedService: SharedService,
  ) { }

  CartList: CartItem[] = [];
  TotalPrice: number = 0;
  BookingList: any = [];
  cartItemConfirmed: boolean = false;

  ngOnInit(): void {
    this.refreshCartList();
    this.sharedService.refreshCartList$.subscribe(() => this.refreshCartList()); // Subscribe to the refresh trigger
  }

  refreshCartList() {

    console.log("REFRESHED Cart")

      const customerID = this.cookieService.get('customerID')
    /*    this.service.getAllCartItems().subscribe(*/
      console.log("MEOW MOEW",customerID);
      this.service.getCartItemsByCustomerID(customerID).subscribe(
      (data: CartResponse) => {
        this.CartList = data.rooms;
        this.TotalPrice = data.totalPrice;
        console.log(this.CartList)
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

  deleteCartItemByCustomerID(): void {
    const customerID = this.cookieService.get('customerID')
    if (confirm("Are you sure you want to delete all cart items?")) {
      this.service.deleteCartItemByCustomerId(customerID).subscribe(response => {
        console.log("Item deleted:", response);
        this.refreshCartList();
      }, error => {
        console.error("Error deleting item:", error);
      });
    }

  }

  
  confirmCartItem(): void {
    const customerID = this.cookieService.get('customerID');
    console.log(customerID);
    if (!customerID) {
      console.error('CustomerID not found in cookies.');
      return;
    }

    this.service.confirmCartItem(customerID).subscribe(
      successMessage => {
        // Handle successful confirmation
        console.log('Cart item confirmed:', successMessage);
        this.cartItemConfirmed = true;
        // Perform any additional actions if needed
        setTimeout(() => {
          this.cartItemConfirmed = false;
        }, 10000); // Notice will disappear after 3 seconds

        this.router.navigate(['/bookingInfo']);
      },
      error => {
        // Handle error
        console.error('Error confirming cart item:', error);
      }
    );

  }

  //getBookingsByCustomer(): void {
  //  const customerID = this.cookieService.get('customerID');
  //  console.log("thisss",customerID);
  //  if (!customerID) {
  //    console.error('CustomerID not found in cookies.');
  //    return;
  //  }

  //  this.service.getBookingsByCustomerID(customerID).subscribe(
  //    response => {
  //      // Handle successful confirmation
  //      this.BookingList = response;
  //      console.log('Allbookings by customer:', this.BookingList);
  //      // Perform any additional actions if needed
  //    },
  //    error => {
  //      // Handle error
  //      console.error('Error confirming bookings:', error);
  //    }
  //  );

  //}

  getRoomImage(roomName: string): string {
    return `assets/RoomImages/${roomName}.png`;
  }

}
