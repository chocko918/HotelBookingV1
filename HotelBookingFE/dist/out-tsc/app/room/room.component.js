import { __decorate } from "tslib";
import { Component } from '@angular/core';
let RoomComponent = class RoomComponent {
    constructor(service) {
        this.service = service;
        this.RoomList = [];
    }
    ngOnInit() {
        this.refreshRoomList();
    }
    refreshRoomList() {
        console.log("REFRESHED");
        this.service.getRoomList().subscribe(data => {
            this.RoomList = data;
            console.log(this.RoomList);
        });
    }
};
RoomComponent = __decorate([
    Component({
        selector: 'app-room',
        templateUrl: './room.component.html',
        styleUrls: ['./room.component.css']
    })
], RoomComponent);
export { RoomComponent };
//# sourceMappingURL=room.component.js.map