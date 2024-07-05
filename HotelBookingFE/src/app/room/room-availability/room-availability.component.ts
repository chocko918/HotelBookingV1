import { Component, OnInit } from '@angular/core';
import { ServicesService } from '../../services.service';
import { DatePipe } from '@angular/common';
import { Router, NavigationEnd } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { CartResponse, CartItem } from '../../cart/cart.model'
import { SharedService } from '../../shared.service'
@Component({
  selector: 'app-room-availability',
  templateUrl: './room-availability.component.html',
  styleUrls: ['./room-availability.component.css']
})
export class RoomAvailabilityComponent implements OnInit {
  pax!: number;
  checkInDate!: Date;
  checkOutDate!: Date;
  today!: string;
  maxDate!: string;
  errorMessage: string = "";
  roomID!: number;
  itemAddedToCart: boolean = false;

  availableRooms: any = [];


  constructor(private sharedService: SharedService, private service: ServicesService, private router: Router, private cookieService: CookieService) { }

  ngOnInit() {
    const currentDate = new Date();
    const nextYearDate = new Date();
    nextYearDate.setFullYear(currentDate.getFullYear() + 1);

    this.today = currentDate.toISOString().split('T')[0];
    this.maxDate = nextYearDate.toISOString().split('T')[0];
  }

  checkAvailability() {

    this.availableRooms = [];

    this.service.getAvailableRooms(this.pax, this.checkInDate, this.checkOutDate).subscribe(
      data => {
        this.availableRooms = data;
        this.errorMessage = '';
        this.sharedService.triggerRefreshCartList();
        console.log('Available rooms:', this.availableRooms);
        //this.router.navigate(['/available-rooms'], { state: { pax: data.pax, checkInDate: data.checkInDate, checkOutDate: data.checkOutDate } });
      },
      error => {
        console.error('Error fetching available rooms:', error);
        this.errorMessage = error.Message || error;
        console.log(error.message);
      }
    );
  }

  addToCart() {
    const selectedRooms = this.availableRooms.filter((room: any) => room.selected);
    if (selectedRooms.length === 0) {
      this.errorMessage = 'Please select at least one room to add to cart.';
      return;
    }
    const customerID = this.cookieService.get('customerID')
    selectedRooms.forEach((room: any) => {
      this.service.addItemToCart(customerID, room.roomID, this.checkInDate, this.checkOutDate).subscribe(
        data => {
          console.log('Room added to cart:', data);
          this.errorMessage = '';
          this.checkAvailability();
          this.itemAddedToCart = true;
          setTimeout(() => {
            this.itemAddedToCart = false;
          }, 3000); // Notice will disappear after 3 seconds
        },
        error => {
          console.log(room.roomID, this.checkInDate, this.checkOutDate)
          console.error('Error adding room to cart:', error);
          this.errorMessage = error.Message || error;
          console.log(error);
        }
      );
    });
  }



  getRoomImage(roomName: string): string {
    return `assets/RoomImages/${roomName}.png`;
  }

  onLogout() {
    this.service.logout().subscribe(
      () => {
        // Clear the JWT token from localStorage
        this.cookieService.delete('customerID');
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
        this.router.navigate(['/login']);
      },
      error => {
        console.error('Logout error', error);
      }
    );
  }

 
  deleteAll() { 
  this.service.deleteAllCartItems();
  }
}
