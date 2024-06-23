import { Component, OnInit } from '@angular/core'; 
import { ServicesService } from './services.service'; 

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'HotelBookingFE';
  RoomList: any[] = [];

  constructor(private service: ServicesService) { }

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

  getRoomImage(roomName: string): string {
    return `assets/RoomImages/${roomName}.png`;
  }

}

