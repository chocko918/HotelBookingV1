import { __decorate } from "tslib";
import { Component } from '@angular/core';
let RoomAvailabilityComponent = class RoomAvailabilityComponent {
    constructor(service, router) {
        this.service = service;
        this.router = router;
        this.errorMessage = "";
        this.availableRooms = [];
    }
    ngOnInit() {
        const currentDate = new Date();
        const nextYearDate = new Date();
        nextYearDate.setFullYear(currentDate.getFullYear() + 1);
        this.today = currentDate.toISOString().split('T')[0];
        this.maxDate = nextYearDate.toISOString().split('T')[0];
    }
    checkAvailability() {
        this.availableRooms = [];
        this.service.getAvailableRooms(this.pax, this.checkInDate, this.checkOutDate).subscribe(data => {
            this.availableRooms = data;
            this.errorMessage = '';
            console.log('Available rooms:', this.availableRooms);
            //this.router.navigate(['/available-rooms'], { state: { pax: data.pax, checkInDate: data.checkInDate, checkOutDate: data.checkOutDate } });
        }, error => {
            console.error('Error fetching available rooms:', error);
            this.errorMessage = error.Message || error;
            console.log(error.message);
        });
    }
    addToCart() {
        const selectedRooms = this.availableRooms.filter((room) => room.selected);
        if (selectedRooms.length === 0) {
            this.errorMessage = 'Please select at least one room to add to cart.';
            return;
        }
        selectedRooms.forEach((room) => {
            this.service.addItemToCart(room.roomID, this.checkInDate, this.checkOutDate).subscribe(data => {
                console.log('Room added to cart:', data);
                this.errorMessage = '';
            }, error => {
                console.log(room.roomID, this.checkInDate, this.checkOutDate);
                console.error('Error adding room to cart:', error);
                this.errorMessage = error.Message || error;
                console.log(error);
            });
        });
    }
};
RoomAvailabilityComponent = __decorate([
    Component({
        selector: 'app-room-availability',
        templateUrl: './room-availability.component.html',
        styleUrls: ['./room-availability.component.css']
    })
], RoomAvailabilityComponent);
export { RoomAvailabilityComponent };
//# sourceMappingURL=room-availability.component.js.map