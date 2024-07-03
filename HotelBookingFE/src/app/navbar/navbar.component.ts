import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  isLoggedIn = !!this.cookieService.get('customerID');
  

  constructor(private cookieService: CookieService) { }

  ngOnInit() {
    console.log(this.isLoggedIn);
    //// Check if 'customerID' cookie exists to set the logged-in status
    //const customerID = this.cookieService.get('customerID');
    //console.log('Checking login status: logged in!');
    //this.isLoggedIn = !!customerID; // true if customerID exists
  }

  logout() {
    // Implement your logout logic here
    this.cookieService.delete('customerID');
    this.isLoggedIn = false;
    console.log('User logged out');
  }
}
