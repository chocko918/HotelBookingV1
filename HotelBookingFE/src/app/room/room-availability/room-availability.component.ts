import { Component, OnInit } from '@angular/core';
import { ServicesService } from '../../services.service';
import { DatePipe } from '@angular/common';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-room-availability',
  templateUrl: './room-availability.component.html',
  styleUrls: ['./room-availability.component.css']
})
export class RoomAvailabilityComponent {
  pax!: number;
  checkInDate!: Date;
  checkOutDate!: Date;
  errorMessage: string = "";
  roomID!: number;

  availableRooms: any = [];

  constructor(private service: ServicesService, private router: Router) { }


  checkAvailability() {

    this.availableRooms = [];

    this.service.getAvailableRooms(this.pax, this.checkInDate, this.checkOutDate).subscribe(
      data => {
        this.availableRooms = data;
        this.errorMessage = '';
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

    selectedRooms.forEach((room: any) => {
      this.service.addItemToCart(room.roomID, this.checkInDate, this.checkOutDate).subscribe(
        data => {
          console.log('Room added to cart:', data);
          this.errorMessage = '';
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



}
