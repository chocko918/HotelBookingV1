import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServicesService } from '../../services.service'; // Import your authentication service
import { Router, NavigationEnd } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-bookinginfo',
  templateUrl: './bookinginfo.component.html',
  styleUrl: './bookinginfo.component.css'
})
export class BookinginfoComponent implements OnInit {
  //token: string = '';
  //user: any = {};
  BookingList: any = [];

  constructor(
    private route: Router,
    private service: ServicesService,
    private cookieService: CookieService,

  ) { }

  ngOnInit(): void {


    this.getBookingsByCustomer()

  }

  getBookingsByCustomer() {
    const customerID = this.cookieService.get('customerID');
    console.log("thisss", customerID);
    if (!customerID) {
      console.error('CustomerID not found in cookies.');
      return;
    }

    this.service.getBookingsByCustomerID(customerID).subscribe(
      response => {
        // Handle successful confirmation
        this.BookingList = response;
        console.log('Allbookings by customer:', this.BookingList);
        // Perform any additional actions if needed
      },
      error => {
        // Handle error
        console.error('Error confirming bookings:', error);
      }
    );

  }

  onLogout() {
    this.service.logout().subscribe(
      () => {
        // Clear the JWT token from localStorage
        this.cookieService.deleteAll();
        this.service.deleteAllCartItems().subscribe(
          () => {
            // Clear the JWT token from localStorage
            console.log("all cart items deleted")

          },
          error => {
            console.error('Logout error', error);
          }
        )
        // Redirect to the login page
        this.route.navigate(['/login']);
      },
      error => {
        console.error('Logout error', error);
      }
    );
  }

  getRoomImage(roomName: string): string {
    return `assets/RoomImages/${roomName}.png`;

  }

}
