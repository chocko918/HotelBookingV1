import { Component, OnInit } from '@angular/core';
import { ServicesService } from '../services.service'; 

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})

export class RoomComponent implements OnInit {

  constructor(private service: ServicesService) { }

  RoomList: any = [];

  ngOnInit(): void {
    this.refreshRoomList()

  }

  refreshRoomList() {

    console.log("REFRESHED")
    this.service.getRoomList().subscribe(data => {
      this.RoomList = data;
      console.log(this.RoomList)
    });
  }

}

