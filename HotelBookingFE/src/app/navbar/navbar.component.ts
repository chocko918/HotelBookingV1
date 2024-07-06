import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { ServicesService } from '../services.service'; // Import your authentication service
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  isLoggedIn = !!this.cookieService.get('customerID');
  

  constructor(private cookieService: CookieService, private service:ServicesService, private route: Router ) { }

  ngOnInit() {
    console.log(this.isLoggedIn);
    //// Check if 'customerID' cookie exists to set the logged-in status
    //const customerID = this.cookieService.get('customerID');
    //console.log('Checking login status: logged in!');
    //this.isLoggedIn = !!customerID; // true if customerID exists
  }



  logout() {
    this.service.logout().subscribe(
      () => {
        this.isLoggedIn = false;
        console.log('User logged out');
        this.cookieService.deleteAll();

      },
      error => {
        console.error('Logout error', error);
      }
    );
  }
}
